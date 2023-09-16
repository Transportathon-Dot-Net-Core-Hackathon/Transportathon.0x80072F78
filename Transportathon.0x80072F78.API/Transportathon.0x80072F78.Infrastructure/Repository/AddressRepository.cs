using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Entities.Offer;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Infrastructure.Database;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Infrastructure.Repository;

public class AddressRepository : AsyncRepository<Address>, IAddressRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IFilter _filter;
    private readonly IHttpContextData _httpContextData;

    public AddressRepository(AppDbContext appDbContext, IFilter filter, IHttpContextData httpContextData) : base(appDbContext, filter)
    {
        _appDbContext = appDbContext;
        _filter = filter;
        _httpContextData = httpContextData;
    }

    public async Task<Address> GetAddressByIdAsync(Guid id)
    {
        Address address = await _appDbContext.Addresses.AsNoTracking()
                                                              .Where(x => x.Id == id && x.UserId == Guid.Parse(_httpContextData.UserId)).
                                                               /*Include(x => x.relatedtable).*/
                                                               FirstOrDefaultAsync();
        if (address == null) return null;
        return address;
    }

    public async Task<List<Address>> GetAllAddressAsync(bool relational = true)
    {
        List<Address> addressList = new();
        IQueryable<Address> query = _appDbContext.Addresses.Where(x=>x.UserId == Guid.Parse(_httpContextData.UserId)).AsNoTracking();
        var entityType = _appDbContext.Model.FindEntityType(typeof(Address));

        if (relational == true)
        {
            addressList = await query/*.Include(x => x.relatedtable)*/
                                       .ToListAsync();
        }
        else
        {
            addressList = await query.ToListAsync();
        }

        return addressList;
    }
}