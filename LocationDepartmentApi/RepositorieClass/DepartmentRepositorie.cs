using LocationDepartmentApi.Data;
using LocationDepartmentApi.Models;
using LocationDepartmentApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationDepartmentApi.RepositorieClass
{
    public class DepartmentRepositorie : IDepartmentRepositorie
    {

        private readonly AppDbContext appDbContext;

        public DepartmentRepositorie(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // for Get Location
        public async Task<IEnumerable<Department>> GetDepartment()
        {
            return await appDbContext.Departments.ToListAsync();
        }

        // for Get by Id

        public async Task<Department> GetDepartmentById(int id)
        {
            return await appDbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);

        }


        // for Create a Location

        public async Task<Department> CreateDepartment(Department department)
        {
            if (department != null)
            {
                appDbContext.Entry(department).State = EntityState.Unchanged;
            }

            var store = await appDbContext.AddAsync(department);
            await appDbContext.SaveChangesAsync();
            return store.Entity;

        }

        // for update Location
        public async Task<Department> UpdateDepartment(Department department)
        {
            var update = await appDbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == department.DepartmentId);
            if (update != null)
            {
                update.LocationId = department.LocationId;
                update.DepartmentName = department.DepartmentName;
                update.DepartmentDescription = department.DepartmentDescription;
                update.PhoneNumber = department.PhoneNumber;
                update.Email = department.Email;
                update.CreatedAt = department.CreatedAt;
                
               

               

                await appDbContext.SaveChangesAsync();
                return update;
            }
            return null;

        }

        public async Task<Department> DeleteDepartment(int departmentid)
        {
            var del = await appDbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == departmentid);

            if (del != null)
            {
                appDbContext.Departments.Remove(del);
                await appDbContext.SaveChangesAsync();
            }
            return null;

        }



    }
}








