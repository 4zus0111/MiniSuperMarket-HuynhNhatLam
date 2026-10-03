using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class _2222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "Address",
                value: "25 Nguyễn Huệ, Quận 1, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "Address",
                value: "118 Võ Văn Tần, Quận 3, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "Address",
                value: "72 Nguyễn Trãi, Quận 5, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "Address",
                value: "156 Thành Thái, Quận 10, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "Address",
                value: "43 Điện Biên Phủ, Quận Bình Thạnh, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                column: "Address",
                value: "89 Phạm Văn Đồng, Quận Gò Vấp, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "Address",
                value: "215 Cộng Hòa, Quận Tân Bình, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "Address",
                value: "36 Phan Đình Phùng, Quận Phú Nhuận, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                column: "Address",
                value: "102 Nguyễn Thị Thập, Quận 7, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "Address",
                value: "68 Hậu Giang, Quận 6, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                column: "Address",
                value: "145 Võ Văn Ngân, TP. Thủ Đức, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "Address",
                value: "234 Lê Văn Khương, Quận 12, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                column: "Address",
                value: "57 Lũy Bán Bích, Quận Tân Phú, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "Address",
                value: "321 Tỉnh Lộ 10, Quận Bình Tân, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                column: "Address",
                value: "78 Nguyễn Hữu Trí, Huyện Bình Chánh, TP.HCM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "Address",
                value: "Quận 1, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "Address",
                value: "Quận 3, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "Address",
                value: "Quận 5, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "Address",
                value: "Quận 10, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "Address",
                value: "Quận Bình Thạnh, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                column: "Address",
                value: "Quận Gò Vấp, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "Address",
                value: "Quận Tân Bình, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "Address",
                value: "Quận Phú Nhuận, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                column: "Address",
                value: "Quận 7, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "Address",
                value: "Quận 6, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                column: "Address",
                value: "TP. Thủ Đức, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "Address",
                value: "Quận 12, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                column: "Address",
                value: "Quận Tân Phú, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "Address",
                value: "Quận Bình Tân, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                column: "Address",
                value: "Huyện Bình Chánh, TP.HCM");
        }
    }
}
