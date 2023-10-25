using DietApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.DAL.Concrete
{
    public class UserRepository :GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
            
        }

        public bool CreateUser(string email, string password)
        {
            User user = new User() { 
                Email = email,
                Password = password
            };

            try
            {
                Add(user);

                if (Get(user.ID) != null)
                {
                    return true;
                }
                else 
                { 
                    return false; 
                }
            }
            catch
            {
                return false;
            }  
        }

        public int Login(string email, string password) 
        {
            if(CheckLoginInfo(email, password))
            {
                return DbSet.Where(u => u.Email == email).Select(u => u.ID).FirstOrDefault();
            }
            else
            { 
                return 0; 
            }
        }

        public bool CheckEmailInDb(string email)
        {
            if(DbSet.Where(u => u.Email == email).Any()) 
            {
                return true;
            }
            else
            {
                return false;
            }
   
        }
        public bool  CheckEmailLogic(string email)
        {
            if (email.Contains("@") && email.Contains(".com"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckLoginInfo(string email, string password)
        {
            if(DbSet.Where(u => (u.Password == password) && (u.Email == email)).Any()) 
            {
                return true;
            }
            else
            { 
                return false; 
            } 
        }
    }
}
