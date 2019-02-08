using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.SqlAzure;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace vidly
{
    public class SessionFactory
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory Session
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();

                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008.ConnectionString(
                        System.Configuration.ConfigurationManager.ConnectionStrings["SqlAzureConnection"].ConnectionString)
                        .Driver<SqlAzureClientDriver>()
                        .ShowSql()
                        .FormatSql())
                        .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                        .BuildSessionFactory();  
        }

        public static ISession OpenSession()
        {
            return Session.OpenSession();
        }
    }
}