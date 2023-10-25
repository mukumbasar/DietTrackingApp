using DietApp.BL.Services;
using DietApp.DAL.Concrete;
using DietApp.Entities.Common;
using DietApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.BL.Managers
{
    public class UserDetailsManager : BaseManager<UserDetails>, IUserDetailsService
    {
        protected UserDetailsRepository _userDetailsRepository;

        public UserDetailsManager(GenericRepository<UserDetails> genericRepository, UserDetailsRepository userDetailsRepository) : base(genericRepository)
        {
            _userDetailsRepository = userDetailsRepository;
        }
        public StructUserDetails GetUserDetails(int ID)
        {
            return _userDetailsRepository.GetUserDetails(ID);
        }

        public void SetUserDetails(int ID, string Name, string Surname, string Height, string Weight)
        {
            _userDetailsRepository.SetUserDetails(ID,Name,Surname,Height,Weight);
        }
    }
}

