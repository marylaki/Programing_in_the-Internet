using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class LibraryCard
    {
        public LibraryCard()
        {
            TakeBooks = new HashSet<TakeBooks>();
        }

        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public ICollection<TakeBooks> TakeBooks { get; set; }
    }
}
