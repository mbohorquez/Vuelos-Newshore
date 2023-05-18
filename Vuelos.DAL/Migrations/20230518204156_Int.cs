using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vuelos.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journeys",
                columns: table => new
                {
                    IdJourney = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetterPriceGo = table.Column<double>(type: "float", nullable: false),
                    BetterPriceBack = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journeys", x => x.IdJourney);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    IdTransport = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightCarrier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.IdTransport);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    IdFlight = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTransport = table.Column<int>(type: "int", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.IdFlight);
                    table.ForeignKey(
                        name: "FK_Flights_Transports_IdTransport",
                        column: x => x.IdTransport,
                        principalTable: "Transports",
                        principalColumn: "IdTransport",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JourneyFlights",
                columns: table => new
                {
                    JourneyId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    NRuta = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneyFlights", x => new { x.JourneyId, x.FlightId, x.NRuta, x.Type });
                    table.ForeignKey(
                        name: "FK_JourneyFlights_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "IdFlight",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JourneyFlights_Journeys_JourneyId",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "IdJourney",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_IdTransport",
                table: "Flights",
                column: "IdTransport");

            migrationBuilder.CreateIndex(
                name: "IX_JourneyFlights_FlightId",
                table: "JourneyFlights",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JourneyFlights");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Journeys");

            migrationBuilder.DropTable(
                name: "Transports");
        }
    }
}
