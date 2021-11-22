using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Exam:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> StartedAt { get; set; }
        public Nullable<DateTime> CompletedAt { get; set; }

        public string StudentID { get; set; }
        public virtual QuizbeeUser Student { get; set; }
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Exam_Question> Exam_Questions { get; set; }

        public Nullable<int> Result { get; set; }


    }
}
