using GraphQL.Types;
using GraphQLSample.Models.GraphQLTypes;
using GraphQLSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLSample.Models.GraphQLQueries
{
    public class BooksQuery : ObjectGraphType
    {
        public BooksQuery(IBookService bookService)
        {
            Field<BookType>("book",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType>() { Name = "isbn" }
                ),
                resolve: context =>
                {
                    var isbn = context.GetArgument<string>("isbn");
                    return bookService.BookByIsbn(isbn);
                });

            Field<ListGraphType<BookType>>("books", resolve: context => bookService.AllBooks());
        }
    }
}
