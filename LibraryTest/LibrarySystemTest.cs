using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    /// <summary>
    /// Тести для класу LibrarySystem
    /// </summary>
    [TestClass]
    public class LibrarySystemTest
    {
        private LibrarySystem? _library;

        [TestMethod]
        [TestCategory("LibrarySystem")]
        [Description("Тест додавання читача")]
        public void AddReader()
        {
            // Arrange
            _library = new LibrarySystem();
            var reader = new Reader();

            // Act
            _library.AddReader(reader);

            // Assert
            Assert.HasCount(1, _library.GetReaders());
        }

        [TestMethod]
        [TestCategory("LibrarySystem")]
        [Description("Тест додавання книги")]
        public void AddBook()
        {
            // Arrange
            _library = new LibrarySystem();

            // Act
            _library.AddBook("Sample Book", "Author Name");

            // Assert
            Assert.HasCount(1, _library.GetBooks());
            Assert.AreEqual("Sample Book", _library.GetBooks()[0].GetTitle());
            Assert.AreEqual("Author Name", _library.GetBooks()[0].GetAuthor());
        }

        [TestMethod]
        [TestCategory("LibrarySystem")]
        [Description("Тест видалення книги")]
        public void RemoveBook()
        {
            // Arrange
            _library = new LibrarySystem();
            var book = new Book("Sample Book", "Author Name");
            _library.AddBook("Sample Book", "Author Name");

            // Act
            _library.RemoveBook(book);

            // Assert
            Assert.HasCount(1, _library.GetBooks());
        }

        [TestMethod]
        [TestCategory("LibrarySystem")]
        [Description("Тест перевірки доступності")]
        public void CheckAvailability()
        {
            // Arrange
            _library = new LibrarySystem();
            var book = new Book("Sample Book", "Author Name");
            book.UpdateAvailability(true);
            _library.AddBook("Sample Book", "Author Name");

            // Act
            bool isAvailable = _library.CheckAvailability(book);

            // Assert
            Assert.IsTrue(isAvailable);
        }

        [TestMethod]
        [TestCategory("LibrarySystem")]
        [Description("Тест пошуку книги")]
        public void SearchBook()
        {
            // Arrange
            _library = new LibrarySystem();
            _library.AddBook("Book One", "John Doe");
            _library.AddBook("Book Two", "Jane Doe");

            // Act
            var result = _library.SearchBook("Jane Doe");

            // Assert
            Assert.HasCount(1, result);
            Assert.AreEqual("Book Two", result[0].GetTitle());
        }

        [TestMethod]
        [TestCategory("LibrarySystem")]
        [Description("Тест пошуку з невалідним запитом")]
        public void SearchBook_InvalidQuery()
        {
            // Arrange
            _library = new LibrarySystem();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _library.SearchBook(""), "Параметр для пошуку не задано");
        }
    }
}