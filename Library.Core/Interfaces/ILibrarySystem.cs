using System.Collections.Generic;

namespace Library.Core
{
    /// <summary>
    /// Інтерфейс для управління бібліотечною системою
    /// </summary>
    public interface ILibrarySystem
    {
        /// <summary>
        /// Додати нову книгу до бібліотеки
        /// </summary>
        void AddBook(string title, string author);

        /// <summary>
        /// Видалити книгу з бібліотеки
        /// </summary>
        void RemoveBook(IBook book);

        /// <summary>
        /// Знайти книгу за запитом (автор/назва)
        /// </summary>
        List<IBook> SearchBook(string query);

        /// <summary>
        /// Перевірити доступність книги
        /// </summary>
        bool CheckAvailability(IBook book);

        /// <summary>
        /// Додати читача до системи
        /// </summary>
        void AddReader(IReader reader);

        /// <summary>
        /// Отримати список всіх книг
        /// </summary>
        List<IBook> GetBooks();

        /// <summary>
        /// Отримати список всіх читачів
        /// </summary>
        List<IReader> GetReaders();
    }
}