using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MovieCatalog.BusinessLogic.Infrastructure;
using MovieCatalog.BusinessLogic.ViewModels;
using MovieCatalog.DataAccess.Interfaces;

namespace MovieCatalog.BusinessLogic.Interfaces
{
    public interface IUserService: IDisposable
    {
        public IDataContext Database { get; }
        Task<OperationResult> CreateAsync(UserViewModel userViewModel);
        Task<OperationResult> UpdateAsync(UserViewModel userViewModel);
        Task<ClaimsIdentity> AuthentificateAsync(UserViewModel userViewModel);
        Task<string> GetUserName(string email);

        Task<string> GetUserId(string name);
    }
}
