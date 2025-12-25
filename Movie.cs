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