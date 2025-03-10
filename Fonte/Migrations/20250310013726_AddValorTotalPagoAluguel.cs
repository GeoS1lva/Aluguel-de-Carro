using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fonte.Migrations
{
    /// <inheritdoc />
    public partial class AddValorTotalPagoAluguel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ValorPago",
                table: "Alugueis",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorPago",
                table: "Alugueis");
        }
    }
}
