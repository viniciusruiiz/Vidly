using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vidly.Models;

namespace Vidly.Models
{
    public class User
    {
        public User()
        {

        }
        public virtual int IdAccount { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Permission Permission { get; set; }
    }
}