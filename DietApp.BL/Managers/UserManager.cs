using DietApp.BL.Services;
using DietApp.DAL.Concrete;
using DietApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp.BL.Managers
{
    public class UserManager : BaseManager<User>, IUserService
    {
        protected UserRepository _userRepository;
        public int _id;

        public UserManager(GenericRepository<User> genericRepository, UserRepository userRepository) : base(genericRepository)
        {
            _userRepository = userRepository;
        }

        public void Login(string username, string password)
        {
            _id = _userRepository.Login(username, password);
        }

        public string AddUser(string email, string password1, string password2)
        {
            //E-mail veritabanında mevcut mu?
            if (_userRepository.CheckEmailLogic(email))
            {
                if (!_userRepository.CheckEmailInDb(email))
                {
                    if (CheckPasswordParity(password1, password2))
                    {
                        if (CheckPasswordComplexity(password1))
                        {
                            if (_userRepository.CreateUser(email, password1))
                            {
                                return "Kullanıcı ekleme başarılı.";
                            }
                            else
                            {
                                return "Kullanıcı ekleme başarısız. Servis sağlayıcınıza müracaat ediniz.";
                            }
                        }
                        else
                        {
                            return "Paralo minumum 6 karaktere sahip olmalıdır.\nParola bir büyük harf, bir küçük harf ve bir özel karakter ( . - ! _ + ) içermelidir";
                        }

                    }
                    else
                    {
                        return "Parolalar eşleşmiyor.";
                    }
                }
                else
                {
                    return "Bu e-mail kullanımda.";
                }
            }
            else
            {
                return "Geçerli Bir Email Giriniz.";
            }
            
        }

        public bool CheckPasswordParity(string password1, string password2)
        {
            if (password1 == password2)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        public bool CheckPasswordComplexity(string password1)
        {
            bool HasUppercaseChar = false;
            bool HasLowercaseChar = false;
            bool HasSpecialChar = false;
            bool PasswordLenghtControl = false;

            if (password1.Length>6)
            {
                PasswordLenghtControl = true;
            }

            foreach (char c in password1)
            {
                if (Char.IsLower(c))
                {
                    HasLowercaseChar = true;
                    break;
                }
            }

            if (HasLowercaseChar)
            {
                foreach (char c in password1)
                {
                    if (Char.IsUpper(c))
                    {
                        HasUppercaseChar = true;
                        break;
                    }
                }
            }

            if (HasUppercaseChar)
            {
                foreach (char c in password1)
                {
                    if (c == '.' || c == '-' || c == ',' || c == '!' || c == '_' || c == '+')
                    {
                        HasSpecialChar = true;
                        break;
                    }
                }
            }
            

            if (HasLowercaseChar && HasUppercaseChar && HasSpecialChar && PasswordLenghtControl)
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
