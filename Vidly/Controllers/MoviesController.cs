using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using vidly.Models;
using vidly.ViewModels;

namespace vidly.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {

            using (ISession session = SessionFactory.OpenSession())
            {
                IList<Movie> movies = null;

                if (User.IsInRole("Kid"))
                    movies = session.QueryOver<Movie>()
                        .Where(c => c.Rating <= 12)
                        .List();

                else
                    movies = session.QueryOver<Movie>().List();

                var viewModel = new MoviesClassViewModel
                {
                    Movies = movies
                };

                return View(viewModel);
            }

        }

        [Authorize(Roles = "Admin")]
        // GET: Movies/AddMovie
        public ActionResult AddMovie()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Movies/AddMovie
        [HttpPost]
        public ActionResult AddMovie(Movie model)
        {
            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(model);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Movies/EditMovie
        public ActionResult EditMovie(int id)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                var movie = session.Get<Movie>(id);
                return View(movie);
            }
        }

        [Authorize(Roles = "Admin")]
        // POST: Movies/EditMovie
        [HttpPost]
        public ActionResult EditMovie(int id, Movie model)
        {
            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    var alteredMovie = session.Get<Movie>(id);

                    if (model.Name != "" || model.Name != null)
                        alteredMovie.Name = model.Name;
                    if (model.Genre != "" || model.Genre != null)
                        alteredMovie.Genre = model.Genre;
                    if (model.Rating != 0)
                        alteredMovie.Rating = model.Rating;
                    if (model.ReleaseDate < new DateTime(1800, 1, 1) || model.ReleaseDate != null)
                        alteredMovie.ReleaseDate = model.ReleaseDate;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(alteredMovie);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        //GET: Movies/DeleteMovie
        public ActionResult DeleteMovie(int id)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                var movie = session.Get<Movie>(id);
                return View(movie);
            }
        }

        [Authorize(Roles = "Admin")]
        // POST: Movies/DeleteMovie
        [HttpPost]
        public ActionResult DeleteMovie(int id, Movie model)
        {
            try
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(model);
                        transaction.Commit();
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        // GET: Movies/MovieDetails
        public ActionResult MovieDetails(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Movies");
            }

            var Id = Convert.ToInt32(id);
            MovieDetailsClassViewModel viewModel = null;

            using (ISession session = SessionFactory.OpenSession())
            {
                var customers = session.QueryOver<Customer>()
                .JoinQueryOver<Movie>(c => c.Movies)
                .Where(c => c.Id == Id)
                .List<Customer>();

                var movie = session.Get<Movie>(Id);

                if (customers.Count > 0)
                {
                    viewModel = new MovieDetailsClassViewModel
                    {
                        Movie = movie,
                        Customers = customers
                    };
                }
                else
                {
                    viewModel = new MovieDetailsClassViewModel
                    {
                        Movie = movie,
                        Customers = null
                    };

                }

                return View(viewModel);
            }
        }

        //GET: Movies/Library
        public ActionResult Library(int id)
        {
            using (ISession session = SessionFactory.OpenSession()) {

                var customer = session.Get<Customer>(id);

                var viewModel = new MoviesClassViewModel
                {
                    Movies = customer.Movies
                };

                return View(viewModel);
            }
        }
    }
}