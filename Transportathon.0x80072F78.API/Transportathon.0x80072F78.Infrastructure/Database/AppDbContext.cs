using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Entities.Offer;
using Transportathon._0x80072F78.Core.Enums;
using Transportathon._0x80072F78.Shared.Constants;

namespace Transportathon._0x80072F78.Infrastructure.Database;

public class AppDbContext : IdentityDbContext<AspNetUser, AspNetRole, Guid,
AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetRoleClaim, AspNetUserToken>, IDataProtectionKeyContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamWorker> TeamWorkers { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<TransportationRequest> TransportationRequests { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Message> Messages { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
        SeedDatabase(modelBuilder);
    }

    protected void SeedDatabase(ModelBuilder modelBuilder)
    {
        var hasher = new PasswordHasher<AspNetUser>();

        var userId = SeedDataConst.User_Id;
        var username = SeedDataConst.User_UserName;
        var username_Normalized = SeedDataConst.User_NormalizedUserName;
        var userPassword = SeedDataConst.User_Password;
        var userEmail = SeedDataConst.User_Email;
        var userFirstName = SeedDataConst.User_FirstName;
        var userFamilyName = SeedDataConst.User_FamilyName;

        modelBuilder.Entity<AspNetUser>().HasData(
            new AspNetUser
            {
                Id = Guid.Parse(userId),
                UserName = username,
                NormalizedUserName = username_Normalized,
                PasswordHash = hasher.HashPassword(null, userPassword),
                Email = userEmail,
                FirstName = userFirstName,
                FamilyName = userFamilyName,
            }
        );

        var companyUserId = SeedDataConst.CompanyUser_Id;
        var companyUsername = SeedDataConst.CompanyUser_UserName;
        var companyUsername_Normalized = SeedDataConst.CompanyUser_NormalizedUserName;
        var companyUserPassword = SeedDataConst.CompanyUser_Password;
        var companyUserEmail = SeedDataConst.CompanyUser_Email;
        var companyUserFirstName = SeedDataConst.CompanyUser_FirstName;
        var companyUserFamilyName = SeedDataConst.CompanyUser_FamilyName;

        modelBuilder.Entity<AspNetUser>().HasData(
            new AspNetUser
            {
                Id = Guid.Parse(companyUserId),
                UserName = companyUsername,
                NormalizedUserName = companyUsername_Normalized,
                PasswordHash = hasher.HashPassword(null, companyUserPassword),
                Email = companyUserEmail,
                FirstName = companyUserFirstName,
                FamilyName = companyUserFamilyName,
            }
        );

        var companyId = SeedDataConst.Company_Id;
        var companyCompanyName = SeedDataConst.Company_CompanyName;
        var companyTitle = SeedDataConst.Company_Title;
        var companyOwnerName = SeedDataConst.Company_OwnerName;
        var companyOwnerSurname = SeedDataConst.Company_OwnerSurname;
        var companyCity = SeedDataConst.Company_City;
        var companyDistrict = SeedDataConst.Company_District;
        var companyStreet = SeedDataConst.Company_Street;
        var companyAlley = SeedDataConst.Company_Alley;
        var companyBuildingNumber = SeedDataConst.Company_BuildingNumber;
        var companyApartmentNumber = SeedDataConst.Company_ApartmentNumber;
        var companyPostCode = SeedDataConst.Company_PostCode;
        var companyVKN = SeedDataConst.Company_VKN;
        var companysUserId = SeedDataConst.Company_UserId;

        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                Id = Guid.Parse(companyId),
                CompanyName = companyCompanyName,
                Title = companyTitle,
                Name = companyOwnerName,
                Surname = companyOwnerSurname,
                City = companyCity,
                District = companyDistrict,
                Street = companyStreet,
                Alley = companyAlley,
                BuildingNumber = companyBuildingNumber,
                ApartmentNumber = companyApartmentNumber,
                PostCode = companyPostCode,
                VKN = companyVKN,
                CompanyUsersId = Guid.Parse(companysUserId)
            }
        );

        for (int i = 1; i <= 5; i++)
        {
            var driverId = Guid.NewGuid();
            var driverName = SeedDataConst.Driver_Name + i;
            var driverSurname = SeedDataConst.Driver_Surname +i;
            var driverExperience = (Convert.ToInt32(SeedDataConst.Driver_Experience) + i).ToString();
            var driverPhoneNumber = SeedDataConst.Driver_PhoneNumber;
            var driverEmail = SeedDataConst.Driver_Email;
            var driverAge = Convert.ToInt32(SeedDataConst.Driver_Age) + i;
            List<DrivingLicenseType> drivingLicenceTypes = Enum.GetValues(typeof(DrivingLicenseType)).Cast<DrivingLicenseType>().OrderBy(x => Guid.NewGuid()).Take(new Random().Next(1, Enum.GetValues(typeof(DrivingLicenseType)).Length + 1)).ToList();
            var driverUserId = SeedDataConst.Driver_UserId;

            modelBuilder.Entity<Driver>().HasData(
            new Driver
            {
                Id = driverId,
                Name = driverName,
                Surname = driverSurname,
                Experience = driverExperience,
                PhoneNumber = driverPhoneNumber,
                EMail = driverEmail,
                Age = driverAge,
                DrivingLicenceTypes = drivingLicenceTypes,
                UserId = Guid.Parse(driverUserId)
            });


            VehicleType vehicleTypes = (VehicleType)new Random().Next(Enum.GetValues(typeof(VehicleType)).Length);
            var vehicleLicensePlate = SeedDataConst.Vehicle_VehicleLicensePlate + i;
            var vehicleVolumeCapacity = SeedDataConst.Vehicle_VehicleVolumeCapacity;
            var vehicleWeightCapacity = SeedDataConst.Vehicle_VehicleWeightCapacity;
            VehicleStatus vehicleStatus = (VehicleStatus)new Random().Next(Enum.GetValues(typeof(VehicleStatus)).Length);
            var vehicleUserId = SeedDataConst.Vehicle_UserId;

            modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle
            {
                Id = Guid.NewGuid(),
                VehicleType = vehicleTypes,
                VehicleLicensePlate = vehicleLicensePlate,
                VehicleVolumeCapacity = vehicleVolumeCapacity,
                VehicleWeightCapacity = vehicleWeightCapacity,
                VehicleStatus = vehicleStatus,
                DriverId = driverId,
                UserId = Guid.Parse(vehicleUserId)
            });
        }

}
}
