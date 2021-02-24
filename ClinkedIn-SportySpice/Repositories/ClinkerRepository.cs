using ClinkedIn_SportySpice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn_SportySpice.Repositories
{
    public class ClinkerRepository
    {
        static List<Clinker> _clinkers = new List<Clinker>
        {
            new Clinker {Name="Prison Mike", ReleaseDate=new DateTime(2021,10,31), Interests = new List<string>(){"Robbing", "Stealing", "Kidnapping"} }

        };
        public List<Clinker> GetAll()
        {
            return _clinkers;
        }
        public Clinker GetById(int id)
        {
            var clinker = _clinkers.FirstOrDefault(c => c.Id == id);
            return clinker;
        }
        public List<Clinker> GetByServices(string service)
        {
            var results = _clinkers.Where(clinker => clinker.Services.Contains(service));
            return results;
        }
        public void Add(Clinker clinker)
        {
            var biggestExistingId = _clinkers.Max(l => l.Id);
            clinker.Id = biggestExistingId + 1;
            _clinkers.Add(clinker);
        }
     }
}
