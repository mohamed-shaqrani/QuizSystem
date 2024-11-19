using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "Instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "ExamStudent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "ExamQuestion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "CourseStudent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "CourseInstructor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "Choices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isDeleted",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "ExamStudent");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "ExamQuestion");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "CourseStudent");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "CourseInstructor");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Choices");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Answers");
        }
    }
}
