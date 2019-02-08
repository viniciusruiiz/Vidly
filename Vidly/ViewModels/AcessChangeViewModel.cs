using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class AcessChangeViewModel
    {
        public AcessChangeViewModel()
        {
            //Permission = new List<Permission>();
        }
        public User User { get; set; }
        public Permission Permission { get; set; }
    }
}