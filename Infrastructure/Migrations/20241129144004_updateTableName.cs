using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamStudent_Exams_ExamId",
                table: "ExamStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStudent_Students_StudentId",
                table: "ExamStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamStudent",
                table: "ExamStudent");

            migrationBuilder.RenameTable(
                name: "ExamStudent",
                newName: "ExamStudents");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStudent_StudentId",
                table: "ExamStudents",
                newName: "IX_ExamStudents_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStudent_ExamId",
                table: "ExamStudents",
                newName: "IX_ExamStudents_ExamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamStudents",
                table: "ExamStudents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStudents_Exams_ExamId",
                table: "ExamStudents",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStudents_Students_StudentId",
                table: "ExamStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamStudents_Exams_ExamId",
                table: "ExamStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamStudents_Students_StudentId",
                table: "ExamStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamStudents",
                table: "ExamStudents");

            migrationBuilder.RenameTable(
                name: "ExamStudents",
                newName: "ExamStudent");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStudents_StudentId",
                table: "ExamStudent",
                newName: "IX_ExamStudent_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamStudents_ExamId",
                table: "ExamStudent",
                newName: "IX_ExamStudent_ExamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamStudent",
                table: "ExamStudent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStudent_Exams_ExamId",
                table: "ExamStudent",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamStudent_Students_StudentId",
                table: "ExamStudent",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
