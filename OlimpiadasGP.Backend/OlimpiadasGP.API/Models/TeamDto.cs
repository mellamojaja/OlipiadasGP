using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlimpiadasGP.Services.Models;

namespace OlimpiadasGP.API.Models
{
    /// <summary>
    /// Return info for a Team
    /// </summary>
    public class TeamDto
    {
        /// <summary>
        /// The elapsed time in milliseconds that takes to retrieve the <see cref="Team"/>
        /// </summary>
        public double ElapsedTime;

        /// <summary>
        /// The teams
        /// </summary>
        public Team Team;
    }
}
