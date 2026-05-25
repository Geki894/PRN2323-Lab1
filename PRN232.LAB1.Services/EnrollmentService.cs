using Mapster;
using PRN232.LAB1.Repositories;
using PRN232.LAB1.Repositories.Models;
using PRN232.LAB1.Services.Helpers;
using PRN232.LAB1.Services.Models;
using PRN232.LAB1.Services.Models.Requests;
using PRN232.LAB1.Services.Models.Responses;
using System.Linq;
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

        public async Task<PagedResult<dynamic>> GetEnrollmentsByCourseIdAsync(int courseId, string? search, string? sort, int page, int size, string? fields, string? expand)
        {
            var query = _repository.GetQueryable().Where(e => e.CourseId == courseId);
            var searchFields = new[] { "Status" };

            return await query.ApplyDynamicQueryAsync(search, searchFields, sort, page, size, fields, expand);
        }

        public async Task<EnrollmentResponseModel?> GetEnrollmentByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return entity.Adapt<EnrollmentResponseModel>();
        }

        public async Task<EnrollmentResponseModel> CreateEnrollmentAsync(EnrollmentRequestModel model)
        {
            var entity = model.Adapt<Enrollment_Tam>();
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return entity.Adapt<EnrollmentResponseModel>();
        }

        public async Task<bool> UpdateEnrollmentAsync(int id, EnrollmentRequestModel model)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            model.Adapt(entity);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEnrollmentAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
