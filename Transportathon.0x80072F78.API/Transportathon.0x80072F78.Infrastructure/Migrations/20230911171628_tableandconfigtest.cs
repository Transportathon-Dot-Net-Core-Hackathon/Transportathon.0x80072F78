using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportathon._0x80072F78.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tableandconfigtest : Migration
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
                    AddressName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    District = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Street = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Alley = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BuildingNumber = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    ApartmentNumber = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    PostCode = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Text = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
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
                    Address = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Alley = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    District = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Experience = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    EMail = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Age = table.Column<int>(type: "integer", maxLength: 20, nullable: false),
                    DrivingLicenceTypes = table.Column<List<int>>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestType = table.Column<int>(type: "integer", maxLength: 30, nullable: false),
                    OutputAddressId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    DestinationAddressId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    Weight = table.Column<float>(type: "real", maxLength: 20, nullable: false),
                    Volume = table.Column<float>(type: "real", maxLength: 20, nullable: false),
                    Note = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 20, nullable: false),
                    DocumentStatus = table.Column<int>(type: "integer", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationRequests_Addresses_DestinationAddressId",
                        column: x => x.DestinationAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportationRequests_Addresses_OutputAddressId",
                        column: x => x.OutputAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportationRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamWorkerId = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleType = table.Column<int>(type: "integer", maxLength: 100, nullable: false),
                    VehicleLicensePlate = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VehicleVolumeCapacity = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    VehicleWeightCapacity = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    VehicleStatus = table.Column<int>(type: "integer", maxLength: 150, nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamWorkers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Surname = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Age = table.Column<int>(type: "integer", maxLength: 5, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    EMail = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Experience = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyUsersId",
                table: "Companies",
                column: "CompanyUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CompanyId",
                table: "Teams",
                column: "CompanyId");

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
                name: "Comments");

            migrationBuilder.DropTable(
                name: "TeamWorkers");

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
        }
    }
}
