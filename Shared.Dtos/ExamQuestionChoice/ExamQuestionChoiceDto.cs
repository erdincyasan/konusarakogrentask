using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.ExamQuestionChoice
{
    public class ExamQuestionChoiceDto:IDto
    {
        public Guid Id { get; set; }
        public string Choice { get; set; }
    }
}
