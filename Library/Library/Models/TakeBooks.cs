using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class TakeBooks
    {
        public int Numbr { get; set; }
        public int? LibraryCard { get; set; }
        public int? BookCode { get; set; }
        public DateTime DataTake { get; set; }
        public DateTime? DataBack { get; set; }
        public int? TimeToBack { get; set; }

        public Books BookCodeNavigation { get; set; }
        public LibraryCard LibraryCardNavigation { get; set; }
    }
}
