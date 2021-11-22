using Microsoft.AspNet.Identity.EntityFramework;
using QuizbeePlus.Entities;
using QuizbeePlus.Entities.CustomEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizbeePlus.ViewModels
{
    public class UserViewModel
    {
        public string NationalID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Elgeha { get; set; }
        public string Sector { get; set; }

        public string MobileNum { get; set; }

    }
    public class ControlPanelViewModel : BaseViewModel
    {
        public bool isPartial { get; set; }
    }

    public class UsersListingViewModel : ListingBaseViewModel
    {
        public List<UserWithRoleEntity> Users { get; set; }
    }

    public class DataReportViewModel : ListingBaseViewModel
    {
        public List<QuizbeeUser> Users { get; set; }
        public List <Exam>  exams { get; set; }
        public List <ExamResult> ExamResult { get; set; }

        public Category category { get; set; }

        public string catsearch { get; set; }
        public DateTime? startsearch{ get; set; }
        public DateTime? endsearch{ get; set; }
        public int? ressearch{ get; set; }
    }

    public class ExamDetailViewModel
    {
        
        public List<Exam_Question>  ExamQuestions { get; set; }

        public List<Option> RightOptions { get; set; }
        public List<Question> Questions{ get; set; }
    }

    public class UserDetailsViewModel : BaseViewModel
    {
        public UserWithRoleEntity User { get; set; }
        public List<IdentityRole> AvailableRoles { get; set; }
    }



    public class RolesListingViewModel : ListingBaseViewModel
    {
        public List<IdentityRole> Roles { get; set; }
    }

    public class NewRoleViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class UpdateRoleViewModel : BaseViewModel
    {
        [Required]
        public string ID { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class RoleDetailsViewModel : BaseViewModel
    {
        public RoleWithUsersEntity Role { get; set; }
    }



    public class CategoriesListingViewModel : ListingBaseViewModel
    {
        public List<Category> Categories { get; set; }
    }
    public class NewCategoryViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }
    }

    public class UpdateCategoryViewModel : BaseViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }

    //public class CategoryDetailsViewModel : BaseViewModel
    //{
    //    public CategoryWithQuestionsEntity Category { get; set; }
    //}


    //public class AttemptQuizViewModel : BaseViewModel
    //{
    //    public IEnumerable<Category> Categories { get; set; }
    //    public int CategoryID { get; set; }
    //}

    public class AttemptQuizQuestionViewModel
    {
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public List<Question> Questions { get; set; }
        public List<int> QuestionsIDs { get; set; }

    }
  

    public class QuestionsListingViewModel : ListingBaseViewModel
    {
        public List<Question> Questions { get; set; }
    }
    public class NewQuestionViewModel: BaseViewModel
    {
        [Required]
        public string Title { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int CategoryID { get; set; }

        public IEnumerable<Degree> Degrees { get; set; }
        public int DegreeID { get; set; }

        public List<Option> Options { get; set; }
        public List<Option> CorrectOptions { get; set; }
    }

    public class EditQuestionViewModel : BaseViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int CategoryID { get; set; }
        public List<Option> Options { get; set; }
        public List<Option> CorrectOptions { get; set; }

    }
    public class ExamResult
    {
        public string NID { get; set; }
        public string CategoryName { get; set; }
        public int Score { get; set; }
        public DateTime ExamDate { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public string MinistryName { get; set; }
        public int ExamId { get; set; }

    }
    public class QuizAttemptsListViewModel : ListingBaseViewModel
    {
        public List<ExamResult> ExamResults { get; set; }
        public List<Exam> exams{ get; set; }
    }

}