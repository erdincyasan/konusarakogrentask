using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Catalog.Quiz
{
    public class ExamQuestion : BaseEntity
    {
        public string Question { get; set; }
        public Guid ExamId { get; set; }
        public Exam Exam { get; set; }
        public List<ExamQuestionChoice> ExamQuestionChoices { get; set; }

        public ExamQuestion(string question, Guid examId)
        {
            Id = Guid.NewGuid();
            ExamId = examId;
            Question = question;
        }
        public ExamQuestion AddQuestion(ExamQuestionChoice examQuestionChoice)
        {
            ExamQuestionChoices.Add(examQuestionChoice);
            return this;
        }
    }
}
