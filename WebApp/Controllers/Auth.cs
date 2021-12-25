using Application.Identity.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Identity.Models;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class Auth : Controller
    {
        private readonly IUserService _userService;
        private readonly IValidator<RegisterModel> _registerModelValidator;
        public Auth(IUserService userService, IValidator<RegisterModel> registerModelValidator)
        {
            _userService = userService;
            _registerModelValidator = registerModelValidator;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _userService.SignInAsync(email, password);
            if (result != null && result.Succeeded)
            {
                return RedirectToAction("GetAllExams", "Exam");
            }
            ModelState.AddModelError("", "Email or password is not correct!");
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            var x = HttpContext.GetRouteValue("registerModel");
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var validResult = _registerModelValidator.Validate(registerModel);
            if (!validResult.IsValid)
            {
                foreach (var error in validResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View();
            }
            var result = await _userService.RegisterAsync(registerModel);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _userService.LogOut();
            return RedirectToAction("Login");
        }
    }
}
