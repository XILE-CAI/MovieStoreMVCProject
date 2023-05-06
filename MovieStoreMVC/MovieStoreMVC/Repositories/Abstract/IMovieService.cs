using MovieStoreMVC.Models.Domain;
using MovieStoreMVC.Models.DTO;

namespace MovieStoreMVC.Repositories.Abstract
{
    public interface IMovieService
    {
        bool Add(Movie model);
        bool Update(Movie model);
        Movie GetById(int id);
        bool Delete(int id);
        MovieListVM List();
    }
}
