using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Books
    {
        public Books()
        {
            BookCopies = new HashSet<BookCopies>();
            TakeBooks = new HashSet<TakeBooks>();
        }

        public int Code { get; set; }
        public string Isbn { get; set; }
        public string BookName { get; set; }
        public string AuthorCode { get; set; }
        public string GenreCode { get; set; }
        public string YdkSectionCode { get; set; }
        public string LangCode { get; set; }
        public int? BookYear { get; set; }
        public string Annotation { get; set; }
        public string Picture { get; set; }
        public string Book { get; set; }

        public Author AuthorCodeNavigation { get; set; }
        public Genre GenreCodeNavigation { get; set; }
        public Lang LangCodeNavigation { get; set; }
        public Ydk YdkSectionCodeNavigation { get; set; }
        public ICollection<BookCopies> BookCopies { get; set; }
        public ICollection<TakeBooks> TakeBooks { get; set; }
    }
}
