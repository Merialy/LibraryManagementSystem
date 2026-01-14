using System;

namespace Library.Core
{
    /// <summary>
    /// Інтерфейс для роботи з читачами
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Зареєструвати нового читача
        /// </summary>
        void Register(string name, string email);

        /// <summary>
        /// Змінити дані читача
        /// </summary>
        void Change(string name, string email);

        /// <summary>
        /// Взяти книгу з бібліотеки
        /// </summary>
        void BorrowBook(IBook book);

        /// <summary>
        /// Повернути книгу в бібліотеку
        /// </summary>
        void ReturnBook(IBook book);

        /// <summary>
        /// Отримати інформацію про читача
        /// </summary>
        string InfoReader();

        /// <summary>
        /// Отримати інформацію про книги читача
        /// </summary>
        string AboutBookOfReader();
    }
}