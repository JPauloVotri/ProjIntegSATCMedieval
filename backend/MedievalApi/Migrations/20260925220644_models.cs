using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedievalApi.Migrations
{
    /// <inheritdoc />
    public partial class models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "grupo_produto_id",
                table: "produto",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "grupo_produto",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "uuid")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "cotacao_item_fornecedor",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "cotacao_item_fornecedor");

            migrationBuilder.AlterColumn<int>(
                name: "grupo_produto_id",
                table: "produto",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "grupo_produto",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
