using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace World.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Stop> Stops { get; set; }
        //public string Comments{ get; set; }
    }
}
