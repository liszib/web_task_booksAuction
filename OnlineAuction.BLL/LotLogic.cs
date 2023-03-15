using OnlineAuction.BLL.Interface;
using OnlineAuction.DAL.Interface;
using OnlineAuction.Entities;
using System.Collections.Generic;

namespace OnlineAuction.BLL
{
    public class LotLogic : ILotLogic
    {
        private ILotDao _lotDao;

        public LotLogic(ILotDao lotDao)
        {
            _lotDao = lotDao;
        }
        public int Add(Lot lot)
        {
           return _lotDao.Add(lot);
        }

        public IEnumerable<Lot> GetAll()
        {
            return _lotDao.GetAll();
        }

        public void BuyLot(int userId, int lotId)
        {
            _lotDao.BuyLot(userId, lotId);
        }

        public Lot GetLotById(int lotId)
        {
            return _lotDao.GetLotById(lotId);
        }

        public IEnumerable<Lot> GetBoughtLots(int userId)
        {
            return _lotDao.GetBoughtLots(userId);
        }

        public IEnumerable<Lot> GetSellLots(int userId)
        {
            return _lotDao.GetSellLots(userId);
        }

        public IEnumerable<Lot> GetNoUserLotForSellByID(int userId)
        {
            return _lotDao.GetNoUserLotForSellByID(userId);
        }
       public IEnumerable<Lot> GetNoUserLotForSellByIDAndGenre(int userId, string genre)
        {
            return _lotDao.GetNoUserLotForSellByIDAndGenre(userId, genre);
        }
        public void DeleteLot(int lotId)
        {
            _lotDao.DeleteLot(lotId);
        }

        public IEnumerable<Lot> GetAllLotsByGenre(string genre)
        {
           return _lotDao.GetAllLotsByGenre(genre);
        }
    }
}
