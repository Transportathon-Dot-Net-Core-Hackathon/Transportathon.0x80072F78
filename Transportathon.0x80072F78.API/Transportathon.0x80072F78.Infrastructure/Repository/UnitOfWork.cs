using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Infrastructure.Database;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;
    private readonly IServiceProvider _serviceProvider;
    private IAsyncRepository<UserRefreshToken> _userRefreshTokenRepository;
    public UnitOfWork(IServiceProvider serviceProvider, IAsyncRepository<UserRefreshToken> userRefreshTokenRepository, AppDbContext appDbContext)
    {
        Transaction = null;
        _serviceProvider = serviceProvider;
        _userRefreshTokenRepository = userRefreshTokenRepository;
        _appDbContext = appDbContext;
    }
    public void Dispose()
    {
        _appDbContext.Dispose();
    }

    public void Save()
    {
        _appDbContext.SaveChanges();
    }

    public void SaveWithoutValidation()
    {
        _appDbContext.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }

    public async Task<int> SaveAsyncWithoutValidation()
    {
        return await _appDbContext.SaveChangesAsync();
    }

    #region

    public IAsyncRepository<UserRefreshToken> UserRefreshTokenRepository
    {
        get
        {
            if (_userRefreshTokenRepository == default(IAsyncRepository<UserRefreshToken>))
                _userRefreshTokenRepository =
                    _serviceProvider.GetRequiredService<IAsyncRepository<UserRefreshToken>>();

            return _userRefreshTokenRepository;
        }
    }

    #endregion

    #region Transaction

    public IDbContextTransaction Transaction { get; private set; }

    public async Task BeginTransactionAsync()
    {
        Transaction = await _appDbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (Transaction == null)
            throw new DataException("No transaction is in progress");
        await Transaction.CommitAsync();
        Transaction = null;
    }

    public async Task RollbackAsync()
    {
        if (Transaction == null)
            throw new DataException("No transaction is in progress");
        await Transaction.RollbackAsync();
        Transaction = null;
    }

    #endregion
}