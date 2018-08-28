using FluentNHibernate.Mapping;
using OlimpiadasGP.Services.Models;

namespace OlimpiadasGP.Services.Maps
{
    class TeamMap : ClassMap<Team>
    {
        public TeamMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
           
        }
    }
}