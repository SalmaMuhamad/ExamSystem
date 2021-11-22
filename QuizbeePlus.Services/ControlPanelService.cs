using Microsoft.AspNet.Identity.EntityFramework;
using QuizbeePlus.Data;
using QuizbeePlus.Entities;
using QuizbeePlus.Entities.CustomEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuizbeePlus.Services
{
    public class ControlPanelService
    {
        #region Define as Singleton
        private static ControlPanelService _Instance;

        public static ControlPanelService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ControlPanelService();
                }

                return (_Instance);
            }
        }

        private ControlPanelService()
        {
        }
        #endregion

        public List<IdentityRole> GetAllRoles()
        {
            using (var context = new QuizbeeContext())
            {
                return context.Roles
                                        .OrderBy(x => x.Name)
                                        .ToList();
            }
        }

        public RolesSearch GetRoles(string searchTerm, int pageNo, int pageSize)
        {
            using (var context = new QuizbeeContext())
            {
                var search = new RolesSearch();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    search.Roles = context.Roles
                                        .Include(u => u.Users)
                                        .OrderBy(x => x.Name)
                                        .Skip((pageNo - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                    search.TotalCount = context.Users.Count();
                }
                else
                {
                    search.Roles = context.Roles
                                        .Where(u => u.Name.ToLower().Contains(searchTerm.ToLower()))
                                        .Include(u => u.Users)
                                        .OrderBy(x => x.Name)
                                        .Skip((pageNo - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                    search.TotalCount = context.Roles
                                        .Where(u => u.Name.ToLower().Contains(searchTerm.ToLower())).Count();
                }

                return search;
            }
        }

        public RoleWithUsersEntity GetRoleByID(string ID)
        {
            using (var context = new QuizbeeContext())
            {
                return context.Roles
                    .Where(r => r.Id == ID)
                    .Include(u => u.Users)
                    .Select(x => new RoleWithUsersEntity()
                    {
                        Role = x,
                        Users = x.Users.Select(userRole => context.Users.Where(user => user.Id == userRole.UserId).FirstOrDefault()).ToList()
                    }).FirstOrDefault();
            }
        }
        public QuizbeeUser GetUserByID(string NID)
        {
            using (var context = new QuizbeeContext())
            {
                return context.Users
                    .Where(r => r.Id == NID)
                    .FirstOrDefault();
            }
        }

        public async Task<bool> NewRole(IdentityRole role)
        {
            using (var context = new QuizbeeContext())
            {
                context.Roles.Add(role);

                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UpdateRole(IdentityRole role)
        {
            using (var context = new QuizbeeContext())
            {
                var oldRole = context.Roles.Find(role.Id);

                if(oldRole != null)
                {
                    oldRole.Name = role.Name;

                    context.Entry(oldRole).State = System.Data.Entity.EntityState.Modified;
                }

                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteRole(string ID)
        {
            using (var context = new QuizbeeContext())
            {
                var role = context.Roles.Find(ID);

                if (role != null)
                {
                    context.Entry(role).State = System.Data.Entity.EntityState.Deleted;
                }

                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> AddUserRole(string userID, string roleID)
        {
            using (var context = new QuizbeeContext())
            {
                var user = context.Users.Find(userID);

                var role = context.Roles.Find(roleID);

                if(user != null && role != null)
                {
                    user.Roles.Add(new IdentityUserRole() { UserId = userID, RoleId = roleID });
                }

                return await context.SaveChangesAsync() > 0;
            }
        }
        //public bool AddEmployee()
        public bool AddCourse(Course Course)
        {
            using (var context = new QuizbeeContext())
            {
                context.Courses.Add(Course);
                return context.SaveChanges() > 0;


            }
        }
        public bool AddUserCourse(QuizbeeUser user,Course Course,int HallID)
        {
            using (var context = new QuizbeeContext())
            {
                UserCourse UserCourse = new UserCourse() {
                 
                    Course=Course,
                    UserID=user.Id,
                    CourseID=Course.ID,
                    HallID=HallID
                };
                try
                {
                    context.UserCourses.Add(UserCourse);

                    return context.SaveChanges() > 0;
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            


            }
        }

        public List<Hall> GetAllHalls()
        {
            using (var context = new QuizbeeContext())
            {
                return context.Halls.OrderBy(x => x.HallNumber)
                                        .ToList();
            }
        }


        public async Task<bool> RemoveUserRole(string userID, string roleID)
        {
            using (var context = new QuizbeeContext())
            {
                var user = context.Users.Find(userID);
                
                if (user != null)
                {
                    var userRole = user.Roles.Where(r => r.RoleId == roleID).FirstOrDefault();

                    if(user != null) user.Roles.Remove(userRole);
                }

                return await context.SaveChangesAsync() > 0;
            }
        }

        
        //public JsonResult UploadExcel(QuizbeeUser users, HttpPostedFileBase FileUpload)
        //{


        //    List<string> data = new List<string>();
        //    if (FileUpload != null)
        //    {
        //        // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
        //        if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //        {
        //            string filename = FileUpload.FileName;
        //            string targetpath = Server.MapPath("~/Doc/");
        //            FileUpload.SaveAs(targetpath + filename);
        //            string pathToExcelFile = targetpath + filename;
        //            var connectionString = "";
        //            if (filename.EndsWith(".xls"))
        //            {
        //                connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
        //            }
        //            else if (filename.EndsWith(".xlsx"))
        //            {
        //                connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
        //            }

        //            var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
        //            var ds = new DataSet();
        //            adapter.Fill(ds, "ExcelTable");
        //            DataTable dtable = ds.Tables["ExcelTable"];
        //            string sheetName = "Sheet1";
        //            var excelFile = new ExcelQueryFactory(pathToExcelFile);
        //            var artistAlbums = from a in excelFile.Worksheet<QuizbeeUser>(sheetName) select a;
        //            foreach (var a in artistAlbums)
        //            {
        //                try
        //                {
        //                    if (a.NationalID != "" && a.UserName != "" && a.Email != "" && a.PasswordHash != "" && a.PhoneNumber != "" && a.Elgeha != "" && a.Sector != "" && a.JobTitle != "")
        //                    {
        //                        QuizbeeUser TU = new QuizbeeUser();
        //                        TU.NationalID = a.NationalID;
        //                        TU.UserName = a.UserName;
        //                        TU.Email = a.Email;
        //                        TU.PasswordHash = a.PasswordHash;
        //                        TU.PhoneNumber = a.PhoneNumber;
        //                        TU.Sector = a.Sector;
        //                        TU.Elgeha = a.Elgeha;
        //                        TU.JobTitle = a.JobTitle;
        //                        db.Users.Add(TU);
        //                        db.SaveChanges();
        //                    }
        //                    else
        //                    {
        //                        data.Add("<ul>");
        //                        if (a.Name == "" || a.Name == null) data.Add("<li> name is required</li>");
        //                        if (a.Address == "" || a.Address == null) data.Add("<li> Address is required</li>");
        //                        if (a.ContactNo == "" || a.ContactNo == null) data.Add("<li>ContactNo is required</li>");
        //                        data.Add("</ul>");
        //                        data.ToArray();
        //                        return Json(data, JsonRequestBehavior.AllowGet);
        //                    }
        //                }
        //                catch (DbEntityValidationException ex)
        //                {
        //                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
        //                    {
        //                        foreach (var validationError in entityValidationErrors.ValidationErrors)
        //                        {
        //                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
        //                        }
        //                    }
        //                }
        //            }
        //            //deleting excel file from folder
        //            if ((System.IO.File.Exists(pathToExcelFile)))
        //            {
        //                System.IO.File.Delete(pathToExcelFile);
        //            }
        //            return Json("success", JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            //alert message for invalid file format
        //            data.Add("<ul>");
        //            data.Add("<li>Only Excel file format is allowed</li>");
        //            data.Add("</ul>");
        //            data.ToArray();
        //            return Json(data, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        data.Add("<ul>");
        //        if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
        //        data.Add("</ul>");
        //        data.ToArray();
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}
