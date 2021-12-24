using Shared.Dtos.ExamQuestionChoice.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.ExamQuestion.Requests;
public class CreateExamQuestionRequest
{
    public string Question { get; set; }
    public List<CreateExamQuestionChoiceRequest> QuestionChoices { get; set; } = new List<CreateExamQuestionChoiceRequest>();
    public int CorrectIndex { get; set; }
}
