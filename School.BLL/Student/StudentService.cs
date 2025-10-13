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

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            try
            {
                return await _studentRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while fetching all students. Please try again later.");
            }
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(id);

                if (student == null)
                    throw new Exception($"Student with Id {id} was not found.");

                return student;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while fetching the student. Please try again later.");
            }
        }

        public async Task AddAsync(Student student)
        {
            try
            {
                await _studentRepository.AddAsync(student);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while adding the student. Please try again later.");
            }
        }

        public async Task UpdateAsync(Student student)
        {
            try
            {
                var exists = await _studentRepository.ExistsAsync(student.Id);
                if (!exists)
                    throw new Exception($"Student with Id {student.Id} does not exist.");

                await _studentRepository.UpdateAsync(student);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while updating the student. Please try again later.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var exists = await _studentRepository.ExistsAsync(id);
                if (!exists)
                    throw new Exception($"Student with Id {id} does not exist.");

                await _studentRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while deleting the student. Please try again later.");
            }
        }
    }
}
