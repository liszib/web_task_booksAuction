using OnlineAuction.Entities;
using System.Collections.Generic;

namespace OnlineAuction.BLL.Interface
{
    public interface ILotLogic
    {
        int Add(Lot lot);
        void BuyLot(int userId, int lotId);
        IEnumerable<Lot> GetAll();
        Lot GetLotById(int lotId);
        IEnumerable<Lot> GetBoughtLots(int userId);
        IEnumerable<Lot> GetSellLots(int userId);
        IEnumerable<Lot> GetNoUserLotForSellByID(int userId);
        IEnumerable<Lot> GetNoUserLotForSellByIDAndGenre(int userId, string genre);
        IEnumerable<Lot> GetAllLotsByGenre(string genre);
        void DeleteLot(int lotId);
    }
}
