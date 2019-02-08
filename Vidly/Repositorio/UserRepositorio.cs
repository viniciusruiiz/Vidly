using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using vidly;
using vidly.Models;
using Vidly.Models;

namespace Vidly.Repositorio
{
    public class UserRepositorio
    {
        public static bool Authentication(string Email, string Password)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                var query = session.QueryOver<User>()
                    .Where(c => c.Email == Email && c.Password == Password)
                    .SingleOrDefault();

                if (query == null)
                    return false;

                FormsAuthentication.SetAuthCookie(query.Email, false);

                return true;
            }
        }

        public static User GetUserLogged()
        {
            string _Login = HttpContext.Current.User.Identity.Name;

            if (_Login == "" || _Login == null)
            {
                return null;
            }
            else
            {
                using (ISession session = SessionFactory.OpenSession())
                {
                    var query = session.QueryOver<User>()
                        .Where(c => c.Email == _Login)
                        .SingleOrDefault();

                    var top = 0;

                    foreach(var movies in query.Customer.Movies)
                    {
                        top++;
                    }

                    return query;
                }
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}