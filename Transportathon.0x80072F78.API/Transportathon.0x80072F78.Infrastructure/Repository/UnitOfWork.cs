using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
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
    private ICompanyRepository _companyRepository;
    private ICommentRepository _commentRepository;
    private IDriverRepository _driverRepository;
    private ITeamRepository _teamRepository;
    private ITeamWorkerRepository _teamWorkerRepository;
    private IVehicleRepository _vehicleRepository;
    private IAddressRepository _addressRepository;
    private ITransportationRequestRepository _transportationRequestRepository;
    private IOfferRepository _offerRepository;

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
    public ICompanyRepository CompanyRepository
    {
        get
        {
            if (_companyRepository == default(ICompanyRepository))
                _companyRepository = _serviceProvider.GetRequiredService<ICompanyRepository>();
            return _companyRepository;
        }

    }
    public ICommentRepository CommentRepository 
    {
        get
        {
            if (_commentRepository == default(ICommentRepository))
                _commentRepository = _serviceProvider.GetRequiredService<ICommentRepository>();
            return _commentRepository;
        }

    }
    public IDriverRepository DriverRepository 
    {
        get
        {
            if (_driverRepository == default(IDriverRepository))
                _driverRepository = _serviceProvider.GetRequiredService<IDriverRepository>();
            return _driverRepository;
        }

    }
    public ITeamRepository TeamRepository 
    {
        get
        {
            if (_teamRepository == default(ITeamRepository))
                _teamRepository = _serviceProvider.GetRequiredService<ITeamRepository>();
            return _teamRepository;
        }

    }
    public ITeamWorkerRepository TeamWorkerRepository 
    {
        get
        {
            if (_teamWorkerRepository == default(ITeamWorkerRepository))
                _teamWorkerRepository = _serviceProvider.GetRequiredService<ITeamWorkerRepository>();
            return _teamWorkerRepository;
        }

    }
    public IVehicleRepository VehicleRepository 
    {
        get
        {
            if (_vehicleRepository == default(IVehicleRepository))
                _vehicleRepository = _serviceProvider.GetRequiredService<IVehicleRepository>();
            return _vehicleRepository;
        }

    }
    public IAddressRepository AddressRepository 
    {
        get
        {
            if (_addressRepository == default(IAddressRepository))
                _addressRepository = _serviceProvider.GetRequiredService<IAddressRepository>();
            return _addressRepository;
        }

    }
    public ITransportationRequestRepository TransportationRequestRepository 
    {
        get
        {
            if (_transportationRequestRepository == default(ITransportationRequestRepository))
                _transportationRequestRepository = _serviceProvider.GetRequiredService<ITransportationRequestRepository>();
            return _transportationRequestRepository;
        }

    }

    public IOfferRepository OfferRepository
    {
        get
        {
            if (_offerRepository == default(IOfferRepository))
                _offerRepository = _serviceProvider.GetRequiredService<IOfferRepository>();
            return _offerRepository;
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