using PRN232.LAB1.Services.Models;
using PRN232.LAB1.Services.Models.Requests;
using PRN232.LAB1.Services.Models.Responses;
using System.Threading.Tasks;

namespace PRN232.LAB1.Services
{
    public interface ISemesterService
    {
        Task<PagedResult<dynamic>> GetSemestersAsync(string? search, string? sort, int page, int size, string? fields, string? expand);
        Task<SemesterResponseModel?> GetSemesterByIdAsync(int id);
        Task<SemesterResponseModel> CreateSemesterAsync(SemesterRequestModel model);
        Task<bool> UpdateSemesterAsync(int id, SemesterRequestModel model);
        Task<bool> DeleteSemesterAsync(int id);
    }
}
