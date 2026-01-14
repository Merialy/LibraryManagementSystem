using System;
using System.Collections.Generic;
using Library.Core;

namespace Library
{
    /// <summary>
    /// Клас для представлення читача бібліотеки
    /// </summary>
    public class Reader : IReader
    {
        private string name = "Unknown";
        private string email = "Unknown";
        private List<IBook> books = new List<IBook>();

        /// <summary>
        /// Зареєструвати нового читача
        /// </summary>
        /// <param name="name">Ім'я читача</param>
        /// <param name="email">Email читача</param>
        public void Register(string name, string email)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Ім'я не вказано");

            this.name = name;

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email), "Пошту не вказано");

            this.email = email;
        }

        /// <summary>
        /// Змінити дані читача
        /// </summary>
        /// <param name="name">Нове ім'я</param>
        /// <param name="email">Новий email</param>
        public void Change(string name, string email)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Ім'я не вказано");

            this.name = name;

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email), "Пошту не вказано");

            this.email = email;
        }

        /// <summary>
        /// Взяти книгу з бібліотеки
        /// </summary>
        /// <param name="book">Книга для взяття</param>
        public void BorrowBook(IBook book)
        {
            books.Add(book);
            book.SetDateTaken(DateTime.Now);
            book.UpdateAvailability(false);
        }

        /// <summary>
        /// Повернути книгу в бібліотеку
        /// </summary>
        /// <param name="book">Книга для повернення</param>
        public void ReturnBook(IBook book)
        {
            book.SetDateBack(DateTime.Now);
            book.UpdateAvailability(true);
        }

        /// <summary>
        /// Отримати інформацію про читача
        /// </summary>
        public string InfoReader()
        {
            return $"Ім'я - {name}\nПошта - {email}";
        }

        /// <summary>
        /// Отримати інформацію про книги читача
        /// </summary>
        public string AboutBookOfReader()
        {
            string result = "";

            foreach (IBook book in books)
            {
                result += $" Книга: {book.GetTitle()} Автор: {book.GetAuthor()}\n";

                if (book.IsAvailable())
                {
                    result += "Книгу можна взяти\n";
                }
                else
                {
                    result += "Книга зайнята\n";
                }

                result += $"Книгу було взято в {book.GetDateTaken()}\n";

                if (book.GetDateBack() != DateTime.MinValue)
                {
                    result += $"Книгу було повернено в {book.GetDateBack()}\n";
                }
            }

            return result;
        }
    }
}