using DietApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.Entities.Concrete
{
    public class User : IEntity
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        #region Navigational Properties

        #region UserDaysMealsFoods
        public ICollection<UserDayMealFood> UserDayMealFoods { get; set; }
        #endregion

        #region UserFoods
        public ICollection<UserFood> UserFoods { get; set; }
        #endregion

        #region UserDetails
        public UserDetails UserDetails { get; set; }
        #endregion

        #endregion

    }
}
