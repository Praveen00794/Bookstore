using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookstore.Models;
using Bookstore.BussinessLogic;
using System.Web.Security;
namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        BookstoreDAL objDAL = new BookstoreDAL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Loginpage()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Loginpage(UserDetails model)
        {
            
            if (ModelState.IsValid)
            {
                if(objDAL.Verifylogin(model.Username, model.Password))
                    {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("BookPage", "Books");
                }
                else
                {
                    TempData["msg"] = "Invalid email or password";
                }
                
            }
            return View(model);
           
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Loginpage","Home");
        }
    }
}