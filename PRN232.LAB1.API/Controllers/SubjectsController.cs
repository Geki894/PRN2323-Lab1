using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.LAB1.Services;
using PRN232.LAB1.Services.Models;
using PRN232.LAB1.Services.Models.Requests;
using PRN232.LAB1.Services.Models.Responses;
using System.Threading.Tasks;

namespace PRN232.LAB1.API.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubjects(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string? fields = null,
            [FromQuery] string? expand = null)
        {
            var result = await _subjectService.GetSubjectsAsync(search, sort, page, size, fields, expand);
            var response = ApiResponse<dynamic>.SuccessResult(result.Items, "Subjects retrieved successfully", result.Pagination);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<SubjectResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            if (subject == null)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Subject with ID {id} not found"));
            }

            return Ok(ApiResponse<SubjectResponseModel>.SuccessResult(subject, "Subject retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<SubjectResponseModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectRequestModel model)
        {
            var createdSubject = await _subjectService.CreateSubjectAsync(model);
            return CreatedAtAction(nameof(GetSubject), new { id = createdSubject.SubjectId }, 
                ApiResponse<SubjectResponseModel>.SuccessResult(createdSubject, "Subject created successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectRequestModel model)
        {
            var success = await _subjectService.UpdateSubjectAsync(id, model);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Subject with ID {id} not found"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new { }, "Subject updated successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var success = await _subjectService.DeleteSubjectAsync(id);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Subject with ID {id} not found"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new { }, "Subject deleted successfully"));
        }
    }
}
