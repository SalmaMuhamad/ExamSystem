using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities
{
    public class Ministry:BaseEntity
    {
        public string Name { get; set; }
        public virtual List<QuizbeeUser> Students { get; set; }
    }
}
