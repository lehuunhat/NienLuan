using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HienTangToc.Migrations
{
    /// <inheritdoc />
    public partial class HienTangToc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoiHien",
                columns: table => new
                {
                    idNH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTenNH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDTNH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiNH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailNH = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiHien", x => x.idNH);
                });

            migrationBuilder.CreateTable(
                name: "NguoiMuon",
                columns: table => new
                {
                    idNM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTenNM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDTNM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiNM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailNM = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiMuon", x => x.idNM);
                });

            migrationBuilder.CreateTable(
                name: "SalonToc",
                columns: table => new
                {
                    idSalon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSalon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChiSalon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDTSalon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalonToc", x => x.idSalon);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HTvsSalon",
                columns: table => new
                {
                    idNH = table.Column<int>(type: "int", nullable: false),
                    idSalon = table.Column<int>(type: "int", nullable: false),
                    HTvsSalonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HTvsSalon", x => new { x.idNH, x.idSalon });
                    table.ForeignKey(
                        name: "FK_HTvsSalon_NguoiHien_idNH",
                        column: x => x.idNH,
                        principalTable: "NguoiHien",
                        principalColumn: "idNH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HTvsSalon_SalonToc_idSalon",
                        column: x => x.idSalon,
                        principalTable: "SalonToc",
                        principalColumn: "idSalon",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MTvsSalon",
                columns: table => new
                {
                    idNM = table.Column<int>(type: "int", nullable: false),
                    idSalon = table.Column<int>(type: "int", nullable: false),
                    MTvsSalonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MTvsSalon", x => new { x.idNM, x.idSalon });
                    table.ForeignKey(
                        name: "FK_MTvsSalon_NguoiMuon_idNM",
                        column: x => x.idNM,
                        principalTable: "NguoiMuon",
                        principalColumn: "idNM",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MTvsSalon_SalonToc_idSalon",
                        column: x => x.idSalon,
                        principalTable: "SalonToc",
                        principalColumn: "idSalon",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HTvsSalon_idSalon",
                table: "HTvsSalon",
                column: "idSalon");

            migrationBuilder.CreateIndex(
                name: "IX_MTvsSalon_idSalon",
                table: "MTvsSalon",
                column: "idSalon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HTvsSalon");

            migrationBuilder.DropTable(
                name: "MTvsSalon");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NguoiHien");

            migrationBuilder.DropTable(
                name: "NguoiMuon");

            migrationBuilder.DropTable(
                name: "SalonToc");
        }
    }
}
