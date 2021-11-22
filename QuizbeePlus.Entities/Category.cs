using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Category:BaseEntity
    {
        public string  Name { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
        public List<Exam> Exams { get; set; }
        public List<Course> Courses { get; set; }

    }
}
