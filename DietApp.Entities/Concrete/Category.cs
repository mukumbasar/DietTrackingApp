using DietApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.Entities.Concrete
{
    public class Category : IEntity
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

        #region Navigational Properties

        #region Food
        public ICollection<UserFood> UserFoods { get; set; }  
        #endregion

        #endregion

        public Category()
        {
             UserFoods = new List<UserFood>();
        }
    }
}
