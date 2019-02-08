using System;
using System.Web.Mvc;
using vidly.Models;
using vidly.ViewModels;
using NHibernate;
using Vidly.Models;
using Vidly.ViewModels;

namespace vidly.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var customers = session.QueryOver<Customer>().List();

                    //int top = 0;
                    //foreach (var customer in customers)
                    //    top = customer.Movies.Count;

                    var viewModel = new CustomersClassViewModel
                    {
                        Customers = customers
                    };

                    return View(viewModel);
                }
            }
        }

        // GET: Customers/CustomerDetails
        public ActionResult CustomerDetails(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Customers");
            }

            id = Convert.ToInt32(id);
            CustomerDetailsClassViewModel viewModel = null;

            using (ISession session = SessionFactory.OpenSession())
            {
                var movies = session.QueryOver<Movie>()
                    .JoinQueryOver<Customer>(c => c.Customers)
                    .Where(c => c.Id == id)
                    .List();
                var customers = session.Get<Customer>(id);

                if (movies.Count > 0)
                {
                    viewModel = new CustomerDetailsClassViewModel
                    {
                        Name = customers.Name,
                        CPF = customers.CPF,
                        DateOfBirth = customers.DateOfBirth,
                        User = customers.User,
                        Movies = movies
                    };
                }
                else
                {
                    viewModel = new CustomerDetailsClassViewModel
                    {
                        Name = customers.Name,
                        CPF = customers.CPF,
                        DateOfBirth = customers.DateOfBirth,
                        User = customers.User,
                        Movies = null
                    };
                }

                return View(viewModel);
            }
        }

        //GET: Customers/ChangeAcessLevel
        public ActionResult ChangeAcessLevel(int id)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                var user = session.Get<User>(id);

                var permission = session.Get<Permission>(user.Permission.Id);

                ViewBag.Message = user.Permission.Name;

                var viewModel = new AcessChangeViewModel
                {
                    User = user,
                    Permission = permission
                };

                return View(viewModel);
            }
        }

        //POST: Customers/ChangeAcessLevel
        [HttpPost]
        public ActionResult ChangeAcessLevel(AcessChangeViewModel model)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var queryUser = session.Get<User>(model.User.IdAccount);

                    var queryPermission = session.Get<Permission>(model.Permission.Id);

                    queryUser.Permission = queryPermission;

                    session.Save(queryUser);

                    transaction.Commit();

                    var queryCustomers = session.QueryOver<Customer>().List();

                    ViewBag.Message = "Nivel de acesso trocado com sucesso!";

                    var viewModel = new CustomersClassViewModel
                    {
                        Customers = queryCustomers
                    };

                    return View("Index", viewModel);
                }
            }
        }
    }
}