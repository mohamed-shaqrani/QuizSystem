using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Courses_CourseId",
                table: "CourseInstructor");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Instructors_InstructorId",
                table: "CourseInstructor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseInstructor",
                table: "CourseInstructor");

            migrationBuilder.RenameTable(
                name: "CourseInstructor",
                newName: "CourseInstructors");

            migrationBuilder.RenameIndex(
                name: "IX_CourseInstructor_InstructorId",
                table: "CourseInstructors",
                newName: "IX_CourseInstructors_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseInstructor_CourseId",
                table: "CourseInstructors",
                newName: "IX_CourseInstructors_CourseId");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "ExamQuestion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseInstructors",
                table: "CourseInstructors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CourseId",
                table: "Questions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_InstructorId",
                table: "Questions",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_InstructorId",
                table: "Exams",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_CourseId",
                table: "ExamQuestion",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructors_Courses_CourseId",
                table: "CourseInstructors",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructors_Instructors_InstructorId",
                table: "CourseInstructors",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestion_Courses_CourseId",
                table: "ExamQuestion",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Instructors_InstructorId",
                table: "Exams",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Courses_CourseId",
                table: "Questions",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Instructors_InstructorId",
                table: "Questions",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructors_Courses_CourseId",
                table: "CourseInstructors");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructors_Instructors_InstructorId",
                table: "CourseInstructors");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestion_Courses_CourseId",
                table: "ExamQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Instructors_InstructorId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Courses_CourseId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Instructors_InstructorId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CourseId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_InstructorId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Exams_InstructorId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_ExamQuestion_CourseId",
                table: "ExamQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseInstructors",
                table: "CourseInstructors");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "ExamQuestion");

            migrationBuilder.RenameTable(
                name: "CourseInstructors",
                newName: "CourseInstructor");

            migrationBuilder.RenameIndex(
                name: "IX_CourseInstructors_InstructorId",
                table: "CourseInstructor",
                newName: "IX_CourseInstructor_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseInstructors_CourseId",
                table: "CourseInstructor",
                newName: "IX_CourseInstructor_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseInstructor",
                table: "CourseInstructor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Courses_CourseId",
                table: "CourseInstructor",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Instructors_InstructorId",
                table: "CourseInstructor",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
