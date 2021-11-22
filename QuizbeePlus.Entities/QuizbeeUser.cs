using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QuizbeePlus.Entities
{
    //You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class QuizbeeUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<QuizbeeUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

       
        public string Photo { get; set; }
        public Nullable<DateTime> RegisteredOn { get; set; }

        public string NationalID { get; set; }
        public string Elgeha { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Grade { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }

        public String GradYear { get; set; }
        public string Sector { get; set; }
        public string Address{ get; set; }


        public string JobTitle { get; set; }

        //public int MinistryId { get; set; }
        //public virtual Ministry Ministry { get; set; }
        public virtual List<Exam> Exams { get; set; }
        //public virtual List<UserCourse> UserCourses { get; set; }

        public string MobileNum { get; set; }


    }
}
