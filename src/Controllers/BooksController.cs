using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQLSample.Services;
using GraphQL.Types;
using GraphQLSample.Models.GraphQLQueries;
using GraphQLSample.Models;
using GraphQL;
using GraphQL.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraphQLSample.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // api/books
        public Task<string> Get([FromBody] string query)
        {
            return GetJsonFromQuery(query);
        }

        private async Task<string> GetJsonFromQuery(string query)
        {
            var schema = new Schema { Query = new BooksQuery(_bookService) };
            var result = await new DocumentExecuter()
                .ExecuteAsync(_ =>
                {
                    _.Schema = schema;
                    _.Query = query;
                }).ConfigureAwait(false);
            var json = new DocumentWriter(indent: true).Write(result);
            return json;
        }

    }
}
