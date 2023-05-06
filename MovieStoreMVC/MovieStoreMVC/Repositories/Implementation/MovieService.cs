using MovieStoreMVC.Models.Domain;
using MovieStoreMVC.Models.DTO;
using MovieStoreMVC.Repositories.Abstract;

namespace MovieStoreMVC.Repositories.Implementation
{
    public class MovieService:IMovieService
    {
        private readonly DatabaseContext context;
        public MovieService(DatabaseContext context)
        {
            this.context = context;
        }

        public bool Add(Movie model)
        {
            try
            {

                context.Movie.Add(model);
                context.SaveChanges();
                foreach (int genreId in model.Genres)
                {
                    var movieGenre = new MovieGenre
                    {
                        MovieId = model.Id,
                        GenreId = genreId
                    };
                    context.MovieGenre.Add(movieGenre);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Movie model)
        {
            try
            {
                context.Movie.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var model = this.GetById(id);
                if (model == null)
                {
                    return false;
                };
                context.Movie.Remove(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Movie GetById(int id)
        {
            return context.Movie.Find(id);
        }

        public MovieListVM List()
        {
            var data = context.Movie.AsQueryable();
            var list = new MovieListVM
            {
                MovieList = data
            };
            return list;
        }
    }
}
