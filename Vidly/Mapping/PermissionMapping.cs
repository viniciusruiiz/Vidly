using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Mapping
{
    public class PermissionMapping : ClassMap<Permission> 
    {
        public PermissionMapping()
        {
            Table("Permission");
            Id(x => x.Id);
            Map(x => x.Name);
            HasMany
                (x => x.User);
        }
    }
}