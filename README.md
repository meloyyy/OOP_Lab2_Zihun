# OOP_Lab2_Zihun

## Практична робота №2

##Мета роботи
- використовувати принцип інкапсуляції ООП;
- створювати і використовувати властивості для забезпечення встановлення коректних значень і зчитування закритих полів класів;
- створювати і використовувати закриті і загальнодоступні методи в класі.

### Опис завдання

На основі отриманого на лекції 2 теоретичного матеріалу скорегувати програму для практичної роботи Lab-1 наступним чином:

1.	Всі поля класу повинні бути інкапсульовані за допомогою модифікатора доступу private.

2.	Для кожного private-поля, яке необхідне для зовнішньої взаємодії, в класі мають бути додані відповідні public-властивості  (public-properties)*.
*якщо предметна область вимагає, то секція set властивості обовʼязково повинна мати відповідні перевірки. При спробі встановлення некоректного значення має генеруватися відповідний за типом exception з інформативним повідомленням.

3.	До класу додати хоча б одну автовластивість, для якої передбачите значення за замовчуванням.

4.	До класу додати хоча б одну обчислювальну властивість, яка не буде повʼязана з відповідним private-полем класу і буде використовувати інші властивості та/або методи.

5.	Хоча б одна властивість повинна мати різній рівень доступу для секцій get i set***
***памʼятаємо, що модифікатор доступу секції має бути більш обмежувальним, ніж модифікатор самої властивості.

6.	У public-методах використати private-методи для приховування деталей реалізації public-методів, що також буде забезпечувати принцип інкапсуляції.
7.	Встановлення та зчитування значень полів обʼєктів в основній програмі реалізувати через додані у клас властивості.
Примітка: при встановленні значення властивості може бути згенерований exception. а значить необхіпне його оброблення.

8.	Меню в програмі залишається з практичної роботи Lab-1:
1 - Додати обʼєкт
2 - Переглянути всі обʼєкти
3 - Знайти обʼєкт
4 - Продемонструвати поведінку
5 - Видалити обʼєкт
0 - Вийти з програми


### Технології

Мова програмування: **C#**
Тип застосунку: **Консольний**

### Код:

####Genre.cs
```
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab2_Zihun
{
    enum Genre
    {
        Action,
        Comedy,
        Drama,
        SciFi,
        Horror,
        Fantasy
    }
}
```
####Movie.cs
```
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab2_Zihun
{
    internal class Movie
    {
        private int _id;
        private string _title;
        private DateTime releaseDate;
        private double _rating;
        private int _duration;

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public DateTime AddDateToDB { get; set; } = DateTime.Now;

        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Назва не може бути порожньою");
                _title = value;
            }
        }

        public Genre MovieGenre { get; set; }

        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set
            {
                if (value.Year < 1895 || value > DateTime.Now)
                    throw new Exception("Дата має починатися від 1895 до сьогодення");
                releaseDate = value;
            }
        }

        public bool IsNew
        {
            get { return releaseDate.Year >= DateTime.Now.Year - 5; }
        }

        public double Rating
        {
            get { return _rating; }
            set
            {
                if (value < 0 || value > 10)
                    throw new Exception("Рейтинг має бути від 0 до 10");
                _rating = value;
            }
        }

        public int Duration
        {
            get { return _duration; }
            set
            {
                if (value <= 0)
                    throw new Exception("Тривалість має бути більше 0");
                _duration = value;
            }
        }

        public Movie(int id, string title, Genre genre, DateTime date, double rating, int duration)
        {
            Id = id;
            Title = title;
            MovieGenre = genre;
            ReleaseDate = date;
            Rating = rating;
            Duration = duration;
        }

        public void FilmInfo()
        {
            ShowInfo();
        }

        private void ShowInfo()
        {
            Console.WriteLine(Id + " | " + Title + " | " + MovieGenre + " | " + ReleaseDate.ToShortDateString() + " | " + Rating + " | " + Duration + " хв");
        }
    }
}
```
####Program.cs
```
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab2_Zihun
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<Movie> movies = new List<Movie>();
            int nextId = 1;

            Console.Write("Введіть кількість фільмів: ");
            int maxMovies = int.Parse(Console.ReadLine());

            while (true)
            {
                Console.WriteLine("1 - Додати об'єкт");
                Console.WriteLine("2 - Переглянути всі об'єкти");
                Console.WriteLine("3 - Знайти об'єкт");
                Console.WriteLine("4 - Продемонструвати поведінку");
                Console.WriteLine("5 - Видалити об'єкт");
                Console.WriteLine("0 - Вийти з програми");

                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    if (movies.Count >= maxMovies)
                    {
                        Console.WriteLine("Досягнуто ліміт фільмів");
                        continue;
                    }

                    Console.Write("Назва: ");
                    string title = Console.ReadLine();

                    Genre genre;
                    do
                    {
                        Console.Write("Жанр (Action, Comedy, Drama, SciFi, Horror, Fantasy): ");
                        string input = Console.ReadLine();

                        try
                        {
                            genre = (Genre)Enum.Parse(typeof(Genre), input, true);

                            if (!Enum.IsDefined(typeof(Genre), genre))
                                throw new ArgumentException();

                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Такого жанру немає в списку");
                        }

                    } while (true);

                    DateTime releaseDate;
                    do
                    {
                        Console.Write("Дата виходу (yyyy-mm-dd): ");
                        try
                        {
                            releaseDate = DateTime.Parse(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Неправильний формат дати");
                        }
                    }
                    while (true);

                    double rating;
                    do
                    {
                        Console.Write("Рейтинг 0 - 10: ");
                        try
                        {
                            rating = double.Parse(Console.ReadLine());

                            if (rating < 0 || rating > 10)
                                throw new ArgumentOutOfRangeException();

                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Некоректне число");
                        }
                    }
                    while (true);

                    int duration;
                    do
                    {
                        Console.Write("Тривалість (у хвилинах): ");
                        try
                        {
                            duration = int.Parse(Console.ReadLine());

                            if (duration <= 0)
                            {
                                Console.WriteLine("Введіть додатнє число");
                                continue;
                            }

                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Введіть ціле число");
                        }
                    }
                    while (true);

                    try
                    {
                        Movie newMovie = new Movie(nextId, title, genre, releaseDate, rating, duration);
                        movies.Add(newMovie);
                        nextId++;
                        Console.WriteLine("Фільм додано");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка: " + ex.Message);
                    }
                }
                else if (choice == "2")
                {
                    if (movies.Count == 0)
                        Console.WriteLine("Пусто");
                    else
                        foreach (var movie in movies) movie.FilmInfo();
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Знайти фільм за ");
                    Console.WriteLine("1 - Жанр");
                    Console.WriteLine("2 - Рейтинг");
                    Console.WriteLine("3 - Тривалість");

                    int searchType = int.Parse(Console.ReadLine());

                    if (searchType == 1)
                    {
                        Console.Write("Жанр (Action, Comedy, Drama, SciFi, Horror, Fantasy): ");
                        Genre searchGenre = (Genre)Enum.Parse(typeof(Genre), Console.ReadLine(), true);
                        bool found = false;

                        foreach (var movie in movies)
                        {
                            if (movie.MovieGenre == searchGenre)
                            {
                                movie.FilmInfo();
                                found = true;
                            }
                        }

                        if (!found)
                            Console.WriteLine("Фільми з такими парамнтрами відсутні");
                    }
                    else if (searchType == 2)
                    {
                        Console.Write("Мінімальний рейтинг: ");
                        double minRating = double.Parse(Console.ReadLine());
                        bool found = false;

                        foreach (var movie in movies)
                        {
                            if (movie.Rating >= minRating)
                            {
                                movie.FilmInfo();
                                found = true;
                            }
                        }

                        if (!found)
                            Console.WriteLine("Фільми з такими парамнтрами відсутні");
                    }
                    else if (searchType == 3)
                    {
                        Console.Write("Мінімальна тривалість: ");
                        int minDuration = int.Parse(Console.ReadLine());
                        bool found = false;

                        foreach (var movie in movies)
                        {
                            if (movie.Duration >= minDuration)
                            {
                                movie.FilmInfo();
                                found = true;
                            }
                        }

                        if (!found)
                            Console.WriteLine("Фільми з такими парамнтрами відсутні");
                    }
                }
                else if (choice == "4")
                {
                    if (movies.Count == 0)
                    {
                        Console.WriteLine("Фільми відсутні");
                    }
                    else
                    {
                        Console.WriteLine("Інформація про фільм:");

                        foreach (var movie in movies)
                        {
                            movie.FilmInfo();

                            if (movie.IsNew)
                                Console.WriteLine(" - Фільм новий");
                            else
                                Console.WriteLine(" - Це класичний фільм");

                            if (movie.Duration > 120)
                                Console.WriteLine(" - Фільм довше за звичайний");

                            if (movie.Rating >= 8)
                                Console.WriteLine(" - Фільм має високий рейтинг");
                            else if (movie.Rating >= 6)
                                Console.WriteLine(" - Фільм має середній рейтинг");
                            else
                                Console.WriteLine(" - Фільм має низький рейтинг");
                        }
                    }
                }
                else if (choice == "5")
                {
                    Console.Write("ID для видалення: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    bool removed = false;

                    foreach (var movie in movies)
                    {
                        if (movie.Id == deleteId)
                        {
                            movies.Remove(movie);
                            Console.WriteLine("Фільм видалено");
                            removed = true;
                            break;
                        }
                    }

                    if (!removed)
                        Console.WriteLine("Фільм не знайдено");
                }
                else if (choice == "0")
                {
                    break;
                }
            }
        }
    }
}
```
