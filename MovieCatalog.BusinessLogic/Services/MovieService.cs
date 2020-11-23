using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieCatalog.BusinessLogic.Infrastructure;
using MovieCatalog.BusinessLogic.Interfaces;
using MovieCatalog.BusinessLogic.ViewModels;
using MovieCatalog.DataAccess.Entities;
using MovieCatalog.DataAccess.Interfaces;

namespace MovieCatalog.BusinessLogic.Services
{
    public class MovieService : IMovieService
    {
        public IDataContext Database{ get;}
        public MovieService(IDataContext dataContext)
        {
            Database = dataContext;
        }
        public async Task<OperationResult> CreateAsync(MovieViewModel movieViewModel)
        {
            try
            {
                var movieRepo = Database.GetRepository<Movie>();
                Movie movie = new Movie();
                movie.Name = movieViewModel.Name;
                movie.Description = movieViewModel.Description;
                movie.Producer = movieViewModel.Producer;
                movie.YearOfProduction = movieViewModel.YearOfProduction;
                movie.UserProfile = Database.GetRepository<UserProfile>().Get(u=>u.ApplicationUser.Id == movieViewModel.UserId).First();
                movie.PosterFileExtension = Path.GetExtension(movieViewModel.Poster.FileName);


                using (var posterStream = movieViewModel.Poster.OpenReadStream())
                {
                    byte[] bytes = new Byte[posterStream.Length];
                    posterStream.Read(bytes, 0, (int)posterStream.Length);
                    posterStream.Seek(0, SeekOrigin.Begin);
                    movie.Poster =bytes;
                }


                movieRepo.Create(movie);
                movieRepo.SaveChanges();

                return new OperationResult(true, "Фильм успешно сохранен");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, null, ex);
            }
        }

        public async Task<OperationResult> DeleteAsync(int movieId)
        {
            var movieRepo = Database.GetRepository<Movie>();
            movieRepo.Remove(m => m.Id == movieId);
            movieRepo.SaveChanges();
            return new OperationResult(true, "Фильм успешно удален");
        }

        public MovieViewModel GetMovieById(int id)
        {
            var movie = Database.GetRepository<Movie>().FindById(id)
                ?? throw new ArgumentNullException($"Фильм c id {id} не найден");
            return MovieViewModel.FromMovie(movie);
        }

        public IEnumerable<MovieViewModel> GetMovies(int skippedItems = 0, int takenItems = 0)
        {
            var movies = Database.GetRepository<Movie>().GetAll();
            takenItems = takenItems == 0 ? movies.Count() : takenItems;
            return movies.Skip(skippedItems).Take(takenItems).Select(m => MovieViewModel.FromMovie(m)).ToList();
        }

        public IEnumerable<MovieViewModel> GetMoviesByUserId(string UserId, int skippedItems = 0, int takenItems = 0)
        {
            var movies = Database.GetRepository<Movie>().GetAll().Where(m => m.UserProfile.ApplicationUser.Id == UserId);
            takenItems = takenItems == 0 ? movies.Count() : takenItems;
            return movies.Skip(skippedItems).Take(takenItems).Select(m => MovieViewModel.FromMovie(m)).ToList();
        }

        public async Task<OperationResult> UpdateAsync(MovieViewModel movieViewModel)
        {
            try
            {
                var movieRepo = Database.GetRepository<Movie>();
                var movie = movieRepo.FindById(movieViewModel.Id)
                 ?? throw new ArgumentNullException($"Фильм c id {movieViewModel.Id} не найден");
                movie.Name = movieViewModel.Name;
                movie.Description = movieViewModel.Description;
                movie.Producer = movieViewModel.Producer;
                movie.YearOfProduction = movieViewModel.YearOfProduction;
                if (movieViewModel.Poster != null)
                {
                    movie.PosterFileExtension = Path.GetExtension(movieViewModel.Poster.FileName);

                    using (var posterStream = movieViewModel.Poster.OpenReadStream())
                    {
                        byte[] bytes = new Byte[posterStream.Length];
                        posterStream.Read(bytes, 0, (int)posterStream.Length);
                        posterStream.Seek(0, SeekOrigin.Begin);
                        movie.Poster = bytes;
                    }
                }


                movieRepo.Update(movie);
                return new OperationResult(true, "Фильм успешно обновлен");
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, null, ex);
            }
        }

        public void Dispose()
        => Database.Dispose();
    }
}
