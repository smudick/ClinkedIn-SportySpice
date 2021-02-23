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
        [HttpPost]
        public IActionResult AddClinker(Clinker clinker)
        {
            _repo.Add(clinker);
            return Created($"api/Loaves/{clinker.Name}", clinker);
        }
    }
}
