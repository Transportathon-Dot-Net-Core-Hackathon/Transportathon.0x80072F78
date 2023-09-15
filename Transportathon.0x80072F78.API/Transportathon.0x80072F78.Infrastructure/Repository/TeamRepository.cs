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

public class TeamRepository : AsyncRepository<Team>, ITeamRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IFilter _filter;

    public TeamRepository(AppDbContext appDbContext, IFilter filter) : base(appDbContext, filter)
    {
        _appDbContext = appDbContext;
        _filter = filter;
    }

    public async Task<List<Team>> GetAllTeamAsync(bool relational = true)
    {
        List<Team> teamList = new();
        IQueryable<Team> query = _appDbContext.Teams.AsNoTracking();
        var entityType = _appDbContext.Model.FindEntityType(typeof(Team));

        if (relational == true)
        {
            teamList = await query.Include(x => x.Company).ToListAsync();
        }
        else
        {
            teamList = await query.ToListAsync();
        }

        return teamList;
    }

    public async Task<Team> GetTeamByIdAsync(Guid id)
    {
        Team team = await _appDbContext.Teams.AsNoTracking()
                                                              .Where(x => x.Id == id).
                                                               Include(x => x.Company).
                                                               FirstOrDefaultAsync();
        if (team == null) return null;
        return team;
    }
}