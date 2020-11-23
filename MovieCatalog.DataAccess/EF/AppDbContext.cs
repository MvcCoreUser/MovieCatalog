using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.DataAccess.Entities;

namespace MovieCatalog.DataAccess.EF
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Movie> Movies { get; set; }

        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserProfile>().HasOne(typeof(ApplicationUser)).WithOne().HasForeignKey(nameof(UserProfile.ApplicationUser));
            base.OnModelCreating(builder);
        }
    }
}
