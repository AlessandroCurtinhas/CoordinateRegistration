using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoordinateRegistration.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class stringReplacePersonRecoveryHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_RecoveryHash",
                table: "Person");

            migrationBuilder.AlterColumn<string>(
                name: "RecoveryHash",
                table: "Person",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "RecoveryHash" },
                values: new object[] { new Guid("f94cdee1-2d83-448f-8345-e609741ddda1"), null });

            migrationBuilder.UpdateData(
                table: "PersonProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "Hash",
                value: new Guid("dfc2880b-7c1e-4929-ba62-5384ab33df74"));

            migrationBuilder.UpdateData(
                table: "ProfileUsr",
                keyColumn: "Id",
                keyValue: 1,
                column: "Hash",
                value: new Guid("965bfb6c-656c-4121-8e70-5345800c5dbb"));

            migrationBuilder.UpdateData(
                table: "ProfileUsr",
                keyColumn: "Id",
                keyValue: 2,
                column: "Hash",
                value: new Guid("8af1e287-f551-43bf-87d6-48d917adf903"));

            migrationBuilder.CreateIndex(
                name: "IX_Person_RecoveryHash",
                table: "Person",
                column: "RecoveryHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_RecoveryHash",
                table: "Person");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecoveryHash",
                table: "Person",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Hash", "RecoveryHash" },
                values: new object[] { new Guid("9772edc4-764c-4576-b864-bdd889369bc2"), null });

            migrationBuilder.UpdateData(
                table: "PersonProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "Hash",
                value: new Guid("e3814f9b-e3df-442e-be80-3c79ce26a0c2"));

            migrationBuilder.UpdateData(
                table: "ProfileUsr",
                keyColumn: "Id",
                keyValue: 1,
                column: "Hash",
                value: new Guid("8abf25ba-cf18-4770-be7a-006c739fcba7"));

            migrationBuilder.UpdateData(
                table: "ProfileUsr",
                keyColumn: "Id",
                keyValue: 2,
                column: "Hash",
                value: new Guid("4d726080-a763-47c0-b15a-7fca707e00e3"));

            migrationBuilder.CreateIndex(
                name: "IX_Person_RecoveryHash",
                table: "Person",
                column: "RecoveryHash",
                unique: true,
                filter: "[RecoveryHash] IS NOT NULL");
        }
    }
}
