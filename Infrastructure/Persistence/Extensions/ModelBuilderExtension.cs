using Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void ApplyConfigurations(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ExamConfiguration());
            builder.ApplyConfiguration(new ExamQuestionConfiguration());
            builder.ApplyConfiguration(new ExamQuestionChoiceConfiguration());
            // builder.ApplyConfiguration();
        }
    }
}
