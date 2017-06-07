using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLSample.Models.GraphQLTypes
{
    public class BookType : ObjectGraphType<Book>
    {
        public BookType()
        {
            Field(b => b.Isbn).Description("ISBN of the book.");
            Field(b => b.Name).Description("Name of the book.");
            Field<AuthorType>("author");
            Field<PublisherType>("publisher");
        }
    }
}
