using System;
using System.ComponentModel;
using Library.Core;

namespace Library
{
    internal class Program
    {
        static LibrarySystem librarySystem;
        static IReader mainReader;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("===========================================");
            Console.WriteLine("  СИСТЕМА УПРАВЛІННЯ БІБЛІОТЕКОЮ");
            Console.WriteLine("===========================================\n");

            librarySystem = new LibrarySystem();

            // Меню бібліотекаря
            LibraryMenu();

            // Меню реєстрації
            RegistrationMenu();
            mainReader = librarySystem.GetReaders()[2];

            // Меню читача
            ReaderMenu();

            // Виведення історії
            Console.WriteLine("\n===========================================");
            Console.WriteLine("  ІСТОРІЯ КОРИСТУВАЧІВ");
            Console.WriteLine("===========================================\n");

            foreach (IReader r in librarySystem.GetReaders())
            {
                Console.WriteLine(r.InfoReader());
                Console.WriteLine("\nІсторія користувача:");
                Console.WriteLine(r.AboutBookOfReader());
                Console.WriteLine("-------------------------------------------\n");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }

        /// <summary>
        /// Меню бібліотекаря для управління книгами
        /// </summary>
        static void LibraryMenu()
        {
            List<IBook> books;
            int mode = 1;

            Console.WriteLine("📚 МЕНЮ БІБЛІОТЕКАРЯ\n");

            do
            {
                try
                {
                    Console.WriteLine("\nОберіть дію:");
                    Console.WriteLine("1 - Додати книгу");
                    Console.WriteLine("2 - Видалити книгу");
                    Console.WriteLine("0 - Завершити роботу");
                    Console.Write("Ваш вибір: ");

                    mode = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    switch (mode)
                    {
                        case 1:
                            {
                                Console.WriteLine("➕ ДОДАВАННЯ КНИГИ");
                                Console.Write("Введіть назву: ");
                                string title = Console.ReadLine();
                                Console.Write("Введіть автора: ");
                                string author = Console.ReadLine();
                                librarySystem.AddBook(title, author);
                                Console.WriteLine("✅ Книгу успішно додано!");
                            }
                            break;

                        case 2:
                            {
                                Console.WriteLine("🗑️  ВИДАЛЕННЯ КНИГИ");
                                books = librarySystem.GetBooks();

                                if (books.Count == 0)
                                {
                                    Console.WriteLine("❌ Бібліотека порожня!");
                                    break;
                                }

                                for (int i = 0; i < books.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {books[i].GetTitle()} - {books[i].GetAuthor()}");
                                }

                                Console.Write("Введіть номер книги для видалення: ");
                                int delete = int.Parse(Console.ReadLine());

                                if (delete <= 0 || delete > books.Count)
                                {
                                    throw new ArgumentOutOfRangeException(nameof(delete), "Такої книги немає");
                                }

                                librarySystem.RemoveBook(books[delete - 1]);
                                Console.WriteLine("✅ Книгу успішно видалено!");
                            }
                            break;

                        case 0:
                            Console.WriteLine("📚 Бібліотека відкривається для читачів...\n");
                            break;

                        default:
                            Console.WriteLine("❌ Невірний вибір!");
                            break;
                    }

                    // Показати поточний список книг
                    if (mode != 0)
                    {
                        Console.WriteLine("\n📖 Поточний список книг:");
                        books = librarySystem.GetBooks();
                        if (books.Count == 0)
                        {
                            Console.WriteLine("  (порожньо)");
                        }
                        else
                        {
                            foreach (IBook book in books)
                            {
                                Console.WriteLine($"  • {book.GetTitle()} - {book.GetAuthor()}");
                            }
                        }
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine($"❌ Помилка: {e.Message}");
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"❌ Помилка: {e.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("❌ Дані не відповідають формату або введено пусте поле");
                }
            } while (mode != 0);
        }

        /// <summary>
        /// Меню читача для роботи з книгами
        /// </summary>
        static void ReaderMenu()
        {
            List<IBook> books;
            int mode = 1;

            Console.WriteLine("👤 МЕНЮ ЧИТАЧА\n");
            Console.WriteLine($"Вітаємо, {mainReader.InfoReader().Split('\n')[0].Split('-')[1].Trim()}!\n");

            do
            {
                books = librarySystem.GetBooks();

                try
                {
                    Console.WriteLine("\nОберіть дію:");
                    Console.WriteLine("1 - Переглянути список книг");
                    Console.WriteLine("2 - Взяти книгу");
                    Console.WriteLine("3 - Повернути книгу");
                    Console.WriteLine("4 - Знайти книгу");
                    Console.WriteLine("0 - Вийти");
                    Console.Write("Ваш вибір: ");

                    mode = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    switch (mode)
                    {
                        case 1:
                            {
                                Console.WriteLine("📚 СПИСОК КНИГ:");
                                if (books.Count == 0)
                                {
                                    Console.WriteLine("  Бібліотека порожня");
                                    break;
                                }

                                foreach (IBook book in books)
                                {
                                    Console.WriteLine($"  • {book.GetTitle()} - {book.GetAuthor()}");
                                    Console.WriteLine($"    Статус: {(librarySystem.CheckAvailability(book) ? "✅ Доступна" : "❌ Зайнята")}");
                                }
                            }
                            break;

                        case 2:
                            {
                                Console.WriteLine("📖 ВЗЯТТЯ КНИГИ:");
                                if (books.Count == 0)
                                {
                                    Console.WriteLine("  Бібліотека порожня");
                                    break;
                                }

                                for (int i = 0; i < books.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {books[i].GetTitle()} - {books[i].GetAuthor()}");
                                    Console.WriteLine($"   Статус: {(librarySystem.CheckAvailability(books[i]) ? "✅ Доступна" : "❌ Зайнята")}");
                                }

                                Console.Write("Введіть номер книги: ");
                                int take = int.Parse(Console.ReadLine()) - 1;

                                if (take < 0 || take >= books.Count)
                                {
                                    throw new ArgumentOutOfRangeException(nameof(take), "Такої книги немає");
                                }

                                if (books[take].IsAvailable())
                                {
                                    Console.WriteLine("✅ Приємного читання!");
                                    mainReader.BorrowBook(books[take]);
                                }
                                else
                                {
                                    Console.WriteLine("❌ Книга вже взята");
                                }
                            }
                            break;

                        case 3:
                            {
                                Console.WriteLine("📥 ПОВЕРНЕННЯ КНИГИ:");
                                if (books.Count == 0)
                                {
                                    Console.WriteLine("  Бібліотека порожня");
                                    break;
                                }

                                for (int i = 0; i < books.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {books[i].GetTitle()} - {books[i].GetAuthor()}");
                                    Console.WriteLine($"   Статус: {(librarySystem.CheckAvailability(books[i]) ? "✅ Доступна" : "❌ Зайнята")}");
                                }

                                Console.Write("Введіть номер книги: ");
                                int take = int.Parse(Console.ReadLine()) - 1;

                                if (take < 0 || take >= books.Count)
                                {
                                    throw new ArgumentOutOfRangeException(nameof(take), "Такої книги немає");
                                }

                                if (!books[take].IsAvailable())
                                {
                                    Console.WriteLine("✅ Дякуємо за повернення!");
                                    mainReader.ReturnBook(books[take]);
                                }
                                else
                                {
                                    Console.WriteLine("❌ Книга ще в бібліотеці");
                                }
                            }
                            break;

                        case 4:
                            {
                                Console.WriteLine("🔍 ПОШУК КНИГИ:");
                                Console.Write("Введіть автора або назву: ");
                                string find = Console.ReadLine();
                                List<IBook> result = librarySystem.SearchBook(find);

                                if (result.Count == 0)
                                {
                                    Console.WriteLine("❌ Нічого не знайдено");
                                }
                                else
                                {
                                    Console.WriteLine($"✅ Знайдено книг: {result.Count}");
                                    foreach (IBook book in result)
                                    {
                                        Console.WriteLine($"  • {book.GetTitle()} - {book.GetAuthor()}");
                                        Console.WriteLine($"    Статус: {(librarySystem.CheckAvailability(book) ? "✅ Доступна" : "❌ Зайнята")}");
                                    }
                                }
                            }
                            break;

                        case 0:
                            Console.WriteLine("👋 До побачення, гарного дня!");
                            break;

                        default:
                            Console.WriteLine("❌ Невірний вибір!");
                            break;
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine($"❌ Помилка: {e.Message}");
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"❌ Помилка: {e.Message}");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"❌ Помилка: {e.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("❌ Дані не відповідають формату або введено пусте поле");
                }
            } while (mode != 0);
        }

        /// <summary>
        /// Меню реєстрації нового читача
        /// </summary>
        static void RegistrationMenu()
        {
            // Додамо тестових читачів
            Reader reader = new Reader();
            reader.Register("Іван", "ivan@gmail.com");
            librarySystem.AddReader(reader);

            reader = new Reader();
            reader.Register("Василь", "vasya@gmail.com");
            librarySystem.AddReader(reader);

            // Реєстрація нового читача
            reader = new Reader();
            bool success;

            Console.WriteLine("📝 РЕЄСТРАЦІЯ НОВОГО ЧИТАЧА\n");

            do
            {
                success = true;
                try
                {
                    Console.Write("Введіть ім'я: ");
                    string name = Console.ReadLine();

                    if (string.IsNullOrEmpty(name))
                    {
                        success = false;
                        throw new ArgumentNullException(nameof(name), "Ім'я не може бути порожнім");
                    }

                    Console.Write("Введіть email: ");
                    string email = Console.ReadLine();

                    if (string.IsNullOrEmpty(email))
                    {
                        success = false;
                        throw new ArgumentNullException(nameof(email), "Email не може бути порожнім");
                    }

                    reader.Register(name, email);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"❌ {e.Message}\n");
                }
            } while (!success);

            librarySystem.AddReader(reader);

            // Підтвердження даних
            int mode = 1;
            do
            {
                success = true;
                try
                {
                    Console.WriteLine($"\n✅ Ви зареєстровані!");
                    Console.WriteLine(reader.InfoReader());
                    Console.Write("\nДані правильні? (1 - Так, інше - Ні): ");
                    mode = int.Parse(Console.ReadLine());

                    if (mode != 1)
                    {
                        Console.WriteLine("\n📝 РЕДАГУВАННЯ ДАНИХ\n");
                        Console.Write("Введіть нове ім'я: ");
                        string name = Console.ReadLine();

                        if (string.IsNullOrEmpty(name))
                        {
                            success = false;
                            throw new ArgumentNullException(nameof(name), "Ім'я не може бути порожнім");
                        }

                        Console.Write("Введіть новий email: ");
                        string email = Console.ReadLine();

                        if (string.IsNullOrEmpty(email))
                        {
                            success = false;
                            throw new ArgumentNullException(nameof(email), "Email не може бути порожнім");
                        }

                        reader.Change(name, email);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("❌ Невірний формат!\n");
                    success = false;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"❌ {e.Message}\n");
                }
            } while (!success || mode != 1);

            Console.WriteLine("\n✅ Реєстрація завершена!\n");
        }
    }
}