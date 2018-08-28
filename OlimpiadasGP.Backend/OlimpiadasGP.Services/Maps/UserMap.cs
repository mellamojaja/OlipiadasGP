using FluentNHibernate.Mapping;
using OlimpiadasGP.Services.Models;

namespace OlimpiadasGP.Services.Maps
{
    class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.RealName);
            Map(x => x.NickName);
            Map(x => x.Email);
        }
    }
}