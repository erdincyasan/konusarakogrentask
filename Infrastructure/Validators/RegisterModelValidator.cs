using FluentValidation;
using Shared.Dtos.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validators
{
    public class RegisterModelValidator:AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email address is required").NotNull().WithMessage("Emai address cant be null");
            RuleFor(x => x.Firstname).NotNull().WithMessage("Firstname is required");
            RuleFor(x => x.Lastname).NotNull().WithMessage("Lastname is required");
            RuleFor(x => x.Username).NotNull().WithMessage("Username is required");
        }
    }
}
