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
        UserLikesFactory userLikesFac;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            homeFac = new HomeFactory(this.HttpContext);
            userFac = new UserFactory(this.HttpContext);
            userLikesFac = new UserLikesFactory(this.HttpContext);
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
            User userLoggedIn = Session["UserLoggedIn"] as User;
            List<User> allUsers = userFac.GetAll();
            if (userLoggedIn != null)
            {
                List<UserLikes> userFilter = userLikesFac.GetAll()
                    .Where(x => x.UserID == userLoggedIn.ID).ToList();

                List<User> filteredListOfUsers = new List<User>();

                foreach (User user in userFac.GetAll())
                {
                    UserLikes ul = userFilter.Find(x => x.OtherUserID == user.ID);
                    if (ul == null || ul.UserLike)
                    {
                        filteredListOfUsers.Add(user);
                    }
                }

                return View(filteredListOfUsers);
            }


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

        public ActionResult Logout()
        {
            Session["UserLoggedIn"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult MyProfile()
        {
            User userLoggedIn = Session["UserLoggedIn"] as User;
            if (userLoggedIn != null)
            {
                List<UserLikes> usersLiked = userLikesFac.GetAll().Where(x => x.UserID == userLoggedIn.ID).ToList();

                List<User> actualListOfUsers = new List<User>();

                foreach (UserLikes uLikes in usersLiked)
                {
                    actualListOfUsers.Add(userFac.Get(uLikes.OtherUserID));
                }

                ViewBag.ProfilesUserLiked = actualListOfUsers;

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
            User oldUser = userFac.GetUser(updatedUser.ID);
            string profileImage = oldUser.ProfileImage;

            updatedUser.ProfileImage = profileImage;

            userFac.Update(updatedUser.ID, updatedUser);

            Session["UserLoggedIn"] = userFac.GetUser(updatedUser.ID);
            return RedirectToAction("MyProfile");
        }

        public ActionResult LikeUser()
        {
            int id = int.Parse(Request.QueryString["id"]);
            bool like = bool.Parse(Request.QueryString["like"]);
            string returnURL = (string)Request.QueryString["returnURL"];

            User userLoggedIn = Session["UserLoggedIn"] as User;

            if (userLoggedIn != null)
            {
                UserLikes userLikes = new UserLikes();
                userLikes.UserID = userLoggedIn.ID;
                userLikes.OtherUserID = id;
                userLikes.UserLike = like;

                userLikesFac.Add(userLikes);



                return RedirectToAction(returnURL.Replace("/Home", ""));
            }

            return RedirectToAction("Login");
        }
    }
}