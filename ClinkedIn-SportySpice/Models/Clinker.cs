using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn_SportySpice.Models
{
    public class Clinker
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<string> Interests { get; set; }
        public List<string> Services { get; set; } = new List<string>();
        public List<int> Friends { get; set; } = new List<int>();
        public List<int> Enemies { get; set; } = new List<int>();

    }
}
