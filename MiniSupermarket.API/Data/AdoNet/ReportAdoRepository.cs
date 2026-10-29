using System.Data;
using MiniSupermarket.API.DTOs;
using MySqlConnector;

namespace MiniSupermarket.API.Data.AdoNet;

/// <summary>Gọi các stored procedure báo cáo (tạo trong migration AddReportStoredProcedures).</summary>
public class ReportAdoRepository
{
    private readonly string _connectionString;

    public ReportAdoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Thiếu ConnectionStrings:DefaultConnection trong appsettings.json");
    }

    /// <summary>sp_GetShiftRevenue: số đơn và doanh thu của một thu ngân trong khoảng thời gian (ca làm việc).</summary>
    public async Task<(int TotalOrders, decimal Revenue)> GetShiftRevenueAsync(int cashierId, DateTime from, DateTime to)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand("sp_GetShiftRevenue", connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("p_cashier_id", cashierId);
        command.Parameters.AddWithValue("p_from", from);
        command.Parameters.AddWithValue("p_to", to);

        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return (0, 0m);

        return (Convert.ToInt32(reader["TotalOrders"]), Convert.ToDecimal(reader["Revenue"]));
    }
}
