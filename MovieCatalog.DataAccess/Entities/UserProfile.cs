using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MovieCatalog.DataAccess.Entities
{
    
    public class UserProfile: BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual IdentityUser ApplicationUser { get; set; }
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
