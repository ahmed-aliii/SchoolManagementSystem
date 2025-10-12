using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DAL
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDB _context = new SchoolDB();
        public StudentRepository() { }

        public async Task<List<Student>> GetAllAsync()
        {
            return _context.Students.ToList();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return _context.Students.FirstOrDefault(std => std.Id == id);
        }
    }
}
