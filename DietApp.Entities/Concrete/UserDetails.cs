using DietApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.Entities.Concrete
{
    public class UserDetails : IEntity
    {
        public int ID { get; set; }

        public string Name  { get; set; }
        public string Surname { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }

        #region Navigation Properties

        public int UserID { get; set; }
        public User User { get; set; }

        #endregion
    }
}
