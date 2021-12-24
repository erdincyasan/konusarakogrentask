using Shared.Dtos.ExamQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Exam
{
    public class ExamAnswersDto :IDto
    {
        public List<QuestionAndAnswerDto> QuestionAndAnswers { get; set; } = new List<QuestionAndAnswerDto>();
        public int CorrectCount { get; set; } = 0;
    }
}
