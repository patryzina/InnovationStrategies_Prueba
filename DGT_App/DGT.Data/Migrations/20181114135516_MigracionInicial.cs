using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DGT.Data.Infrastructure.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conductor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DNI = table.Column<string>(maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(maxLength: 150, nullable: false),
                    Puntos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conductor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoInfraccion",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 25, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 250, nullable: false),
                    CostePuntos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoInfraccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Matricula = table.Column<string>(maxLength: 10, nullable: false),
                    Marca = table.Column<string>(maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Infraccion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConductorId = table.Column<int>(nullable: false),
                    IdTipoInfraccion = table.Column<string>(nullable: true),
                    FechaInfraccion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infraccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Infraccion_Conductor_ConductorId",
                        column: x => x.ConductorId,
                        principalTable: "Conductor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Infraccion_TipoInfraccion_IdTipoInfraccion",
                        column: x => x.IdTipoInfraccion,
                        principalTable: "TipoInfraccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConductorVehiculo",
                columns: table => new
                {
                    ConductorId = table.Column<int>(nullable: false),
                    VehiculoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConductorVehiculo", x => new { x.ConductorId, x.VehiculoId });
                    table.ForeignKey(
                        name: "FK_ConductorVehiculo_Conductor_ConductorId",
                        column: x => x.ConductorId,
                        principalTable: "Conductor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConductorVehiculo_Vehiculo_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConductorVehiculo_VehiculoId",
                table: "ConductorVehiculo",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Infraccion_ConductorId",
                table: "Infraccion",
                column: "ConductorId");

            migrationBuilder.CreateIndex(
                name: "IX_Infraccion_IdTipoInfraccion",
                table: "Infraccion",
                column: "IdTipoInfraccion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConductorVehiculo");

            migrationBuilder.DropTable(
                name: "Infraccion");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Conductor");

            migrationBuilder.DropTable(
                name: "TipoInfraccion");
        }
    }
}
