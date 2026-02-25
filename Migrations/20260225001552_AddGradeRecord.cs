using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassroomRecordApi.Migrations
{
    /// <inheritdoc />
    public partial class AddGradeRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GradeRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Quarter = table.Column<int>(type: "int", nullable: true),
                    Semester = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InitialGrade = table.Column<double>(type: "double", nullable: true),
                    TransmutedGrade = table.Column<double>(type: "double", nullable: true),
                    Remarks = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolYear = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SubjectId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClassroomId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeRecords_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_GradeRecords_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeRecords_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GradeRecords_ClassroomId",
                table: "GradeRecords",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeRecords_StudentId",
                table: "GradeRecords",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeRecords_SubjectId",
                table: "GradeRecords",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeRecords");
        }
    }
}
