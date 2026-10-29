using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReportStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Doanh thu theo từng ngày trong khoảng [p_from, p_to] (dùng cho biểu đồ cột)
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetDailyRevenue");
            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_GetDailyRevenue(IN p_from DATE, IN p_to DATE)
BEGIN
    SELECT CAST(o.CreatedAt AS DATE) AS RevenueDate,
           COUNT(*)                  AS TotalOrders,
           COALESCE(SUM(o.Total), 0) AS Revenue
    FROM Orders o
    WHERE o.Status = 'PAID'
      AND o.CreatedAt >= p_from
      AND o.CreatedAt < DATE_ADD(p_to, INTERVAL 1 DAY)
    GROUP BY CAST(o.CreatedAt AS DATE)
    ORDER BY RevenueDate;
END");

            // Doanh thu ca làm việc của một thu ngân trong khoảng [p_from, p_to]
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetShiftRevenue");
            migrationBuilder.Sql(@"
CREATE PROCEDURE sp_GetShiftRevenue(IN p_cashier_id INT, IN p_from DATETIME, IN p_to DATETIME)
BEGIN
    SELECT COUNT(*)                  AS TotalOrders,
           COALESCE(SUM(o.Total), 0) AS Revenue
    FROM Orders o
    WHERE o.CashierId = p_cashier_id
      AND o.Status = 'PAID'
      AND o.CreatedAt >= p_from
      AND o.CreatedAt <= p_to;
END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetShiftRevenue");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetDailyRevenue");
        }
    }
}
