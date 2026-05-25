using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.LAB1.Services;
using PRN232.LAB1.Services.Models;
using PRN232.LAB1.Services.Models.Requests;
using PRN232.LAB1.Services.Models.Responses;
using System.Threading.Tasks;

namespace PRN232.LAB1.API.Controllers
{
    [Route("api/semesters")]
    [ApiController]
    public class SemestersController : ControllerBase
    {
        private readonly ISemesterService _semesterService;
        private readonly ICourseService _courseService;

        public SemestersController(ISemesterService semesterService, ICourseService courseService)
        {
            _semesterService = semesterService;
            _courseService = courseService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSemesters(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string? fields = null,
            [FromQuery] string? expand = null)
        {
            var result = await _semesterService.GetSemestersAsync(search, sort, page, size, fields, expand);
            var response = ApiResponse<dynamic>.SuccessResult(result.Items, "Semesters retrieved successfully", result.Pagination);
            return Ok(response);
        }

        [HttpGet("{semesterId}/courses")]
        [ProducesResponseType(typeof(ApiResponse<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCoursesBySemester(
            int semesterId,
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string? fields = null,
            [FromQuery] string? expand = null)
        {
            var result = await _courseService.GetCoursesBySemesterIdAsync(semesterId, search, sort, page, size, fields, expand);
            var response = ApiResponse<dynamic>.SuccessResult(result.Items, "Courses retrieved successfully", result.Pagination);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<SemesterResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSemester(int id)
        {
            var semester = await _semesterService.GetSemesterByIdAsync(id);
            if (semester == null)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Semester with ID {id} not found"));
            }

            return Ok(ApiResponse<SemesterResponseModel>.SuccessResult(semester, "Semester retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<SemesterResponseModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSemester([FromBody] SemesterRequestModel model)
        {
            var createdSemester = await _semesterService.CreateSemesterAsync(model);
            return CreatedAtAction(nameof(GetSemester), new { id = createdSemester.SemesterId }, 
                ApiResponse<SemesterResponseModel>.SuccessResult(createdSemester, "Semester created successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSemester(int id, [FromBody] SemesterRequestModel model)
        {
            var success = await _semesterService.UpdateSemesterAsync(id, model);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Semester with ID {id} not found"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new { }, "Semester updated successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSemester(int id)
        {
            var success = await _semesterService.DeleteSemesterAsync(id);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Semester with ID {id} not found"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new { }, "Semester deleted successfully"));
        }
    }
}
