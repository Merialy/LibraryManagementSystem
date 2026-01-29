using System;
using Library.Core;

namespace Library
{
    /// <summary>
    /// Клас для представлення книги в бібліотеці
    /// </summary>
    public class Book : IBook
    {
        private string title = "Unknown";
        private string author = "Unknown";
        private bool isAvailable = true;
        // New feature in progress
        private DateTime dateTaken = DateTime.MinValue;
        private DateTime dateBack = DateTime.MinValue;

        /// <summary>
        /// Конструктор класу Book
        /// </summary>
        /// <param name="title">Назва книги</param>
        /// <param name="author">Автор книги</param>
        /// <exception cref="ArgumentNullException">Якщо назва або автор порожні</exception>
        public Book(string title, string author)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException(nameof(title), "Назву не вказано");

            this.title = title;

            if (string.IsNullOrEmpty(author))
                throw new ArgumentNullException(nameof(author), "Автор не вказан");

            this.author = author;
        }

        /// <summary>
        /// Отримати назву книги
        /// </summary>
        public string GetTitle()
        {
            return title;
        }

        /// <summary>
        /// Отримати автора книги
        /// </summary>
        public string GetAuthor()
        {
            return author;
        }

        /// <summary>
        /// Перевірити чи доступна книга
        /// </summary>
        public bool IsAvailable()
        {
            return isAvailable;
        }

        /// <summary>
        /// Оновити статус доступності книги
        /// </summary>
        /// <param name="isAvailable">Новий статус</param>
        public void UpdateAvailability(bool isAvailable)
        {
            this.isAvailable = isAvailable;
        }

        /// <summary>
        /// Отримати дату взяття книги
        /// </summary>
        public DateTime GetDateTaken()
        {
            return dateTaken;
        }

        /// <summary>
        /// Отримати дату повернення книги
        /// </summary>
        public DateTime GetDateBack()
        {
            return dateBack;
        }

        /// <summary>
        /// Встановити дату взяття книги
        /// </summary>
        /// <param name="date">Дата взяття</param>
        public void SetDateTaken(DateTime date)
        {
            dateTaken = date;
        }

        /// <summary>
        /// Встановити дату повернення книги
        /// </summary>
        /// <param name="date">Дата повернення</param>
        public void SetDateBack(DateTime date)
        {
            dateBack = date;
        }
    }
}