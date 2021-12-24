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

        // public DateTime ExamDate { get; set; }

        public Exam(string examTitle,string examText)
        {
            Id = Guid.NewGuid();

            // ExamDate = DateTime.Now;
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
