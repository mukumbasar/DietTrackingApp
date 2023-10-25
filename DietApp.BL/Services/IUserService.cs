using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.BL.Services
{
    public interface IUserService
    {
        string AddUser(string email, string password1, string password2);
        bool CheckPasswordParity(string password1, string password2);
        bool CheckPasswordComplexity(string password1);
    }
}
