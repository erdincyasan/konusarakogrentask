using Microsoft.AspNetCore.Identity;
using Shared.Dtos.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Identity.Interfaces
{
    public interface IUserService
    {
        Task<SignInResult> SignInAsync(string email, string password);
        Task<IdentityResult> RegisterAsync(RegisterModel model);
        Task<Guid> GetCurrentUserId();
        Task LogOut();
    }
}
