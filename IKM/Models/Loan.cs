using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace LibraryManagementApp.Models
{
    public class Loan : INotifyPropertyChanged
    {
        private int _loanID;
        private int _bookID;
        private int _readerID;
        private DateTime _loanDate;
        private DateTime? _returnDate;

        public int LoanID
        {
            get => _loanID;
            set { _loanID = value; OnPropertyChanged(nameof(LoanID)); }
        }

        public int BookID
        {
            get => _bookID;
            set { _bookID = value; OnPropertyChanged(nameof(BookID)); }
        }

        public int ReaderID
        {
            get => _readerID;
            set { _readerID = value; OnPropertyChanged(nameof(ReaderID)); }
        }

        public DateTime LoanDate
        {
            get => _loanDate;
            set { _loanDate = value; OnPropertyChanged(nameof(LoanDate)); }
        }

        public DateTime? ReturnDate
        {
            get => _returnDate;
            set { _returnDate = value; OnPropertyChanged(nameof(ReturnDate)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


