using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalog.DataAccess.Entities
{
    public class Movie: BaseEntity
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public int YearOfProduction { get; set; }
        public string Producer { get; set; }
        public byte[] Poster { get; set; }
        public string PosterFileExtension { get; set; }
        [Required]
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
