using OnlineAuction.Entities;
using System.Collections.Generic;

namespace OnlineAuction.BLL.Interface
{
    public interface IUserLogic
    {
        int Add(User user);
        IEnumerable<User> GetAll();
        User GetUserById(int userId);
        void DeleteUser(int userId);
    }
}
