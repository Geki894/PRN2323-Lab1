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
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course_Tam> _repository;

        public CourseService(IGenericRepository<Course_Tam> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<dynamic>> GetCoursesAsync(string? search, string? sort, int page, int size, string? fields, string? expand)
        {
            var query = _repository.GetQueryable();
            var searchFields = new[] { "Name" };

            return await query.ApplyDynamicQueryAsync(search, searchFields, sort, page, size, fields, expand);
        }

        public async Task<PagedResult<dynamic>> GetCoursesBySemesterIdAsync(int semesterId, string? search, string? sort, int page, int size, string? fields, string? expand)
        {
            var query = _repository.GetQueryable().Where(c => c.SemesterId == semesterId);
            var searchFields = new[] { "Name" };

            return await query.ApplyDynamicQueryAsync(search, searchFields, sort, page, size, fields, expand);
        }

        public async Task<CourseResponseModel?> GetCourseByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return entity.Adapt<CourseResponseModel>();
        }

        public async Task<CourseResponseModel> CreateCourseAsync(CourseRequestModel model)
        {
            var entity = model.Adapt<Course_Tam>();
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return entity.Adapt<CourseResponseModel>();
        }

        public async Task<bool> UpdateCourseAsync(int id, CourseRequestModel model)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            model.Adapt(entity);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
