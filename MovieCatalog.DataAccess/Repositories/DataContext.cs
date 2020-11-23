using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieCatalog.DataAccess.EF;
using MovieCatalog.DataAccess.Entities;
using MovieCatalog.DataAccess.Interfaces;

namespace MovieCatalog.DataAccess.Repositories
{
    public class DataContext : IDataContext
    {

        public DataContext(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            this.DbContext = dbContext;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public UserManager<IdentityUser> UserManager { get; }

        public RoleManager<IdentityRole> RoleManager { get; }

        public AppDbContext DbContext { get; }

        public void Dispose()
        {
            DbContext.Dispose();
        }


        public IRepository<T> GetRepository<T>() where T: BaseEntity
        => new BaseRepository<T>(DbContext);

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
