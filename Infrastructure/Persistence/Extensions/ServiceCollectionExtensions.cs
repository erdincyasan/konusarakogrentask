using Application.Catalog.Interfaces;
using Application.Catalog.Services;
using Application.Common.Interfaces;
using Infrastructure.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddHttpClient();
            services.AddScoped<IWebsiteContent, WebsiteContent>();
        }
    }
}
