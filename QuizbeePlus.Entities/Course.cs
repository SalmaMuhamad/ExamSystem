using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Course:BaseEntity
    {
        public string Name { get; set; }
        public Nullable<DateTime> StartedAt { get; set; }
        public Nullable<DateTime> CompletedAt { get; set; }
        public virtual List<UserCourse> UserCourses { get; set; }
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
