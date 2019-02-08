using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidly.Mapping
{
    public class MoviesMapping : ClassMap<Models.Movie>
    {
        public MoviesMapping()
        {
            Table("Movie");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Genre);
            Map(x => x.Rating);
            Map(x => x.ReleaseDate);
            HasManyToMany(x => x.Customers)
                .Cascade.All()
                .Inverse()
                .Table("CustomerMovie");
        }
    }
}