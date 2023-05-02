using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using OnlineAuction.BLL.Interface;
using OnlineAuction.Entities;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserLogic _userLogic;

        public HomeController(ILogger<HomeController> logger, IUserLogic userLogic)
        {
            _logger = logger;
            _userLogic = userLogic;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                int id = _userLogic.HaveUser(loginModel.Login, loginModel.Password);

                if (ModelState.IsValid && id != 0)
                {
                    Response.Cookies.Append("logIn", id.ToString());

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("ErrorPage");
                }
            }
            catch
            {
                return RedirectToAction("ErrorPage");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var id = _userLogic.Add(new User()
                {
                    Name = userModel.Name,
                    SurName = userModel.SurName,
                    Password = userModel.Password,
                    PhoneNumber = userModel.PhoneNumber,
                    Email = userModel.Email,
                    DateOfBirth = userModel.DateOfBirth
                });

                Response.Cookies.Append("logIn", id.ToString());

                return RedirectToAction("Index");
            }

            return RedirectToAction("ErrorPage");
        }

        [HttpGet]
        public IActionResult ErrorPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (Request.Cookies.ContainsKey("logIn"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}