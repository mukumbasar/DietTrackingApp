using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.Entities.Common
{
    public struct StructMostEatenFoodsByMealName
    {
        public string MealName { get; set; }
        public string FoodName { get; set; }
        public int Count { get; set; }
        
    }
}
