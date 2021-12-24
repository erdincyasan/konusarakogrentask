using Shared.Dtos.ExamQuestion.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Exam.Requests
{
    public class CreateExamRequest
    {
        public string ExamTitle { get; set; }
        public string ExamText { get; set; }
        public List<CreateExamQuestionRequest> ExamQuestions { get; set; } = new List<CreateExamQuestionRequest>();
    }
}
