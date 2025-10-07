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
                name: "User",
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
                    table.PrimaryKey("PK_User", x => x.Id);
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "SmallDatetime", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "SmallDatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marker_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
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
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserUpdateId = table.Column<int>(type: "int", nullable: true),
                    UserDeleteId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "SmallDatetime", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "SmallDatetime", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "SmallDatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOccurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeOccurrence_User_UserDeleteId",
                        column: x => x.UserDeleteId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeOccurrence_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TypeOccurrence_User_UserUpdateId",
                        column: x => x.UserUpdateId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_ProfileUsr_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "ProfileUsr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
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
                    UserId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Comment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
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
                table: "ProfileUsr",
                columns: new[] { "Id", "Hash", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("d44a56d9-fe82-4213-9e74-6de492dd3d5e"), "Admin" },
                    { 2, new Guid("f6052b15-acf4-44ad-996c-fdf90c019404"), "User" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateCreated", "DateDeleted", "DateUpdated", "Email", "Hash", "Name", "Password", "PasswordDateRequest", "RecoveryHash" },
                values: new object[] { 1, "01/01/2025" , null, null, "admin@admin.com.br", new Guid("e8bec31a-ed0c-4762-9011-f4b3d69eb6cc"), "Admin", "0f2797f2182804d0cc7f0b85d254c146", null, null });

            migrationBuilder.InsertData(
                table: "UserProfile",
                columns: new[] { "Id", "Hash", "ProfileId", "UserId" },
                values: new object[] { 1, new Guid("583383e7-4b31-4b1b-9d54-5c5b155b11e3"), 1, 1 });

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
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Marker_Hash",
                table: "Marker",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marker_UserId",
                table: "Marker",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkerTypeOccurrence_MarkerId",
                table: "MarkerTypeOccurrence",
                column: "MarkerId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkerTypeOccurrence_TypeOccurrenceId",
                table: "MarkerTypeOccurrence",
                column: "TypeOccurrenceId");

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
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOccurrence_Hash",
                table: "TypeOccurrence",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeOccurrence_UserDeleteId",
                table: "TypeOccurrence",
                column: "UserDeleteId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOccurrence_UserId",
                table: "TypeOccurrence",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOccurrence_UserUpdateId",
                table: "TypeOccurrence",
                column: "UserUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Hash",
                table: "User",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RecoveryHash",
                table: "User",
                column: "RecoveryHash",
                unique: true,
                filter: "[RecoveryHash] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_ProfileId",
                table: "UserProfile",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserId",
                table: "UserProfile",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "MarkerTypeOccurrence");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "TypeOccurrence");

            migrationBuilder.DropTable(
                name: "Marker");

            migrationBuilder.DropTable(
                name: "ProfileUsr");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
