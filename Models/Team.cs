using System.Collections.Generic;

namespace Showdown3.Models
{
    public class Team
    {
        public List<Racer> Racers { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }


        public Team(List<Racer> racers, string name, string tag)
        {
            Racers = racers ?? new List<Racer>(); // Vermeidet Null-Werte
            Name = name;
            Tag = tag;
        }

        public Team(string name, string tag) : this(new List<Racer>(), name, tag)
        {
        }

        public Team() : this(new List<Racer>(), null, null)
        {
        }
    }
}