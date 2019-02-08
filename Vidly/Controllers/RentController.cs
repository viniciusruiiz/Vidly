using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidly;
using vidly.Models;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class RentController : Controller
    {
        [Authorize(Roles = "Standard, Kid")]
        // GET: Rent
        public ActionResult Index(int id)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var userEmail = HttpContext.User.Identity.Name;

                    var queryUser = session.QueryOver<User>()
                        .Where(c => c.Email == userEmail)
                        .SingleOrDefault();

                    var queryMovie = session.Get<Movie>(id);

                    foreach (Movie filme in queryUser.Customer.Movies)
                        if (filme == queryMovie)
                        {
                            ViewBag.Message = "Desculpe, você ja alugou esse filme";

                            return View("RentDenied");
                        }


                    if (queryUser.Customer.CanReserveMore() && queryMovie.Disponibility())
                    {
                        queryUser.Customer.Movies.Add(queryMovie);

                        session.SaveOrUpdate(queryUser);

                        transaction.Commit();

                        ViewBag.Message = "Filme alugado com sucesso!";

                        return View("RentSucefull");
                    }

                    if (!queryUser.Customer.CanReserveMore())
                    {
                        ViewBag.Message = "Desculpe, você já alugou 5 filmes de uma única vez :'(";

                        return View("RentDenied");
                    }

                    ViewBag.Message = "Desculpe, esse filme já foi alugado por mais de 50 pessoas :'(";

                    return View("RentDenied");
                }
            }
        }
    }
}