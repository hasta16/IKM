using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace LibraryManagementApp.Models
{
    public class Author : INotifyPropertyChanged
    {
        private int _authorID;
        private string _authorName;

        public int AuthorID
        {
            get => _authorID;
            set { _authorID = value; OnPropertyChanged(nameof(AuthorID)); }
        }

        public string AuthorName
        {
            get => _authorName;
            set { _authorName = value; OnPropertyChanged(nameof(AuthorName)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


