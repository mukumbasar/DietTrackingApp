using DietApp.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.BL.Services
{
    public interface IUserDetailsService
    {
        StructUserDetails GetUserDetails(int ID);
        void SetUserDetails(int ID, string Name, string Surname, string Height, string Weight);
        
    }
}
