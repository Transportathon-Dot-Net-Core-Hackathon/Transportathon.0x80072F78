using Microsoft.EntityFrameworkCore.Storage;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Core.Repository;

public interface IUnitOfWork : IDisposable
{
    void Save();
    void SaveWithoutValidation();
    Task<int> SaveAsync();
    Task<int> SaveAsyncWithoutValidation();

    #region Repositories

    IAsyncRepository<UserRefreshToken> UserRefreshTokenRepository { get; }

    #endregion

    #region Transaction

    IDbContextTransaction Transaction { get; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();

    #endregion
}