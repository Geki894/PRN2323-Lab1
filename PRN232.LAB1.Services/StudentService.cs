using Mapster;
using PRN232.LAB1.Repositories;
using PRN232.LAB1.Repositories.Models;
using PRN232.LAB1.Services.Helpers;
using PRN232.LAB1.Services.Models;
using System.Threading.Tasks;

namespace PRN232.LAB1.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student_Tam> _repository;

        public StudentService(IGenericRepository<Student_Tam> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<dynamic>> GetStudentsAsync(string? search, string? sort, int page, int size, string? fields, string? expand)
        {
            var query = _repository.GetQueryable();
            var searchFields = new[] { "FullName", "Email" }; // Fields to apply text search

            return await query.ApplyDynamicQueryAsync(search, searchFields, sort, page, size, fields, expand);
        }

        public async Task<StudentResponseModel?> GetStudentByIdAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null) return null;
            return student.Adapt<StudentResponseModel>();
        }

        public async Task<StudentResponseModel> CreateStudentAsync(StudentRequestModel model)
        {
            var entity = model.Adapt<Student_Tam>();
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return entity.Adapt<StudentResponseModel>();
        }

        public async Task<bool> UpdateStudentAsync(int id, StudentRequestModel model)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            model.Adapt(entity); // Update entity fields
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
