using GraphQLSample.Models;
using GraphQLSample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLSample.Services
{
    public interface IBookService
    {
        IEnumerable<Author> AllAuthors();
        IEnumerable<Book> AllBooks();
        IEnumerable<Publisher> AllPublishers();

        Author AuthorById(int id);
        Book BookByIsbn(string isbn);
        Publisher PublisherById(int id);
    }

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService (IBookRepository bookRepo)
        {
            _bookRepository = bookRepo;
        }

        public IEnumerable<Author> AllAuthors()
        {
            return _bookRepository.AllAuthors();
        }

        public IEnumerable<Book> AllBooks()
        {
            return _bookRepository.AllBooks();
        }

        public IEnumerable<Publisher> AllPublishers()
        {
            return _bookRepository.AllPublishers();
        }

        public Author AuthorById(int id)
        {
            return _bookRepository.AuthorById(id);
        }

        public Book BookByIsbn(string isbn)
        {
            return _bookRepository.BookByIsbn(isbn);
        }

        public Publisher PublisherById(int id)
        {
            return _bookRepository.PublisherById(id);
        }
    }
}
