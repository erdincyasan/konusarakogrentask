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
    public class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamQuestion> builder)
        {
            builder.HasKey(q => q.Id);
            builder.HasMany(q => q.ExamQuestionChoices).WithOne(qc => qc.ExamQuestion).HasForeignKey(qc => qc.ExamQuestionId);
        }
    }
}
