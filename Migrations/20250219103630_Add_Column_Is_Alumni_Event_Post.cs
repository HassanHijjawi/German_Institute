using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GermanInstitute.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_Is_Alumni_Event_Post : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Applicant",
                table: "Applicant");

            migrationBuilder.RenameTable(
                name: "Applicant",
                newName: "applicant");

            migrationBuilder.AddColumn<bool>(
                name: "IsEventPost",
                table: "alumni-posts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_applicant",
                table: "applicant",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_applicant",
                table: "applicant");

            migrationBuilder.DropColumn(
                name: "IsEventPost",
                table: "alumni-posts");

            migrationBuilder.RenameTable(
                name: "applicant",
                newName: "Applicant");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applicant",
                table: "Applicant",
                column: "Id");
        }
    }
}
