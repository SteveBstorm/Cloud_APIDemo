using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {

        public User Create(User user)
        {
            int Id = FakeDB.Users.Max(x => x.Id) + 1;

            user.Id = Id;

            FakeDB.Users.Add(user);

            return user;
        }

        public User? GetByEmail(string email)
        {
            User? u = FakeDB.Users.SingleOrDefault(x => x.Email == email);

            return u;
        }

        public User? GetById(int id)
        {
            User? u = FakeDB.Users.Find(x => x.Id == id);

            return u;
        }

        public IEnumerable<User> GetAll()
        {
            return FakeDB.Users;
        }

        public bool Update(User user)
        {
            User? u = FakeDB.Users.Find(x => x.Id == user.Id);

            if (u != null)
            {
                u.Pseudo = user.Pseudo;
                u.Email = user.Email;
                u.Password = user.Password;
                return true;
            }

            return false;
        }

        public bool Delete(User user)
        {
            FakeDB.Users.Remove(user);
            return true;
        }
    }
}
