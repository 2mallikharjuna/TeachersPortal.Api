using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Application.DTOs.Requests;
using TeachersPortal.Application.Interfaces;

namespace TeachersPortal.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsAppService _studentsAppService;
        private readonly ICoursesAppService _coursesAppService;

        public StudentsController(IStudentsAppService studentsAppService, ICoursesAppService coursesAppService)
        {
            _studentsAppService = studentsAppService;
            _coursesAppService = coursesAppService;
        }

        // GET: api/Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentsAppService.GetAllStudents();
            return Ok(students);
        }

        // GET: api/Students/{id}
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentsAppService.GetStudentById(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        /// <summary>
        /// Creates a new student.
        /// </summary>
        /// <param name="studentDto">The student data transfer object.</param>
        /// <returns>The created student.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudent studentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdStudent = await _studentsAppService.CreateStudent(studentDto);
            return Ok(createdStudent);
        }

        // PUT: api/Students/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedStudent = _studentsAppService.UpdateStudent(id, student);
            if (updatedStudent == null)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Students/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var isDeleted = _studentsAppService.DeleteStudent(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("students/{studentId}/courses")]
        public async Task<IActionResult> GetAllCourcesOfStudent(int id)
        {
            var student = _studentsAppService.GetStudentById(id);
            if (student == null)
                return NotFound("Student not found.");
            var courses = await _coursesAppService.GetAllCourcesOfStudent(id);
            return Ok(courses);
        }

        [HttpGet("my-students")]
        public async Task<IActionResult> GetStudentsForLoggedInTeacher()
        {
            var teacherId = int.Parse(User.FindFirst("id")!.Value);
            if (teacherId <= 0)
                return BadRequest("Invalid teacher ID.");            
            var students = await _studentsAppService.GetStudentsOfTeacher(teacherId);
            return Ok(students);
        }
    }
}

