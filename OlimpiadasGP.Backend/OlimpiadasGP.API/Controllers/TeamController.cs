using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OlimpiadasGP.API.Models;
using OlimpiadasGP.Services.Models;
using OlimpiadasGP.Services.Repositories;

namespace OlimpiadasGP.API.Controllers
{
    /// <summary>
    /// Values controller summary info
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        private readonly ITeamRepository TeamRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamRepository"></param>
        public TeamController(ITeamRepository teamRepository)
        {
            TeamRepository = teamRepository;
        }

        // GET api/values
        /// <summary>
        ///  Get values summary info
        /// </summary>
        [HttpGet]
        public ActionResult<TeamListDto> Get()
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            IList<Team> teams = TeamRepository.GetAllTeams();
            sw.Stop();

            var result = new TeamListDto() {Teams = teams, ElapsedTime = sw.Elapsed.TotalMilliseconds};
            return new ActionResult<TeamListDto>(result);
        }
        
        // GET api/values/5
        /// <summary>
        ///  Get values summary info
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<TeamDto> Get(int id)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            Team team = TeamRepository.GetTeam(id);
            sw.Stop();

            var result = new TeamDto() {Team = team, ElapsedTime = sw.Elapsed.TotalMilliseconds};
            return new ActionResult<TeamDto>(result);
        }

       

        // GET api/values
        /// <summary>
        ///  Add quantity random teams and resturn the elapsed time in milliseconds 
        /// </summary>
        [HttpGet("{quantity}")]
        public ActionResult<double> AddRandomTeams(int quantity)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            TeamRepository.AddRandomTeams(quantity);
            sw.Stop();

            return new ActionResult<double>(sw.Elapsed.TotalMilliseconds);
        }

        // POST api/values
        /// <summary>
        ///  Add value summary info
        /// </summary>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        /// <summary>
        ///  Update value summary info
        /// </summary>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        /// <summary>
        ///  Delete value summary info
        /// </summary>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
