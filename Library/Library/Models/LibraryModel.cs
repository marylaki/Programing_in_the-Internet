using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class LibraryModel
    {
        static private LibraryModel _instance;
        public LibraryContext Library;
        static public LibraryModel getLib()
        {
            if (_instance == null)
            {
                _instance = new LibraryModel();
                _instance.Library = new LibraryContext();
            }
            return _instance;
        }
    }
}
