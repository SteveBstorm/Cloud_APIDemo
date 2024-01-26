using Cloud_APIDemo.Models;
using System.Collections.Generic;

namespace Cloud_APIDemo.Services
{
    public class MovieService
    {
        List<Movie> MovieList{ get; set; }

        public MovieService()
        {
            MovieList = new List<Movie>();
            MovieList.Add(new Movie(1, "LOTR : The Two Towers", 1999));
            MovieList.Add(new Movie(2, "Star Wars : New Hope", 1977));
            MovieList.Add(new Movie(3, "Pacific Rim", 2013));
            MovieList.Add(new Movie(4, "Joker", 2021));

        }

        public Movie GetById(int id)
        {
            return MovieList.FirstOrDefault(x => x.Id == id);
        }

        public List<Movie> GetAll()
        {
            return MovieList;
        }

        public void Add(Movie m)
        {
            m.Id = MovieList.Max(x => x.Id) + 1;
            MovieList.Add(m);
        }

        public void Delete(int id)
        {
            MovieList.Remove(MovieList.First(x => x.Id == id));
        }
    }
}
