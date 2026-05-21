using PRN232.LAB1.Services.Models;
using System.Threading.Tasks;

namespace PRN232.LAB1.Services
{
    public interface IEnrollmentService
    {
        Task<PagedResult<dynamic>> GetEnrollmentsAsync(string? search, string? sort, int page, int size, string? fields, string? expand);
    }
}
