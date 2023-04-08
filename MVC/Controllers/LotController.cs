using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using OnlineAuction.BLL.Interface;
using OnlineAuction.Entities;

namespace MVC.Controllers
{
    public class LotController : Controller
    {
        private ILotLogic _lotLogic;
        private IUserLogic _userLogic;

        public LotController(IUserLogic userLogic, ILotLogic lotLogic)
        {
            _lotLogic = lotLogic;
            _userLogic = userLogic;
        }

        [HttpGet]
        public IActionResult SellALot()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SellALot(LotModel lotModel)
        {
            int userID = 0;

            if (Request.Cookies.ContainsKey("logIn"))
            {
                userID = int.Parse(Request.Cookies["logIn"]);
            }

            if (userID != 0)
            {
                var user = _userLogic.GetUserById(userID);

                int id = _lotLogic.Add(new Lot()
                {
                    Author = lotModel.Author,
                    Genre = lotModel.Genre,
                    Name = lotModel.Name,
                    Price = lotModel.Price,
                    Seller = user,
                    Description = lotModel.Description
                });

                if (ModelState.IsValid && id != 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("ErrorPage");
        }
    }
}
