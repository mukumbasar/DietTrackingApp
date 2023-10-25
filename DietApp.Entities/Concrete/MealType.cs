using DietApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.Entities.Concrete
{
    public class MealType : IEntity
    {
        public int ID { get; set; }
        public string MealName { get; set; }

        public ICollection<UserDayMealFood> UserDayMealFoods { get; set; }
    }
}
