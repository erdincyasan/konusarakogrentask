using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Catalog.Quiz
{
    public class ExamQuestionChoice : BaseEntity
    {
        public Guid ExamQuestionId { get; set; }
        public ExamQuestion ExamQuestion { get; set; }
        public string Choice { get; set; }
        public bool IsCorrect { get; set; } = false;
        public ExamQuestionChoice(Guid examQuestionId, string choice,bool isCorrect=false)
        {
            Id = Guid.NewGuid();
            ExamQuestionId = examQuestionId;
            Choice = choice;
            IsCorrect = isCorrect;
        }
    }
}
