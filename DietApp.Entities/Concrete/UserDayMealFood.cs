using DietApp.Entities.Abstract;
using DietApp.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.Entities.Concrete
{
    public class UserDayMealFood : IEntity
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Portion { get; set; }
        public Status Status { get; set; }

        //Status

        #region Navigational Properties

        #region Food
        public int UserFoodID { get; set; }
        public UserFood UserFood { get; set; }
        #endregion 

        public int MealTypeID { get; set; }
        public MealType MealType { get; set; }

        #region FoodPhoto
        public int FoodPhotoID { get; set; }
        public FoodPhoto FoodPhoto { get; set; }
        #endregion 

        #endregion

    }
}
