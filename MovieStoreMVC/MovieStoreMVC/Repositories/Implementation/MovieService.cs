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

                var movieGenres = context.MovieGenre.Where(a => a.MovieId == model.Id);
                foreach (var item in movieGenres)
                {
                    context.MovieGenre.Remove(item);
                }

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


        public MovieListVM List(string term="", bool paging=false, int currentPage = 0)
        {

            var list = new MovieListVM();

            var data = context.Movie.ToList();

            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                data = data.Where(a => a.Title.ToLower().StartsWith(term)).ToList();
            }

            if (paging == true)
            {
                int pageSize = 5;
                int count = data.Count;
                int TotalPages = (int)Math.Ceiling(count /(double)pageSize);
                data = data.Skip(currentPage-1).Take(pageSize).ToList();
                list.PageSize = pageSize;
                list.CurrentPage = currentPage;
                list.TotalPages = TotalPages;
            }

            foreach (var movie in data)
            {
                var genres = (from genre in context.Genre join mg in context.MovieGenre on genre.Id equals mg.GenreId where mg.MovieId == movie.Id select genre.GenreName).ToList();
                var genreNames = string.Join(",",genres);
                movie.GenreNames = genreNames;
            }

            list.MovieList = data.AsQueryable();
            return list;
        }
    }
}
