using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BebopTransferService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserIdentificationNumber = table.Column<long>(type: "bigint", maxLength: 10, nullable: false),
                    ExternalOperatorId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    UserAddress = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsKeroAuthUserDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsCoplandUserDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsStandUserDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentTitle = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UrlDocument = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    TransferId = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Transfers_TransferId",
                        column: x => x.TransferId,
                        principalTable: "Transfers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_Id",
                table: "Files",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_TransferId",
                table: "Files",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_Id",
                table: "Transfers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_UserId",
                table: "Transfers",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Transfers");
        }
    }
}
