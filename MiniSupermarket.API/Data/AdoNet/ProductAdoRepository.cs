using MiniSupermarket.API.DTOs;
using MySqlConnector;

namespace MiniSupermarket.API.Data.AdoNet;

/// <summary>Tra cứu sản phẩm bằng ADO.NET thuần (MySqlConnector) cho thao tác quét mã vạch ở POS.</summary>
public class ProductAdoRepository
{
    private readonly string _connectionString;

    public ProductAdoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Thiếu ConnectionStrings:DefaultConnection trong appsettings.json");
    }

    public async Task<ProductItemDto?> GetByBarcodeAsync(string barcode)
    {
        const string sql = @"
            SELECT p.ProductId, p.Barcode, p.ProductName, p.Price, p.StockQuantity,
                   p.CategoryId, c.CategoryName, p.BrandId
            FROM Products p
            INNER JOIN Categories c ON c.CategoryId = p.CategoryId
            WHERE p.Barcode = @barcode
            LIMIT 1";

        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@barcode", barcode); // luôn dùng tham số, không nối chuỗi SQL

        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;

        return new ProductItemDto
        {
            ProductId = reader.GetInt32("ProductId"),
            Barcode = reader.GetString("Barcode"),
            ProductName = reader.GetString("ProductName"),
            Price = reader.GetDecimal("Price"),
            StockQuantity = reader.GetInt32("StockQuantity"),
            CategoryId = reader.GetInt32("CategoryId"),
            CategoryName = reader.GetString("CategoryName"),
            BrandId = reader.GetInt32("BrandId")
        };
    }
}
