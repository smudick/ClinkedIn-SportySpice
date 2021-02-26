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
            new Clinker {Id=0,Name="Prison Mike", ReleaseDate=new DateTime(2021,10,31), Interests = new List<string>(){"Robbing", "Stealing", "Kidnapping"} },
            new Clinker {Id=1,Name="Piper", ReleaseDate=new DateTime(2021,2,27), Interests = new List<string>(){"Smuggling", "Stealing", "Kidnapping"} },
            new Clinker {Id=2,Name="Alex", ReleaseDate=new DateTime(2021,6,15), Interests = new List<string>(){"Smuggling", "Stealing", "Kidnapping"} },
            new Clinker {Id=3,Name="Suzanne", ReleaseDate=new DateTime(2021,12,31), Interests = new List<string>(){"Robbing", "Embezzlement", "Corporate Fraud"} }

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
            var results = _clinkers.Where(clinker => clinker.Services.Contains(service, StringComparer.InvariantCultureIgnoreCase));
            return results.ToList();
        }
        public void Add(Clinker clinker)
        {
            var biggestExistingId = _clinkers.Max(l => l.Id);
            clinker.Id = biggestExistingId + 1;
            _clinkers.Add(clinker);
        }

        public List<Clinker> GetByInterest(string interest)
        {
            var clinkers = _clinkers.FindAll(clinker => clinker.Interests.Contains(interest, StringComparer.InvariantCultureIgnoreCase));
            return clinkers;
        }
        public void AddEnemy(int userId, int enemyId)
        {
            var userClinker = GetById(userId);
            var enemyClinker = GetById(enemyId);
            userClinker.Enemies.Add(enemyClinker);
        }

        public void AddFriend(int userId, int friendId)
        {
            var userClinker = GetById(userId);
            var friendClinker = GetById(friendId);
            userClinker.Friends.Add(friendClinker);
        }

        public HashSet<Clinker> GetSecondFriends(int id)
        {
            var user = GetById(id);
            var secondFriends = new HashSet<Clinker>();
            foreach (var friend in user.Friends)
            {
                var addList = friend.Friends.Where(f => !user.Friends.Contains(f)).ToList();
                addList.ForEach(f => secondFriends.Add(f));
            }
            return secondFriends;

        }
     }
}
