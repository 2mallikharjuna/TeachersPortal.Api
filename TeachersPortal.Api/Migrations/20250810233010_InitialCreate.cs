using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachersPortal.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Cources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Cources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_teachers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_teachers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StudentCourse",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StudentCourse", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_tbl_StudentCourse_tbl_Cources_CourseId",
                        column: x => x.CourseId,
                        principalTable: "tbl_Cources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_StudentCourse_tbl_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "tbl_Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HashPassword = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AppUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_AppUser_tbl_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "tbl_teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_TeacherCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TeacherCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_TeacherCourse_tbl_Cources_CourseId",
                        column: x => x.CourseId,
                        principalTable: "tbl_Cources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_TeacherCourse_tbl_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "tbl_teachers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tbl_Students",
                columns: new[] { "Id", "Address", "CountryId", "CreatedBy", "CreatedDate", "DateOfBirth", "Email", "EndDate", "FirstName", "Gender", "LastName" },
                values: new object[,]
                {
                    { 1, "11 Collins Street, Melbourne", 1, "", new DateTime(2025, 8, 10, 23, 30, 9, 164, DateTimeKind.Utc).AddTicks(407), new DateTime(1991, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test1@yahoo.com", null, "TestFirstName1", 0, "TestLastName1" },
                    { 2, "22 Collins Street, Melbourne", 1, "", new DateTime(2025, 8, 10, 23, 30, 9, 164, DateTimeKind.Utc).AddTicks(1371), new DateTime(1992, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test2@yahoo.com", null, "TestFirstName2", 1, "TestLastName2" },
                    { 3, "33 Collins Street, Melbourne", 1, "", new DateTime(2025, 8, 10, 23, 30, 9, 164, DateTimeKind.Utc).AddTicks(1379), new DateTime(1993, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test3@yahoo.com", null, "TestFirstName3", 2, "TestLastName3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AppUser_TeacherId",
                table: "tbl_AppUser",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StudentCourse_CourseId",
                table: "tbl_StudentCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TeacherCourse_CourseId",
                table: "tbl_TeacherCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_TeacherCourse_TeacherId",
                table: "tbl_TeacherCourse",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_AppUser");

            migrationBuilder.DropTable(
                name: "tbl_StudentCourse");

            migrationBuilder.DropTable(
                name: "tbl_TeacherCourse");

            migrationBuilder.DropTable(
                name: "tbl_Students");

            migrationBuilder.DropTable(
                name: "tbl_Cources");

            migrationBuilder.DropTable(
                name: "tbl_teachers");
        }
    }
}
