using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizbeePlus.Helpers
{
    public static class URLHelper
    {
        public static string Home(this UrlHelper helper, string searchTerm = "", int? pageNo = 1, int? pageSize = 10)
        {
            string routeURL = string.Empty;
            
            if(string.IsNullOrEmpty(searchTerm) && (!pageNo.HasValue || pageNo.Value == 1) && (!pageSize.HasValue || pageSize.Value == 10))
            {
                routeURL = helper.RouteUrl("Home", new
                {
                    controller = "Home",
                    action = "Initial"
                });
            }
            else
            {
                routeURL = helper.RouteUrl("Home", new
                {
                    controller = "Home",
                    action = "Initial",
                    search = searchTerm,
                    page = pageNo.Value,
                    items = pageSize.Value
                });
            }
            
            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);

            return routeURL.ToLower();
        }

        public static string Main(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Main", new
            {
                controller = "Home",
                action = "Index"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }



        public static string Register(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Register", new
            {
                controller = "Account",
                action = "StudentRegister"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string EmployeeRegister(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("EmployeeRegister", new
            {
                controller = "Account",
                action = "EmployeeRegister"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Login(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Login", new
            {
                controller = "Account",
                action = "Login"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        
        public static string Logout(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Logout", new
            {
                controller = "Account",
                action = "LogOff"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string Me(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Me", new
            {
                controller = "Manage",
                action = "Index"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UploadImage(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UploadImage", new
            {
                controller = "Shared",
                action = "UploadImage"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        
        public static string MyPhoto(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("MyPhoto", new
            {
                controller = "Manage",
                action = "MyPhoto"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UpdateInfo(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdateInfo", new
            {
                controller = "Manage",
                action = "UpdateInfo"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UpdateUserInfo(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdateUserInfo", new
            {
                controller = "Manage",
                action = "UpdateUserInfo",
                userID= userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string DeleteUser(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("DeleteUser", new
            {
                controller = "Manage",
                action = "DeleteUser",
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UpdatePassword(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdatePassword", new
            {
                controller = "Manage",
                action = "UpdatePassword"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

  
   
        public static string ControlPanel(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("ControlPanel", new
            {
                controller = "ControlPanel",
                action = "Index"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }


        public static string AddEmployee(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("AddEmployee", new
            {
                controller = "ControlPanel",
                action = "AddEmployee"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string DisplayUserSheet(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("DisplayUserSheet", new
            {
                controller = "ControlPanel",
                action = "DisplayUserSheet"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string ExamDetails(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("ExamDetails", new
            {
                controller = "ControlPanel",
                action = "ExamDetails"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }


        public static string PrintViewToPdf(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("PrintViewToPdf", new
            {
                controller = "ControlPanel",
                action = "PrintViewToPdf"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }




        public static string Users(this UrlHelper helper, string searchTerm = "", int? pageNo = 1, int? pageSize = 10)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Users", new
            {
                controller = "ControlPanel",
                action = "Users",
                search = searchTerm,
                page = pageNo.Value,
                items = pageSize.Value
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string DataReport(this UrlHelper helper, string categories, string result_range, int? result_nom, DateTime? start_date, DateTime? end_date, int? pageNo = 1, int? pageSize = 10)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Report", new
            {
                controller = "ControlPanel",
                action = "DataReport",
                categories = categories,
                result_range = result_range,
                result_nom = result_nom,
                start_date = start_date,
                end_date = end_date,
                page = pageNo.Value,
                items = pageSize.Value
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UserDetails(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UserDetails", new
            {
                controller = "ControlPanel",
                action = "UserDetails",
                ID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        
        public static string UserPhoto(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UserPhoto", new
            {
                controller = "Manage",
                action = "UserPhoto",
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string CPRoles(this UrlHelper helper, string searchTerm = "", int? pageNo = 1, int? pageSize = 10)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Roles", new
            {
                controller = "ControlPanel",
                action = "Roles",
                search = searchTerm,
                page = pageNo.Value,
                items = pageSize.Value
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string CPNewRole(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("NewRole", new
            {
                controller = "ControlPanel",
                action = "NewRole"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

   

        public static string RoleDetails(this UrlHelper helper, string roleID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("RoleDetails", new
            {
                controller = "ControlPanel",
                action = "RoleDetails",
                ID = roleID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UpdateRole(this UrlHelper helper, string roleID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdateRole", new
            {
                controller = "ControlPanel",
                action = "UpdateRole",
                ID = roleID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        
        public static string DeleteRole(this UrlHelper helper, string roleID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("DeleteRole", new
            {
                controller = "ControlPanel",
                action = "DeleteRole",
                ID = roleID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string AddUserRole(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("AddUserRole", new
            {
                controller = "ControlPanel",
                action = "AddUserRole",
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string RemoveUserRole(this UrlHelper helper, string userID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("RemoveUserRole", new
            {
                controller = "ControlPanel",
                action = "RemoveUserRole",
                userID = userID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }



        //Categories
        public static string CPCategories(this UrlHelper helper, string searchTerm = "", int? pageNo = 1, int? pageSize = 10)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Categories", new
            {
                controller = "ControlPanel",
                action = "Categories",
                search = searchTerm,
                page = pageNo.Value,
                items = pageSize.Value
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string CPNewCategory(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("NewCategory", new
            {
                controller = "ControlPanel",
                action = "NewCategory"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }



        public static string CategoryDetails(this UrlHelper helper, int categoryID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("CategoryDetails", new
            {
                controller = "ControlPanel",
                action = "CategoryDetails",
                ID = categoryID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string UpdateCategory(this UrlHelper helper, int categoryID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("UpdateCategory", new
            {
                controller = "ControlPanel",
                action = "UpdateCategory",
                ID = categoryID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string DeleteCategory(this UrlHelper helper, int categoryID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("DeleteCategory", new
            {
                controller = "ControlPanel",
                action = "DeleteCategory",
                ID = categoryID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        //End of Categories

        public static string CPQuestions(this UrlHelper helper, string searchTerm = "", int? pageNo = 1, int? pageSize = 10)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("Questions", new
            {
                controller = "ControlPanel",
                action = "Questions",
                search = searchTerm,
                page = pageNo.Value,
                items = pageSize.Value
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        public static string CPNewQuestion(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("NewQuestion", new
            {
                controller = "ControlPanel",
                action = "NewQuestion"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }
        public static string AttemptQuiz(this UrlHelper helper,int CategoryID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("AttemptQuiz", new
            {
                controller = "AttemptQuiz",
                action = "Attempt",
                CategoryID = CategoryID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string AnswerQuestion(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("StartExam", new
            {
                controller = "ControlPanel",
                action = "StartExam"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string StartExam(this UrlHelper helper)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("StartExam", new
            {
                controller = "ControlPanel",
                action = "StartExam"
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }


        public static string EditQuestion(this UrlHelper helper,int questionID)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("EditQuestion", new
            {
                controller = "ControlPanel",
                action = "EditQuestion",
                ID = questionID
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }



    }
}