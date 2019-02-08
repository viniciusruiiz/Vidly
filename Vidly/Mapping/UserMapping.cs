using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace vidly.Mapping
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Table("Users");
            Id(x => x.IdAccount).Column("Id");
            Map(x => x.Email).Unique().Not.Nullable().Unique();
            Map(x => x.Password).Not.Nullable();
            References(x => x.Customer)
                    .Cascade.All()
                    .Not.LazyLoad();                    
            References(x => x.Permission);
        }
    }
}