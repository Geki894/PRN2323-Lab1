using PRN232.LAB1.Services.Models;
using PRN232.LAB1.Services.Models.Requests;
using PRN232.LAB1.Services.Models.Responses;
using System.Threading.Tasks;

namespace PRN232.LAB1.Services
{
    public interface ISubjectService
    {
        Task<PagedResult<dynamic>> GetSubjectsAsync(string? search, string? sort, int page, int size, string? fields, string? expand);
        Task<SubjectResponseModel?> GetSubjectByIdAsync(int id);
        Task<SubjectResponseModel> CreateSubjectAsync(SubjectRequestModel model);
        Task<bool> UpdateSubjectAsync(int id, SubjectRequestModel model);
        Task<bool> DeleteSubjectAsync(int id);
    }
}
