using OnlineAuction.Entities;
using System.Collections.Generic;

namespace OnlineAuction.DAL.Interface
{
    public interface IUserDao
    {
        int Add(User user);
        IEnumerable<User> GetAll();
        User GetUserById(int userId);
        void DeleteUser(int userId);
    }
}
