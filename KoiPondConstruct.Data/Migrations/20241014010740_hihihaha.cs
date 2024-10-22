using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiPondConstruct.Data.Migrations
{
    /// <inheritdoc />
    public partial class hihihaha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "tbl_user_email_unique",
                table: "tbl_user");

            migrationBuilder.DropIndex(
                name: "tbl_user_phone_number_unique",
                table: "tbl_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "tbl_user_email_unique",
                table: "tbl_user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tbl_user_phone_number_unique",
                table: "tbl_user",
                column: "phone_number",
                unique: true);
        }
    }
}
