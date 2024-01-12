namespace Cloud_APIDemo.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }

        public Movie(int id, string title, int releaseYear)
        {
            Id = id;
            Title = title;
            ReleaseYear = releaseYear;
        }
    }
}
