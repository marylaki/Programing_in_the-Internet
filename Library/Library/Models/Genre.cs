using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Books>();
        }

        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<Books> Books { get; set; }
    }
}
