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