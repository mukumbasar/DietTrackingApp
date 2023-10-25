using DietApp.BL.Services;
using DietApp.DAL.Concrete;
using DietApp.Entities.Common;
using DietApp.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.BL.Managers
{
    public class UserDayMealFoodManager : BaseManager<UserDayMealFood>, IUserDayMealFoodService
    {
        public int CurrentID { get; set; }

        protected UserDayMealFoodRepository _userDayMealFoodRepository;

        public UserDayMealFoodManager(GenericRepository<UserDayMealFood> genericRepository, UserDayMealFoodRepository userDayMealFoodRepository) : base(genericRepository)
        {
            _userDayMealFoodRepository = userDayMealFoodRepository;
        }

        public string AddDayMealFood(StructUserDayMealFood sUserDayMealFood)
        {

            if (_userDayMealFoodRepository.AddDayMealFood(sUserDayMealFood.UserID, sUserDayMealFood.FoodName,
             sUserDayMealFood.Portion, sUserDayMealFood.CategoryName, sUserDayMealFood.Calories, sUserDayMealFood.MealName, sUserDayMealFood.DateTime, sUserDayMealFood.PhotoPath))
            {
                return "Öğün içeriği eklendi.";
            }
             else 
            { 
                return "Öğün içeriği eklenemedi."; 
            }
        }

        public string DeleteDayMealFood(int id)
        {
            if (_userDayMealFoodRepository.DeleteDayMealFood(id))
            {
                return "Öğün içeriği silindi.";
            }
            else
            {
                return "Öğün içeriği silinemedi.";
            }
        }

        public string UpdateDayMealFood(StructUserDayMealFood sUserDayMealFood)
        {
            if(_userDayMealFoodRepository.UpdateDayMealFood(sUserDayMealFood.ID, sUserDayMealFood.UserID, sUserDayMealFood.FoodName,
             sUserDayMealFood.Portion, sUserDayMealFood.CategoryName, sUserDayMealFood.Calories, sUserDayMealFood.MealName, sUserDayMealFood.DateTime, sUserDayMealFood.PhotoPath))
            {
                return "Öğün içeriği güncellendi.";
            }
            else
            {
                return "Öğün içeriği güncellenemedi.";
            }
        }

        public List<StructDataGridMeal> ShowDayMealFoods(int id)
        {
            return _userDayMealFoodRepository.ShowDayMealFoods(id);
        }

        public List<StructDailyMealCalories> ShowDailyMealCalories(int id, DateTime dateTime)
        {
            return _userDayMealFoodRepository.ShowDailyMealCalories(id, dateTime);
        }

        public List<StructDailyMealCalories> ShowReportWeeklyOrMonthlyUserMealCalories(int id, int path)
        {
            return _userDayMealFoodRepository.ShowReportWeeklyOrMonthlyUserMealCalories( id,  path);
        }
        
        public List<StructDailyMealCalories> ShowReportWeeklyOrMonthlyEveryoneMealCalories(int id, int path)
        {
            return _userDayMealFoodRepository.ShowReportWeeklyOrMonthlyEveryoneMealCalories(id, path);
        }

        public List<StructMostEatenFoodsByFoodName> ShowReportMostEatenFoodsByFoodName(int id)
        {
            return _userDayMealFoodRepository.ShowReportMostEatenFoodsByFoodName(id);
        }

        public List<StructMostEatenFoodsByMealName> ShowReportMostEatenFoodsByMeaName(int id)
        {
            return _userDayMealFoodRepository.ShowReportMostEatenFoodsByMealType(id);
        }

        
    }
    
}
