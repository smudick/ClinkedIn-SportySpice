using ClinkedIn_SportySpice.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn_SportySpice.Repositories
{
    public class ClinkerRepository
    {
        const string ConnectionString = "Server=localhost;Database=ClinkedIn;Trusted_Connection=True;";
        public List<Clinker> GetAll()
        {
            using var db = new SqlConnection(ConnectionString);

            var sql = @"Select c.id
                        from Clinkers c";
            var ids = db.Query<int>(sql).ToList();
            var _clinkers = new List<Clinker>();

            foreach (var id in ids)
            {
                var clinker = GetById(id);
                _clinkers.Add(clinker);
            }
            return _clinkers;
        }
        public Clinker GetById(int id)
        {

            using var db = new SqlConnection(ConnectionString);

            var sql = @"Select * 
                        from Clinkers c
                        where c.id = @id";
            var clinker = db.QueryFirstOrDefault<Clinker>(sql, new { id = id });

            var interestsSql = @"Select i.Name
                                        from Interests i 
                                        join Clinkers c
                                            on c.id = i.clinkerId
                                         where c.id = @id";
            var interests = db.Query<string>(interestsSql, new { id = id }).ToList();

            clinker.Interests = interests;
            var servicesSql = @"Select s.Name
                                        from Services s 
                                        join Clinkers c
                                            on c.id = s.clinkerId
                                         where c.id = @id";
            var services = db.Query<string>(servicesSql, new { id = id }).ToList();
            clinker.Services = services;

            var friendsSql = @"select c.Name 
                                    from Clinkers c
                                        join Friends f 
	                                        on f.ClinkerId2 = c.id
	                                where f.ClinkerId1 = @id";
            var friends = db.Query<string>(friendsSql, new { id = id }).ToList();
            clinker.Friends = friends;
            var enemiesSql = @"select c.Name 
                                    from Clinkers c
                                        join Enemies e 
	                                        on e.ClinkerId2 = c.id
	                                where e.ClinkerId1 = @id";
            var enemies = db.Query<string>(enemiesSql, new { id = id }).ToList();
            clinker.Enemies = enemies;

            return clinker;
        }
        /*
        public List<Clinker> GetByServices(string service)
        {
            var results = new List<Clinker>();
            results = _clinkers.Where(clinker => clinker.Services.Contains(service, StringComparer.InvariantCultureIgnoreCase)).ToList();
            return results;
        }
        public void Add(Clinker clinker)
        {
            var biggestExistingId = _clinkers.Max(l => l.Id);
            clinker.Id = biggestExistingId + 1;
            _clinkers.Add(clinker);
        }

        public List<Clinker> GetByInterest(string interest)
        {
            var clinkers = new List<Clinker>();
            clinkers = _clinkers.FindAll(clinker => clinker.Interests.Contains(interest, StringComparer.InvariantCultureIgnoreCase));
            return clinkers;
        }
        public bool AddEnemy(int userId, int enemyId)
        {
            var userClinker = GetById(userId);
            var enemyClinker = GetById(enemyId);

            if (userClinker == null || enemyClinker == null || userClinker == enemyClinker || userClinker.Enemies.Contains(enemyId))
            {
                return false;
            }
            else if (userClinker.Friends.Contains(enemyId))
            {
                userClinker.Friends.Remove(enemyId);
            }

            userClinker.Enemies.Add(enemyClinker.Id);
            return true;
        }

        public bool AddFriend(int userId, int friendId)
        {
            var userClinker = GetById(userId);
            var friendClinker = GetById(friendId);

            if (friendClinker == null || userClinker == null || userClinker == friendClinker || userClinker.Friends.Contains(friendId))
            {
                return false;
            }
            else if (userClinker.Enemies.Contains(friendId))
            {
                userClinker.Enemies.Remove(friendId);
            }

            userClinker.Friends.Add(friendClinker.Id);
            return true;
        }

        public HashSet<Clinker> GetSecondFriends(int id)
        {
            var user = GetById(id);
            var secondFriends = new HashSet<Clinker>();

            if (user != null)
            {
                foreach (var friendId in user.Friends)
                {
                    var friend = GetById(friendId);
                    var secondFriendIds = friend.Friends.Where(f => !user.Friends.Contains(f)).ToList();

                    if (secondFriendIds.Contains(user.Id))
                    {
                        secondFriendIds.Remove(user.Id);
                    }

                    secondFriendIds.ForEach(friendId => secondFriends.Add(GetById(friendId)));

                }
            }

            return secondFriends;
        }

        public bool AddInterests(int userId, string interest)
        {
            var user = GetById(userId);
            if (user == null)
            {
                return false;
            }
            user.Interests.Add(interest);
            return true;
        }

        public bool DeleteInterests(int userId, int interestId)
        {
            var user = GetById(userId);
            if (user == null)
            {
                return false;
            }
            try
            {
                var interestToRemove = user.Interests[interestId];
                user.Interests.Remove(interestToRemove);
            }
            catch (ArgumentOutOfRangeException)
            {

            }
            return true;

        }
        public bool UpdateInterests(int userId, int interestId, string newInterest)
        {
            var user = GetById(userId);
            if (user == null)
            {
                return false;
            }
            try
            {
                user.Interests[interestId] = newInterest;
            }
            catch (ArgumentOutOfRangeException)
            {
                AddInterests(userId, newInterest);
            }
            return true;
        }

        public bool RemoveService(int id, int position)
        {
            var clinker = GetById(id);
            if (clinker == null)
            {
                return false;
            }
            var serviceToRemove = clinker.Services.ElementAtOrDefault(position);

            clinker.Services.Remove(serviceToRemove);
            return true;
        }

        public bool UpdateService(int id, int position, string newService)
        {
            var clinker = GetById(id);
            if (clinker == null)
            {
                return false;
            }
            try
            {
                clinker.Services[position] = newService;
            }
            catch (ArgumentOutOfRangeException)
            {
                clinker.Services.Add(newService);
            }
            return true;
        }
        */
    }
}
