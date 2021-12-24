using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Exam
{
    public class ExamDetailDto:IDto
    {
        public Guid Id { get; set; }
        public string ExamTitle { get; set; }
        public string ExamText { get; set; }
    }
}
