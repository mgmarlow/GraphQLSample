using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLSample.Models.GraphQLTypes
{
    public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType()
        {
            Field(a => a.Id).Description("Id of the person.");
            Field(a => a.Name).Description("Name of the person.");
            Field(a => a.BirthDate).Description("Birthdate of the person.");
            Field<ListGraphType<BookType>>("books", resolve: context => new Book[] { });
            Field<ListGraphType<PublisherType>>("publishers", resolve: context => new Publisher[] { });
        }
    }
}
