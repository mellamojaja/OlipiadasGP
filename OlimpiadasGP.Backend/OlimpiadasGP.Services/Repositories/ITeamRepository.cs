using System.Collections.Generic;
using OlimpiadasGP.Services.Models;

namespace OlimpiadasGP.Services.Repositories
{
    public interface ITeamRepository
    {
        IList<Team> GetAllTeams();      

        void AddRandomTeams(int quantity);

        Team GetTeam(int id);
    }
}