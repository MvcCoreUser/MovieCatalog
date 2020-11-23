﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MovieCatalog.DataAccess.Entities;

namespace MovieCatalog.BusinessLogic.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название фильма")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Год выпуска")]
        public int YearOfProduction { get; set; }
        [Display(Name = "Режиссер")]
        public string Producer { get; set; }
        [Display(Name = "Постер")]
        public IFormFile Poster { get; set; }
        public byte[] PosterContent { get; set; }
        public string PosterExtension { get; set; }

        public string UserId { get; set; }

        public static MovieViewModel FromMovie(Movie movie)
        {
            MovieViewModel res = new MovieViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Producer = movie.Producer,
                UserId = movie.UserProfileId,
                YearOfProduction = movie.YearOfProduction,
                PosterContent = movie.Poster,
                PosterExtension = movie.PosterFileExtension,
            };
            return res;
        }
    }
}
