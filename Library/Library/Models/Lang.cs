using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Lang
    {
        public Lang()
        {
            Books = new HashSet<Books>();
        }

        public string Code { get; set; }
        public string Lang1 { get; set; }

        public ICollection<Books> Books { get; set; }
    }
}
