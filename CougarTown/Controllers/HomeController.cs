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
        HomeFactory homeFac = new HomeFactory();
        UserFactory userFac = new UserFactory();

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
    }
}