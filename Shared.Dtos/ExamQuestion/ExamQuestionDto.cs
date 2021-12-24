using Shared.Dtos.ExamQuestionChoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.ExamQuestion
{
    public class ExamQuestionDto:IDto
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public List<ExamQuestionChoiceDto> ExamQuestionChoices { get; set; }
    }
}
