using PRN232.LAB1.Services.Models;
using System.Threading.Tasks;

namespace PRN232.LAB1.Services
{
    public interface IStudentService
    {
        Task<PagedResult<dynamic>> GetStudentsAsync(string? search, string? sort, int page, int size, string? fields, string? expand);
        Task<StudentResponseModel?> GetStudentByIdAsync(int id);
        Task<StudentResponseModel> CreateStudentAsync(StudentRequestModel model);
        Task<bool> UpdateStudentAsync(int id, StudentRequestModel model);
        Task<bool> DeleteStudentAsync(int id);
    }
}
