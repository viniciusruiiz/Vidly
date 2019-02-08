using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidly.Mapping
{
    class CustomersMapping: ClassMap<Models.Customer>
    {
        public CustomersMapping()
        {
            Table("Customer");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.DateOfBirth);
            Map(x => x.CPF).Unique();
            HasManyToMany(x => x.Movies)
                .Not.LazyLoad()
                .Table("CustomerMovie");
            References(x => x.User)
                .Not.LazyLoad();
        }
    }
}