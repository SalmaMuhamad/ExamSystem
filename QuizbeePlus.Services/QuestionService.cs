using QuizbeePlus.Data;
using QuizbeePlus.Entities;
using QuizbeePlus.Entities.CustomEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Services
{
    public class QuestionService
    {
        #region Define as Singleton
        private static QuestionService _Instance;

        public static QuestionService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new QuestionService();
                }

                return (_Instance);
            }
        }

        private QuestionService()
        {
        }
        #endregion
        public List<Question> GetAllQuestions()
        {
            using (var context = new QuizbeeContext())
            {
                return context.Questions
                                        .OrderBy(x => x.Title)
                                        .ToList();
            }
        }

        public IEnumerable<Question> GetRandomQuestionsByCategoryID(int CategoryID)
        {
            using (var context = new QuizbeeContext())
            {
                var QuestionResult = context.Questions
                   .Where(r => r.CategoryID == CategoryID).ToList();
                var ran = new Random();
                IEnumerable<Question> result = QuestionResult.OrderBy(x => ran.Next()).Take(30);

                return result;



            }
        }


        public QuestionsSearch GetQuestions(string searchTerm, int pageNo, int pageSize)
        {
            using (var context = new QuizbeeContext())
            {
                var search = new QuestionsSearch();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    search.Questions = context.Questions
                                        .Include(u => u.Category)
                                        .OrderBy(x => x.Title)
                                        .Skip((pageNo - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                    search.TotalCount = context.Users.Count();
                }
                else
                {
                    search.Questions = context.Questions
                                        .Where(u => u.Title.ToLower().Contains(searchTerm.ToLower()))
                                        .Include(u => u.Category)
                                        .OrderBy(x => x.Title)
                                        .Skip((pageNo - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                    search.TotalCount = context.Categories
                                        .Where(u => u.Name.ToLower().Contains(searchTerm.ToLower())).Count();
                }

                return search;
            }
        }


        public async Task<bool> NewQuestion(Question question)
        {
            using (var context = new QuizbeeContext())
            {
                context.Questions.Add(question);

                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> SaveNewQuestion(Question question)
        {
            using (var context = new QuizbeeContext())
            {
                context.Questions.Add(question);

                return await context.SaveChangesAsync() > 0;
            }
        }
        public async Task<bool> UpdateQuestion(Question question)
        {
            using (var context = new QuizbeeContext())
            {
                context.Entry(question).State = System.Data.Entity.EntityState.Modified;


                return await context.SaveChangesAsync() > 0;
            }
        }

        
        public List<Question> GetQuestionsByCate()
        {
            using (var context = new QuizbeeContext())
            {
                return context.Questions
                                        .OrderBy(x => x.Title)
                                        .ToList();
            }
        }
        public List<Option> GetQuestionOptions(int questionID, List<int> optionIDs)
        {
            using (var context = new QuizbeeContext())
            {
                var question = context.Questions.Where(x => x.ID == questionID).Include(x => x.Options).FirstOrDefault();

                if (question == null) return null;

                List<Option> Options = new List<Option>();

                foreach (var optionID in optionIDs)
                {
                    var option = question.Options.Where(x => x.ID == optionID).FirstOrDefault();

                    if (option != null) Options.Add(option);
                }

                return Options;
            }
        }

        public List<Option> GetOptionsByIDs(List<int> optionIDs)
        {
            using (var context = new QuizbeeContext())
            {
                var options = new List<Option>();

                if (optionIDs != null)
                {
                    foreach (var ID in optionIDs)
                    {
                        var option = context.Options.Find(ID);

                        if (option != null) options.Add(option);
                    }
                }

                return options;
            }
        }


        public Question GetQuestion( int ID)
        {
            using (var context = new QuizbeeContext())
            {
                return context.Questions
                               .Where(q => q.ID == ID )
                               .Include(x => x.Options)
                               .FirstOrDefault();
            }
        }
        public bool isCorrect(Question question, List<Option> selectedOptions)
        {
            bool eq= question.Options.Where(a=>a.IsCorrect).Select(a => a.ID).SequenceEqual(selectedOptions.Select(x => x.ID));
            return eq;
        }
    }
}
