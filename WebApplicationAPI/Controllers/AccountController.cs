using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAPI.Interfaces;
using WebApplicationAPI.ViewModels;
using WebApplicationAPI.ViewModels.Account;

namespace WebApplicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class AccountController : Controller
    {
    
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        public AccountController( UserManager<User> userManager, IUserService userService)
        {
     
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(LoginViewModel vm)
        {
            var user = await _userService.Authenticate(vm);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var user = new User { UserName = vm.Email, Email = vm.Email, Name = vm.Name, DOB = vm.DOB, PhoneNumber = vm.PhoneNumber };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                var login = new LoginViewModel { UserName = user.UserName, Password = user.PasswordHash, Remember = false };
                return Ok(login);
            }
            else
            {
                return new BadRequestObjectResult(result.Errors);
            }
        }

       
    }
}