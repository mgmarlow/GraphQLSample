using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLSample.Models.GraphQLTypes
{
    public class PublisherType : ObjectGraphType<Publisher>
    {
        public PublisherType()
        {
            Field(p => p.Id).Description("Id of publisher.");
            Field(p => p.Name).Description("Name of publisher.");
            Field<ListGraphType<BookType>>("books", resolve: context => new Book[] { });
            Field<ListGraphType<AuthorType>>("authors", resolve: context => new Author[] { });
        }
    }
}
