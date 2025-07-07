using LocationDepartmentApi.Dtos;
using LocationDepartmentApi.Models;
using LocationDepartmentApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDepartmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DepartmentsController : ControllerBase
    {

        private readonly IDepartmentRepositorie departmentRepositorie;

        public DepartmentsController(IDepartmentRepositorie departmentRepositorie)
        {
            this.departmentRepositorie = departmentRepositorie;
        }

        // Get 

        [HttpGet("GetAllDepartment")]
        public async Task<ActionResult> GetDepartment()
        {
            try
            {
                return Ok(await departmentRepositorie.GetDepartment());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // Full List With Dto 


        [HttpGet("GetDepartmentDtoList")]
        public async Task<IActionResult> GetDepartmentDtoList()
        {
            try
            {
                var departments = await departmentRepositorie.GetDepartment();

                var dtoList = departments.Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    LocationId = d.LocationId,
                    DepartmentDescription = d.DepartmentDescription
                }).ToList();

                return Ok(dtoList);
            }
            catch
            {
                return StatusCode(500, "Something went wrong while getting departments.");
            }
        }


        // Get BY ID

        [HttpGet("GetDepartmentById {id:int}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            try
            {
                var result = await departmentRepositorie.GetDepartmentById(id);

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


        // Dto By Id List


        [HttpGet("GetDepartmentDto/{id:int}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentDto(int id)
        {
            try
            {
                var department = await departmentRepositorie.GetDepartmentById(id);
                if (department == null)
                {
                    return NotFound();
                }

                var departmentDto = new DepartmentDto
                {
                    DepartmentId = department.DepartmentId,
                    LocationId = department.LocationId,
                    DepartmentDescription = department.DepartmentDescription
                };

                return Ok(departmentDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        // Create Location 


        [HttpPost("CreateNewDepartment")]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            try
            {

                if (department == null)
                    return BadRequest();

                var createddepartment = await departmentRepositorie.CreateDepartment(department);

                return CreatedAtAction(nameof(GetDepartment),
                    new { id = createddepartment.DepartmentId }, createddepartment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }


        }


        // update by id

        [HttpPut("UpdateDepartment {id:int}")]
        public async Task<ActionResult<Department>> UpdateDepartment(int id, Department department)
        {
            try
            {
                if (id != department.DepartmentId)
                    return BadRequest("Location Id mismatch");

                var existingdepartment = await departmentRepositorie.GetDepartmentById(id);

                if (existingdepartment == null)
                    return NotFound($"Location with Id = {id} not found");

                return await departmentRepositorie.UpdateDepartment(department);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating location record");
            }
        }
      
        // Delete by id

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            try
            {

                var departmentToDelete = await departmentRepositorie.GetDepartmentById(id);

                if (departmentToDelete == null)
                {
                    return NotFound($"Location with Id = {id} not found");
                }


                await departmentRepositorie.DeleteDepartment(id);

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

