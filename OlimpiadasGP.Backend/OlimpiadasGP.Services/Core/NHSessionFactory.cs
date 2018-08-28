using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace OlimpiadasGP.Services.Core
{
    public class NHSessionFactory : INHSessionFactory
    {
        public Configuration Configuration { get; }

        public NHSessionFactory(INHProvider dbProvider, NHSettings dbSettings)
        {
            Configuration = dbProvider.Configuration;
            _dbSettings = dbSettings;            

            _sessionFactory = Configuration.BuildSessionFactory();          
        }

        public ISession OpenSession()
        {
            lock (Lock)
            {
                if (_firstSession)
                {
                    _firstSession = false;

                    if (_dbSettings.RecreateSchema)
                    {
                        VerifyDbIsTestDb();
                        RebuildSchema();                        
                    }
                    else
                    {
                        UpdateSchema();
                    }
                };
            }

            return _sessionFactory.OpenSession();
        }

        public void Dispose()
        {
            _sessionFactory.Close();
        }

        #region Private
        private static readonly object Lock = new object();
        private static bool _firstSession = true;

        private readonly ISessionFactory _sessionFactory;
        private readonly NHSettings _dbSettings;        

        private void UpdateSchema()
        {
            using (_sessionFactory.OpenSession())
            {
                try
                {
                    new SchemaValidator(Configuration).Validate();
                }
                catch (SchemaValidationException)
                {
                    new SchemaUpdate(Configuration).Execute(true, true);
                }
            }
        }

        private void RebuildSchema()
        {
            new SchemaExport(Configuration).Create(false, true);
        }

        private void VerifyDbIsTestDb()
        {
            if (!_sessionFactory.OpenSession().Connection.Database.ToLower().Contains("test"))
            {
                throw new Exception("Current db is not a test db");
            }
        }        
        #endregion
    }
}
