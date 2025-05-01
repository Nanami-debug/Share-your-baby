using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCFFinalHomework.Migrations
{
    /// <inheritdoc />
    public partial class FixGoodIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    password = table.Column<string>(type: "longtext", nullable: false),
                    call = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    des = table.Column<string>(type: "longtext", nullable: false),
                    solded = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    img = table.Column<byte[]>(type: "longblob", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goods_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    goodId = table.Column<string>(type: "varchar(255)", nullable: true),
                    orderStatus = table.Column<int>(type: "int", nullable: false),
                    buyerId = table.Column<string>(type: "varchar(255)", nullable: false),
                    sellerId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Goods_goodId",
                        column: x => x.goodId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_Users_buyerId",
                        column: x => x.buyerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_sellerId",
                        column: x => x.sellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserStarredGoods",
                columns: table => new
                {
                    StarredById = table.Column<string>(type: "varchar(255)", nullable: false),
                    staredId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStarredGoods", x => new { x.StarredById, x.staredId });
                    table.ForeignKey(
                        name: "FK_UserStarredGoods_Goods_staredId",
                        column: x => x.staredId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStarredGoods_Users_StarredById",
                        column: x => x.StarredById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_UserId",
                table: "Goods",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_buyerId",
                table: "Orders",
                column: "buyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_goodId",
                table: "Orders",
                column: "goodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_sellerId",
                table: "Orders",
                column: "sellerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStarredGoods_staredId",
                table: "UserStarredGoods",
                column: "staredId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "UserStarredGoods");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
