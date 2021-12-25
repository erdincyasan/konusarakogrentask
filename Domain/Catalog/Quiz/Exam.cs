using Domain.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Catalog.Quiz
{
    public class Exam : BaseEntity
    {
        public string ExamTitle { get; set; }
        public string ExamText { get; set; }
        public List<ExamQuestion> Questions { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExamDate { get; set; }

        public Exam(string examTitle,string examText,Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ExamDate = DateTime.Now;
            ExamTitle = examTitle;
            ExamText = examText;
        }
        public Exam AddQuestion(ExamQuestion examQuestion)
        {
            Questions.Add(examQuestion);
            return this;
        }
    }
}
