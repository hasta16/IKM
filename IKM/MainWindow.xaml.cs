using LibraryManagementApp.Models;
using LibraryManagementApp.DataAccess;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Runtime.Remoting.Messaging;

namespace LibraryManagementApp
{
    public partial class MainWindow : Window
    {
        private DatabaseManager _dbManager;

        public MainWindow()
        {
            InitializeComponent();
            _dbManager = new DatabaseManager();
            LoadBooks();
            LoadGenres(); // Загружаем жанры при старте приложения
            LoadBookAuthors(); // Загружаем записи авторов при старте приложения
            LoadAuthors(); // Загрузка списка авторов при старте приложения
            LoadLoans(); // Загрузка списка выдач
            LoadReaders();
        }

        // Методы для управления книгами
        private void LoadBooks()
        {
            List<Book> books = _dbManager.GetBooks();
            BooksDataGrid.ItemsSource = books;
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            if (int.TryParse(YearPublishedTextBox.Text, out int year) && int.TryParse(GenreIDTextBoxBook.Text, out int genreId))
            {
                _dbManager.AddBook(new Book { Title = title, YearPublished = year, GenreID = genreId });
                LoadBooks();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных.");
            }
        }

        private void UpdateBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BookIDTextBox.Text, out int bookId) &&
                int.TryParse(YearPublishedTextBox.Text, out int year) &&
                int.TryParse(GenreIDTextBoxBook.Text, out int genreId))
            {
                string title = TitleTextBox.Text;
                _dbManager.UpdateBook(new Book { BookID = bookId, Title = title, YearPublished = year, GenreID = genreId });
                LoadBooks();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных.");
            }
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BookIDTextBox.Text, out int bookId))
            {
                _dbManager.DeleteBook(bookId);
                LoadBooks();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Выберите книгу для удаления.");
            }
        }

        // Методы для управления жанрами
        private void LoadGenres()
        {
            List<Genre> genres = _dbManager.GetGenres(); // Загружаем все жанры из базы данных
            GenresDataGrid.ItemsSource = genres;
        }

        private void AddGenreButton_Click(object sender, RoutedEventArgs e)
        {
            string genreName = GenreNameTextBox.Text;
            if (!string.IsNullOrEmpty(genreName))
            {
                _dbManager.AddGenre(new Genre { GenreName = genreName });
                LoadGenres();
                ClearGenreForm();
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных.");
            }
        }

        private void UpdateGenreButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(GenreIDTextBoxGenre.Text, out int genreId))
            {
                string genreName = GenreNameTextBox.Text;
                _dbManager.UpdateGenre(new Genre { GenreID = genreId, GenreName = genreName });
                LoadGenres();
                ClearGenreForm();
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных.");
            }
        }

        private void DeleteGenreButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(GenreIDTextBoxGenre.Text, out int genreId))
            {
                _dbManager.DeleteGenre(genreId);
                LoadGenres();
                ClearGenreForm();
            }
            else
            {
                MessageBox.Show("Выберите жанр для удаления.");
            }
        }

        private void ClearGenreForm()
        {
            GenreIDTextBoxGenre.Clear();
            GenreNameTextBox.Clear();
        }

        private void ClearFormButton_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            BookIDTextBox.Clear();
            TitleTextBox.Clear();
            YearPublishedTextBox.Clear();
            GenreIDTextBoxBook.Clear();
        }

        // Методы для управления записями авторов
        private void LoadBookAuthors()
        {
            List<BookAuthor> bookAuthors = _dbManager.GetBookAuthors();
            AuthorsBooksDataGrid.ItemsSource = bookAuthors;
        }

        private void AddAuthorRecordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(AuthorBookIDTextBox.Text, out int bookId) && int.TryParse(AuthorIDTextBox.Text, out int authorId))
                {
                    var bookAuthor = new BookAuthor
                    {
                        BookID = bookId,
                        AuthorID = authorId
                    };
                    _dbManager.AddBookAuthor(bookAuthor);
                    LoadBookAuthors();
                    ClearAuthorForm();
                    MessageBox.Show("Запись автора успешно добавлена.");
                }
                else
                {
                    MessageBox.Show("Проверьте правильность введенных данных.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении записи автора: " + ex.Message);
            }
        }

        private void UpdateAuthorRecordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(AuthorRecordIDTextBox.Text, out int recordId) &&
                    int.TryParse(AuthorBookIDTextBox.Text, out int bookId) &&
                    int.TryParse(AuthorIDTextBox.Text, out int authorId))
                {
                    var bookAuthor = new BookAuthor
                    {
                        RecordID = recordId,
                        BookID = bookId,
                        AuthorID = authorId
                    };
                    _dbManager.UpdateBookAuthor(bookAuthor);
                    LoadBookAuthors();
                    ClearAuthorForm();
                    MessageBox.Show("Запись автора успешно обновлена.");
                }
                else
                {
                    MessageBox.Show("Проверьте правильность введенных данных.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при обновлении записи автора: " + ex.Message);
            }
        }

        private void DeleteAuthorRecordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(AuthorRecordIDTextBox.Text, out int recordId))
                {
                    _dbManager.DeleteBookAuthor(recordId);
                    LoadBookAuthors();
                    ClearBookAuthorForm();
                    MessageBox.Show("Запись автора успешно удалена.");
                }
                else
                {
                    MessageBox.Show("Выберите запись автора для удаления.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении записи автора: " + ex.Message);
            }
        }

        private void ClearBookAuthorForm()
        {
            AuthorRecordIDTextBox.Clear();
            AuthorBookIDTextBox.Clear();
            AuthorIDTextBox.Clear();
        }


        // Методы для управления авторами
        private void LoadAuthors()
        {
            var authors = _dbManager.GetAuthors(); // Загрузка авторов из базы данных
            AuthorsDataGrid.ItemsSource = authors;
        }


        private void AddAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            string authorName = AuthorNameTextBox.Text;
            if (!string.IsNullOrEmpty(authorName))
            {
                _dbManager.AddAuthor(new Author { AuthorName = authorName });
                LoadAuthors();
                ClearAuthorForm();
            }
            else
            {
                MessageBox.Show("Введите имя автора.");
            }
        }

        private void UpdateAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(AuthorIDTextBoxAuthor.Text, out int authorId))
            {
                string authorName = AuthorNameTextBox.Text;
                _dbManager.UpdateAuthor(new Author { AuthorID = authorId, AuthorName = authorName });
                LoadAuthors();
                ClearAuthorForm();
            }
            else
            {
                MessageBox.Show("Выберите автора для обновления.");
            }
        }

        private void DeleteAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(AuthorIDTextBoxAuthor.Text, out int authorId))
            {
                _dbManager.DeleteAuthor(authorId);
                LoadAuthors();
                ClearAuthorForm();
            }
            else
            {
                MessageBox.Show("Выберите автора для удаления.");
            }
        }

        private void ClearAuthorForm()
        {
            AuthorIDTextBoxAuthor.Clear();
            AuthorNameTextBox.Clear();
        }



        private void LoadLoans()
        {
            try
            {
                var loans = _dbManager.GetLoans(); // Загрузка данных из базы
                LoansDataGrid.ItemsSource = loans; // Привязка к таблице
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки выдач: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Добавление новой выдачи.
        /// </summary>
        private void AddLoanButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var loan = new Loan
                {
                    BookID = int.Parse(LoanBookIDTextBox.Text),
                    ReaderID = int.Parse(LoanReaderIDTextBox.Text),
                    LoanDate = LoanDatePicker.SelectedDate ?? DateTime.Now,
                    ReturnDate = ReturnDatePicker.SelectedDate
                };

                _dbManager.AddLoan(loan); // Добавление записи в базу
                MessageBox.Show("Выдача успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadLoans(); // Обновление списка выдач
                ClearLoanForm(); // Очистка формы
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления выдачи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обновление существующей выдачи.
        /// </summary>
        private void UpdateLoanButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LoanIDTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, укажите ID выдачи для обновления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var loan = new Loan
                {
                    LoanID = int.Parse(LoanIDTextBox.Text),
                    BookID = int.Parse(LoanBookIDTextBox.Text),
                    ReaderID = int.Parse(LoanReaderIDTextBox.Text),
                    LoanDate = LoanDatePicker.SelectedDate ?? DateTime.Now,
                    ReturnDate = ReturnDatePicker.SelectedDate
                };

                _dbManager.UpdateLoan(loan); // Обновление записи в базе
                MessageBox.Show("Выдача успешно обновлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadLoans(); // Обновление списка выдач
                ClearLoanForm(); // Очистка формы
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления выдачи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Удаление существующей выдачи.
        /// </summary>
        private void DeleteLoanButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LoanIDTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, укажите ID выдачи для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int loanID = int.Parse(LoanIDTextBox.Text);
                _dbManager.DeleteLoan(loanID); // Удаление записи из базы
                MessageBox.Show("Выдача успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadLoans(); // Обновление списка выдач
                ClearLoanForm(); // Очистка формы
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления выдачи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Очистка формы для работы с выдачами.
        /// </summary>
        private void ClearLoanFormButton_Click(object sender, RoutedEventArgs e)
        {
            ClearLoanForm();
        }

        /// <summary>
        /// Очистка всех полей формы для выдач.
        /// </summary>
        private void ClearLoanForm()
        {
            LoanIDTextBox.Clear();
            LoanBookIDTextBox.Clear();
            LoanReaderIDTextBox.Clear();
            LoanDatePicker.SelectedDate = null;
            ReturnDatePicker.SelectedDate = null;
        }











        private void LoadReaders()
        {
            var readers = _dbManager.GetReaders(); // Загрузка читателей из базы данных
            ReadersDataGrid.ItemsSource = readers;
        }

        // Обработчик кнопки "Добавить"
        private void AddReaderButton_Click(object sender, RoutedEventArgs e)
        {
            var newReader = new Reader
            {
                ReaderName = ReaderNameTextBox.Text,
                ContactInfo = ReaderContactInfoTextBox.Text
            };

            _dbManager.AddReader(newReader);  // Вызов метода для добавления читателя в базу данных
            LoadReaders();
        }

        // Обработчик кнопки "Обновить"
        private void UpdateReaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReadersDataGrid.SelectedItem is Reader selectedReader)
            {
                selectedReader.ReaderName = ReaderNameTextBox.Text;
                selectedReader.ContactInfo = ReaderContactInfoTextBox.Text;

                _dbManager.UpdateReader(selectedReader);  // Вызов метода для обновления данных читателя
                LoadReaders();
            }
        }

        // Обработчик кнопки "Удалить"
        private void DeleteReaderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReadersDataGrid.SelectedItem is Reader selectedReader)
            {
                _dbManager.DeleteReader(selectedReader.ReaderID);  // Вызов метода для удаления читателя
                LoadReaders();
            }
        }
    }
}
