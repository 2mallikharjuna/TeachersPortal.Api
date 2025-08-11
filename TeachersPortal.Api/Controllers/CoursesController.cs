using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TeachPortal.Application.DTOs.Requests;
using TeachersPortal.Application.Interfaces;
using TeachersPortal.Api.Domain.Entities;

namespace TeachersPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesAppService _courseService;

        public CoursesController(ICoursesAppService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourse courseDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Map CreateCourse DTO to Course entity
            var newCourse = new Course
            {
                CourseName = courseDto.CourseName,
                CourseDescription = courseDto.CourseDescription
            };

            var course = await _courseService.CreateCourseAsync(newCourse);
            return Ok(course);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }
    }
}
