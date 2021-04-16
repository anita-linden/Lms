using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Core.Repositories
{
    public interface IModuleRepository
    {
        public Task<IEnumerable<Module>> GetAllModules();
        public Task<Module> GetModule(int? Id);
        public Task<Module> GetModule(string? title);
        public Task<bool> SaveAsync();
        public Task AddAsync<T>(T added);
        public void Remove(Module deleted);
    }
}
