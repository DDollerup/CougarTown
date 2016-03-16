using CougarTown.Models.BaseModels;
using CougarTown.Models.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CougarTown.Areas.CougarManagement.Controllers
{
    public class PimpController : Controller
    {
        UserFactory userFac;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            userFac = new UserFactory(this.HttpContext);
            base.OnActionExecuting(filterContext);
        }

        // GET: CougarManagement/Pimp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }

        public ActionResult AllUsers()
        {
            List<User> allUsers = userFac.GetAll();
            return View(allUsers);
        }

        [HttpPost]
        public ActionResult AddUserSubmit(User user, HttpPostedFileBase file)
        {
            // Is there a file, and is the file larger than 0 bytes
            if (file != null && file.ContentLength > 0)
            {
                user.ProfileImage = file.FileName;
                string appPath = Request.PhysicalApplicationPath;
                file.SaveAs(appPath + @"/Content/Images/Users/" + file.FileName);
            }

            userFac.Add(user);

            return RedirectToAction("AllUsers");
        }
    }
}