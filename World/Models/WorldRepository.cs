using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace World.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _repoContext;

        public WorldRepository(WorldContext context)
        {
            _repoContext = context;
        }   

        public ICollection<Trip> GetAllTrips()
        {
            return _repoContext.Trips.ToList();
        }
    }
}
