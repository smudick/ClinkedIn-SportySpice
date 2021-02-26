using ClinkedIn_SportySpice.Models;
using ClinkedIn_SportySpice.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn_SportySpice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinkersController : ControllerBase
    {
        ClinkerRepository _repo;
        public ClinkersController()
        {
            _repo = new ClinkerRepository();
        }
        [HttpGet]
        public IActionResult GetAllClinkers()
        {
            return Ok(_repo.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_repo.GetById(id));
        }
        [HttpGet("search/service/{service}")]
        public IActionResult GetByServices(string service)
        {
            var clinkers = _repo.GetByServices(service);

            if (clinkers.Count == 0)
            {
                return NotFound("No clinkers matched your search request.");
            }

            return Ok(clinkers);
        }
        [HttpPost]
        public IActionResult AddClinker(Clinker clinker)
        {
            _repo.Add(clinker);
            return Created($"api/clinkers/{clinker.Name}", clinker);
        }

        [HttpPut("{id}/add-enemy/{enemyId}")]
        public IActionResult AddEnemy(int id, int enemyId)
        {
            _repo.AddEnemy(id, enemyId);
            return Created($"api/clinkers/{id}/add-enemy/{enemyId}", "Enemy successfully added");
        }

        [HttpPut("{id}/add-friend/{friendId}")]
        public IActionResult AddFriend(int id, int friendId)
        {
            _repo.AddFriend(id, friendId);
            return Created($"api/clinkers/{id}/add-friend/{friendId}", "Friend successfully added");
        }

        [HttpGet("{id}/second-friends")]
        public IActionResult GetSecondFriends(int id)
        {

        }

        [HttpPut("{id}/services")]
        public IActionResult ListService(int id, [FromBody] string service)
        {
            var clinker = _repo.GetById(id);
            clinker.Services.Add(service);

            return Ok(_repo.GetById(id));
        }

        //GET to /api/clinkers/{interest}
        [HttpGet("search/interest/{interest}")]
        public IActionResult GetByInterest(string interest)
        {
            var clinkers = _repo.GetByInterest(interest);

            if (clinkers.Count == 0)
            {
                return NotFound("No clinkers matched your search request.");
            }

            return Ok(clinkers);
        }

        // GET to /api/clinkers/{id}/days-left
        [HttpGet("{id}/days-left")]
        public IActionResult GetDaysLeft(int id)
        {
            var clinker = _repo.GetById(id);
            DateTime today = DateTime.Now;
            var releaseDate = clinker.ReleaseDate;
            double daysLeft = Math.Round(releaseDate.Subtract(today).TotalDays);
            
            if (daysLeft == 1)
            {
                return Ok($"You have {daysLeft} day left in your sentence!");
            } 
            else
            {
                return Ok($"You have {daysLeft} days left in your sentence.");
            }

        }

    }
}
