using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.LAB1.Services;
using PRN232.LAB1.Services.Models;
using PRN232.LAB1.Services.Models.Requests;
using PRN232.LAB1.Services.Models.Responses;
using System.Threading.Tasks;

namespace PRN232.LAB1.API.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnrollments(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string? fields = null,
            [FromQuery] string? expand = null)
        {
            var result = await _enrollmentService.GetEnrollmentsAsync(search, sort, page, size, fields, expand);
            var response = ApiResponse<dynamic>.SuccessResult(result.Items, "Enrollments retrieved successfully", result.Pagination);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<EnrollmentResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEnrollment(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Enrollment with ID {id} not found"));
            }

            return Ok(ApiResponse<EnrollmentResponseModel>.SuccessResult(enrollment, "Enrollment retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<EnrollmentResponseModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEnrollment([FromBody] EnrollmentRequestModel model)
        {
            var createdEnrollment = await _enrollmentService.CreateEnrollmentAsync(model);
            return CreatedAtAction(nameof(GetEnrollment), new { id = createdEnrollment.EnrollmentId }, 
                ApiResponse<EnrollmentResponseModel>.SuccessResult(createdEnrollment, "Enrollment created successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEnrollment(int id, [FromBody] EnrollmentRequestModel model)
        {
            var success = await _enrollmentService.UpdateEnrollmentAsync(id, model);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Enrollment with ID {id} not found"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new { }, "Enrollment updated successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var success = await _enrollmentService.DeleteEnrollmentAsync(id);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Enrollment with ID {id} not found"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new { }, "Enrollment deleted successfully"));
        }
    }
}
