using Microsoft.EntityFrameworkCore.Migrations;

namespace alunos.api.Migrations
{
    public partial class Temporizador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Temporizador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temporizador", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Temporizador",
                columns: new[] { "Id", "Temp" },
                values: new object[] { 1, 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temporizador");
        }
    }
}
