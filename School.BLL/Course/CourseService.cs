using School.DAL;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.BLL
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository = new CourseRepository();

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            try
            {
                return await _courseRepository.GetAllAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while fetching all courses.");
            }
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(id);
                if (course == null)
                    throw new Exception($"Course with Id {id} not found.");

                return course;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while fetching the course.");
            }
        }

        public async Task AddAsync(Course course)
        {
            try
            {
                await _courseRepository.AddAsync(course);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while adding the course.");
            }
        }

        public async Task UpdateAsync(Course course)
        {
            try
            {
                var exists = await _courseRepository.ExistsAsync(course.Id);
                if (!exists)
                    throw new Exception($"Course with Id {course.Id} does not exist.");

                await _courseRepository.UpdateAsync(course);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while updating the course.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var exists = await _courseRepository.ExistsAsync(id);
                if (!exists)
                    throw new Exception($"Course with Id {id} does not exist.");

                await _courseRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while deleting the course.");
            }
        }
        public Course? GetByNameAsync(string name)
        {
            try
            {
                var course = _courseRepository.GetByNameAsync(name);

                return course;
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while fetching the course by name.");
            }
        }

    }
}
