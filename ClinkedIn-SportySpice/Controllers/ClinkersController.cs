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

    }
}
