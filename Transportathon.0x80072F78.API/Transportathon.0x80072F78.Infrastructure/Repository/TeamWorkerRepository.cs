using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Infrastructure.Database;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Infrastructure.Repository;

public class TeamWorkerRepository : AsyncRepository<TeamWorker>, ITeamWorkerRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IFilter _filter;
    private readonly IHttpContextData _httpContextData;

    public TeamWorkerRepository(AppDbContext appDbContext, IFilter filter, IHttpContextData httpContextData) : base(appDbContext, filter)
    {
        _appDbContext = appDbContext;
        _filter = filter;
        _httpContextData = httpContextData;
    }

    public async Task<List<TeamWorker>> GetAllTeamWorkerAsync(bool relational = true)
    {
        List<TeamWorker> teamWorkerList = new();
        IQueryable<TeamWorker> query = _appDbContext.TeamWorkers.Where(x => x.UserId == Guid.Parse(_httpContextData.UserId)).AsNoTracking();
        var entityType = _appDbContext.Model.FindEntityType(typeof(TeamWorker));

        if (relational == true)
        {
            teamWorkerList = await query.ToListAsync();
        }
        else
        {
            teamWorkerList = await query.ToListAsync();
        }

        return teamWorkerList;
    }

    public async Task<TeamWorker> GetTeamWorkerByIdAsync(Guid id)
    {
        TeamWorker teamWorker = await _appDbContext.TeamWorkers.AsNoTracking()
                                                              .Where(x => x.Id == id && x.UserId == Guid.Parse(_httpContextData.UserId)).
                                                               
                                                               FirstOrDefaultAsync();
        if (teamWorker == null) return null;
        return teamWorker;
    }
}
