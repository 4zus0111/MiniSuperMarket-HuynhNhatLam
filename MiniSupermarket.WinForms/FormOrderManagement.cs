using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormOrderManagement : Form
    {
        private readonly HttpClient _http = SessionManager.ApiClientService.Client;
        private const string API_URL = "https://localhost:7158/api/orders";

        // Biến lưu ID của đơn hàng đang được chọn
        private int _selectedOrderId = 0;

        public FormOrderManagement()
        {
            InitializeComponent();
        }

        private async void FormOrderManagement_Load(object sender, EventArgs e)
        {
            await LoadOrders();
        }

        // Tải danh sách đơn hàng
        private async System.Threading.Tasks.Task LoadOrders()
        {
            try
            {
                var orders = await _http.GetFromJsonAsync<List<OrderDto>>(API_URL);
                dgvOrders.DataSource = orders;

                // Ẩn cột OrderDetails đi cho lưới gọn gàng
                if (dgvOrders.Columns["OrderDetails"] != null)
                {
                    dgvOrders.Columns["OrderDetails"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
        }

        // Xử lý khi CLICK vào 1 Đơn hàng -> Sẽ hiển thị Chi tiết của đơn đó xuống Grid phía dưới
        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy ID Đơn hàng
                _selectedOrderId = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["OrderId"].Value);

                // Lấy data Order nguyên bản từ dòng được chọn
                var selectedOrder = (OrderDto)dgvOrders.Rows[e.RowIndex].DataBoundItem;

                // Hiển thị danh sách các món hàng (OrderDetails) xuống lưới thứ 2
                dgvOrderDetails.DataSource = selectedOrder.OrderDetails;
            }
        }

        // Nút Duyệt Đơn
        private async void btnApprove_Click(object sender, EventArgs e)
        {
            await UpdateOrderStatus("Đã duyệt");
        }

        // Nút Hủy Đơn
        private async void btnCancel_Click(object sender, EventArgs e)
        {
            await UpdateOrderStatus("Đã hủy");
        }

        // Hàm dùng chung để Cập nhật Trạng Thái Đơn Hàng
        private async System.Threading.Tasks.Task UpdateOrderStatus(string status)
        {
            if (_selectedOrderId == 0)
            {
                MessageBox.Show("Vui lòng click chọn một đơn hàng trên lưới!");
                return;
            }

            // Gửi chữ status lên API dưới dạng chuỗi (JSON string)
            var response = await _http.PutAsJsonAsync($"{API_URL}/{_selectedOrderId}/status", status);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"Đã chuyển trạng thái đơn hàng thành: {status}");
                await LoadOrders();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }
    }

    // Các class phụ trợ DTO dùng cho Form Đơn Hàng
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        // Thuộc tính này sẽ chứa luôn danh sách sản phẩm bên trong đơn
        public List<OrderDetailDto> OrderDetails { get; set; }
    }

    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}