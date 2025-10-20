using School.DAL;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL
{
    public class DepartmentService : GenericService<Department>, IDepartmentService
    {
      
        private readonly IGenericRepository<Department> _departmentRepository;

        public DepartmentService(IGenericRepository<Department> repository) : base(repository)
        {
        }

        //public async Task<IEnumerable<Department>> GetAllAsync()
        //{
        //    try
        //    {
        //        return await _departmentRepository.GetAllAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("An error occurred while fetching all departments.");
        //    }
        //}

        //public async Task<Department> GetByIdAsync(int id)
        //{
        //    try
        //    {
        //        var dept = await _departmentRepository.GetByIdAsync(id);
        //        if (dept == null)
        //            throw new Exception($"Department with Id {id} not found.");
        //        return dept;
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("An error occurred while fetching the department.");
        //    }
        //}

        //public async Task AddAsync(Department department)
        //{
        //    try
        //    {
        //        await _departmentRepository.AddAsync(department);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("An error occurred while adding the department.");
        //    }
        //}

        //public async Task UpdateAsync(Department department)
        //{
        //    try
        //    {
        //        var exists = await _departmentRepository.ExistsAsync(department.Id);
        //        if (!exists)
        //            throw new Exception($"Department with Id {department.Id} does not exist.");

        //        await _departmentRepository.UpdateAsync(department);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("An error occurred while updating the department.");
        //    }
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    try
        //    {
        //        var exists = await _departmentRepository.ExistsAsync(id);
        //        if (!exists)
        //            throw new Exception($"Department with Id {id} does not exist.");

        //        await _departmentRepository.DeleteAsync(id);
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("An error occurred while deleting the department.");
        //    }
        //}
    }
}
