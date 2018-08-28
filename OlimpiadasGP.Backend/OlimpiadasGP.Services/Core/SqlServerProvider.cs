using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using OlimpiadasGP.Services.Maps;


namespace OlimpiadasGP.Services.Core
{
    public class SqlServerProvider : INHProvider
    {
        public Configuration Configuration { get; }

        public SqlServerProvider(string connectionString)
        {
            Configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TeamMap>())
                .BuildConfiguration();
        }
    }
}
