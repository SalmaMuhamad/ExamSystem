using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Question:BaseEntity
    {
        public string Title { get; set; }
        public string Desciption { get; set; }

        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int? DegreeID { get; set; }
        public virtual Degree Degree { get; set; }


        public virtual List<Option> Options { get; set; }
        public virtual List<Exam_Question> Exam_Questions { get; set; }

    }
}
