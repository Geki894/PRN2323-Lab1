using PRN232.LAB1.Services.Models;
using PRN232.LAB1.Services.Models.Requests;
using PRN232.LAB1.Services.Models.Responses;
using System.Threading.Tasks;

namespace PRN232.LAB1.Services
{
    public interface IEnrollmentService
    {
        Task<PagedResult<dynamic>> GetEnrollmentsAsync(string? search, string? sort, int page, int size, string? fields, string? expand);
        Task<PagedResult<dynamic>> GetEnrollmentsByCourseIdAsync(int courseId, string? search, string? sort, int page, int size, string? fields, string? expand);
        Task<EnrollmentResponseModel?> GetEnrollmentByIdAsync(int id);
        Task<EnrollmentResponseModel> CreateEnrollmentAsync(EnrollmentRequestModel model);
        Task<bool> UpdateEnrollmentAsync(int id, EnrollmentRequestModel model);
        Task<bool> DeleteEnrollmentAsync(int id);
    }
}
