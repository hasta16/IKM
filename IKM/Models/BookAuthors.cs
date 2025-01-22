using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace LibraryManagementApp.Models
{
    public class BookAuthor : INotifyPropertyChanged
    {
        private int _recordID;
        private int _bookID;
        private int _authorID;

        public int RecordID
        {
            get => _recordID;
            set { _recordID = value; OnPropertyChanged(nameof(RecordID)); }
        }

        public int BookID
        {
            get => _bookID;
            set { _bookID = value; OnPropertyChanged(nameof(BookID)); }
        }

        public int AuthorID
        {
            get => _authorID;
            set { _authorID = value; OnPropertyChanged(nameof(AuthorID)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


