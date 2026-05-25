using Mapster;
using PRN232.LAB1.Repositories;
using PRN232.LAB1.Repositories.Models;
using PRN232.LAB1.Services.Helpers;
using PRN232.LAB1.Services.Models;
using PRN232.LAB1.Services.Models.Requests;
using PRN232.LAB1.Services.Models.Responses;
using System.Threading.Tasks;

namespace PRN232.LAB1.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IGenericRepository<Subject_Tam> _repository;

        public SubjectService(IGenericRepository<Subject_Tam> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<dynamic>> GetSubjectsAsync(string? search, string? sort, int page, int size, string? fields, string? expand)
        {
            var query = _repository.GetQueryable();
            var searchFields = new[] { "Name", "Description" };

            return await query.ApplyDynamicQueryAsync(search, searchFields, sort, page, size, fields, expand);
        }

        public async Task<SubjectResponseModel?> GetSubjectByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return entity.Adapt<SubjectResponseModel>();
        }

        public async Task<SubjectResponseModel> CreateSubjectAsync(SubjectRequestModel model)
        {
            var entity = model.Adapt<Subject_Tam>();
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return entity.Adapt<SubjectResponseModel>();
        }

        public async Task<bool> UpdateSubjectAsync(int id, SubjectRequestModel model)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            model.Adapt(entity);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSubjectAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
