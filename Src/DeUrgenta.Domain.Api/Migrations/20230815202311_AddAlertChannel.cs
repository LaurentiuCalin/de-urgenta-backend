using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeUrgenta.Domain.Migrations
{
    public partial class AddAlertChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertChannel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlertChannelUser",
                columns: table => new
                {
                    SelectedAlertChannelsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertChannelUser", x => new { x.SelectedAlertChannelsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AlertChannelUser_AlertChannels_SelectedAlertChannelsId",
                        column: x => x.SelectedAlertChannelsId,
                        principalTable: "AlertChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlertChannelUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AlertChannels",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "email" },
                    { 2, "sms" },
                    { 3, "push-notification" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertChannelUser_UsersId",
                table: "AlertChannelUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertChannelUser");

            migrationBuilder.DropTable(
                name: "AlertChannels");
        }
    }
}
