using Application.Identity.Interfaces;
using Domain.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signinManager;
        private IHttpContextAccessor _httpContextAccessor;
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SignInResult> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user!=null)
            {
                var result = await _signinManager.PasswordSignInAsync(user, password, false, false);
                return result;
            }
            return null;
           
        }

        public async Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            var appUser = new ApplicationUser();
            appUser.FirstName = model.Firstname;
            appUser.LastName = model.Lastname;
            appUser.Email = model.Email;
            appUser.UserName = model.Username;
            var user = await _userManager.CreateAsync(appUser, model.Password);
            return user;
        }

        public async Task<Guid> GetCurrentUserId()
        {
            ClaimsPrincipal claims = _httpContextAccessor.HttpContext.User;
            return Guid.Parse(_userManager.GetUserId(claims));
        }

        public async  Task LogOut()
        {
            await _signinManager.SignOutAsync();
        }
    }
}
