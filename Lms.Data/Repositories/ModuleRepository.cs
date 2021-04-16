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
    class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _context;

        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T added)
        {
            await _context.AddAsync(added);
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await _context.Module.ToListAsync();
        }

        public async Task<Module> GetModule(int? Id)
        {
            return await _context.Module.FindAsync(Id);
        }

        public async Task<Module> GetModule(string? title)
        {
            return await _context.Module.Where(m => m.Title.Equals(title)).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) >= 0;
        }

        public void Remove(Module deleted)
        {
            _context.Module.Remove(deleted);
        }
    }
}
