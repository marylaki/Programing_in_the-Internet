using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class BookCopies
    {
        public int Code { get; set; }
        public int? BookCode { get; set; }
        public string Issued { get; set; }
        public string Destroyed { get; set; }

        public Books BookCodeNavigation { get; set; }
    }
}
