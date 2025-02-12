using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Абстракция
abstract class User
{
    public string Name { get; set; }

    public User(string name)
    {
        Name = name;
    }

    // Полиморфизм
    public abstract void DisplayMenu(Library library);
}

// Наследование
class Librarian : User
{
    public Librarian(string name) : base(name) { }

    public override void DisplayMenu(Library library)
    {
        while (true)
        {
            Console.WriteLine("\n--- Меню библиотекаря ---");
            Console.WriteLine("1. Добавить новую книгу");
            Console.WriteLine("2. Удалить книгу из системы");
            Console.WriteLine("3. Зарегистрировать нового пользователя");
            Console.WriteLine("4. Просмотреть список всех пользователей");
            Console.WriteLine("5. Просмотреть список всех книг (с их статусами)");
            Console.WriteLine("6. Выйти");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook(library);
                    break;
                case "2":
                    RemoveBook(library);
                    break;
                case "3":
                    RegisterUser(library);
                    break;
                case "4":
                    ViewAllUsers(library);
                    break;
                case "5":
                    ViewAllBooks(library);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    private void AddBook(Library library)
    {
        Console.Write("Введите название книги: ");
        string title = Console.ReadLine();
        Console.Write("Введите автора книги: ");
        string author = Console.ReadLine();

        library.AddBook(title, author);
    }

    private void RemoveBook(Library library)
    {
        Console.Write("Введите название книги для удаления: ");
        string title = Console.ReadLine();

        library.RemoveBook(title);
    }

    private void RegisterUser(Library library)
    {
        Console.Write("Введите имя нового пользователя: ");
        string name = Console.ReadLine();

        library.AddUser(name);
    }

    private void ViewAllUsers(Library library)
    {
        library.ViewAllUsers();
    }

    private void ViewAllBooks(Library library)
    {
        library.ViewAllBooks();
    }
}

// Наследование
class Customer : User
{
    public Customer(string name) : base(name) { }

    public override void DisplayMenu(Library library)
    {
        while (true)
        {
            Console.WriteLine("\n--- Меню пользователя ---");
            Console.WriteLine("1. Просмотреть доступные книги");
            Console.WriteLine("2. Взять книгу");
            Console.WriteLine("3. Вернуть книгу");
            Console.WriteLine("4. Просмотреть список взятых книг");
            Console.WriteLine("5. Выйти");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewAvailableBooks(library);
                    break;
                case "2":
                    BorrowBook(library);
                    break;
                case "3":
                    ReturnBook(library);
                    break;
                case "4":
                    ViewBorrowedBooks(library);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    private void ViewAvailableBooks(Library library)
    {
        library.ViewAvailableBooks();
    }

    private void BorrowBook(Library library)
    {
        Console.Write("Введите название книги для взятия: ");
        string title = Console.ReadLine();

        library.BorrowBook(Name, title);
    }

    private void ReturnBook(Library library)
    {
        Console.Write("Введите название книги для возврата: ");
        string title = Console.ReadLine();

        library.ReturnBook(Name, title);
    }

    private void ViewBorrowedBooks(Library library)
    {
        library.ViewBorrowedBooks(Name);
    }
}

// Класс для представления книги
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }

    public Book(string title, string author, bool isAvailable = true)
    {
        Title = title;
        Author = author;
        IsAvailable = isAvailable;
    }

    public override string ToString()
    {
        return $"{Title} - {Author} ({(IsAvailable ? "Доступна" : "Выдана")})";
    }
}

// Класс для представления пользователя библиотеки
class LibraryUser
{
    public string Name { get; set; }
    public List<string> BorrowedBooks { get; set; }

    public LibraryUser(string name)
    {
        Name = name;
        BorrowedBooks = new List<string>();
    }

    public override string ToString()
    {
        return Name;
    }
}

// Инкапсуляция
class Library
{
    // Инкапсуляция
    private List<Book> books;
    private List<LibraryUser> users;
    private const string BooksFilePath = "books.txt";
    private const string UsersFilePath = "users.txt";

    public Library()
    {
        books = new List<Book>();
        users = new List<LibraryUser>();
        LoadData();
    }

    // Методы для работы с книгами
    public void AddBook(string title, string author)
    {
        books.Add(new Book(title, author));
        Console.WriteLine($"Книга '{title}' добавлена.");
        SaveData();
    }

    public void RemoveBook(string title)
    {
        Book bookToRemove = books.FirstOrDefault(b => b.Title == title);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine($"Книга '{title}' удалена.");
            SaveData();
        }
        else
        {
            Console.WriteLine($"Книга '{title}' не найдена.");
        }
    }

    public void ViewAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("В библиотеке нет книг.");
            return;
        }

        Console.WriteLine("\n--- Список всех книг ---");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    public void ViewAvailableBooks()
    {
        var availableBooks = books.Where(b => b.IsAvailable).ToList();
        if (availableBooks.Count == 0)
        {
            Console.WriteLine("Нет доступных книг.");
            return;
        }

        Console.WriteLine("\n--- Доступные книги ---");
        foreach (var book in availableBooks)
        {
            Console.WriteLine(book);
        }
    }

    public void BorrowBook(string userName, string title)
    {
        Book bookToBorrow = books.FirstOrDefault(b => b.Title == title && b.IsAvailable);
        LibraryUser user = users.FirstOrDefault(u => u.Name == userName);

        if (bookToBorrow != null && user != null)
        {
            bookToBorrow.IsAvailable = false;
            user.BorrowedBooks.Add(title);
            Console.WriteLine($"Книга '{title}' выдана пользователю {userName}.");
            SaveData();
        }
        else if (bookToBorrow == null)
        {
            Console.WriteLine($"Книга '{title}' не доступна или не найдена.");
        }
        else
        {
            Console.WriteLine($"Пользователь '{userName}' не найден.");
        }
    }

    public void ReturnBook(string userName, string title)
    {
        Book bookToReturn = books.FirstOrDefault(b => b.Title == title && !b.IsAvailable);
        LibraryUser user = users.FirstOrDefault(u => u.Name == userName);

        if (bookToReturn != null && user != null && user.BorrowedBooks.Contains(title))
        {
            bookToReturn.IsAvailable = true;
            user.BorrowedBooks.Remove(title);
            Console.WriteLine($"Книга '{title}' возвращена пользователем {userName}.");
            SaveData();
        }
        else if (bookToReturn == null)
        {
            Console.WriteLine($"Книга '{title}' не найдена или уже доступна.");
        }
        else if (user == null)
        {
            Console.WriteLine($"Пользователь '{userName}' не найден.");
        }
        else
        {
            Console.WriteLine($"У пользователя '{userName}' нет книги '{title}'.");
        }
    }

    public void ViewBorrowedBooks(string userName)
    {
        LibraryUser user = users.FirstOrDefault(u => u.Name == userName);

        if (user != null)
        {
            if (user.BorrowedBooks.Count == 0)
            {
                Console.WriteLine($"У пользователя '{userName}' нет взятых книг.");
                return;
            }

            Console.WriteLine($"\n--- Книги, взятые пользователем {userName} ---");
            foreach (var bookTitle in user.BorrowedBooks)
            {
                Console.WriteLine(bookTitle);
            }
        }
        else
        {
            Console.WriteLine($"Пользователь '{userName}' не найден.");
        }
    }

    // Методы для работы с пользователями
    public void AddUser(string name)
    {
        if (users.Any(u => u.Name == name))
        {
            Console.WriteLine($"Пользователь '{name}' уже существует.");
            return;
        }
        users.Add(new LibraryUser(name));
        Console.WriteLine($"Пользователь '{name}' зарегистрирован.");
        SaveData();
    }

    public void ViewAllUsers()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("В системе нет пользователей.");
            return;
        }

        Console.WriteLine("\n--- Список всех пользователей ---");
        foreach (var user in users)
        {
            Console.WriteLine(user);
        }
    }

    // Методы для сохранения и загрузки данных из файлов
    private void LoadData()
    {
        try
        {
            // Загрузка книг
            if (File.Exists(BooksFilePath))
            {
                string[] bookLines = File.ReadAllLines(BooksFilePath);
                foreach (string line in bookLines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        string title = parts[0];
                        string author = parts[1];
                        bool isAvailable = bool.Parse(parts[2]);
                        books.Add(new Book(title, author, isAvailable));
                    }
                }
            }

            // Загрузка пользователей
            if (File.Exists(UsersFilePath))
            {
                string[] userLines = File.ReadAllLines(UsersFilePath);
                foreach (string line in userLines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length >= 1)
                    {
                        string name = parts[0];
                        LibraryUser user = new LibraryUser(name);
                        // Загрузка взятых книг
                        if (parts.Length > 1)
                        {
                            for (int i = 1; i < parts.Length; i++)
                            {
                                user.BorrowedBooks.Add(parts[i]);
                            }

                        }
                        users.Add(user);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
        }
    }

    private void SaveData()
    {
        try
        {
            // Сохранение книг
            using (StreamWriter writer = new StreamWriter(BooksFilePath))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.Title}|{book.Author}|{book.IsAvailable}");
                }
            }

            // Сохранение пользователей
            using (StreamWriter writer = new StreamWriter(UsersFilePath))
            {
                foreach (var user in users)
                {
                    writer.Write(user.Name);
                    foreach (var book in user.BorrowedBooks)
                    {
                        writer.Write("|" + book);
                    }
                    writer.WriteLine();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        while (true)
        {
            Console.WriteLine("\n||||| Главное меню |||||");
            Console.WriteLine("1. Войти как библиотекарь");
            Console.WriteLine("2. Войти как пользователь");
            Console.WriteLine("3. Выйти");

            Console.Write("Выберите роль: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите ваше имя: ");
                    string librarianName = Console.ReadLine();
                    Librarian librarian = new Librarian(librarianName);
                    librarian.DisplayMenu(library);
                    break;
                case "2":
                    Console.Write("Введите ваше имя: ");
                    string userName = Console.ReadLine();
                    Customer customer = new Customer(userName);
                    customer.DisplayMenu(library);
                    break;
                case "3":
                    Console.WriteLine("Выход из программы.");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}