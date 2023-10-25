using DietApp.Entities.Common;
using DietApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DietApp.DAL.Concrete
{
    public class UserDayMealFoodRepository : GenericRepository<UserDayMealFood>
    {
        private DbSet<UserFood> DbSetUserFood;
        private DbSet<FoodPhoto> DbSetFoodPhoto;
        private DbSet<MealType> DbSetMealTypes;
        private DbSet<Category> DbSetCategory;

        public UserDayMealFoodRepository(DbContext context) : base(context)
        {
            DbSetUserFood = context.Set<UserFood>();
            DbSetFoodPhoto = context.Set<FoodPhoto>();
            DbSetMealTypes = context.Set<MealType>();
            DbSetCategory = context.Set<Category>();

        }

        public bool AddDayMealFood(int id, string foodName, decimal portion, string categoryName, decimal calories, string MealName, DateTime dateTime, string photoPath)
        {

            if (!DbSetUserFood.Where(f => f.FoodName == foodName && f.UserID == id).Any())
            {
                FoodPhoto newPhoto = new();

                try
                {
                    var categoryID = DbSetCategory.Where(c => c.CategoryName == categoryName).Select(c => c.ID).FirstOrDefault();

                    var newFood = new UserFood()
                    {
                        UserID = id,
                        FoodName = foodName,
                        CategoryID = categoryID,
                        Calories = calories

                    };

                    DbSetUserFood.Add(newFood);


                    var newUserDayMealFood = new UserDayMealFood();

                    if (DbSetFoodPhoto.Where(fd => fd.PhotoPath == photoPath).Any())
                    {
                        newUserDayMealFood.FoodPhotoID = DbSetFoodPhoto.Where(fd => fd.PhotoPath == photoPath).Select(fd => fd.ID).FirstOrDefault();
                    }
                    else
                    {
                        newPhoto.PhotoPath = photoPath;

                        DbSetFoodPhoto.Add(newPhoto);
                        SaveChanges();
                        newUserDayMealFood.FoodPhotoID = newPhoto.ID;
                    }
                    SaveChanges();
                    int mealID = DbSetMealTypes.Where(x => x.MealName == MealName).Select(x => x.ID).FirstOrDefault();

                    newUserDayMealFood.Status = Status.Active;
                    newUserDayMealFood.MealTypeID = mealID;
                    newUserDayMealFood.DateTime = dateTime;
                    newUserDayMealFood.UserFoodID = newFood.ID;
                    newUserDayMealFood.Portion = portion;


                    Add(newUserDayMealFood);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                FoodPhoto newPhoto = new();

                try
                {
                    var categoryID = DbSetCategory.Where(c => c.CategoryName == categoryName).Select(c => c.ID).FirstOrDefault();

                    var newUserDayMealFood = new UserDayMealFood();

                    if (photoPath != null)
                    {
                        if (DbSetFoodPhoto.Where(fd => fd.PhotoPath == photoPath).Any())
                        {
                            newUserDayMealFood.FoodPhotoID = DbSetFoodPhoto.Where(fd => fd.PhotoPath == photoPath).Select(fd => fd.ID).FirstOrDefault();
                        }
                        else
                        {
                            newPhoto.PhotoPath = photoPath;

                            DbSetFoodPhoto.Add(newPhoto);
                            SaveChanges();
                            newUserDayMealFood.FoodPhotoID = newPhoto.ID;
                        }
                    }

                    var userFoodID = DbSetUserFood.Where(uf => uf.FoodName == foodName && uf.UserID == id).Select(uf => uf.ID).FirstOrDefault();

                    int mealID = DbSetMealTypes.Where(x => x.MealName == MealName).Select(x => x.ID).FirstOrDefault();

                    newUserDayMealFood.Status = Status.Active;
                    newUserDayMealFood.MealTypeID = mealID;
                    newUserDayMealFood.DateTime = dateTime;
                    newUserDayMealFood.UserFoodID = userFoodID;
                    newUserDayMealFood.Portion = portion;

                    Add(newUserDayMealFood);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteDayMealFood(int id)
        {
            try
            {
                var softDeletedData = Get(id);
                softDeletedData.Status = Status.Passive;
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDayMealFood(int dayMealFoodId, int id, string foodName, decimal portion, string categoryName, decimal calories, string MealName, DateTime dateTime, string photoPath)
        {

            if (!DbSetUserFood.Where(f => f.FoodName == foodName).Any())
            {
                FoodPhoto newPhoto = new();

                try
                {
                    var categoryID = DbSetCategory.Where(c => c.CategoryName == categoryName).Select(c => c.ID).FirstOrDefault();

                    var newFood = new UserFood()
                    {
                        UserID = id,
                        FoodName = foodName,
                        CategoryID = categoryID,
                        Calories = calories

                    };

                    DbSetUserFood.Add(newFood);

                    var newUserDayMealFood = DbSet.Where(x => x.ID == dayMealFoodId).First();

                    if (DbSetFoodPhoto.Where(fd => fd.PhotoPath == photoPath).Any())
                    {
                        newUserDayMealFood.FoodPhotoID = DbSetFoodPhoto.Where(fd => fd.PhotoPath == photoPath).Select(fd => fd.ID).FirstOrDefault();
                    }
                    else
                    {
                        newPhoto.PhotoPath = photoPath;

                        DbSetFoodPhoto.Add(newPhoto);
                        SaveChanges();
                        newUserDayMealFood.FoodPhotoID = newPhoto.ID;
                    }
                    SaveChanges();
                    int mealID = DbSetMealTypes.Where(x => x.MealName == MealName).Select(x => x.ID).FirstOrDefault();

                    newUserDayMealFood.Status = Status.Active;
                    newUserDayMealFood.MealTypeID = mealID;
                    newUserDayMealFood.DateTime = dateTime;
                    newUserDayMealFood.UserFoodID = newFood.ID;
                    newUserDayMealFood.Portion = portion;


                    SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                FoodPhoto newPhoto = new();

                try
                {
                    var categoryID = DbSetCategory.Where(c => c.CategoryName == categoryName).Select(c => c.ID).FirstOrDefault();

                    var newUserDayMealFood = DbSet.Where(x => x.ID == dayMealFoodId).First();

                    if (photoPath != null)
                    {
                        if (DbSetFoodPhoto.Where(fd => fd.PhotoPath == photoPath).Any())
                        {
                            newUserDayMealFood.FoodPhotoID = DbSetFoodPhoto.Where(fd => fd.PhotoPath == photoPath).Select(fd => fd.ID).FirstOrDefault();
                        }
                        else
                        {
                            newPhoto.PhotoPath = photoPath;

                            DbSetFoodPhoto.Add(newPhoto);
                            SaveChanges();
                            newUserDayMealFood.FoodPhotoID = newPhoto.ID;
                        }
                    }

                    var userFoodID = DbSetUserFood.Where(uf => uf.FoodName == foodName).Select(uf => uf.ID).FirstOrDefault();

                    int mealID = DbSetMealTypes.Where(x => x.MealName == MealName).Select(x => x.ID).FirstOrDefault();

                    newUserDayMealFood.Status = Status.Active;
                    newUserDayMealFood.MealTypeID = mealID;
                    newUserDayMealFood.DateTime = dateTime;
                    newUserDayMealFood.UserFoodID = userFoodID;
                    newUserDayMealFood.Portion = portion;

                    SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<StructDataGridMeal> ShowDayMealFoods(int id)
        {
            var currentList = DbSet.Include(uf => uf.UserFood)
                                   .ThenInclude(uf => uf.UserDayMealFoods)
                                   .ThenInclude(uf => uf.MealType)
                                   .Where(uf => (uf.Status == Status.Active) && uf.UserFood.UserID == id)
                                   .OrderByDescending(uf => uf.DateTime)
                                   .Select(uf => new StructDataGridMeal

                                   {

                                       ID = uf.ID,
                                       MealName = uf.MealType.MealName,
                                       CategoryName = uf.UserFood.Category.CategoryName,
                                       FoodName = uf.UserFood.FoodName,
                                       Portion = uf.Portion,
                                       Calories = (uf.UserFood.Calories) * uf.Portion,
                                       DateTime = uf.DateTime,
                                       PhotoPath = uf.FoodPhoto.PhotoPath

                                   }).ToList();
            return currentList;
        }




        //gün sonu raporu

        public List<StructDailyMealCalories> ShowDailyMealCalories(int id, DateTime dateTime)
        {
            var list = DbSet.Include(uf => uf.UserFood)
                       .ThenInclude(uf => uf.UserDayMealFoods)
                       .ThenInclude(uf => uf.MealType)
                       .Where(uf => (uf.Status == Status.Active) && uf.UserFood.UserID == id)
                       .OrderBy(uf => uf.MealTypeID)
                       .Select(uf => new StructDataGridMeal

                       {

                           ID = uf.ID,
                           MealName = uf.MealType.MealName,
                           CategoryName = uf.UserFood.Category.CategoryName,
                           FoodName = uf.UserFood.FoodName,
                           Portion = uf.Portion,
                           Calories = (uf.UserFood.Calories) * uf.Portion,
                           DateTime = uf.DateTime

                       }).ToList();



            var solList = from mc in list
                          where mc.DateTime.Year == dateTime.Year && mc.DateTime.Month == dateTime.Month && mc.DateTime.Day == dateTime.Day
                          group mc by mc.MealName into g

                          select new StructDailyMealCalories
                          {
                              MealName = g.Key,
                              Calories = g.Sum(x => x.Calories)
                          };

            return solList.ToList();
        }

        // kıyas raporları:

        public List<StructDailyMealCalories> ShowReportWeeklyOrMonthlyUserMealCalories(int id, int path)
        {
            var list = DbSet.Include(uf => uf.UserFood)
                            .ThenInclude(uf => uf.UserDayMealFoods)
                            .ThenInclude(uf => uf.MealType)
                            .Where(uf => (uf.Status == Status.Active) && uf.UserFood.UserID == id)
                            .OrderBy(uf => uf.MealTypeID)
                            .Select(uf => new StructDataGridMeal
                            
                            {
                            
                                ID = uf.ID,
                                MealName = uf.MealType.MealName,
                                CategoryName = uf.UserFood.Category.CategoryName,
                                FoodName = uf.UserFood.FoodName,
                                Portion = uf.Portion,
                                Calories = (uf.UserFood.Calories) * uf.Portion,
                                DateTime = uf.DateTime
                            
                            }).ToList();


            var solList = from mc in list
                          where mc.DateTime < DateTime.Now && mc.DateTime > DateTime.Now.AddYears(-path)
                          group mc by mc.MealName into g

                          select new StructDailyMealCalories
                          {
                              MealName = g.Key,
                              Calories = g.Sum(x => x.Calories)
                          };

            return solList.ToList();
        }
        public List<StructDailyMealCalories> ShowReportWeeklyOrMonthlyEveryoneMealCalories(int id, int path)
        {

            var currentList = DbSet.Include(uf => uf.UserFood)
                                       .ThenInclude(uf => uf.UserDayMealFoods)
                                       .ThenInclude(uf => uf.MealType)
                                       .Where(uf => (uf.Status == Status.Active) && uf.UserFood.UserID != id)
                                       .OrderBy(uf => uf.MealTypeID)
                                       .Select(uf => new StructDataGridMeal

                                       {

                                           ID = uf.ID,
                                           MealName = uf.MealType.MealName,
                                           CategoryName = uf.UserFood.Category.CategoryName,
                                           FoodName = uf.UserFood.FoodName,
                                           Portion = uf.Portion,
                                           Calories = (uf.UserFood.Calories) * uf.Portion,
                                           DateTime = uf.DateTime

                                       }).ToList();



            var solList = from mc in currentList
                          where mc.DateTime < DateTime.Now && mc.DateTime > DateTime.Now.AddYears(-path)
                          group mc by mc.MealName into g
                          select new StructDailyMealCalories
                          {
                              MealName = g.Key,
                              Calories = g.Sum(x => x.Calories)
                          };

            return solList.ToList();
        }

        //yemek çeşidi raporu

        public List<StructMostEatenFoodsByFoodName> ShowReportMostEatenFoodsByFoodName(int id)
        {

            var list = DbSet.Include(uf => uf.UserFood)
                            .ThenInclude(uf => uf.UserDayMealFoods)
                            .ThenInclude(uf => uf.MealType)
                            .Where(uf => (uf.Status == Status.Active) && uf.UserFood.UserID == id)
                            .OrderBy(uf => uf.MealTypeID)
                            .Select(uf => new StructDataGridMeal
                            {
                                ID = uf.ID,
                                MealName = uf.MealType.MealName,
                                CategoryName = uf.UserFood.Category.CategoryName,
                                FoodName = uf.UserFood.FoodName,
                                Portion = uf.Portion,
                                Calories = (uf.UserFood.Calories) * uf.Portion,
                                DateTime = uf.DateTime

                            }).ToList();

            var newList = list.GroupBy(x => x.FoodName)
                              .OrderByDescending(g => g.Count())
                              .Select(x => new StructMostEatenFoodsByFoodName
                              {
                                  FoodName = x.Key,
                                  Count = x.Count()
                              }).ToList();
            return newList;
        }
        public List<StructMostEatenFoodsByMealName> ShowReportMostEatenFoodsByMealType(int id)
        {

            var list = DbSet.Include(uf => uf.UserFood)
                            .ThenInclude(uf => uf.UserDayMealFoods)
                            .ThenInclude(uf => uf.MealType)
                            .Where(uf => (uf.Status == Status.Active) && uf.UserFood.UserID == id)
                            .OrderBy(uf => uf.MealTypeID)
                            .Select(uf => new StructDataGridMeal
                            {
                                ID = uf.ID,
                                MealName = uf.MealType.MealName,
                                CategoryName = uf.UserFood.Category.CategoryName,
                                FoodName = uf.UserFood.FoodName,
                                Portion = uf.Portion,
                                Calories = (uf.UserFood.Calories) * uf.Portion,
                                DateTime = uf.DateTime

                            }).ToList();

            var newList = list.GroupBy(x => new { x.MealName, x.FoodName })
                              .Select(x => new StructMostEatenFoodsByMealName
                              {
                                  MealName = x.Key.MealName,
                                  FoodName = x.Key.FoodName,
                                  Count = x.Count()
                              }).ToList();
            return newList;
        }
    }


}

