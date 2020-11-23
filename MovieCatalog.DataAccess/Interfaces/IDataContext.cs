using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MovieCatalog.DataAccess.EF;
using MovieCatalog.DataAccess.Entities;

namespace MovieCatalog.DataAccess.Interfaces
{
    public interface IDataContext: IDisposable
    {
        UserManager<IdentityUser> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        AppDbContext DbContext { get; }
        Task SaveChangesAsync();
    }
}
