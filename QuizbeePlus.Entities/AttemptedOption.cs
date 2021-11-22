using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class AttemptedOption: BaseEntity
    {
        public int Exam_QuestionID { get; set; }
        public virtual Exam_Question AttemptedQuestion { get; set; }

        public int OptionID { get; set; }
        public virtual Option Option { get; set; }
    }
}
