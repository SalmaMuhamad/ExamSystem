using QuizbeePlus.Data;
using QuizbeePlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizbeePlus.Services
{
    public class MinistryService
    {
        #region Define as Singleton
        private static MinistryService _Instance;

        public static MinistryService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MinistryService();
                }

                return (_Instance);
            }
        }

        private MinistryService()
        {
        }
        #endregion
        public List<Ministry> GetAllMinistries()
        {
            using (var context = new QuizbeeContext())
            {
                return context.Ministries
                                        .OrderBy(x => x.Name)
                                        .ToList();
            }
        }

    }
}
