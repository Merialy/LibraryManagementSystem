using Library;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    /// <summary>
    /// Тести для класу Book
    /// </summary>
    [TestClass]
    public class BookTest
    {
        [TestMethod]
        [TestCategory("Book")]
        [Description("Тест некоректної ініціалізації з порожніми параметрами")]
        public void IncorrectInitialisation()
        {
            // Arrange
            string title = "";
            string author = "";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Book(title, author), "Назву не вказано");
        }

        [TestMethod]
        [TestCategory("Book")]
        [Description("Тест коректної ініціалізації")]
        public void CorrectInitialisation()
        {
            // Arrange
            string title = "Test Book";
            string author = "Test Author";

            // Act
            Book book = new Book(title, author);

            // Assert
            Assert.AreEqual(title, book.GetTitle());
            Assert.AreEqual(author, book.GetAuthor());
        }

        [TestMethod]
        [TestCategory("Book")]
        [Description("Тест отримання назви книги")]
        public void GetTitle()
        {
            // Arrange
            var book = new Book("Clean Code", "Robert C. Martin");

            // Act
            var title = book.GetTitle();

            // Assert
            Assert.AreEqual("Clean Code", title);
        }

        [TestMethod]
        [TestCategory("Book")]
        [Description("Тест отримання автора книги")]
        public void GetAuthor()
        {
            // Arrange
            var book = new Book("Clean Code", "Robert C. Martin");

            // Act
            var author = book.GetAuthor();

            // Assert
            Assert.AreEqual("Robert C. Martin", author);
        }

        [TestMethod]
        [TestCategory("Book")]
        [Description("Тест встановлення дати взяття")]
        public void SetDate()
        {
            // Arrange
            Book book = new Book("Test Book", "Test Author");
            DateTime date = new DateTime(2022, 1, 1);

            // Act
            book.SetDateTaken(date);

            // Assert
            Assert.AreEqual(date, book.GetDateTaken());
        }

        [TestMethod]
        [TestCategory("Book")]
        [Description("Тест встановлення дати повернення")]
        public void SetDateBack()
        {
            // Arrange
            Book book = new Book("Test Book", "Test Author");
            DateTime date = new DateTime(2022, 2, 1);

            // Act
            book.SetDateBack(date);

            // Assert
            Assert.AreEqual(date, book.GetDateBack());
        }

        [TestMethod]
        [TestCategory("Book")]
        [Description("Тест перевірки доступності книги")]
        public void IsAvailable()
        {
            // Arrange
            var book = new Book("Clean Code", "Robert C. Martin");

            // Act & Assert
            Assert.IsTrue(book.IsAvailable());
        }

        [TestMethod]
        [TestCategory("Book")]
        [Description("Тест отримання дати взяття")]
        public void GetDateTaken()
        {
            // Arrange
            var book = new Book("Clean Code", "Robert C. Martin");
            var dateTaken = new DateTime(2020, 1, 1);

            // Act
            book.SetDateTaken(dateTaken);
            var result = book.GetDateTaken();

            // Assert
            Assert.AreEqual(dateTaken, result);
        }

        [TestMethod]
        [TestCategory("Book")]
        [Description("Тест отримання дати повернення")]
        public void GetDateBack()
        {
            // Arrange
            var book = new Book("Clean Code", "Robert C. Martin");
            var dateBack = new DateTime(2020, 1, 10);

            // Act
            book.SetDateBack(dateBack);
            var result = book.GetDateBack();

            // Assert
            Assert.AreEqual(dateBack, result);
        }

        [TestMethod]
        [TestCategory("Book")]
        [Description("Тест оновлення доступності")]
        public void UpdateAvailability()
        {
            // Arrange
            Book book = new Book("Test Book", "Test Author");

            // Act
            book.UpdateAvailability(false);

            // Assert
            Assert.IsFalse(book.IsAvailable());
        }
    }
}
