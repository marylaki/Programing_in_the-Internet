using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Ydk
    {
        public Ydk()
        {
            Books = new HashSet<Books>();
        }

        public string YdkCode { get; set; }
        public string Section { get; set; }

        public ICollection<Books> Books { get; set; }
    }
}
