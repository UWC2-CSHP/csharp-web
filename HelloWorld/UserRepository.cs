using System.Linq;
using HelloWorld.Models;
using System.Collections.Generic;

namespace HelloWorld
{
    public interface IUserRepository
    {
        User LogIn(string email, string password);
    }

    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> Users
        {
            get
            {
                var items = new[]
                {
                    new User{ Id=100, Email="admin", Password="admin", Role="Admin, User"},
                    new User{ Id=101, Email="mike", Password="mike", Role="User"},
                    new User{ Id=102, Email="dave", Password="dave", Role="User"},
                    new User{ Id=103, Email="lisa", Password="lisa", Role="User"},
                };

                return items;
            }
        }

        public User LogIn(string email, string password)
        {
            return Users.SingleOrDefault(t => t.Email.ToLower() == email.ToLower()
                                        && t.Password == password);
        }
    }
}