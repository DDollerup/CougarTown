using CougarTown.Models.BaseModels;
using CougarTown.Models.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CougarTown.Controllers
{
    public class HomeController : Controller
    {
        HomeFactory homeFac;
        UserFactory userFac;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            homeFac = new HomeFactory(this.HttpContext);
            userFac = new UserFactory(this.HttpContext);
            base.OnActionExecuting(filterContext);
        }

        // GET: Home
        public ActionResult Index()
        {
            Home home = homeFac.GetHome(1);
            return View(home);
        }

        public ActionResult HotOrNot()
        {
            List<User> allUsers = userFac.GetAll();
            return View(allUsers);
        }

        public ActionResult PublicProfile(int id)
        {
            User requestedUser = userFac.GetUser(id);
            return View(requestedUser);
        }

        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginSubmit(string displayName, string password)
        {
            User userToLogin = userFac.UserLogin(displayName, password);
            if (userToLogin == null)
            {
                TempData["MSG"] = "Wrong password or display name!";
                return RedirectToAction("Login");
            }

            Session["UserLoggedIn"] = userToLogin;

            return RedirectToAction("Index");
        }

        public ActionResult MyProfile()
        {
            User userLoggedIn = Session["UserLoggedIn"] as User;
            if (userLoggedIn != null)
            {
                return View(userLoggedIn);
            }
            return RedirectToAction("Login");
        }

        public ActionResult EditUser()
        {
            User userToEdit = Session["UserLoggedIn"] as User;
            return View(userToEdit);
        }

        [HttpPost]
        public ActionResult UpdateUserSubmit(User updatedUser)
        {
            userFac.Update(updatedUser.ID, updatedUser);
            Session["UserLoggedIn"] = userFac.GetUser(updatedUser.ID);
            return RedirectToAction("MyProfile");
        }
    }
}