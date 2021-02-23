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

        //GET to /api/clinkers/{id}
        [HttpGet("{id}")]
        public IActionResult GetByInterest(string interest)
        {
            var clinkers = _repo.Get(interest);

            if (clinkers == null)
            {
                return NotFound("No clinkers matched your search request.");
            }

            return Ok(clinkers);
        }

    }
}
