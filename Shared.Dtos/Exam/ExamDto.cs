using Shared.Dtos.ExamQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Exam
{
    public class ExamDto:IDto
    {
        public Guid Id { get; set; }
        public string ExamTitle { get; set; }
        public string ExamText { get; set; }
        public List<ExamQuestionDto> Questions { get; set; }
    }
}
