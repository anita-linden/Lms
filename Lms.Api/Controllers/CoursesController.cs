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
using Microsoft.AspNetCore.JsonPatch;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IUoW _uow;
        private readonly IMapper _mapper;

        public CoursesController(IUoW uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse(bool getModules = true)
        {
            return Ok(_mapper.Map<CourseDto>(await _uow.CourseRepository.GetAllCourses(getModules)));
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var courseDto = _mapper.Map<CourseDto>(await _uow.CourseRepository.GetCourse(id));

            if (courseDto == null)
            {
                //todo put the logic here so you can return notfound
                return NotFound();
            }

            return Ok(courseDto);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _uow.ModifyState<Course>(course);

            try
            {
                await _uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            await _uow.CourseRepository.AddAsync(course);
            if(await _uow.CourseRepository.SaveAsync() == false)
            {
                return StatusCode(500);
            }

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _uow.CourseRepository.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            _uow.CourseRepository.Remove(course);

            if (await _uow.CourseRepository.SaveAsync() == false)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpPatch("{courseId}")]
        public async Task<ActionResult<CourseDto>> PatchCourse(int courseId, JsonPatchDocument<CourseDto> patchDocument)
        {
            if (CourseExists(courseId)) return NotFound();

            var course = _uow.CourseRepository.GetCourse(courseId).Result;
            var model = _mapper.Map<CourseDto>(course);

            patchDocument.ApplyTo(model, ModelState);

            if (model.Title != course.Title)
            {
                return BadRequest();
            }

            _mapper.Map(model, course);

            if (await _uow.CourseRepository.SaveAsync())
            {
                return Ok(_mapper.Map<CourseDto>(course));
            }
            else return StatusCode(500);
        }

        private bool CourseExists(int id)
        {
            return (_uow.CourseRepository.GetCourse(id) is not null);
        }
    }
}
