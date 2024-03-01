using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketTable",
                columns: table => new
                {
                    Ticket_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticket_Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Category = table.Column<string>(type: "varchar(16)", nullable: true),
                    Priority_Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Issues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ticket_Status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "Pending"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTable", x => x.Ticket_Id);
                });

            migrationBuilder.CreateTable(
                name: "MultiAttachmentTable",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attachment_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrignalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TickId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiAttachmentTable", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_MultiAttachmentTable_TicketTable_TickId",
                        column: x => x.TickId,
                        principalTable: "TicketTable",
                        principalColumn: "Ticket_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultiAttachmentTable_TickId",
                table: "MultiAttachmentTable",
                column: "TickId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultiAttachmentTable");

            migrationBuilder.DropTable(
                name: "TicketTable");
        }
    }
}
