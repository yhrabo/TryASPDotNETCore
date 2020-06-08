using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Purchase.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    DoneAt = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchases_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.SetNull);
                });

            #region Insert Categories data.
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Name", "Description" },
                values: new object[] { "Food", "Home food." }
                );
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Name", "Description" },
                values: new object[] { "Rent", "Bills payments." }
                );
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Name", "Description" },
                values: new object[] { "Evening time", "Theaters, cinemas, etc." }
                );
            #endregion

            #region Insert Purchases data.
            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "Name", "Price", "Quantity", "DoneAt", "CategoryId" },
                values: new object[] { "Supermarket", 565.3m, 1, new DateTime(2020, 4, 4, 19, 20, 0), 1 }
                );

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "Name", "Price", "Quantity", "DoneAt", "CategoryId" },
                values: new object[] { "Violin gathering", 200m, 2, new DateTime(2020, 4, 5, 15, 11, 0), 3 }
                );

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "Name", "Price", "Quantity", "DoneAt", "CategoryId" },
                values: new object[] { "Supper", 99.1m, 1, new DateTime(2020, 4, 7, 20, 2, 3), 1 }
                );

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "Name", "Price", "Quantity", "DoneAt", "CategoryId" },
                values: new object[] { "Water bill", 80m, 1, new DateTime(2020, 4, 15, 17, 29, 35), 2 }
                );

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "Name", "Price", "Quantity", "DoneAt", "CategoryId" },
                values: new object[] { "Philarmonia Orchestra", 250m, 2, new DateTime(2020, 4, 17, 9, 31, 0), 3 }
                );
            #endregion

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CategoryId",
                table: "Purchases",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_Name_DoneAt",
                table: "Purchases",
                columns: new[] { "Name", "DoneAt" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
