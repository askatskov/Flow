using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flow.Migrations
{
    /// <inheritdoc />
    public partial class kala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileToDatabase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileToDatabase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Song = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.ArtistId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileToDatabase");

            migrationBuilder.DropTable(
                name: "Musics");
        }
    }
}
