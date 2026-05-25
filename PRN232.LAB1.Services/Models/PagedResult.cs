using System.Collections.Generic;

namespace PRN232.LAB1.Services.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public PaginationMetadata Pagination { get; set; } = new PaginationMetadata();
    }
}
