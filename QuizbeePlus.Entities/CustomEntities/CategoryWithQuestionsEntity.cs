using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Entities.CustomEntities
{
    public class CategoryWithQuestionsEntity
    {
        public Category Category { get; set; }
        public List<Question> Questions { get; set; }
    }
}
