using QuizbeePlus.Data;
using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Services
{
    public class ExamService
    {

        #region Define as Singleton
        private static ExamService _Instance;

        public static ExamService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ExamService();
                }

                return (_Instance);
            }
        }

        private ExamService()
        {
        }
        #endregion
        public bool CreateExam(Exam Exam)
        {
            using (var context = new QuizbeeContext())
            {
                context.Exams.Add(Exam);
                return context.SaveChanges() > 0;
               
                
            }
        }
        public Exam GetExamByID(int ID)
        {
            using (var context = new QuizbeeContext())
            {
                var resutl =context.Exams
                  .Where(r => r.ID == ID)
                  .Include(u => u.Exam_Questions.Select(a=>a.Question.Options)).FirstOrDefault();
                return resutl;
            }
        }
        public Exam_Question GetExamQuestionByID(int ExamID,int QuestionID)
        {
            using (var context = new QuizbeeContext())
            {
                var resutl = context.Exam_Questions
                  .Where(r => r.ExamID == ExamID && r.QuestionID==QuestionID).FirstOrDefault();
                return resutl;
            }
        }
        public Exam GetExamDetailsByID(int ID)
        {
            using (var context = new QuizbeeContext())
            {
                var resutl = context.Exams
                  .Where(r => r.ID == ID)
                  .Include(u=>u.Category)
                  .Include(a=>a.Student)
                  .FirstOrDefault();
                return resutl;
            }
        }
        public bool CreateExamQuestions(Exam_Question ExamQuestion)
        {
            using (var context = new QuizbeeContext())
            {
                context.Exam_Questions.Add(ExamQuestion);
                return context.SaveChanges() > 0;


            }
        }
        public int UpdateExamQuestion(Exam_Question Exam_Question)
        {
            using (var context = new QuizbeeContext())
            {
                context.Entry(Exam_Question).State = System.Data.Entity.EntityState.Modified;

                return  context.SaveChanges();
            }
        }
        public List<Exam> GetAllExams()
        {
            using (var context = new QuizbeeContext())
            {
                return context.Exams.Include(a=>a.Exam_Questions).ToList();
            }
        }
        public List<Exam_Question> GetAllExamQuestions()
        {
            using (var context = new QuizbeeContext())
            {
                return context.Exam_Questions.Include(a => a.Exam).ToList();
            }
        }
    }
}
