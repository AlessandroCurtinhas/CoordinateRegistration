using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoordinateRegistration.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordDateRequest = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecoveryHash = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "SmallDatetime", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "SmallDatetime", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "SmallDatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileUsr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileUsr", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "SmallDatetime", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "SmallDatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marker_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeOccurrence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    PersonUpdateId = table.Column<int>(type: "int", nullable: true),
                    PersonDeleteId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "SmallDatetime", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "SmallDatetime", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "SmallDatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOccurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeOccurrence_Person_PersonDeleteId",
                        column: x => x.PersonDeleteId,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeOccurrence_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeOccurrence_Person_PersonUpdateId",
                        column: x => x.PersonUpdateId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonProfile_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonProfile_ProfileUsr_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileUsr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarkerId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "SmallDatetime", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "SmallDatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Marker_MarkerId",
                        column: x => x.MarkerId,
                        principalTable: "Marker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    MarkerId = table.Column<int>(type: "int", nullable: false),
                    Positive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Negative = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DateCreated = table.Column<DateTime>(type: "SmallDatetime", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "SmallDatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Marker_MarkerId",
                        column: x => x.MarkerId,
                        principalTable: "Marker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MarkerTypeOccurrence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarkerId = table.Column<int>(type: "int", nullable: false),
                    TypeOccurrenceId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkerTypeOccurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarkerTypeOccurrence_Marker_MarkerId",
                        column: x => x.MarkerId,
                        principalTable: "Marker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarkerTypeOccurrence_TypeOccurrence_TypeOccurrenceId",
                        column: x => x.TypeOccurrenceId,
                        principalTable: "TypeOccurrence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "DateCreated", "DateDeleted", "DateUpdated", "Email", "Hash", "Name", "Password", "PasswordDateRequest", "RecoveryHash" },
                values: new object[] { 1,"01/01/2025", null, null, "admin@admin.com.br", new Guid("9772edc4-764c-4576-b864-bdd889369bc2"), "Admin", "4de93544234adffbb681ed60ffcfb941", null, null });

            migrationBuilder.InsertData(
                table: "ProfileUsr",
                columns: new[] { "Id", "Hash", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("8abf25ba-cf18-4770-be7a-006c739fcba7"), "Admin" },
                    { 2, new Guid("4d726080-a763-47c0-b15a-7fca707e00e3"), "User" }
                });

            migrationBuilder.InsertData(
                table: "PersonProfile",
                columns: new[] { "Id", "Hash", "PersonId", "ProfileId" },
                values: new object[] { 1, new Guid("e3814f9b-e3df-442e-be80-3c79ce26a0c2"), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Hash",
                table: "Comment",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_MarkerId",
                table: "Comment",
                column: "MarkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PersonId",
                table: "Comment",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Marker_Hash",
                table: "Marker",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marker_PersonId",
                table: "Marker",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkerTypeOccurrence_MarkerId",
                table: "MarkerTypeOccurrence",
                column: "MarkerId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkerTypeOccurrence_TypeOccurrenceId",
                table: "MarkerTypeOccurrence",
                column: "TypeOccurrenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Email",
                table: "Person",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_Hash",
                table: "Person",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_RecoveryHash",
                table: "Person",
                column: "RecoveryHash",
                unique: true,
                filter: "[RecoveryHash] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonProfile_PersonId",
                table: "PersonProfile",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonProfile_ProfileId",
                table: "PersonProfile",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileUsr_Hash",
                table: "ProfileUsr",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_Hash",
                table: "Review",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_MarkerId",
                table: "Review",
                column: "MarkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_PersonId",
                table: "Review",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOccurrence_Hash",
                table: "TypeOccurrence",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeOccurrence_PersonDeleteId",
                table: "TypeOccurrence",
                column: "PersonDeleteId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOccurrence_PersonId",
                table: "TypeOccurrence",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOccurrence_PersonUpdateId",
                table: "TypeOccurrence",
                column: "PersonUpdateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "MarkerTypeOccurrence");

            migrationBuilder.DropTable(
                name: "PersonProfile");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "TypeOccurrence");

            migrationBuilder.DropTable(
                name: "ProfileUsr");

            migrationBuilder.DropTable(
                name: "Marker");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
