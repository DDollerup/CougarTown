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
        public ActionResult AddUserSubmit(int id, User user, HttpPostedFileBase file)
        {
            // Is there a file, and is the file larger than 0 bytes
            if (file != null && file.ContentLength > 0)
            {
                user.ProfileImage = file.FileName;
                string appPath = Request.PhysicalApplicationPath;
                file.SaveAs(appPath + @"/Content/Images/Users/" + file.FileName);
            }

            userFac.Add(user);

            if (id == 0)
            {
                return RedirectToAction("AllUsers");
            }
            else
            {
                return RedirectToAction("HotOrNot", "Home", new { area = "" });
            }
        }

        public ActionResult UpdateUser(int id)
        {
            User userToUpdate = userFac.GetUser(id);
            return View(userToUpdate);
        }

        [HttpPost]
        public ActionResult UpdateUserSubmit(User updatedUser)
        {
            userFac.Update(updatedUser.ID, updatedUser);
            return RedirectToAction("AllUsers");
        }

        public ActionResult DeleteUser(int id)
        {
            userFac.Remove(id);
            return RedirectToAction("AllUsers");
        }
    }
}