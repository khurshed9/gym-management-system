using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GymManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Memberships_SessionId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Schedules_ScheduleId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_Specializations_SpecializationId",
                table: "Trainers");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_SpecializationId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Clients_SessionId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Trainers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Clients",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MembershipId",
                table: "Clients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MembershipId",
                table: "Clients",
                column: "MembershipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Memberships_MembershipId",
                table: "Clients",
                column: "MembershipId",
                principalTable: "Memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Schedules_ScheduleId",
                table: "Clients",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Memberships_MembershipId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Schedules_ScheduleId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_MembershipId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "Trainers",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Clients",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Clients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_SpecializationId",
                table: "Trainers",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SessionId",
                table: "Clients",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Memberships_SessionId",
                table: "Clients",
                column: "SessionId",
                principalTable: "Memberships",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Schedules_ScheduleId",
                table: "Clients",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_Specializations_SpecializationId",
                table: "Trainers",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id");
        }
    }
}
