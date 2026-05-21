using Mapster;
using PRN232.LAB1.Repositories;
using PRN232.LAB1.Repositories.Models;
using PRN232.LAB1.Services.Helpers;
using PRN232.LAB1.Services.Models;
using System.Threading.Tasks;

namespace PRN232.LAB1.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IGenericRepository<Enrollment_Tam> _repository;

        public EnrollmentService(IGenericRepository<Enrollment_Tam> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<dynamic>> GetEnrollmentsAsync(string? search, string? sort, int page, int size, string? fields, string? expand)
        {
            var query = _repository.GetQueryable();
            var searchFields = new[] { "Status" };

            return await query.ApplyDynamicQueryAsync(search, searchFields, sort, page, size, fields, expand);
        }
    }
}
