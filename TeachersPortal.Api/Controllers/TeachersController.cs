using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TeachersPortal.Application.Interfaces;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Application.DTOs.Response;

namespace TeachersPortal.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersAppService _teachersAppService;
        private readonly ICoursesAppService _coursesAppService;

        public TeachersController(ITeachersAppService teachersAppService, ICoursesAppService coursesAppService)
        {
            _teachersAppService = teachersAppService ?? throw new ArgumentNullException(nameof(teachersAppService));
            _coursesAppService = coursesAppService ?? throw new ArgumentNullException(nameof(coursesAppService));
        }
        
        [HttpGet]
        public IActionResult GetAllteachers()
        {
            try
            {
                var students = _teachersAppService.GetAllTeachers();
                return Ok(students);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }


        [HttpGet("{Id}")]
        public IActionResult GetTeacherById(int Id)
        {
            try
            {
                var teacherBl = _teachersAppService.GetTeacherById(Id);
                return Ok(teacherBl);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }

        [HttpGet("of-student/{studentId}")]
        public async Task<IActionResult> GetTeachersOfStudent(int studentId)
        {
            try
            {
                if (studentId <= 0)
                {
                    return BadRequest("Invalid student ID.");
                }
                return Ok(await _teachersAppService.GetTeachersOfStudent(studentId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }

        [HttpGet("{teacherId}/courses")]
        public async Task<IActionResult> GetCourcesOfTeacher(int teacherId)
        {
            try
            {
                if (teacherId <= 0)
                {
                    return BadRequest("Invalid Teacher ID.");
                }
                return Ok(await _coursesAppService.GetAllCourcesOfTeacher(teacherId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }

        // Creates a new teacher.        
        /// <param name="teacher">The teacher DTO to create.</param>
        /// <returns>Action result with the created teacher.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(TeacherDto teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher data is required.");
            }

            // Map TeacherDto to Teacher entity
            var teacherEntity = new Teacher
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                Gender = teacher.Gender,
                DateOfBirth = teacher.DateOfBirth,
                Address = teacher.Address                
            };

            var createdTeacher = await _teachersAppService.CreateTeacher(teacherEntity);
            return CreatedAtAction(nameof(GetTeacherById), new { Id = createdTeacher.Id }, createdTeacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Teacher teacher)
        {
            if (id != teacher.Id) return BadRequest();
            await _teachersAppService.UpdateTeacher(teacher);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                var IsTeacherDeleted = _teachersAppService.DeleteTeacher(Id);
                return Ok(IsTeacherDeleted);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error");
            }
        }
    }
}
