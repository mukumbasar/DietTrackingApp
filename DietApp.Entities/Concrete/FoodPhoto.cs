using DietApp.Entities.Abstract;
using DietApp.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.Entities.Concrete
{
    public class FoodPhoto : IEntity
    {
        public int ID { get; set; }
        public string PhotoPath { get; set; }

        public ICollection<UserDayMealFood> UserDayMealFoods { get; set; }

    }
}
