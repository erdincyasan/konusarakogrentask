using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Exam.Requests;
public class GetExamQuestionAnswers
{
    public Guid QuestionId { get; set; }
    public List<Guid> AnswersId { get; set; }
}
