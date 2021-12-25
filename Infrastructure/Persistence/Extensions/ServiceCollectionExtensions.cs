using Application.Catalog.Interfaces;
using Application.Catalog.Services;
using Application.Common.Interfaces;
using Application.Identity.Interfaces;
using Domain.Identity.Models;
using FluentValidation;
using Infrastructure.Common;
using Infrastructure.Identity;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Dtos.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddContexts(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(x => x.UseSqlite("Data Source =KonusrakOgren.Db"));
            services.AddTransient<IUow, Uow>();
            services.AddTransient<IExamService, ExamService>();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            services.AddScoped<IValidator<RegisterModel>, RegisterModelValidator>();
            services.AddHttpClient();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IWebsiteContent, WebsiteContent>();
            services.AddScoped<IUserService, UserService>();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/auth/login";
            });
        }
    }
}
