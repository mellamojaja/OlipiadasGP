using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;

namespace OlimpiadasGP.Services.Core
{
    public interface INHProvider
    {
        Configuration Configuration { get; }
    }
}
