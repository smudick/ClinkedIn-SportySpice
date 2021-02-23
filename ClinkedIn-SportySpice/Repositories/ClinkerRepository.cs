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
        public void Add(Clinker clinker)
        {
            var biggestExistingId = _clinkers.Max(l => l.Id);
            clinker.Id = biggestExistingId + 1;
            _clinkers.Add(clinker);
        }

        public List<Clinker> Get(string interest)
        {
            var clinkers = _clinkers.FindAll(clinker => clinker.Interests.Contains(interest));
            return clinkers;
        }
     }
}
