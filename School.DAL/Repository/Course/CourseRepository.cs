using Microsoft.EntityFrameworkCore;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DAL
{
    public class CourseRepository : ICourseRepository
    {
        private readonly SchoolDB _context = new SchoolDB();

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.StudentCourses)
                    .ThenInclude(sc => sc.Student) // Include related Student
                .Include(c => c.InstructorCourses)
                    .ThenInclude(ic => ic.Instructor) // Include related Instructor
                .ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.StudentCourses)
                    .ThenInclude(sc => sc.Student)
                .Include(c => c.InstructorCourses)
                    .ThenInclude(ic => ic.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await GetByIdAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Courses.AnyAsync(c => c.Id == id);
        }
        public Course? GetByNameAsync(string name)
        {
            return  _context.Courses
                                 .FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        }

    }
}
