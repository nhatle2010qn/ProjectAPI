using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAPI.Helpers;
using WebApplicationAPI.Interfaces;
using WebApplicationAPI.ViewModels;
using WebApplicationAPI.ViewModels.Account;

namespace WebApplicationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppSettings _appSettings;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        public async Task<CurrentUserViewModel> Authenticate(LoginViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, vm.Remember, lockoutOnFailure: false);
            CurrentUserViewModel crUser = new CurrentUserViewModel();
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(vm.UserName);
                var role = await _userManager.GetRolesAsync(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role[0])
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                crUser.Token = tokenHandler.WriteToken(token);
                crUser.Role = role[0];
                crUser.Id = user.Id;
                crUser.Name = user.Name;
                crUser.UserName = user.UserName;
                crUser.Address = user.Address;
                crUser.PhoneNumber = user.PhoneNumber;
                return crUser;
            }
            else
            {
                return null;
            }
            
        }

        public async Task<CurrentUserViewModel> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(user);
            CurrentUserViewModel userVm = new CurrentUserViewModel
            {
               Id = Guid.Parse(id),
               Name = user.Name,
               UserName = user.UserName,
               Role = role[0]
            };
            return userVm;
        }

        public async Task<List<UserViewModel>> GetListUser()
        {
            var list = await _userManager.Users.ToListAsync();
            List<UserViewModel> listVm = new List<UserViewModel>();
            foreach(var item in list)
            {
                var role = await _userManager.GetRolesAsync(item);
                UserViewModel user = new UserViewModel()
                {
                    Id = item.Id.ToString(),
                    Email = item.Email,
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    UserName = item.UserName,
                    Role = role[0]
                };
                listVm.Add(user);
            }
            return listVm;
        }

        public async Task<string> GetUserName(string id)
        {
           var user = await _userManager.FindByIdAsync(id);
           return user.Name;
        }
    }
}
