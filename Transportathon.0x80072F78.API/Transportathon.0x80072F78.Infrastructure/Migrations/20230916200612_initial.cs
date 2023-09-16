using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Transportathon._0x80072F78.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressType = table.Column<int>(type: "integer", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    District = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LocalAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FamilyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UserImage = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Text = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FriendlyName = table.Column<string>(type: "text", nullable: true),
                    Xml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Experience = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    EMail = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Age = table.Column<int>(type: "integer", maxLength: 20, nullable: false),
                    DrivingLicenceTypes = table.Column<int[]>(type: "integer[]", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageContent = table.Column<string>(type: "text", nullable: false),
                    SendTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    City = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    District = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Alley = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BuildingNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApartmentNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PostCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VKN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CompanyUsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_AspNetUsers_CompanyUsersId",
                        column: x => x.CompanyUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransportationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestType = table.Column<int>(type: "integer", nullable: false),
                    OutputAddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    DestinationAddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Volume = table.Column<float>(type: "real", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FirstDateRange = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastDateRange = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DocumentStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationRequests_Addresses_DestinationAddressId",
                        column: x => x.DestinationAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportationRequests_Addresses_OutputAddressId",
                        column: x => x.OutputAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransportationRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleType = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    VehicleLicensePlate = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VehicleVolumeCapacity = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    VehicleWeightCapacity = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    VehicleStatus = table.Column<int>(type: "integer", maxLength: 150, nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TransportationRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TransportationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OfferTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offers_TransportationRequests_TransportationRequestId",
                        column: x => x.TransportationRequestId,
                        principalTable: "TransportationRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Offers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TeamWorkers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Age = table.Column<int>(type: "integer", maxLength: 5, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    EMail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Experience = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamWorkers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyName", "FirstName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserImage", "UserName" },
                values: new object[,]
                {
                    { new Guid("07078181-35ae-4f45-a8d1-c3327efdc0d4"), 0, "ce8fa663-5f7b-424a-b1b5-45628fe65950", "ismailabanoz1213@gmail.com", false, "Abanoz", "İsmail", false, null, null, "ISMAIL", "AQAAAAIAAYagAAAAEG11jDyWLS6Lfww+1TqmBWrocPKEaruQ8y7i1xEPx32/ZCcs1O8zpXqmkZwjyYbU2g==", null, false, null, false, null, "ismail" },
                    { new Guid("08d364c6-45fd-434e-83c9-eb363849d921"), 0, "e57ff8c8-496c-4cb3-bb8a-684de65231e9", "serhat16_@hotmail.com", false, "Sandıkçıoğlu", "Serhat", false, null, null, "SERHAT", "AQAAAAIAAYagAAAAEMjhrMRn5E1EAO7PZRzvyqo+npO3v0Afbs21+RxLw4rAIXRutHXkFNLv8p1c54Ub2w==", null, false, null, false, null, "serhat" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "Age", "DrivingLicenceTypes", "EMail", "Experience", "Name", "PhoneNumber", "Surname", "UserId" },
                values: new object[,]
                {
                    { new Guid("0b1fe640-2efc-4b68-9f61-6efc5cf2170d"), 26, new[] { 0 }, "driver@driver.com", "6", "DriverName1", "1234567890", "DriverSurname1", new Guid("08d364c6-45fd-434e-83c9-eb363849d921") },
                    { new Guid("6600291f-27d7-4e92-9583-2401f22198a0"), 28, new[] { 0 }, "driver@driver.com", "8", "DriverName3", "1234567890", "DriverSurname3", new Guid("08d364c6-45fd-434e-83c9-eb363849d921") },
                    { new Guid("99e157be-a0be-4c44-8e1c-7d7e4b4a361f"), 30, new[] { 2, 0 }, "driver@driver.com", "10", "DriverName5", "1234567890", "DriverSurname5", new Guid("08d364c6-45fd-434e-83c9-eb363849d921") },
                    { new Guid("bad1d1c9-0f00-4919-9587-8ff8e9629fa2"), 27, new[] { 0 }, "driver@driver.com", "7", "DriverName2", "1234567890", "DriverSurname2", new Guid("08d364c6-45fd-434e-83c9-eb363849d921") },
                    { new Guid("c7f69770-5444-4146-a0d4-b2604caf50d6"), 29, new[] { 0 }, "driver@driver.com", "9", "DriverName4", "1234567890", "DriverSurname4", new Guid("08d364c6-45fd-434e-83c9-eb363849d921") }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Alley", "ApartmentNumber", "BuildingNumber", "City", "CompanyName", "CompanyUsersId", "District", "Name", "PostCode", "Street", "Surname", "Title", "VKN" },
                values: new object[] { new Guid("6c266446-0a71-4469-8f1f-f26a3f4d3f81"), "Bursa Sokak", "1", "1", "Bursa Şehri", "Serhat Co.", new Guid("08d364c6-45fd-434e-83c9-eb363849d921"), "Bursa İlçesi", "Serhat", "16200", "Bursa Mahallesi", "Sandıkçıoğlu", "LTD", "1234567890" });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "DriverId", "UserId", "VehicleLicensePlate", "VehicleStatus", "VehicleType", "VehicleVolumeCapacity", "VehicleWeightCapacity" },
                values: new object[,]
                {
                    { new Guid("0dee1f1f-38bb-4e9b-836e-4ea38a5510ef"), new Guid("c7f69770-5444-4146-a0d4-b2604caf50d6"), new Guid("08d364c6-45fd-434e-83c9-eb363849d921"), "16 SI 54", 0, 1, "50", "1200" },
                    { new Guid("90859246-16bf-4a5a-bcb4-b5ec6d9216db"), new Guid("0b1fe640-2efc-4b68-9f61-6efc5cf2170d"), new Guid("08d364c6-45fd-434e-83c9-eb363849d921"), "16 SI 51", 1, 1, "50", "1200" },
                    { new Guid("a8a867a4-d940-437f-8d8c-57894b4aba72"), new Guid("bad1d1c9-0f00-4919-9587-8ff8e9629fa2"), new Guid("08d364c6-45fd-434e-83c9-eb363849d921"), "16 SI 52", 0, 1, "50", "1200" },
                    { new Guid("cd859506-6838-45d3-9c1f-0e05b03b5ac0"), new Guid("6600291f-27d7-4e92-9583-2401f22198a0"), new Guid("08d364c6-45fd-434e-83c9-eb363849d921"), "16 SI 53", 3, 0, "50", "1200" },
                    { new Guid("ce101cee-f038-4c62-a736-c738dd94ff8b"), new Guid("99e157be-a0be-4c44-8e1c-7d7e4b4a361f"), new Guid("08d364c6-45fd-434e-83c9-eb363849d921"), "16 SI 55", 3, 0, "50", "1200" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyUsersId",
                table: "Companies",
                column: "CompanyUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CompanyId",
                table: "Offers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_TeamId",
                table: "Offers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_TransportationRequestId",
                table: "Offers",
                column: "TransportationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserId",
                table: "Offers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_VehicleId",
                table: "Offers",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompanyId",
                table: "Teams",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "uCompanyIdName",
                table: "Teams",
                columns: new[] { "Name", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamWorkers_TeamId",
                table: "TeamWorkers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_DestinationAddressId",
                table: "TransportationRequests",
                column: "DestinationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_OutputAddressId",
                table: "TransportationRequests",
                column: "OutputAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_UserId",
                table: "TransportationRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_DriverId",
                table: "Vehicles",
                column: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DataProtectionKeys");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "TeamWorkers");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TransportationRequests");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
