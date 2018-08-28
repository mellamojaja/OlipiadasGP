using FluentNHibernate.Mapping;
using OlimpiadasGP.Services.Models;

namespace OlimpiadasGP.Services.Maps
{
    class SportMap : ClassMap<Sport>
    {
        public SportMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);            
        }
    }
}