using School.DAL;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository = new StudentRepository();

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var result = await _studentRepository.GetAllAsync();
            
            if(result == null)
            {
                new List<Student>();
            }

            return result;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var result = await _studentRepository.GetByIdAsync(id);

            return result;
        }
    }
}
