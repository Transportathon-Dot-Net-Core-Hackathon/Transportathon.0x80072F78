using Microsoft.AspNetCore.Identity;
using Transportathon._0x80072F78.API.Extensions;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Infrastructure.FilterUtils;
using Transportathon._0x80072F78.Core.Interfaces;
using Transportathon._0x80072F78.Core.Mapping;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Infrastructure.Database;
using Transportathon._0x80072F78.Infrastructure.Repository;
using Transportathon._0x80072F78.Infrastructure;
using Transportathon._0x80072F78.Shared.Interfaces;
using Transportathon._0x80072F78.Shared.Models;
using Transportathon._0x80072F78.Shared.Extensions;
using Transportathon._0x80072F78.Services.Identity;
using Transportathon._0x80072F78.Shared.Constants;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHttpContextData, HttpContextData>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
builder.Services.AddScoped<IFilter, Filter>();
builder.Services.AddIdentity<AspNetUser, AspNetRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCustomTokenAuth(builder.Configuration);
builder.Services.AddScoped<ICompanyRepository,CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamWorkerRepository, TeamWorkerRepository>();
builder.Services.AddScoped<ITeamWorkerService, TeamWorkerService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ITransportationRequestRepository, TransportationRequestRepository>();
builder.Services.AddScoped<ITransportationRequestService, TransportationRequestService>();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetSection(ConfigurationSectionConst.ConnectionStrings)[ConfigurationEntityConst.AppDbConnection]!, "Postgres", tags: new[] { "readiness" });

builder.Services.AddExConfigOptions(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();