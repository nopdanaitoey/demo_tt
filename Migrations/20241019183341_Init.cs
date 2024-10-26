using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AffectedAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrgencyLevel = table.Column<int>(type: "int", nullable: false),
                    TimeConstraint = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffectedAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceAffectedAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceAffectedAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceAffectedAreas_AffectedAreas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "AffectedAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceAffectedAreas_MasterResources_ResourceID",
                        column: x => x.ResourceID,
                        principalTable: "MasterResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceTrucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTrucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceTrucks_MasterResources_ResourceID",
                        column: x => x.ResourceID,
                        principalTable: "MasterResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceTrucks_Trucks_TruckID",
                        column: x => x.TruckID,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelTimeToAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TruckID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TravelTime = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelTimeToAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelTimeToAreas_AffectedAreas_AreaID",
                        column: x => x.AreaID,
                        principalTable: "AffectedAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelTimeToAreas_Trucks_TruckID",
                        column: x => x.TruckID,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAffectedAreas_AreaID",
                table: "ResourceAffectedAreas",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceAffectedAreas_ResourceID",
                table: "ResourceAffectedAreas",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceTrucks_ResourceID",
                table: "ResourceTrucks",
                column: "ResourceID");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceTrucks_TruckID",
                table: "ResourceTrucks",
                column: "TruckID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelTimeToAreas_AreaID",
                table: "TravelTimeToAreas",
                column: "AreaID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelTimeToAreas_TruckID",
                table: "TravelTimeToAreas",
                column: "TruckID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceAffectedAreas");

            migrationBuilder.DropTable(
                name: "ResourceTrucks");

            migrationBuilder.DropTable(
                name: "TravelTimeToAreas");

            migrationBuilder.DropTable(
                name: "MasterResources");

            migrationBuilder.DropTable(
                name: "AffectedAreas");

            migrationBuilder.DropTable(
                name: "Trucks");
        }
    }
}
