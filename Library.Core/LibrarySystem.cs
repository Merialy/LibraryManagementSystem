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
        // Тепер поля readonly - інкапсуляція залежностей
        private readonly List<IBook> _books;
        private readonly List<IReader> _readers;

        /// <summary>
        /// Конструктор з впровадженням залежностей (Constructor Injection)
        /// </summary>
        /// <param name="books">Зовнішній список книг (для тестів можна передати фейковий)</param>
        /// <param name="readers">Зовнішній список читачів</param>
        public LibrarySystem(List<IBook> books = null, List<IReader> readers = null)
        {
            _books = books ?? new List<IBook>();
            _readers = readers ?? new List<IReader>();
        }

        /// <summary>
        /// Додати нову книгу до бібліотеки
        /// </summary>
        public void AddBook(string title, string author)
        {
            // Створюємо конкретний екземпляр, але додаємо в впроваджений список
            IBook book = new Book(title, author);
            _books.Add(book);
        }

        /// <summary>
        /// Видалити книгу з бібліотеки
        /// </summary>
        public void RemoveBook(IBook book)
        {
            _books.Remove(book);
        }

        /// <summary>
        /// Знайти книгу за запитом
        /// </summary>
        public List<IBook> SearchBook(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("Параметр для пошуку не задано", nameof(query));
            }

            // Використовуємо LINQ для пошуку
            return _books.Where(b => b.GetAuthor().Contains(query) || b.GetTitle().Contains(query)).ToList();
        }

        /// <summary>
        /// Перевірити доступність книги
        /// </summary>
        public bool CheckAvailability(IBook book)
        {
            return book?.IsAvailable() ?? false;
        }

        /// <summary>
        /// Додати читача до системи
        /// </summary>
        public void AddReader(IReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            _readers.Add(reader);
        }

        /// <summary>
        /// Отримати список всіх книг
        /// </summary>
        public List<IBook> GetBooks() => _books;

        /// <summary>
        /// Отримати список всіх читачів
        /// </summary>
        public List<IReader> GetReaders() => _readers;
    }
}