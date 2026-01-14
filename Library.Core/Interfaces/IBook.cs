using System;

namespace Library.Core
{
    /// <summary>
    /// Інтерфейс для роботи з книгами
    /// </summary>
    public interface IBook
    {
        /// <summary>
        /// Отримати назву книги
        /// </summary>
        string GetTitle();

        /// <summary>
        /// Отримати автора книги
        /// </summary>
        string GetAuthor();

        /// <summary>
        /// Перевірити чи доступна книга
        /// </summary>
        bool IsAvailable();

        /// <summary>
        /// Оновити статус доступності книги
        /// </summary>
        void UpdateAvailability(bool isAvailable);

        /// <summary>
        /// Отримати дату взяття книги
        /// </summary>
        DateTime GetDateTaken();

        /// <summary>
        /// Отримати дату повернення книги
        /// </summary>
        DateTime GetDateBack();

        /// <summary>
        /// Встановити дату взяття книги
        /// </summary>
        void SetDateTaken(DateTime date);

        /// <summary>
        /// Встановити дату повернення книги
        /// </summary>
        void SetDateBack(DateTime date);
    }
}