using Microsoft.EntityFrameworkCore;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DAL
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly SchoolDB _context = new SchoolDB();

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            // Include Department and Courses
            return await _context.Instructors
                .Include(i => i.Department)
                .Include(i => i.InstructorCourses)
                .ToListAsync();
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            return await _context.Instructors
                .Include(i => i.Department)
                .Include(i => i.InstructorCourses)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Instructors.AnyAsync(i => i.Id == id);
        }
    }
}
