using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountantWPF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pos = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "CashPosIncomes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IncomeName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pos = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashPosIncomes", x => x.Name);
                    table.ForeignKey(
                        name: "FK_CashPosIncomes_Incomes_IncomeName",
                        column: x => x.IncomeName,
                        principalTable: "Incomes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashRegisters",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CashPosName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pos = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashRegisters", x => x.Name);
                    table.ForeignKey(
                        name: "FK_CashRegisters_CashPosIncomes_CashPosName",
                        column: x => x.CashPosName,
                        principalTable: "CashPosIncomes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CashierName = table.Column<string>(type: "TEXT", nullable: true),
                    Bonnets = table.Column<int>(type: "INTEGER", nullable: true),
                    CashRegisterName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pos = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Shifts_CashRegisters_CashRegisterName",
                        column: x => x.CashRegisterName,
                        principalTable: "CashRegisters",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashPosIncomes_IncomeName",
                table: "CashPosIncomes",
                column: "IncomeName");

            migrationBuilder.CreateIndex(
                name: "IX_CashRegisters_CashPosName",
                table: "CashRegisters",
                column: "CashPosName");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_CashRegisterName",
                table: "Shifts",
                column: "CashRegisterName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "CashRegisters");

            migrationBuilder.DropTable(
                name: "CashPosIncomes");

            migrationBuilder.DropTable(
                name: "Incomes");
        }
    }
}
