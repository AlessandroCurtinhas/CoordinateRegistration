using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoordinateRegistration.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonCity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    State = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UF = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCity_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 1,
                column: "Hash",
                value: new Guid("7d296869-b7b5-46d2-b009-41c598c1da71"));

            migrationBuilder.UpdateData(
                table: "PersonProfile",
                keyColumn: "Id",
                keyValue: 1,
                column: "Hash",
                value: new Guid("8d50322d-c315-4c77-ae07-81f449225709"));

            migrationBuilder.UpdateData(
                table: "ProfileUsr",
                keyColumn: "Id",
                keyValue: 1,
                column: "Hash",
                value: new Guid("a7e242e8-72bd-4aae-abb4-1795cf88e922"));

            migrationBuilder.UpdateData(
                table: "ProfileUsr",
                keyColumn: "Id",
                keyValue: 2,
                column: "Hash",
                value: new Guid("4fc78473-2e1b-4bc4-91cf-f3440712e0b3"));

            migrationBuilder.CreateIndex(
                name: "IX_PersonCity_PersonId",
                table: "PersonCity",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonCity");

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Id",
                keyValue: 1,
                column: "Hash",
                value: new Guid("f94cdee1-2d83-448f-8345-e609741ddda1"));

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
        }
    }
}
