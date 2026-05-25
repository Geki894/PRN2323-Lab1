using PRN232.LAB1.Services.Models;
using PRN232.LAB1.Services.Models.Requests;
using PRN232.LAB1.Services.Models.Responses;
using System.Threading.Tasks;

namespace PRN232.LAB1.Services
{
    public interface ICourseService
    {
        Task<PagedResult<dynamic>> GetCoursesAsync(string? search, string? sort, int page, int size, string? fields, string? expand);
        Task<PagedResult<dynamic>> GetCoursesBySemesterIdAsync(int semesterId, string? search, string? sort, int page, int size, string? fields, string? expand);
        Task<CourseResponseModel?> GetCourseByIdAsync(int id);
        Task<CourseResponseModel> CreateCourseAsync(CourseRequestModel model);
        Task<bool> UpdateCourseAsync(int id, CourseRequestModel model);
        Task<bool> DeleteCourseAsync(int id);
    }
}
