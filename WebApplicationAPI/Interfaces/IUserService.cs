using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.ViewModels;
using WebApplicationAPI.ViewModels.Account;

namespace WebApplicationAPI.Interfaces
{
    public interface IUserService 
    {
        Task<CurrentUserViewModel> GetById(string id);
        Task<CurrentUserViewModel> Authenticate(LoginViewModel lg);
        Task<List<UserViewModel>> GetListUser();
        Task<string> GetUserName(string id);
    }
}
