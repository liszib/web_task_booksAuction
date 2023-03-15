using OnlineAuction.BLL.Interface;
using OnlineAuction.DAL.Interface;
using OnlineAuction.Entities;
using System.Collections.Generic;

namespace OnlineAuction.BLL
{
    public class UserLogic : IUserLogic
    {
        private IUserDao _userDao;

        public UserLogic() { }
        public UserLogic(IUserDao userDao)
        {
            _userDao = userDao;
        }
        public int Add(User user)
        {
           return _userDao.Add(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _userDao.GetAll();
        }

        public User GetUserById(int userId)
        {
            return _userDao.GetUserById(userId);
        }

        public void DeleteUser(int userId)
        {
            _userDao.DeleteUser(userId);
        }
    }
}
