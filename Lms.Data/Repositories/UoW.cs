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
    public class UoW : IUoW
    {
        private readonly ApplicationDbContext _context;

        public ICourseRepository CourseRepository { get; private set; }

        public IModuleRepository ModuleRepository { get; private set; }

        public UoW(ApplicationDbContext context)
        {
            _context = context;
            CourseRepository = new CourseRepository(context);
            ModuleRepository = new ModuleRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void ModifyState<T>(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }
    }
}
