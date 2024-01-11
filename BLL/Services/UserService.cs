using BLL.Forms;
using BLL.Mapper;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User? Create(RegisterForm form)
        {
            User? u = _userRepository.GetByEmail(form.Email);

            if (u == null)
            {
                User user = form.ToUser();

                user.Password = BCrypt.Net.BCrypt.HashPassword(form.Password);

                user = _userRepository.Create(user);
                return user;
            }

            return null;
            
        }

        public bool EmailAlreadyUsed(string email)
        {
            return _userRepository.GetByEmail(email) != null;
        }

        public User? GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public bool UpdatePassword(UpdatePasswordForm form)
        {
            User? u = _userRepository.GetById(form.Id);

            if (u != null)
            {
                if (BCrypt.Net.BCrypt.Verify(form.OldPassword, u.Password))
                {
                    u.Password = BCrypt.Net.BCrypt.HashPassword(form.Password);
                    return _userRepository.Update(u);
                }
            }

            return false;
        }

        public bool Delete(int id)
        {
            User? u = _userRepository.GetById(id);

            if (u is not null)
            {
                return _userRepository.Delete(u);
            }

            return false;
        }

    }
}
