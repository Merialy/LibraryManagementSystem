using System;
using System.Collections.Generic;
using System.Linq;
using Library.Core;

namespace Library
{
    /// <summary>
    /// Основний клас системи управління бібліотекою
    /// </summary>
    public class LibrarySystem : ILibrarySystem
    {
        private List<IBook> books = new List<IBook>();
        private List<IReader> readers = new List<IReader>();

        /// <summary>
        /// Додати нову книгу до бібліотеки
        /// </summary>
        /// <param name="title">Назва книги</param>
        /// <param name="author">Автор книги</param>
        public void AddBook(string title, string author)
        {
            Book book = new Book(title, author);
            books.Add(book);
        }

        /// <summary>
        /// Видалити книгу з бібліотеки
        /// </summary>
        /// <param name="book">Книга для видалення</param>
        public void RemoveBook(IBook book)
        {
            books.Remove(book);
        }

        /// <summary>
        /// Знайти книгу за запитом
        /// </summary>
        /// <param name="query">Пошуковий запит (автор або назва)</param>
        /// <returns>Список знайдених книг</returns>
        public List<IBook> SearchBook(string query)
        {
            List<IBook> result = new List<IBook>();

            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("Параметр для пошуку не задано", nameof(query));
            }

            foreach (IBook book in books)
            {
                if (book.GetAuthor() == query || book.GetTitle() == query)
                {
                    result.Add(book);
                }
            }

            return result;
        }

        /// <summary>
        /// Перевірити доступність книги
        /// </summary>
        /// <param name="book">Книга для перевірки</param>
        /// <returns>True якщо книга доступна</returns>
        public bool CheckAvailability(IBook book)
        {
            return book.IsAvailable();
        }

        /// <summary>
        /// Додати читача до системи
        /// </summary>
        /// <param name="reader">Читач для додавання</param>
        public void AddReader(IReader reader)
        {
            readers.Add(reader);
        }

        /// <summary>
        /// Отримати список всіх книг
        /// </summary>
        /// <returns>Список книг</returns>
        public List<IBook> GetBooks()
        {
            return books;
        }

        /// <summary>
        /// Отримати список всіх читачів
        /// </summary>
        /// <returns>Список читачів</returns>
        public List<IReader> GetReaders()
        {
            return readers;
        }
    }
}