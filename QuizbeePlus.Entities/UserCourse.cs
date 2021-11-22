using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class UserCourse:BaseEntity
    {
     
        public int? CourseID { get; set; }
        public virtual Course Course { get; set; }


        public int? HallID { get; set; }
        public virtual Hall Hall { get; set; }

        public string UserID { get; set; }
        public virtual QuizbeeUser QuizbeeUser { get; set; }
    }
}
