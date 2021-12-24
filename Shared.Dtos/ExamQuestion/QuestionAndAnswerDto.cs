using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.ExamQuestion
{
    public class QuestionAndAnswerDto : IDto
    {
        public Guid AnswerId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
