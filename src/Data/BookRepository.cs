using System;
using GraphQLSample.Models;
using System.Collections.Generic;
using GenFu;
using System.Linq;

public interface IBookRepository
{
    IEnumerable<Author> AllAuthors();
    IEnumerable<Book> AllBooks();
    IEnumerable<Publisher> AllPublishers();

    Author AuthorById(int id);
    Book BookByIsbn(string isbn);
    Publisher PublisherById(int id);
}

public class BookRepository : IBookRepository
{
    private IEnumerable<Book> _books = new List<Book>();
    private IEnumerable<Author> _authors = new List<Author>();
    public IEnumerable<Publisher> _publishers = new List<Publisher>();

    public BookRepository()
    {
        GenFu.GenFu.Configure<Author>()
            .Fill(_ => _.Name).AsLastName()
            .Fill(_ => _.BirthDate).AsPastDate();
        GenFu.GenFu.Configure<Publisher>()
            .Fill(_ => _.Name).AsMusicArtistName();

        _authors = A.ListOf<Author>(40);
        _publishers = A.ListOf<Publisher>(10);

        GenFu.GenFu.Configure<Book>()
            .Fill(_ => _.Isbn).AsISBN()
            .Fill(_ => _.Name).AsLoremIpsumWords(5)
            .Fill(_ => _.Author).WithRandom(_authors)
            .Fill(_ => _.Publisher).WithRandom(_publishers);

        _books = A.ListOf<Book>(100);
    }

    public IEnumerable<Author> AllAuthors()
    {
        return _authors;
    }

    public IEnumerable<Book> AllBooks()
    {
        return _books;
    }

    public IEnumerable<Publisher> AllPublishers()
    {
        return _publishers;
    }

    public Author AuthorById(int id)
    {
        return _authors.First(a => a.Id == id);
    }

    public Book BookByIsbn(string isbn)
    {
        return _books.First(b => b.Isbn == isbn);
    }

    public Publisher PublisherById(int id)
    {
        return _publishers.First(p => p.Id == id);
    }

}

public static class StringFillerExtensions
{
    public static GenFuConfigurator<T> AsISBN<T>(this GenFuStringConfigurator<T> configurator) where T : new()
    {
        var filler = new CustomFiller<string>(configurator.PropertyInfo.Name, typeof(T), () =>
        {
            return MakeIsbn();
        });
        configurator.Maggie.RegisterFiller(filler);
        return configurator;
    }

    public static string MakeIsbn()
    {
        // 978-1-933988-27-6
        var a = A.Random.Next(100, 999);
        var b = A.Random.Next(1, 9);
        var c = A.Random.Next(100000, 999999);
        var d = A.Random.Next(10, 99);
        var e = A.Random.Next(1, 9);

        return $"{a}-{b}-{c}-{d}-{e}";
    }
}