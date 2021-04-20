using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Data.Data;
using Lms.Core.Entities;
using Lms.Core.Repositories;
using AutoMapper;
using Lms.Core.Dto;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IUoW _uow;
        private readonly IMapper _mapper;

        public ModulesController(IUoW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            var modules = await _uow.ModuleRepository.GetAllModules();
            var modulesDto = _mapper.Map<CourseDto[]>(modules);

            return Ok(modulesDto);
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
            var @moduleDto = _mapper.Map<ModuleDto>(await _uow.ModuleRepository.GetModule(id));
            
            if (@moduleDto == null)
            {
                return NotFound();
            }

            return Ok(moduleDto);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module @module)
        {
            if (id != @module.Id)
            {
                return BadRequest();
            }

            _uow.ModifyState<Module>(@module);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(Module @module)
        {
            await _uow.ModuleRepository.AddAsync(@module);
            
            if(await _uow.ModuleRepository.SaveAsync() == false)
            {
                return StatusCode(500);
            }

            return CreatedAtAction("GetModule", new { id = @module.Id }, @module);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var @module = await _uow.ModuleRepository.GetModule(id);
            if (@module == null)
            {
                return NotFound();
            }

            _uow.ModuleRepository.Remove(@module);
            if(await _uow.ModuleRepository.SaveAsync() == false)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return (_uow.ModuleRepository.GetModule(id) is not null);
        }
    }
}
