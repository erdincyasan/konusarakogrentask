using Domain.Catalog.Quiz;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class ExamQuestionChoiceConfiguration : IEntityTypeConfiguration<ExamQuestionChoice>
    {
        public void Configure(EntityTypeBuilder<ExamQuestionChoice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ExamQuestion).WithMany(x => x.ExamQuestionChoices).HasForeignKey(x => x.ExamQuestionId);
        }
    }
}
