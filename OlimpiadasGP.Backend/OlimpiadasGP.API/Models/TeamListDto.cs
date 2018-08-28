using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlimpiadasGP.Services.Models;

namespace OlimpiadasGP.API.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TeamListDto
    {
        /// <summary>
        /// The elapsed time in milliseconds that takes to retrieve <see cref="Team"/>
        /// </summary>
        public double ElapsedTime;

        /// <summary>
        /// The list of teams
        /// </summary>
        public IList<Team> Teams;
    }
}
