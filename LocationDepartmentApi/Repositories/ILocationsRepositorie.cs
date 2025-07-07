using LocationDepartmentApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationDepartmentApi.Repositories
{
    public interface ILocationsRepositorie
    {

        Task<IEnumerable<Location>> GetLocations();
        Task<Location> GetLocationById(int id);
        Task<Location> CreateLocation(Location location);
        Task<Location> UpdateLocation(Location location);
        Task<Location> DeleteLocation(int locationId);


    }
}
