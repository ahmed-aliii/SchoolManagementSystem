﻿using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DAL
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        //Task<IEnumerable<Department>> GetAllAsync();
        //Task<Department> GetByIdAsync(int id);
        //Task AddAsync(Department department);
        //Task UpdateAsync(Department department);
        //Task DeleteAsync(int id);
        //Task<bool> ExistsAsync(int id);
    }
}
