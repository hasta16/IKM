using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace LibraryManagementApp.Models
{
    public class Reader : INotifyPropertyChanged
    {
        private int _readerID;
        private string _readerName;
        private string _contactInfo;

        public int ReaderID
        {
            get => _readerID;
            set { _readerID = value; OnPropertyChanged(nameof(ReaderID)); }
        }

        public string ReaderName
        {
            get => _readerName;
            set { _readerName = value; OnPropertyChanged(nameof(ReaderName)); }
        }

        public string ContactInfo
        {
            get => _contactInfo;
            set { _contactInfo = value; OnPropertyChanged(nameof(ContactInfo)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

