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

        [HttpGet]
        public IActionResult Search()
        {
            try
            {
                var lots = _lotLogic.GetAll();
                List<LotModel> lotsList = new List<LotModel>();

                foreach (var selllot in lots)
                {
                    lotsList.Add(new LotModel()
                    {
                        ID = selllot.Id,
                        Name = selllot.Name,
                        Price = selllot.Price,
                        Seller = new UserModel()
                        {
                            Name = selllot.Seller.Name,
                            SurName = selllot.Seller.SurName,
                            PhoneNumber = selllot.Seller.PhoneNumber,
                            Email = selllot.Seller.Email
                        },
                        Author = selllot.Author,
                        Description = selllot.Description,
                        Genre = selllot.Genre
                    });

                    if (selllot.Customer != null)
                    {
                        lotsList.Last().Customer = new UserModel()
                        {
                            Name = selllot.Customer.Name,
                            SurName = selllot.Customer.SurName,
                            PhoneNumber = selllot.Customer.PhoneNumber,
                            Email = selllot.Customer.Email
                        };
                    }
                }
                return View(lotsList);
            }
            catch
            {
                return RedirectToAction("ErrorPage", "Home");
            }
        }

        public IActionResult Buy(IFormCollection formCollection)
        {
            var value = formCollection["selectedLot"];

            int userID = 0;

            if (Request.Cookies.ContainsKey("logIn"))
            {
                userID = int.Parse(Request.Cookies["logIn"]);
            }

            if (userID != 0)
            {
                _lotLogic.BuyLot(userID, int.Parse(value));

                return RedirectToAction("Search", "Lot");
            }
            else
            {
                return RedirectToAction("ErrorPage", "Home");
            }
        }
    }
}
