using MovieStoreMVC.Models.Domain;

namespace MovieStoreMVC.Models.DTO
{
    public class MovieListVM
    {
        public IQueryable<Movie> MovieList { get; set; }
    }
}
