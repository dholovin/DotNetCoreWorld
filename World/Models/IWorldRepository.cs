using System.Collections.Generic;

namespace World.Models
{
    public interface IWorldRepository
    {
        ICollection<Trip> GetAllTrips();
    }
}