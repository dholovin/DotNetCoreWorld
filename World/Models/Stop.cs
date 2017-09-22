using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace World.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Order { get; set; }
        public DateTime Arrival { get; set; }        
    }


}
