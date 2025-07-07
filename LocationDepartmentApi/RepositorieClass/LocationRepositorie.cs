using LocationDepartmentApi.Data;
using LocationDepartmentApi.Models;
using LocationDepartmentApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationDepartmentApi.RepositorieClass
{
    public class LocationRepositorie : ILocationsRepositorie
    {
        private readonly AppDbContext appDbContext;

        public LocationRepositorie(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // for Get Location
        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await appDbContext.Locations.ToListAsync();
        }

        // for Get by Id
        
        public async Task<Location> GetLocationById(int id)
        {
            return await appDbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == id);

        }


        // for Create a Location

        public async Task<Location> CreateLocation(Location location)
        {
            if(location != null)
            {
                appDbContext.Entry(location).State = EntityState.Unchanged;
            }

            var store = await appDbContext.AddAsync(location);
            await appDbContext.SaveChangesAsync();
            return store.Entity;

        }

        // for update Location
        public async Task<Location> UpdateLocation(Location location)
        {
          var update = await appDbContext.Locations.FirstOrDefaultAsync(x=> x.LocationId==location.LocationId);
            if(update != null)
            {
                update.LocationName = location.LocationName;
                update.LocationDescrption = location.LocationDescrption;
                update.Address = location.Address;
                update.City = location.City;
                update.State = location.State;
                update.Country = location.Country;
                update.CreatedAt = location.CreatedAt;

                await appDbContext.SaveChangesAsync();
                return update;
            }
            return null;

        }

        public async Task<Location> DeleteLocation(int locationId)
        {
            var del = await appDbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == locationId);

            if (del != null)
            {
                appDbContext.Locations.Remove(del);
                await appDbContext.SaveChangesAsync();
            }
            return null;

        }



    }
}
