using QuizbeePlus.Data;
using QuizbeePlus.Services;
using QuizbeePlus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using QuizbeePlus.Entities;
using QuizbeePlus.Commons;
using LinqToExcel;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.OleDb;
using System.Data;
using System.Data.Entity.Validation;
using System.Configuration;
using BoldReports;
using System.Data.Common;
using Rotativa.MVC;
using System.Web.Services.Description;
using System.IO;

namespace QuizbeePlus.Controllers
{
    //[CustomAuthorize(Roles = "Administrator")]


    public class ControlPanelController : BaseController
    {
        private readonly  QuizbeeContext  db ;

        public ControlPanelController()
        {
            db = new QuizbeeContext();
        }
        public ActionResult Index(bool? isPartial = false)
        {
            ControlPanelViewModel model = new ControlPanelViewModel();

            model.PageInfo = new PageInfo()
            {
                PageTitle = "Control Panel",
                PageDescription = "Manage Quizbee."
            };

            return View(model);
        }

        public ActionResult Users(string search, int? page, int? items)
        {
            UsersListingViewModel model = new UsersListingViewModel();

            model.PageInfo = new PageInfo()
            {
                PageTitle = "Users",
                PageDescription = "Quizbee Users."
            };

            model.searchTerm = search;
            model.pageNo = page ?? 1;
            model.pageSize = items ?? 10;

            var usersSearch = UsersService.Instance.GetUsersWithRoles(model.searchTerm, model.pageNo, model.pageSize);

            model.Users = usersSearch.Users;
            model.TotalCount = usersSearch.TotalCount;

            model.Pager = new Pager(model.TotalCount, model.pageNo, model.pageSize);

            return PartialView("_Users", model);
        }

        public ActionResult DataReport( string categories , string result_range, int? result_nom , DateTime? start_date , DateTime? end_date , int? page, int? items)
        {
            
            var db = new QuizbeeContext();
            DataReportViewModel model = new DataReportViewModel();

            model.catsearch = categories;
            model.ressearch = result_nom;
            model.startsearch = Convert.ToDateTime(start_date); 
            model.endsearch = Convert.ToDateTime(end_date);


            if(categories == "category_all")
            {
                model.exams = db.Exams.Where(x => x.StartedAt >= model.startsearch && x.StartedAt <= model.endsearch ).ToList();
            }
            else
            {
                var catId = db.Categories
                .Where(x => x.Name == model.catsearch)
                .Select(x => x.ID)
                .SingleOrDefault();
                model.exams = db.Exams.Where(x => x.StartedAt >= model.startsearch && x.StartedAt <= model.endsearch && x.CategoryID == catId).ToList();
            }

            
            model.Users = new List<QuizbeeUser> ();

            foreach(var st in model.exams)
            {
               
              var x = db.Users.Where(u => u.Id == st.StudentID).FirstOrDefault();
              model.Users.Add(x);
            }


            return View("_DataReport", model);

        }

        //public ActionResult PrintViewToPdf()
        //{
        //    //var report = new Rotativa.MVC.ActionAsPdf("DataReport");
        //    //return report;

        //    //Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
        //    //foreach (var key in Request.Cookies.AllKeys)
        //    //{
        //    //    cookieCollection.Add(key, Request.Cookies.Get(key).Value);
        //    //}
        //    //return new ActionAsPdf("DataReport")
        //    //{
        //    //    FileName = "Name.pdf",
        //    //    Cookies = cookieCollection
        //    //};

            

        //}

        public ActionResult UserDetails(string ID)
        {
            UserDetailsViewModel model = new UserDetailsViewModel();

            model.PageInfo = new PageInfo()
            {
                PageTitle = "User Details",
                PageDescription = "User Details"
            };

            model.User = UsersService.Instance.GetUserWithRolesByID(ID);
            model.AvailableRoles = ControlPanelService.Instance.GetAllRoles();

            //remove roles from dropdown which are already with the user 
            foreach (var userRole in model.User.Roles)
            {
                var availableRole = model.AvailableRoles.Where(x => x.Id.Equals(userRole.Id)).FirstOrDefault();

                if (availableRole != null)
                {
                    model.AvailableRoles.Remove(availableRole);
                }
            }

            return PartialView("_UserDetails", model);
        }

        [HttpGet]
        public ActionResult ExamDetails(int? ExamId)
        {
            QuizbeeContext db = new QuizbeeContext();
            ExamDetailViewModel model = new ExamDetailViewModel();
            Exam_Question model2 = new Exam_Question();

            model.ExamQuestions = new List<Exam_Question>();
            model.RightOptions = new List<Option>();


            model.ExamQuestions = db.Exam_Questions.Where(e => e.ExamID == ExamId).ToList();

            foreach(var ques in model.ExamQuestions)
            {
                var x = db.Options.Where(o => o.ID == ques.OptionID).FirstOrDefault();
                var y = db.Questions.Where(q => q.ID == ques.ExamID).FirstOrDefault();
                //model.Questions.Add(y);
                //model.RightOptions.Add(x);
            }
   

            return View(model);
        }



        public ActionResult Roles(string search, int? page, int? items)
        {
            RolesListingViewModel model = new RolesListingViewModel();

            model.PageInfo = new PageInfo()
            {
                PageTitle = "Roles",
                PageDescription = "Quizbee Roles."
            };

            model.searchTerm = search;
            model.pageNo = page ?? 1;
            model.pageSize = items ?? 10;

            var rolesSearch = ControlPanelService.Instance.GetRoles(model.searchTerm, model.pageNo, model.pageSize);

            model.Roles = rolesSearch.Roles;
            model.TotalCount = rolesSearch.TotalCount;

            model.Pager = new Pager(model.TotalCount, model.pageNo, model.pageSize);

            return PartialView("_Roles", model);
        }

        public ActionResult NewRole(string ID)
        {
            NewRoleViewModel model = new NewRoleViewModel();

            return PartialView("_NewRole", model);
        }

        [HttpPost]
        public async Task<JsonResult> NewRole(NewRoleViewModel model)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!ModelState.IsValid)
            {
                var Errors = new List<string>();

                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        Errors.Add(error.ErrorMessage.ToString());
                    }
                }

                result.Data = new { Success = false, Errors = Errors };

                return result;
            }
            else
            {
                try
                {
                    result.Data = new { Success = await ControlPanelService.Instance.NewRole(new IdentityRole() { Name = model.Name }) };
                }
                catch (Exception ex)
                {
                    result.Data = new { Success = false, Errors = ex.InnerException.Message };
                }
            }

            return result;
        }

        [HttpPost]
        public async Task<JsonResult> AddUserRole(string userID, string roleID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                result.Data = new { Success = await ControlPanelService.Instance.AddUserRole(userID, roleID) };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Errors = ex.InnerException.Message };
            }

            return result;
        }

        [HttpPost]
        public async Task<JsonResult> RemoveUserRole(string userID, string roleID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                result.Data = new { Success = await ControlPanelService.Instance.RemoveUserRole(userID, roleID) };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Errors = ex.InnerException.Message };
            }

            return result;
        }

        public ActionResult RoleDetails(string ID)
        {
            RoleDetailsViewModel model = new RoleDetailsViewModel();

            model.PageInfo = new PageInfo()
            {
                PageTitle = "Role Details",
                PageDescription = "Role Details"
            };

            model.Role = ControlPanelService.Instance.GetRoleByID(ID);

            return PartialView("_RoleDetails", model);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateRole(UpdateRoleViewModel model)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!ModelState.IsValid)
            {
                var Errors = new List<string>();

                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        Errors.Add(error.ErrorMessage.ToString());
                    }
                }

                result.Data = new { Success = false, Errors = Errors };

                return result;
            }
            else
            {
                try
                {
                    await ControlPanelService.Instance.UpdateRole(new IdentityRole() { Id = model.ID, Name = model.Name });

                    result.Data = new { Success = true };
                }
                catch (Exception ex)
                {
                    result.Data = new { Success = false, Errors = ex.InnerException };
                }
            }

            return result;
        }

        [HttpPost]
        public async Task<JsonResult> DeleteRole(string ID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (string.IsNullOrEmpty(ID))
            {
                result.Data = new { Success = false, Errors = "Role can not be identified." };

                return result;
            }
            else
            {
                try
                {
                    await ControlPanelService.Instance.DeleteRole(ID);

                    result.Data = new { Success = true };
                }
                catch (Exception ex)
                {
                    result.Data = new { Success = false, Errors = ex.Message };
                }
            }

            return result;
        }



        public ActionResult Categories(string search, int? page, int? items)
        {
            CategoriesListingViewModel model = new CategoriesListingViewModel();

            model.PageInfo = new PageInfo()
            {
                PageTitle = "Categories",
                PageDescription = "Question Categories."
            };

            model.searchTerm = search;
            model.pageNo = page ?? 1;
            model.pageSize = items ?? 10;

            var categoriesSearch = CategoryService.Instance.GetCategories(model.searchTerm, model.pageNo, model.pageSize);

            model.Categories = categoriesSearch.Categories;
            model.TotalCount = categoriesSearch.TotalCount;

            model.Pager = new Pager(model.TotalCount, model.pageNo, model.pageSize);

            return PartialView("_Categories", model);
        }

        public ActionResult NewCategory()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView("_NewCategory", model);
        }

        [HttpPost]
        public async Task<JsonResult> NewCategory(NewCategoryViewModel model)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!ModelState.IsValid)
            {
                var Errors = new List<string>();

                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        Errors.Add(error.ErrorMessage.ToString());
                    }
                }

                result.Data = new { Success = false, Errors = Errors };

                return result;
            }
            else
            {
                try
                {
                    result.Data = new { Success = await CategoryService.Instance.NewCategory(new Category() { Name = model.Name }) };
                }
                catch (Exception ex)
                {
                    result.Data = new { Success = false, Errors = ex.InnerException.Message };
                }
            }

            return result;
        }

        //public ActionResult CategoryDetails(int ID)
        //{
        //    CategoryDetailsViewModel model = new CategoryDetailsViewModel();

        //    model.PageInfo = new PageInfo()
        //    {
        //        PageTitle = "Category Details",
        //        PageDescription = "Category Details"
        //    };

        //    model.Category = CategoryService.Instance.GetCategoryByID(ID);

        //    return PartialView("_CategoryDetails", model);
        //}

        [HttpPost]
        public async Task<JsonResult> UpdateCategory(UpdateCategoryViewModel model)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!ModelState.IsValid)
            {
                var Errors = new List<string>();

                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        Errors.Add(error.ErrorMessage.ToString());
                    }
                }

                result.Data = new { Success = false, Errors = Errors };

                return result;
            }
            else
            {
                try
                {
                    await CategoryService.Instance.UpdateCategory(new Category() { Name = model.Name });

                    result.Data = new { Success = true };
                }
                catch (Exception ex)
                {
                    result.Data = new { Success = false, Errors = ex.InnerException };
                }
            }

            return result;
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCategory(int ID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;


            try
            {
                await CategoryService.Instance.DeleteCategory(ID);

                result.Data = new { Success = true };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Errors = ex.Message };
            }


            return result;
        }

        public ActionResult Questions(string search, int? page, int? items)
        {
            QuestionsListingViewModel model = new QuestionsListingViewModel();

            model.PageInfo = new PageInfo()
            {
                PageTitle = "Questions",
                PageDescription = "Question Categories."
            };

            model.searchTerm = search;
            model.pageNo = page ?? 1;
            model.pageSize = items ?? 10;

            var questionsSearch = QuestionService.Instance.GetQuestions(model.searchTerm, model.pageNo, model.pageSize);

            model.Questions = questionsSearch.Questions;
            model.TotalCount = questionsSearch.TotalCount;

            model.Pager = new Pager(model.TotalCount, model.pageNo, model.pageSize);

            return PartialView("_Questions", model);
        }
        public ActionResult NewQuestion()
        {
            NewQuestionViewModel model = new NewQuestionViewModel() {
                Categories = CategoryService.Instance.GetAllCategories(),
                Degrees = CategoryService.Instance.GetAllDegrees()
            };
            model.PageInfo = new PageInfo()
            {
                PageTitle = "Add New Question",
                PageDescription = "Add questions to selected quiz."
            };
            return View("_NewQuestion", model);
        }

        [HttpPost]

        public ActionResult StartExam(int ExamID, List<string> SelectedOptionIDs, List<int> QuestionIds)
        {
            int i = 0;
            foreach (var questionid in QuestionIds)
            {

                var question = QuestionService.Instance.GetQuestion(questionid);
                var selectedOptions = QuestionService.Instance.GetOptionsByIDs(SelectedOptionIDs[i].CSVToListInt());
                Exam_Question EQ = ExamService.Instance.GetExamQuestionByID(ExamID, questionid);
                EQ.AnsweredAt = DateTime.Now;
                EQ.OptionID = int.Parse(SelectedOptionIDs[i]);
                EQ.IsCorrect = QuestionService.Instance.isCorrect(question, selectedOptions);
                
                if (EQ.IsCorrect.Value)
                {
                    EQ.Score = 1;

                }
                else
                {
                    EQ.Score = 0;
                }

                EQ.ExamID = ExamID;

                i++;
                ExamService.Instance.UpdateExamQuestion(EQ);



            }

            return new HttpStatusCodeResult(500);

        }



        [HttpPost]
        public async Task<ActionResult> NewQuestion(FormCollection collection)
        {
            NewQuestionViewModel model = new NewQuestionViewModel();

            model = GetNewTextQuestionViewModelFromFormCollection(model, collection);

            model.PageInfo = new PageInfo()
            {
                PageTitle = "Add New Question",
                PageDescription = "Add questions to selected quiz."
            };

            if (string.IsNullOrEmpty(model.Title) || model.CorrectOptions.Count == 0 || model.Options.Count == 0)
            {
                if (string.IsNullOrEmpty(model.Title))
                {
                    ModelState.AddModelError("Title", "Please enter question title.");
                }

                if (model.CorrectOptions.Count == 0)
                {
                    ModelState.AddModelError("CorrectOptions", "Please enter some correct options.");
                }

                if (model.Options.Count == 0)
                {
                    ModelState.AddModelError("Options", "Please enter some other options.");
                }


                return View(model);
            }

            var question = new Question();


            question.Title = model.Title;
            question.CategoryID = model.CategoryID;
            question.DegreeID = model.DegreeID;
            question.Options = new List<Option>();
            question.Options.AddRange(model.CorrectOptions);
            question.Options.AddRange(model.Options);

            question.ModifiedOn = DateTime.Now;

            if (await QuestionService.Instance.SaveNewQuestion(question))
            {
                return RedirectToAction("NewQuestion");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }


        private NewQuestionViewModel GetNewTextQuestionViewModelFromFormCollection(NewQuestionViewModel model, FormCollection collection)
        {
            model.Options = new List<Option>();
            model.CorrectOptions = new List<Option>();

            if (collection.AllKeys.Count() > 0)
            {
                foreach (string key in collection)
                {
                    if (key == "Title")
                    {
                        model.Title = collection[key];
                    }
                    else if (key == "CategoryID")
                    {
                        model.CategoryID = Convert.ToInt32(collection[key]);
                    }
                    else if (key == "DegreeID")
                    {
                        model.DegreeID = Convert.ToInt32(collection[key]);
                    }
                    else if (key.Contains("correctOption")) //this must be Correct Option
                    {
                        if (!string.IsNullOrEmpty(collection[key]))
                        {
                            var correctOption = new Option();
                            correctOption.Answer = collection[key];
                            correctOption.IsCorrect = true;
                            correctOption.ModifiedOn = DateTime.Now;

                            model.CorrectOptions.Add(correctOption);
                        }
                    }
                    else if (key.Contains("option")) //this must be Option
                    {
                        if (!string.IsNullOrEmpty(collection[key]))
                        {
                            var option = new Option();
                            option.Answer = collection[key];
                            option.IsCorrect = false;
                            option.ModifiedOn = DateTime.Now;

                            model.Options.Add(option);
                        }
                    }
                }
            }

            return model;
        }
        private int CreateExamWorkFlow(int CategoryID)
        {
            Exam Exam = new Exam()
            {
                StudentID = User.Identity.GetUserId(),
                Name = User.Identity.GetUserId(),
                Description = "Description",
                IsActive = true,
                ModifiedOn = DateTime.Now

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


        public ActionResult Attempts(string search, int? page, int? items)
        {
            var db = new QuizbeeContext();
            Exam examResult = new Exam();
            QuizAttemptsListViewModel model = new QuizAttemptsListViewModel();

            model.searchTerm = search;
            model.pageNo = page ?? 1;
            model.pageSize = items ?? 10;
            model.ExamResults = new List<ExamResult>();
            //var Exams = ExamService.Instance.GetAllExams().GroupBy(a=>a.StudentID);
            var ExamQuestion = ExamService.Instance.GetAllExamQuestions();
            var result = ExamQuestion.GroupBy(x => x.ExamID)
                  .Select(g => new { Id = g.Key, Sum = g.Sum(x => x.Score) });


            foreach (var item in result)
            {
                Exam Exam = ExamService.Instance.GetExamDetailsByID(item.Id.Value);

                ExamResult ExamResult = new ExamResult()
                {
                    CategoryName = Exam.Category.Name,
                    ExamDate = Exam.StartedAt.Value,
                    Score = Convert.ToInt32(item.Sum),
                    NID = Exam.Student.UserName,
                    ExamId = Exam.ID,
                    //MinistryName=Exam.Student.Ministry.Name,
                    //JobTitle=Exam.Student.JobTitle
                };

                Exam ExamRes = new Exam()
                {
                    Result = Convert.ToInt32(item.Sum)
                };

                db.Exams.Add(ExamRes);
                model.ExamResults.Add(ExamResult);
               
            }


            model.Pager = new Pager(model.TotalCount, model.pageNo, model.pageSize);

            return PartialView("_Attempts", model);
        }
        [HttpGet]
        public ActionResult EditQuestion(int ID)
        {
            EditQuestionViewModel model = new EditQuestionViewModel();



            var question = QuestionService.Instance.GetQuestion(ID);

            if (question == null) return HttpNotFound();

            model.PageInfo = new PageInfo()
            {
                PageTitle = "Modify Question",
                PageDescription = "Modify selected question."
            };

            model.ID = question.ID;
            model.Categories = CategoryService.Instance.GetAllCategories();
            model.Title = question.Title;
            model.CorrectOptions = question.Options.Where(q => q.IsCorrect).ToList();
            model.Options = question.Options.Where(q => !q.IsCorrect).ToList();

            return View(model);
        }

        private EditQuestionViewModel GetEditTextQuestionViewModelFromFormCollection(EditQuestionViewModel model, FormCollection collection)
        {
            model.Options = new List<Option>();
            model.CorrectOptions = new List<Option>();

            if (collection.AllKeys.Count() > 0)
            {
                foreach (string key in collection)
                {
                    if (key == "ID")
                    {
                        model.ID = int.Parse(collection[key]);
                    }
                    else if (key == "Title")
                    {
                        model.Title = collection[key];
                    }
                    else if (key == "CategoryID")
                    {
                        model.CategoryID = Convert.ToInt32(collection[key]);
                    }
                    else if (key.Contains("correctOption")) //this must be Correct Option
                    {
                        if (!string.IsNullOrEmpty(collection[key]))
                        {
                            var correctOption = new Option();
                            correctOption.Answer = collection[key];
                            correctOption.IsCorrect = true;
                            correctOption.ModifiedOn = DateTime.Now;

                            model.CorrectOptions.Add(correctOption);
                        }
                    }
                    else if (key.Contains("option")) //this must be Option
                    {
                        if (!string.IsNullOrEmpty(collection[key]))
                        {
                            var option = new Option();
                            option.Answer = collection[key];
                            option.IsCorrect = false;
                            option.ModifiedOn = DateTime.Now;

                            model.Options.Add(option);
                        }
                    }
                }
            }

            return model;
        }

        [HttpPost]
        public async Task<ActionResult> EditQuestion(FormCollection collection)
        {
            EditQuestionViewModel model = new EditQuestionViewModel();


            model = GetEditTextQuestionViewModelFromFormCollection(model, collection);
            var question = QuestionService.Instance.GetQuestion(model.ID);

            if (question == null) return HttpNotFound();

            model.PageInfo = new PageInfo()
            {
                PageTitle = "Modify Question",
                PageDescription = "Modify selected question."
            };

            if (model == null || string.IsNullOrEmpty(model.Title) || model.CorrectOptions.Count == 0 || model.Options.Count == 0)
            {
                if (string.IsNullOrEmpty(model.Title))
                {
                    ModelState.AddModelError("Title", "Please enter question title.");
                }

                if (model.CorrectOptions.Count == 0)
                {
                    ModelState.AddModelError("CorrectOptions", "Please enter some correct options.");
                }

                if (model.Options.Count == 0)
                {
                    ModelState.AddModelError("Options", "Please enter some other options.");
                }


                return View(model);
            }

            question.Title = model.Title;
            question.CategoryID = model.CategoryID;
            question.Options = new List<Option>();
            question.Options.AddRange(model.CorrectOptions);
            question.Options.AddRange(model.Options);

            question.ModifiedOn = DateTime.Now;

            if (await QuestionService.Instance.UpdateQuestion(question))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        public ActionResult AddEmployee()
        {
            return PartialView("_AddEmployee");
        }

        public FileResult DownloadExcel()
        {
            string path = "~/Files/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }

        public ActionResult UploadExcel(QuizbeeUser users, HttpPostedFileBase FileUpload)
        {


            List<string> data = new List<string>();
          
            List<QuizbeeUser> ObjUser = new List<QuizbeeUser>();

            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Sheet1";
                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<QuizbeeUser>(sheetName) select a;
                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.NationalID != "" && a.UserName != "" && a.Email != "" && a.PasswordHash != "" && a.PhoneNumber != "" && a.Elgeha != "" && a.Sector != "" && a.JobTitle != "")
                            {
                                string result = a.PasswordHash;
                                //QuizbeeUser TU = new QuizbeeUser();
                                QuizbeeUser TU = new QuizbeeUser();
                                TU.NationalID = a.NationalID;
                                TU.UserName = a.UserName;
                                TU.Email = a.Email;
                                TU.PhoneNumber = a.PhoneNumber;
                                TU.Sector = a.Sector;
                                TU.Elgeha = a.Elgeha;
                                TU.JobTitle = a.JobTitle;
                                db.Users.Add(TU);
                                db.SaveChanges();
                                ObjUser.Add(TU);
                                ViewData["Obj"] = ObjUser;
                            }

                            else
                            {
                                data.Add("<ul>");
                                if (a.UserName == "" || a.UserName == null) data.Add("<li> name is required</li>");
                                if (a.NationalID == "" || a.NationalID == null) data.Add("<li> NationalID is required</li>");
                                if (a.Email == "" || a.Email == null) data.Add("<li>Email is required</li>");
                                if (a.PasswordHash == "" || a.PasswordHash == null) data.Add("<li>Passwor is required</li>");
                                if (a.PhoneNumber == "" || a.PhoneNumber == null) data.Add("<li>ContactNo is required</li>");
                                if (a.Sector == "" || a.Sector == null) data.Add("<li>Sector is required</li>");
                                if (a.Elgeha == "" || a.Elgeha == null) data.Add("<li>Elgeha is required</li>");
                                if (a.JobTitle == "" || a.JobTitle == null) data.Add("<li>JobTitle is required</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            List<string> errors = new List<string>();

                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    errors.Add(validationError.ErrorMessage);
                                    continue;

                                    //Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }
                    }
                    //deleting excel file from folder
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    ViewBag.x = ObjUser;
                    //return Json(ObjUser, JsonRequestBehavior.AllowGet);
                    //return Json("success", JsonRequestBehavior.AllowGet);
                    return RedirectToAction("showErrors");
                }
                else
                {
                    //alert message for invalid file format
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DisplayUserSheet()
        {
            return PartialView("_DisplayUserSheet");
        }





    }
}



