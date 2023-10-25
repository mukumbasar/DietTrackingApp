using DietApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.DAL.Concrete
{
    public class UserFoodRepository : GenericRepository<UserFood>
    {
       
        public UserFoodRepository(DbContext context) : base(context)
        {

        }

        public List<string> GetUserFoods(int id)
        {

            return DbSet.Where(x => x.UserID == id)
                        .Select(x => x.FoodName)
                        .ToList(); 
        }

    }
}
