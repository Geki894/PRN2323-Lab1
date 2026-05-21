using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using PRN232.LAB1.Services.Models;

namespace PRN232.LAB1.Services.Helpers
{
    public static class QueryHelper
    {
        public static async Task<PRN232.LAB1.Services.Models.PagedResult<dynamic>> ApplyDynamicQueryAsync<T>(
            this IQueryable<T> query,
            string? search,
            string[] searchFields,
            string? sort,
            int page,
            int size,
            string? fields,
            string? expand) where T : class
        {
            // 1. Expansion
            if (!string.IsNullOrWhiteSpace(expand))
            {
                var includes = expand.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var include in includes)
                {
                    // EF Core Include expects exact navigation property name. 
                    // Usually Title case, so we might need to capitalize the first letter.
                    var propName = char.ToUpper(include.Trim()[0]) + include.Trim().Substring(1);
                    query = query.Include(propName);
                }
            }

            // 2. Searching
            if (!string.IsNullOrWhiteSpace(search) && searchFields != null && searchFields.Length > 0)
            {
                var conditions = string.Join(" || ", searchFields.Select(f => $"{f}.Contains(@0)"));
                query = query.Where(conditions, search);
            }

            // 3. Sorting (e.g. "fullName,-dateOfBirth")
            if (!string.IsNullOrWhiteSpace(sort))
            {
                var sortParams = sort.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s =>
                {
                    s = s.Trim();
                    if (s.StartsWith("-")) return s.Substring(1) + " descending";
                    return s + " ascending";
                });
                var orderByStr = string.Join(", ", sortParams);
                query = query.OrderBy(orderByStr);
            }

            // 4. Paging
            var totalItems = await query.CountAsync();
            var pagedQuery = query.Skip((page - 1) * size).Take(size);

            // 5. Selection (e.g. "studentId,fullName,email")
            IQueryable resultQuery;
            if (!string.IsNullOrWhiteSpace(fields))
            {
                var selectFields = string.Join(", ", fields.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(f => f.Trim()));
                resultQuery = pagedQuery.Select($"new({selectFields})");
            }
            else
            {
                resultQuery = pagedQuery;
            }

            var items = await resultQuery.ToDynamicListAsync();

            return new PRN232.LAB1.Services.Models.PagedResult<dynamic>
            {
                Items = items,
                Pagination = new PaginationMetadata
                {
                    Page = page,
                    PageSize = size,
                    TotalItems = totalItems
                }
            };
        }
    }
}
