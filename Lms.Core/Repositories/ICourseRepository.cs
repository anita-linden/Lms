using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Repositories
{
    public interface ICourseRepository
    {
        public Task<IEnumerable<Course>> GetAllCourses(bool includeModels = true);
        public Task<Course> GetCourse(int? Id);
        public Task<bool> SaveAsync();
        public Task AddAsync<T>(T added);
        public void Remove(Course deleted);
    }
}
