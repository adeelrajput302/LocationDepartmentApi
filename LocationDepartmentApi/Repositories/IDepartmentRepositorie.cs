using LocationDepartmentApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocationDepartmentApi.Repositories
{
    public interface IDepartmentRepositorie
    {
        Task<IEnumerable<Department>> GetDepartment();
        Task<Department> GetDepartmentById(int id);
        Task<Department> CreateDepartment(Department department);
        Task<Department> UpdateDepartment(Department department);
        Task<Department> DeleteDepartment(int departmentid);





    }
}
