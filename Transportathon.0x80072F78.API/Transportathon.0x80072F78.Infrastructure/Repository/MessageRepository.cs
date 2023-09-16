using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Infrastructure.Database;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Infrastructure.Repository;

public class MessageRepository : AsyncRepository<Message>, IMessageRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IFilter _filter;

    public MessageRepository(AppDbContext appDbContext, IFilter filter) : base(appDbContext, filter)
    {
        _appDbContext = appDbContext;
        _filter = filter;
    }
}