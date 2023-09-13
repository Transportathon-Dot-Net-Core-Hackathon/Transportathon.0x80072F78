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

public class AddressRepository : AsyncRepository<Address>, IAddressRepository
{
    private readonly DbContext _dbContext;
    private readonly IFilter _filter;

    public AddressRepository(AppDbContext dbContext, IFilter filter) : base(dbContext, filter)
    {
        _dbContext = dbContext;
        _filter = filter;
    }
}