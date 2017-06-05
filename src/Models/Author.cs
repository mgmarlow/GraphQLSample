using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLSample.Models
{
    public class Author
    {
        public Author()
        {
            Books = new Book[] { };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Book[] Books { get; set; }
    }
}
