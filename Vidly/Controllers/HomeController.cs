using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidly;
using vidly.Models;
using Vidly.Models;
using Vidly.Repositorio;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class HomeController : Controller
    {
        //GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }

        //GET: Home/Login
        public ActionResult Login()
        {
            if(!Request.IsAuthenticated)
                return View();

            return RedirectToAction("Index", "Home");
        }

        //POST: Home/Login
        [HttpPost]
        public ActionResult Login(User model)
        {
            if (UserRepositorio.Authentication(model.Email, model.Password) == false)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        //GET: Home/Logout
        [Authorize]
        public ActionResult Logout()
        {
            var top = new UserRepositorio();

            top.Logout();

            return RedirectToAction("Index", "Home");
        }

        //GET: Home/Singup
        public ActionResult SingUp()
        {
            if (!Request.IsAuthenticated)
                return View();

            return RedirectToAction("Index", "Home");
        }

        //POST: Home/Singup
        [HttpPost]
        public ActionResult SingUp(SingUpClassViewModel model)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var permission = session.Get<Permission>(model.User.Permission.Id);

                    var user = new User
                    {
                        Email = model.User.Email,
                        Password = model.User.Password,
                        Customer = new Customer
                        {
                            Name = model.Customer.Name,
                            DateOfBirth = model.Customer.DateOfBirth,
                            CPF = model.Customer.CPF
                        },
                        Permission = permission
                    };

                    user.Customer.User = user;

                    session.Save(user);

                    transaction.Commit();

                    return RedirectToAction("Login");
                }
            }
        }
    }
}