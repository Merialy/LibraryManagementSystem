using System;
using System.Collections.Generic;
using Library;
using Library.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTest
{
    /// <summary>
    /// Тести для перевірки Dependency Injection та незалежних класів
    /// </summary>
    [TestClass]
    public class DITests
    {
        /// <summary>
        /// Тест: Constructor Injection працює коректно - система використовує передані зовнішні списки
        /// </summary>
        [TestMethod]
        [TestCategory("DI")]
        [Description("Перевірка Constructor Injection - LibrarySystem використовує впроваджені списки")]
        public void LibrarySystem_ConstructorInjection_UsesProvidedLists()
        {
            // Arrange - створюємо "фейкові" залежності (не моки, а реальні порожні списки)
            var injectedBooks = new List<IBook>();
            var injectedReaders = new List<IReader>();

            // Act - впроваджуємо залежності через конструктор
            var library = new LibrarySystem(injectedBooks, injectedReaders);
            library.AddBook("DI Test Book", "DI Author");

            // Assert - перевіряємо, що система використала наші списки, а не створила нові
            Assert.HasCount(1, injectedBooks);
            Assert.AreEqual("DI Test Book", injectedBooks[0].GetTitle());
        }

        /// <summary>
        /// Тест: Book є незалежним класом (не залежить від інших класів проєкту)
        /// </summary>
        [TestMethod]
        [TestCategory("Independent")]
        [Description("Book - незалежний клас, може бути створений без зовнішніх залежностей")]
        public void Book_IsIndependentClass()
        {
            // Arrange & Act - створюємо книгу без жодних зовнішніх залежностей
            var book = new Book("Clean Code", "Robert C. Martin");

            // Assert - перевіряємо коректність роботи
            Assert.IsNotNull(book);
            Assert.AreEqual("Clean Code", book.GetTitle());
            Assert.IsTrue(book.IsAvailable());
        }

        /// <summary>
        /// Тест: Reader працює з IBook (залежність від абстракції, не конкретики)
        /// </summary>
        [TestMethod]
        [TestCategory("DI")]
        [Description("Reader працює з інтерфейсом IBook (Dependency Inversion)")]
        public void Reader_WorksWithIBookInterface()
        {
            // Arrange - створюємо Reader та Book (Book реалізує IBook)
            IReader reader = new Reader();
            reader.Register("Test User", "test@test.com");

            IBook book = new Book("Test Book", "Author");

            // Act
            reader.BorrowBook(book);

            // Assert
            Assert.IsFalse(book.IsAvailable());
        }

        /// <summary>
        /// Тест: Можна підмінити залежності для ізольованого тестування LibrarySystem
        /// </summary>
        [TestMethod]
        [TestCategory("DI")]
        [Description("Ізольоване тестування LibrarySystem з підміненими залежностями")]
        public void LibrarySystem_CanBeTestedWithFakeDependencies()
        {
            // Arrange - створюємо фейковий стан бібліотеки
            var fakeBooks = new List<IBook>
            {
                new Book("Existing Book 1", "Author 1"),
                new Book("Existing Book 2", "Author 2")
            };

            var library = new LibrarySystem(fakeBooks, null); // readers не важливі для цього тесту

            // Act
            var result = library.SearchBook("Existing");

            // Assert
            Assert.HasCount(2, result);
        }

        /// <summary>
        /// Тест: Constructor Injection з null-параметрами створює дефолтні списки
        /// </summary>
        [TestMethod]
        [TestCategory("DI")]
        [Description("Конструктор коректно обробляє null-значення (створює дефолтні списки)")]
        public void LibrarySystem_Constructor_HandlesNullParameters()
        {
            // Act - передаємо null (імітація виклику без параметрів)
            var library = new LibrarySystem(null, null);

            // Assert - система повинна працювати зі створеними дефолтними списками
            Assert.IsNotNull(library.GetBooks());
            Assert.IsNotNull(library.GetReaders());
            Assert.IsEmpty(library.GetBooks());
        }
    }
}