using Microsoft.EntityFrameworkCore.Metadata;

namespace Transportathon._0x80072F78.Shared.Interfaces;

public interface IFilter
{
    IQueryable<T> PrepareFilterQuery<T>(IEntityType entityType, IQueryable<T> query,
        string filter = "", string search = "", string sort = "") where T : class;
}