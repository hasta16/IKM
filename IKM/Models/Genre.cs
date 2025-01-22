using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace LibraryManagementApp.Models
{
    public class Genre : INotifyPropertyChanged
    {
        private int _genreID;
        private string _genreName;

        public int GenreID
        {
            get => _genreID;
            set { _genreID = value; OnPropertyChanged(nameof(GenreID)); }
        }

        public string GenreName
        {
            get => _genreName;
            set { _genreName = value; OnPropertyChanged(nameof(GenreName)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


