using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace LibraryManagementApp.Models
{
    public class Book
    {
        public int BookID { get; set; } // Уникальный идентификатор книги
        public string Title { get; set; } // Название книги
        public int YearPublished { get; set; } // Год издания книги
        public int GenreID { get; set; } // Идентификатор жанра
    }
}



