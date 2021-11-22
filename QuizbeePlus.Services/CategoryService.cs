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
    public class CategoryService
    {
        #region Define as Singleton
        private static CategoryService _Instance;

        public static CategoryService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CategoryService();
                }

                return (_Instance);
            }
        }

        private CategoryService()
        {
        }
        #endregion

        public List<Category> GetAllCategories()
        {
            using (var context = new QuizbeeContext())
            {
                return context.Categories
                                        .OrderBy(x => x.Name)
                                        .ToList();
            }
        }

        public List<Degree> GetAllDegrees()
        {
            using (var context = new QuizbeeContext())
            {
                return context.Degrees
                                        .OrderBy(x => x.Name)
                                        .ToList();
            }
        }

        public CategoriesSearch GetCategories(string searchTerm, int pageNo, int pageSize)
        {
            using (var context = new QuizbeeContext())
            {
                var search = new CategoriesSearch();

                if (string.IsNullOrEmpty(searchTerm))
                {
                    search.Categories = context.Categories
                                        .Include(u => u.Questions)
                                        .OrderBy(x => x.Name)
                                        .Skip((pageNo - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                    search.TotalCount = context.Users.Count();
                }
                else
                {
                    search.Categories = context.Categories
                                        .Where(u => u.Name.ToLower().Contains(searchTerm.ToLower()))
                                        .Include(u => u.Questions)
                                        .OrderBy(x => x.Name)
                                        .Skip((pageNo - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                    search.TotalCount = context.Categories
                                        .Where(u => u.Name.ToLower().Contains(searchTerm.ToLower())).Count();
                }

                return search;
            }
        }

        public CategoryWithQuestionsEntity GetCategoryByID(int ID)
        {
            using (var context = new QuizbeeContext())
            {
                return context.Categories
                    .Where(r => r.ID == ID)
                    .Include(u => u.Questions)
                    .Select(x => new CategoryWithQuestionsEntity()
                    {
                        Category = x,
                        Questions = x.Questions.Select(questioncategory => context.Questions.Where(user => user.ID == questioncategory.ID).FirstOrDefault()).ToList()
                    }).FirstOrDefault();
            }
        }

        public Category GetCategory(int ID)
        {
            using (var context = new QuizbeeContext())
            {
                return context.Categories
                                    .Where(q => q.ID == ID)
                                    .Include(q => q.Questions)
                                    .Include("Questions.Options")
                                    .Where(q => q.Questions.Count > 0) //get quiz with Questions greater than 0
                                    .FirstOrDefault();
            }
        }

        public async Task<bool> NewCategory(Category category)
        {
            using (var context = new QuizbeeContext())
            {
                context.Categories.Add(category);

                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            using (var context = new QuizbeeContext())
            {
                var oldCategory = context.Roles.Find(category.ID);

                if (oldCategory != null)
                {
                    oldCategory.Name = category.Name;

                    context.Entry(oldCategory).State = System.Data.Entity.EntityState.Modified;
                }

                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteCategory(int ID)
        {
            using (var context = new QuizbeeContext())
            {
                var category = context.Categories.Find(ID);

                if (category != null)
                {
                    context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                }

                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
