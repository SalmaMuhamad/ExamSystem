using Microsoft.AspNet.Identity;
using QuizbeePlus.Entities;
using QuizbeePlus.Services;
using QuizbeePlus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuizbeePlus.Controllers
{
    public class AttemptQuizController : Controller
    {
        [HttpGet]
        public ActionResult Attempt(int CategoryID)
        {
            var category = CategoryService.Instance.GetCategory(CategoryID);

            if (category == null) return HttpNotFound();

            CategoryDetailViewModel model = new CategoryDetailViewModel();

            model.PageInfo = new PageInfo()
            {
                PageTitle = string.Format("Quiz : {0}", category.Name),
                PageDescription = category.Description
            };

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
          

            return View(model);
        }

        [HttpPost]
        public ActionResult Attempt(AttemptQuizViewModel AttemptQuizViewModel)
        {
            int ExamID = CreateExamWorkFlow(AttemptQuizViewModel.CategoryID);

            if (ExamID != 0)
            {
                Exam model = ExamService.Instance.GetExamByID(ExamID);
                AttemptQuizQuestionViewModel AttemptQuizQuestionViewModel = new AttemptQuizQuestionViewModel()
                {
                    ExamID=model.ID,
                    ExamName = model.Name,
                    
                    Questions = model.Exam_Questions.Select(a => a.Question).ToList(),
                    QuestionsIDs=new List<int>()
                };
                foreach(var ques in AttemptQuizQuestionViewModel.Questions)
                {
                    AttemptQuizQuestionViewModel.QuestionsIDs.Add(ques.ID);
                }
                return View("_QuizQuestion", AttemptQuizQuestionViewModel);
            }
            else
            {
                return View("_QuizQuestion");
            }
        }

     
     
   

        private int CreateExamWorkFlow(int CategoryID)
        {
            Category Category=CategoryService.Instance.GetCategory(CategoryID);
            Exam Exam = new Exam()
            {
                StudentID = User.Identity.GetUserId(),
                Name = User.Identity.GetUserId(),
                Description = "Description",
                IsActive = true,
                ModifiedOn = DateTime.Now,
                CategoryID=CategoryID,
                StartedAt=DateTime.Now,
                

            };
            if (ExamService.Instance.CreateExam(Exam))
            {
                IEnumerable<Question> LQuestins = QuestionService.Instance.GetRandomQuestionsByCategoryID(CategoryID);

                foreach (var question in LQuestins)
                {
                    var ExamQuestions = new Exam_Question
                    {
                        ExamID = Exam.ID,
                        QuestionID = question.ID,
                        //Question = question,
                        IsCorrect = false,


                        ModifiedOn = DateTime.Now
                    };
                    ExamService.Instance.CreateExamQuestions(ExamQuestions);
                }
                return Exam.ID;
            }
            return 0;
        }

    }
}