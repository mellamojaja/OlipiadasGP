using System;
using System.Collections.Generic;
using NHibernate;
using OlimpiadasGP.Services.Models;

namespace OlimpiadasGP.Services.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ISession _session;

        public TeamRepository(ISession session)
        {
            _session = session;
        }

        public IList<Team> GetAllTeams()
        {
            return _session.QueryOver<Team>().List<Team>();
        }       

        public Team GetTeam(int id)
        {
            return _session.QueryOver<Team>().Where(c => c.Id == id).SingleOrDefault();
        }
        
        public void AddRandomTeams(int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var newTeam = new Team() { Name = "Bloque " + i };
                _session.SaveOrUpdate(newTeam);
            }
        }

        #region Private               
        #endregion
    }
}
