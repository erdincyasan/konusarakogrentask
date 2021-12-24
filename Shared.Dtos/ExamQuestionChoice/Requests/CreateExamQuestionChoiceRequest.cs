using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.ExamQuestionChoice.Requests;
public class CreateExamQuestionChoiceRequest
{
    public string Choice { get; set; }
    public bool isCorrect { get; set; }
}
