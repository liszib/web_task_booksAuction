using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using OnlineAuction.BLL.Interface;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        private IUserLogic _userLogic;
        private ILotLogic _lotLogic;


        public UserController(IUserLogic userLogic, ILotLogic lotLogic)
        {
            _userLogic = userLogic;
            _lotLogic = lotLogic;

        }

        [HttpGet]
        public IActionResult PersonalInformation()
        {
            int userID = 0;

            if (Request.Cookies.ContainsKey("logIn"))
            {
                userID = int.Parse(Request.Cookies["logIn"]);
            }

            if (userID != 0)
            {
                var user = _userLogic.GetUserById(userID);

                if (user != null)
                {
                    var selllots = _lotLogic.GetSellLots(userID);
                    var boughtLots = _lotLogic.GetBoughtLots(userID);

                    List<LotModel> SellLots = new List<LotModel>();
                    List<LotModel> BoughtLots = new List<LotModel>();

                    foreach (var selllot in selllots)
                    {
                        SellLots.Add(new LotModel()
                        {
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
                            SellLots.Last().Customer = new UserModel()
                            {
                                Name = selllot.Customer.Name,
                                SurName =selllot.Customer.SurName,
                                PhoneNumber =selllot.Customer.PhoneNumber,
                                Email =selllot.Customer.Email
                            };
                        }
                    }

                    foreach (var boughtlot in boughtLots)
                    {
                        BoughtLots.Add(new LotModel()
                        {
                            Name = boughtlot.Name,
                            Price = boughtlot.Price,
                            Seller = new UserModel()
                            {
                                Name = boughtlot.Seller.Name,
                                SurName = boughtlot.Seller.SurName,
                                PhoneNumber = boughtlot.Seller.PhoneNumber,
                                Email = boughtlot.Seller.Email
                            },
                            Author = boughtlot.Author,
                            
                            Description = boughtlot.Description,
                            Genre = boughtlot.Genre
                        });

                        if (boughtlot.Customer != null)
                        {
                            BoughtLots.Last().Customer = new UserModel()
                            {
                                Name = boughtlot.Customer.Name,
                                SurName = boughtlot.Customer.SurName,
                                PhoneNumber = boughtlot.Customer.PhoneNumber,
                                Email = boughtlot.Customer.Email
                            };
                        }
                    }

                    UserModel userModel = new UserModel()
                    {
                        Name = user.Name,
                        SurName = user.SurName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        SellLots = SellLots,
                        BoughtLots = BoughtLots
                    };

                    return View(userModel);
                }
                return RedirectToAction("ErrorPage", "Home");
            }

            else
            {
                return RedirectToAction("ErrorPage", "Home");
            }
        }
    }
}
