using DietApp.Entities.Common;
using DietApp.Entities.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.BL.Services
{
    public interface IUserDayMealFoodService
    {
        string AddDayMealFood(StructUserDayMealFood sUserDayMealFood);

        string DeleteDayMealFood(int id);


        string UpdateDayMealFood(StructUserDayMealFood sUserDayMealFood);

        //Günlük Rapor
        List<StructDataGridMeal> ShowDayMealFoods(int id);

        // Genel Raporlar
        List<StructDailyMealCalories> ShowReportWeeklyOrMonthlyUserMealCalories(int id, int path);
        List<StructDailyMealCalories> ShowReportWeeklyOrMonthlyEveryoneMealCalories(int id, int path);

        // En Çok Yenen Yemekler Raporu

        List<StructMostEatenFoodsByMealName> ShowReportMostEatenFoodsByMeaName(int id);

    }
}
