using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace OlimpiadasGP.Services.Core
{
    public interface INHSessionFactory : IDisposable
    {
        ISession OpenSession();

        Configuration Configuration { get; }
    }
}
