namespace DAL.Entities
{
    public class User
    {

        public User()
        {
            
        }
        public User(int id, string email, string password, string pseudo)
        {
            Id = id;
            Email = email;
            Password = password;
            Pseudo = pseudo;
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Pseudo { get; set; }
    }
}
