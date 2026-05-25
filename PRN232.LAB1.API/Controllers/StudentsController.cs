using Microsoft.AspNetCore.Mvc;
using PRN232.LAB1.Services;
using PRN232.LAB1.Services.Models;
using System.Threading.Tasks;

namespace PRN232.LAB1.API.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET api/students?search=nguyen&sort=fullName,-dateOfBirth&page=2&size=10&fields=studentId,fullName&expand=enrollments
        [HttpGet]
        public async Task<IActionResult> GetStudents(
            [FromQuery] string? search,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string? fields = null,
            [FromQuery] string? expand = null)
        {
            var result = await _studentService.GetStudentsAsync(search, sort, page, size, fields, expand);
            var response = ApiResponse<dynamic>.SuccessResult(result.Items, "Students retrieved successfully", result.Pagination);
            return Ok(response);
        }

        // GET api/students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Student with ID {id} not found"));
            }

            return Ok(ApiResponse<StudentResponseModel>.SuccessResult(student, "Student retrieved successfully"));
        }

        // POST api/students
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentRequestModel model)
        {
            var createdStudent = await _studentService.CreateStudentAsync(model);
            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.StudentId }, 
                ApiResponse<StudentResponseModel>.SuccessResult(createdStudent, "Student created successfully"));
        }

        // PUT api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentRequestModel model)
        {
            var success = await _studentService.UpdateStudentAsync(id, model);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Student with ID {id} not found"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new { }, "Student updated successfully"));
        }

        // DELETE api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var success = await _studentService.DeleteStudentAsync(id);
            if (!success)
            {
                return NotFound(ApiResponse<object>.ErrorResult($"Student with ID {id} not found"));
            }

            return Ok(ApiResponse<object>.SuccessResult(new { }, "Student deleted successfully"));
        }
    }
}
