using School.DAL;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL
{ 
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository = new InstructorRepository();

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            try
            {
                return await _instructorRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while fetching all instructors. Please try again later.");
            }
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            try
            {
                var instructor = await _instructorRepository.GetByIdAsync(id);

                if (instructor == null)
                    throw new Exception($"Instructor with Id {id} was not found.");

                return instructor;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while fetching the instructor. Please try again later.");
            }
        }

        public async Task AddAsync(Instructor instructor)
        {
            try
            {
                await _instructorRepository.AddAsync(instructor);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while adding the instructor. Please try again later.");
            }
        }

        public async Task UpdateAsync(Instructor instructor)
        {
            try
            {
                var exists = await _instructorRepository.ExistsAsync(instructor.Id);
                if (!exists)
                    throw new Exception($"Instructor with Id {instructor.Id} does not exist.");

                await _instructorRepository.UpdateAsync(instructor);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while updating the instructor. Please try again later.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var exists = await _instructorRepository.ExistsAsync(id);
                if (!exists)
                    throw new Exception($"Instructor with Id {id} does not exist.");

                await _instructorRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while deleting the instructor. Please try again later.");
            }
        }
    }

}
