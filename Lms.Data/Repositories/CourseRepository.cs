using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lms.Core.Entities;
using Lms.Core.Repositories;
using Lms.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T added)
        {
            await _context.AddAsync(added);
        }

        public async Task<IEnumerable<Course>> GetAllCourses(bool includeModels = true)
        {
            return includeModels ? await _context.Course.Include(course => course.Modules).ToListAsync()
                :await _context.Course.ToListAsync();
        }

        public async Task<Course> GetCourse(int? Id)
        {
            return await _context.Course.Include(course => course.Modules).FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) >= 0;
        }

        public void Remove(Course deleted)
        {
            _context.Course.Remove(deleted);
        }
    }
}
