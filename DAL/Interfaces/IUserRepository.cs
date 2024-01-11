using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {

        public User Create(User user);

        public User? GetByEmail(string email);

        public User? GetById(int id);

        public IEnumerable<User> GetAll();

        public bool Update(User user);

        public bool Delete(User user);

    }
}
