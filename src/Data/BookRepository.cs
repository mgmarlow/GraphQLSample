using System;
using GraphQLSample.Models;
using System.Collections.Generic;

interface IBookRepository
{

}

public class BookRepository : IBookRepository
{
    private IEnumerable<Book> _books = new List<Book>();
    private IEnumerable<Author> _authors = new List<Author>();

    public BookRepository()
    {
        GenFu.GenFu.Configure<Author>()
            .Fill(_ => _.Name).AsLastName()
            .Fill(_ => _.Birthdate).AsPastDate();
        _authors = A.ListOf<Author>(40);
    }
}