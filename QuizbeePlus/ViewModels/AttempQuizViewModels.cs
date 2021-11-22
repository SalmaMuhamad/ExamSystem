using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizbeePlus.ViewModels
{
    public class AttemptQuizViewModel
    {
        public int ExamID { get; set; }
        public Question Question { get; set; }
        public List<Option> Options { get; set; }
        public int CategoryID { get; set; }
        public string ExamName { get; set; }

        public int QuestionID { get; set; }
        public string SelectedOptionIDs { get; set; }

    }
    public class AttemptQuestionViewModel
    {
      
        public string ExamName { get; set; }

        
    }

    public class CategoryDetailViewModel : BaseViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
    }
    public class AnswerForm
    {
        public int QuestionID { get; set; }
        public int ExamID { get; set; }
        public string SelectedOptionIDs { get; set; }
    }
    public class AnswerFormList
    {
        public List<AnswerForm> AnswerForm { get; set; }
    }
}