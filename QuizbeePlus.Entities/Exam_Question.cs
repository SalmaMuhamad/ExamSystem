using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Exam_Question:BaseEntity
    {

        public int? ExamID { get; set; }
        public virtual Exam Exam { get; set; }

        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }
        public Nullable<DateTime> AnsweredAt { get; set; }
        public bool? IsCorrect { get; set; }
        public decimal Score { get; set; }
        public int? OptionID { get; set; }
        public virtual Option Option { get; set; }
    }
}
