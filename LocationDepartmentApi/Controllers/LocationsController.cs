using LocationDepartmentApi.Dtos;
using LocationDepartmentApi.Models;
using LocationDepartmentApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LocationDepartmentApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {

        private readonly ILocationsRepositorie locationRepository;

        public LocationsController(ILocationsRepositorie locationRepository)
        {
            this.locationRepository = locationRepository;
        }



        [HttpGet("GetAllLoations")]
        public async Task<ActionResult> GetLocations()
        {
            try
            {
                return Ok(await locationRepository.GetLocations());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // Check Stuats
        [HttpPut("CheckStatus")] 

        public async Task<ActionResult<IsActiveDTO>> CheckStuats(IsActiveDTO location)
        {
            try
            {
                
                var Exitinglocation = await locationRepository.GetLocationById(location.LocationId);
                if(Exitinglocation == null)
                {
                    return NotFound();
                }

                Exitinglocation.IsActive = location.IsActive; 

                await locationRepository.UpdateLocation(Exitinglocation);

                var IsActiveDTO = new IsActiveDTO
                {
                    LocationId = location.LocationId,
                    IsActive = Exitinglocation.IsActive
                };
                return Ok(IsActiveDTO);

            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }


    // Get BY ID

    [HttpGet("GetLocationById {id:int}")]
        public async Task<ActionResult<Location>> GetLocationById(int id)
        {
            try
            {
                var result = await locationRepository.GetLocationById(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
    catch (Exception)
    {
        return StatusCode(StatusCodes.Status500InternalServerError,
            "Error retrieving data from the database");
    }
}





        // Create Location 

        [HttpPost("CreateNewLocation")]
        public async Task<ActionResult<Location>> CreateLocation(Location location)
        {
            try
            {

                if (location == null)
                    return BadRequest();

                var CreateLocation = await locationRepository.CreateLocation(location);

                return CreatedAtAction(nameof(GetLocations),
                    new { id = CreateLocation.LocationId }, CreateLocation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }


        }

        // for update 


        [HttpPut("UpdateLocation {id:int}")]
        public async Task<ActionResult<Location>> UpdateLocation(int id, Location location)
        {
            try
            {
                if (id != location.LocationId)
                    return BadRequest("Location Id mismatch");

                var existingLocation = await locationRepository.GetLocationById(id);

                if (existingLocation == null)
                    return NotFound($"Location with Id = {id} not found");

                return await locationRepository.UpdateLocation(location);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,

                    "Error updating location record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteLocation(int id)
        {
            try
            {
                
                var locationToDelete = await locationRepository.GetLocationById(id);

                if (locationToDelete == null)
                {
                    return NotFound($"Location with Id = {id} not found");
                }

                
                await locationRepository.DeleteLocation(id);

                return Ok($"Location with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting location record");
            }
        }


    }
}
