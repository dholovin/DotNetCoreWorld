using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace World.Models
{
    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext _repoContext;
        private readonly ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _repoContext = context;
            _logger = logger;
        }   

        public ICollection<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All trips from the Database");

            return _repoContext.Trips.ToList();
        }
    }
}
