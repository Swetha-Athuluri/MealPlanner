using Capstone.Web.Crypto;
using Capstone.Web.DAL;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDAL userDal;


        public UserController(IUserDAL userDal)
        {
            this.userDal = userDal;
        }

        // ChildActionOnly makes it so that only another View can call this
        // action. We don't want a regular user to navigate to ~/User/Navigation
        [ChildActionOnly]
        public ActionResult Navigation()
        {
            if (Session[SessionKeys.EmailAddress] == null)
            {
                return PartialView("_AnonymousNav");
            }
            else
            {
                return PartialView("_AuthenticatedNav");
            }
        }

        // GET: User/Index
        public ActionResult Index()
        {
            if (Session[SessionKeys.Username] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: User/Login
        public ActionResult Login()
        {
            return View("Login");
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            User user = userDal.GetUser(model.EmailAddress);
            if (user != null)
            {
                HashProvider hashProvider = new HashProvider();
                bool doesPasswordMatch = hashProvider.VerifyPasswordMatch(user.Password, model.Password, user.Salt); //user.Salt)

                if (!doesPasswordMatch)
                {
                    ModelState.AddModelError("invalid-login", "The username or password combination is not valid");
                    
                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    Session[SessionKeys.Username] = user.Username;
                    Session[SessionKeys.EmailAddress] = user.Email;
                    Session[SessionKeys.UserId] = user.Id;
                    return View("Login", model);
                }
            }
            else
            {
                ModelState.AddModelError("invalid-login", "The username or password combination is not valid");
                return View("Login", model);
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: User/Register
        public ActionResult Register()
        {
            return View("Register");
        }

        // POST: User/Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            User user = userDal.GetUser(model.EmailAddress);
            // Check to see if the username already exists
            if (user != null)
            {
                ModelState.AddModelError("username-exists", "That email address is not available");
                return View("Register", model);
            }
            else
            {
                HashProvider hashProvider = new HashProvider();
                string hashedPassword = hashProvider.HashPassword(model.Password);
                string salt = hashProvider.SaltValue; //<-- the user doesn't have  a salt value in the DAL 
                // Convert from the ViewModel to a Data Model and Save
                user = new User()
                {
                    Username = model.Username,
                    Email = model.EmailAddress,
                    Password = hashedPassword,
                    Salt = salt
                };
                userDal.SaveUser(user);

                FormsAuthentication.SetAuthCookie(user.Email, true);
                Session[SessionKeys.Username] = model.Username;
                Session[SessionKeys.EmailAddress] = model.EmailAddress;
                Session[SessionKeys.UserId] = user.Id;
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: User/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove(SessionKeys.EmailAddress);
            Session.Remove(SessionKeys.UserId);
            return RedirectToAction("Index", "Home");
        }

    }
}