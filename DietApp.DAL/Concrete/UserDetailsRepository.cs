using DietApp.Entities.Common;
using DietApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.DAL.Concrete
{
    public class UserDetailsRepository : GenericRepository<UserDetails>
    {
        

        public UserDetailsRepository(DbContext context) : base(context)
        {

        }

        public StructUserDetails GetUserDetails(int ID)
        {
            var user =DbSet.Where(u => u.UserID == ID)
                            .First();
            return new StructUserDetails()
            {
                Name = user.Name,
                Surname = user.Surname,
                Height = user.Height,
                Weight = user.Weight
            };
        }

        public void SetUserDetails(int ID , string Name, string Surname, string Height, string Weight)
        {
            DbSet.Add(new UserDetails
            {
                UserID = ID,
                Name = Name,
                Surname = Surname,
                Height = Height,
                Weight = Weight
            });
            SaveChanges();
        }
    }
}
