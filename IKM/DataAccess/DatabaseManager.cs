using System;
using System.Collections.Generic;
using Npgsql;
using LibraryManagementApp.Models;
using System.Data.SqlClient;

namespace LibraryManagementApp.DataAccess
{
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager()
        {
            _connectionString = "Host=localhost;Port=5432;Database=library_management;Username=postgres;Password=48148";
        }

        // Метод для получения списка книг
        public List<Book> GetBooks()
        {
            var books = new List<Book>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Books";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book
                            {
                                BookID = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                YearPublished = reader.GetInt32(2),
                                GenreID = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }

            return books;
        }

        // Метод для добавления книги
        public void AddBook(Book book)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Books (Title, YearPublished, GenreID) VALUES (@Title, @YearPublished, @GenreID)";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                    command.Parameters.AddWithValue("@GenreID", book.GenreID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для обновления книги
        public void UpdateBook(Book book)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Books SET Title = @Title, YearPublished = @YearPublished, GenreID = @GenreID WHERE BookID = @BookID";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                    command.Parameters.AddWithValue("@GenreID", book.GenreID);
                    command.Parameters.AddWithValue("@BookID", book.BookID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для удаления книги
        public void DeleteBook(int bookID)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                // Удаление книги
                var deleteQuery = "DELETE FROM Books WHERE bookid = @BookID"; // Убедитесь, что bookid совпадает с именем столбца
                using (var deleteCommand = new NpgsqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@BookID", bookID);
                    deleteCommand.ExecuteNonQuery();
                }

                // Сбросить последовательность
                var resetSequenceQuery = "SELECT setval(pg_get_serial_sequence('Books', 'bookid'), COALESCE(MAX(BookID), 0)) FROM Books";
                using (var resetCommand = new NpgsqlCommand(resetSequenceQuery, connection))
                {
                    resetCommand.ExecuteNonQuery();
                }
            }
        }

        // Методы для работы с жанрами

        // Получить все жанры
        public List<Genre> GetGenres()
        {
            var genres = new List<Genre>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Genres";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            genres.Add(new Genre
                            {
                                GenreID = reader.GetInt32(0),
                                GenreName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return genres;
        }

        // Добавить новый жанр
        public void AddGenre(Genre genre)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Genres (GenreName) VALUES (@GenreName)";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GenreName", genre.GenreName);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Обновить существующий жанр
        public void UpdateGenre(Genre genre)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Genres SET GenreName = @GenreName WHERE GenreID = @GenreID";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GenreName", genre.GenreName);
                    command.Parameters.AddWithValue("@GenreID", genre.GenreID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Удалить жанр
        public void DeleteGenre(int genreID)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Genres WHERE GenreID = @GenreID";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GenreID", genreID);
                    command.ExecuteNonQuery();
                }

                // Сбросить последовательность
                var resetSequenceQuery = "SELECT setval(pg_get_serial_sequence('Genres', 'genreid'), COALESCE(MAX(GenreID), 0)) FROM Genres";
                using (var resetCommand = new NpgsqlCommand(resetSequenceQuery, connection))
                {
                    resetCommand.ExecuteNonQuery();
                }
            }








        }



        // Методы для работы с авторами книг

        // Получить все записи авторов
        public List<BookAuthor> GetBookAuthors()
        {
            var bookAuthors = new List<BookAuthor>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM BookAuthors";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bookAuthors.Add(new BookAuthor
                            {
                                RecordID = reader.GetInt32(0),
                                BookID = reader.GetInt32(1),
                                AuthorID = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }

            return bookAuthors;
        }

        // Добавить новую запись автора
        public void AddBookAuthor(BookAuthor bookAuthor)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO BookAuthors (BookID, AuthorID) VALUES (@BookID, @AuthorID)";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookID", bookAuthor.BookID);
                    command.Parameters.AddWithValue("@AuthorID", bookAuthor.AuthorID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Обновить существующую запись автора
        public void UpdateBookAuthor(BookAuthor bookAuthor)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE BookAuthors SET BookID = @BookID, AuthorID = @AuthorID WHERE RecordID = @RecordID";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookID", bookAuthor.BookID);
                    command.Parameters.AddWithValue("@AuthorID", bookAuthor.AuthorID);
                    command.Parameters.AddWithValue("@RecordID", bookAuthor.RecordID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Удалить запись автора
        public void DeleteBookAuthor(int recordID)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM BookAuthors WHERE RecordID = @RecordID";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecordID", recordID);
                    command.ExecuteNonQuery();
                }

                // Сбросить последовательность
                var resetSequenceQuery = "SELECT setval(pg_get_serial_sequence('BookAuthors', 'recordid'), COALESCE(MAX(RecordID), 0)) FROM BookAuthors";
                using (var resetCommand = new NpgsqlCommand(resetSequenceQuery, connection))
                {
                    resetCommand.ExecuteNonQuery();
                }
            }
        }



        // Получить всех авторов
        public List<Author> GetAuthors()
        {
            var authors = new List<Author>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT AuthorID, AuthorName FROM Authors";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            authors.Add(new Author
                            {
                                AuthorID = reader.GetInt32(0),
                                AuthorName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return authors;
        }

        // Добавить нового автора
        public void AddAuthor(Author author)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Authors (AuthorName) VALUES (@AuthorName)";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorName", author.AuthorName);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Обновить существующего автора
        public void UpdateAuthor(Author author)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Authors SET AuthorName = @AuthorName WHERE AuthorID = @AuthorID";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorName", author.AuthorName);
                    command.Parameters.AddWithValue("@AuthorID", author.AuthorID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Удалить автора
        public void DeleteAuthor(int authorId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Authors WHERE AuthorID = @AuthorID";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorID", authorId);
                    command.ExecuteNonQuery();
                }

                // Сбросить последовательность, если используется SERIAL
                var resetSequenceQuery = "SELECT setval(pg_get_serial_sequence('Authors', 'authorid'), COALESCE(MAX(AuthorID), 0)) FROM Authors";
                using (var resetCommand = new NpgsqlCommand(resetSequenceQuery, connection))
                {
                    resetCommand.ExecuteNonQuery();
                }
            }
        }




        public List<Loan> GetLoans()
        {
            var loans = new List<Loan>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT LoanID, BookID, ReaderID, LoanDate, ReturnDate FROM Loans";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loans.Add(new Loan
                        {
                            LoanID = reader.GetInt32(0),
                            BookID = reader.GetInt32(1),
                            ReaderID = reader.GetInt32(2),
                            LoanDate = reader.GetDateTime(3),
                            ReturnDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4)
                        });
                    }
                }
            }

            return loans;
        }

        /// <summary>
        /// Добавить новую запись выдачи.
        /// </summary>
        public void AddLoan(Loan loan)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    INSERT INTO Loans (BookID, ReaderID, LoanDate, ReturnDate) 
                    VALUES (@BookID, @ReaderID, @LoanDate, @ReturnDate)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookID", loan.BookID);
                    command.Parameters.AddWithValue("@ReaderID", loan.ReaderID);
                    command.Parameters.AddWithValue("@LoanDate", loan.LoanDate);
                    command.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Обновить существующую запись выдачи.
        /// </summary>
        public void UpdateLoan(Loan loan)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    UPDATE Loans 
                    SET BookID = @BookID, ReaderID = @ReaderID, LoanDate = @LoanDate, ReturnDate = @ReturnDate
                    WHERE LoanID = @LoanID";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoanID", loan.LoanID);
                    command.Parameters.AddWithValue("@BookID", loan.BookID);
                    command.Parameters.AddWithValue("@ReaderID", loan.ReaderID);
                    command.Parameters.AddWithValue("@LoanDate", loan.LoanDate);
                    command.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate ?? (object)DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Удалить запись выдачи.
        /// </summary>
        public void DeleteLoan(int loanID)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Loans WHERE LoanID = @LoanID";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoanID", loanID);
                    command.ExecuteNonQuery();
                }

                // Сбросить последовательность, если используется SERIAL или IDENTITY
                var resetSequenceQuery = "SELECT setval(pg_get_serial_sequence('Loans', 'LoanID'), COALESCE(MAX(LoanID), 0)) FROM Loans";
                using (var resetCommand = new NpgsqlCommand(resetSequenceQuery, connection))
                {
                    resetCommand.ExecuteNonQuery();
                }
            }
        }








        public List<Reader> GetReaders()
        {
            var readers = new List<Reader>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT ReaderID, ReaderName, ContactInfo FROM Readers";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        readers.Add(new Reader
                        {
                            ReaderID = reader.GetInt32(0),
                            ReaderName = reader.GetString(1),
                            ContactInfo = reader.GetString(2)
                        });
                    }
                }
            }

            return readers;
        }

        /// <summary>
        /// Добавить нового читателя.
        /// </summary>
        public void AddReader(Reader reader)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                INSERT INTO Readers (ReaderName, ContactInfo) 
                VALUES (@ReaderName, @ContactInfo)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReaderName", reader.ReaderName);
                    command.Parameters.AddWithValue("@ContactInfo", reader.ContactInfo);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Обновить данные читателя.
        /// </summary>
        public void UpdateReader(Reader reader)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                UPDATE Readers 
                SET ReaderName = @ReaderName, ContactInfo = @ContactInfo
                WHERE ReaderID = @ReaderID";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReaderID", reader.ReaderID);
                    command.Parameters.AddWithValue("@ReaderName", reader.ReaderName);
                    command.Parameters.AddWithValue("@ContactInfo", reader.ContactInfo);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Удалить читателя.
        /// </summary>
        public void DeleteReader(int readerID)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Readers WHERE ReaderID = @ReaderID";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReaderID", readerID);
                    command.ExecuteNonQuery();
                }

                // Сбросить последовательность, чтобы следующий ID был равен максимальному в таблице
                var resetSequenceQuery = "SELECT setval(pg_get_serial_sequence('Readers', 'readerid'), COALESCE(MAX(ReaderID), 1)) FROM Readers";
                using (var resetCommand = new NpgsqlCommand(resetSequenceQuery, connection))
                {
                    resetCommand.ExecuteNonQuery();
                }
            }

        }
    }
}