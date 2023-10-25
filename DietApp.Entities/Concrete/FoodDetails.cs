using DietApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.Entities.Concrete
{
    public class FoodDetails : IEntity
    {
        public int ID { get; set; }

        public int Calories { get; set; }

        public UserFood UserFood { get; set; }
    }
}
