using System;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    /// <summary>
    /// Тести для класу Reader
    /// </summary>
    [TestClass]
    public class ReaderTest
    {
        private Reader? _reader;

        [TestMethod]
        [TestCategory("Reader")]
        [Description("Тест успішної реєстрації")]
        public void Register()
        {
            // Arrange
            Reader reader = new Reader();

            // Act
            reader.Register("John Doe", "john.doe@example.com");

            // Assert
            Assert.AreEqual("Ім'я - John Doe\nПошта - john.doe@example.com", reader.InfoReader());
        }

        [TestMethod]
        [TestCategory("Reader")]
        [Description("Тест реєстрації з порожнім ім'ям")]
        public void Register_UnvalidInputs()
        {
            // Arrange
            Reader reader = new Reader();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => reader.Register("", "john.doe@example.com"), "Ім'я не вказано");
        }

        [TestMethod]
        [TestCategory("Reader")]
        [Description("Тест реєстрації з порожнім email")]
        public void Register_UnvalidInputs2()
        {
            // Arrange
            Reader reader = new Reader();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => reader.Register("John", ""), "Пошту не вказано");
        }

        [TestMethod]
        [TestCategory("Reader")]
        [Description("Тест взяття книги")]
        public void BorrowBook()
        {
            // Arrange
            Reader reader = new Reader();
            Book book = new Book("Sample Book", "John Smith");

            // Act
            reader.BorrowBook(book);

            // Assert
            Assert.IsFalse(book.IsAvailable());
        }

        [TestMethod]
        [TestCategory("Reader")]
        [Description("Тест повернення книги")]
        public void ReturnBook()
        {
            // Arrange
            Reader reader = new Reader();
            Book book = new Book("Sample Book", "John Smith");

            // Act
            reader.BorrowBook(book);
            reader.ReturnBook(book);

            // Assert
            Assert.IsTrue(book.IsAvailable());
            Assert.IsNotNull(book.GetDateBack());
        }

        [TestMethod]
        [TestCategory("Reader")]
        [Description("Тест зміни даних з валідними параметрами")]
        public void Change_WithValidInputs()
        {
            // Arrange
            _reader = new Reader();
            _reader.Register("John", "john@example.com");
            string newName = "Jane";
            string newEmail = "jane@example.com";

            // Act
            _reader.Change(newName, newEmail);

            // Assert
            Assert.AreEqual(newName, _reader.InfoReader().Split('\n')[0].Split('-')[1].Trim());
            Assert.AreEqual(newEmail, _reader.InfoReader().Split('\n')[1].Split('-')[1].Trim());
        }

        [TestMethod]
        [TestCategory("Reader")]
        [Description("Тест зміни даних з порожнім ім'ям")]
        public void Change_UnvalidInputs()
        {
            // Arrange
            _reader = new Reader();
            string newName = "";
            string newEmail = "jojo@example.com";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _reader.Change(newName, newEmail), "Ім'я не вказано");
        }

        [TestMethod]
        [TestCategory("Reader")]
        [Description("Тест зміни даних з порожнім email")]
        public void Change_UnvalidInputs2()
        {
            // Arrange
            _reader = new Reader();
            string newName = "Jane";
            string newEmail = "";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _reader.Change(newName, newEmail), "Пошту не вказано");
        }
    }
}
