using Microsoft.EntityFrameworkCore;
using MovieStoreMVC.Models.Domain;
using MovieStoreMVC.Repositories.Abstract;

namespace MovieStoreMVC.Repositories.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext context;
        public GenreService(DatabaseContext context)
        {

            this.context = context;

        }


        public bool Add(Genre model)
        {
            try
            {
                context.Genre.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Genre model)
        {
            try
            {
                context.Genre.Update(model);
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
                if(model == null)
                {
                    return false;
                };
                context.Genre.Remove(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Genre GetById(int id)
        {
            return context.Genre.Find(id);
        }

        public IQueryable<Genre> List()
        {
            var data = context.Genre.AsQueryable();
            return data;
        }
    }
}
