using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using MovieCatalog.BusinessLogic.Infrastructure;
using MovieCatalog.BusinessLogic.Interfaces;
using MovieCatalog.BusinessLogic.ViewModels;
using MovieCatalog.DataAccess.Entities;
using MovieCatalog.DataAccess.Interfaces;

namespace MovieCatalog.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        public UserService(IDataContext database)
        {
            this.Database = database;
        }

        public IDataContext Database { get; }

        public async Task<ClaimsIdentity> AuthentificateAsync(UserViewModel userViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> CreateAsync(UserViewModel userViewModel)
        {
            try
            {
                var user = await Database.UserManager.FindByEmailAsync(userViewModel.Email);
                if (user == null)
                {
                    user = new IdentityUser()
                    {
                        Email = userViewModel.Email,
                        UserName = userViewModel.Name
                    };
                    var result = await Database.UserManager.CreateAsync(user, userViewModel.Password);
                    if (result.Succeeded == false)
                    {
                        return new OperationResult(result.Succeeded, string.Join(OperationResult.ErrorSeparator, result.Errors));
                    }

                    await Database.UserManager.AddToRoleAsync(user, userViewModel.Role);

                    UserProfile userProfile = new UserProfile()
                    {
                        ApplicationUserId = user.Id,
                        Name = userViewModel.Name,
                        Phone = userViewModel.Phone
                    };
                    var userProfileRepo = Database.GetRepository<UserProfile>();
                    userProfileRepo.Create(userProfile);
                    await Database.SaveChangesAsync();
                    return new OperationResult(true, "Регистрация успешно пройдена");
                }
                else
                {
                    return new OperationResult(false, "Пользователь с таким логином уже существует", nameof(userViewModel.Email));
                }
            }
            catch (Exception ex)
            {

                return new OperationResult(false, ex.Message, string.Empty, ex);
            }
        }

        public void Dispose()
         =>Database.Dispose();


        public async Task<string> GetUserId(string name)
        {
            var user = await Database.UserManager.FindByNameAsync(name);
            if (user!=null)
            {
                return user.Id;
            }
            else
            {
                throw new NullReferenceException("Пользователь не найден");
            }
        }

        public async Task<string> GetUserName(string email)
        {
            var user = await Database.UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                return user.Email;
            }
            else
            {
                throw new NullReferenceException("Пользователь не найден");
            }
        }

        public Task<OperationResult> UpdateAsync(UserViewModel userViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
