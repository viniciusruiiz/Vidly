using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Permission
    {
        public Permission()
        {
            User = new List<User>();
        }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<User> User { get; set; }
    }
}