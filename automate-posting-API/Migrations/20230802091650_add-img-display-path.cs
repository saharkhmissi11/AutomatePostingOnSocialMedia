using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostingOnSocialMedia.Migrations
{
    /// <inheritdoc />
    public partial class addimgdisplaypath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayPath",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayPath",
                table: "Images");
        }
    }
}
