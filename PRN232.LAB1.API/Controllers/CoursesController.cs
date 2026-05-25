using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.LAB1.Services;
using PRN232.LAB1.Services.Models;
using PRN232.LAB1.Services.Models.Requests;
using PRN232.LAB1.Services.Models.Responses;
using System.Threading.Tasks;

namespace PRN232.LAB1.API.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;

        public CoursesController(ICourseService courseService, IEnrollmentService enrollmentService)
        {
            _courseService = courseService;
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourses(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string? fields = null,
            [FromQuery] string? expand = null)
        {
            var result = await _courseService.GetCoursesAsync(search, sort, page, size, fields, expand);
            var response = ApiResponse<dynamic>.SuccessResult(result.Items, "Courses retrieved successfully", result.Pagination);
            return Ok(response);
        }

        [HttpGet("{courseId}/enrollments")]
        [ProducesResponseType(typeof(ApiResponse<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEnrollmentsByCourse(
            int courseId,
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string? fields = null,
            [FromQuery] string? expand = null)
        {
            var result = await _enrollmentService.GetEnrollmentsByCourseIdAsync(courseId, search, sort, page, size, fields, expand);
            var response = ApiResponse<dynamic>.SuccessResult(result.Items, "Enrollments retrieved successfully", result.Pagination);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CourseResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Course with ID {id} not found"));
            }

            return Ok(ApiResponse<CourseResponseModel>.SuccessResult(course, "Course retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CourseResponseModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCourse([FromBody] CourseRequestModel model)
        {
            var createdCourse = await _courseService.CreateCourseAsync(model);
            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.CourseId }, 
                ApiResponse<CourseResponseModel>.SuccessResult(createdCourse, "Course created successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseRequestModel model)
        {
            var success = await _courseService.UpdateCourseAsync(id, model);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Course with ID {id} not found"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new { }, "Course updated successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var success = await _courseService.DeleteCourseAsync(id);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Course with ID {id} not found"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new { }, "Course deleted successfully"));
        }
    }
}
