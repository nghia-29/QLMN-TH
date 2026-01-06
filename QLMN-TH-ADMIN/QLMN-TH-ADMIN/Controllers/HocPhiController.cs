using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using System.Data;
using System.Linq; // Cần thiết cho LINQ
using System;

namespace QLMN_TH_ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocPhiController : ControllerBase
    {
        private readonly HocPhiBLL _bll = new HocPhiBLL();

        // 1. Lấy danh sách phiếu thu
        // URL: GET /api/HocPhi/get-all
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                var dt = _bll.GetAll();

                // Dùng LINQ để map dữ liệu nhanh chóng
                var data = dt.AsEnumerable().Select(row => new
                {
                    MaHocPhi = row["MaHocPhi"],
                    ThangThu = row["ThangThu"] != DBNull.Value ? Convert.ToInt32(row["ThangThu"]) : 0,
                    TongTien = row["TongTien"] != DBNull.Value ? Convert.ToDecimal(row["TongTien"]) : 0,
                    TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"] : "Chưa đóng",

                    // Ngày thanh toán (Nullable)
                    NgayThanhToan = row["NgayThanhToan"] != DBNull.Value ? row["NgayThanhToan"] : null,
                    HinhThuc = row["HinhThuc"] != DBNull.Value ? row["HinhThuc"] : "",
                    MaHocSinh = row["MaHocSinh"],

                    // Lấy tên học sinh nếu có (An toàn hơn)
                    HoTenHocSinh = row.Table.Columns.Contains("HoTen") && row["HoTen"] != DBNull.Value
                                   ? row["HoTen"] : ""
                }).ToList();

                // Trả về trực tiếp danh sách mảng JSON
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi hệ thống: " + ex.Message);
            }
        }

        // 2. Lấy chi tiết
        // URL: GET /api/HocPhi/get-by-id?id=HP01
        [HttpGet("get-by-id")]
        public IActionResult GetById(string id)
        {
            var dt = _bll.GetById(id);
            if (dt == null || dt.Rows.Count == 0)
                return NotFound("Không tìm thấy phiếu thu");

            var row = dt.Rows[0];
            // Trả về trực tiếp Object
            return Ok(new
            {
                MaHocPhi = row["MaHocPhi"],
                ThangThu = row["ThangThu"] != DBNull.Value ? Convert.ToInt32(row["ThangThu"]) : 0,
                TongTien = row["TongTien"] != DBNull.Value ? Convert.ToDecimal(row["TongTien"]) : 0,
                TrangThai = row["TrangThai"] != DBNull.Value ? row["TrangThai"] : "Chưa đóng",
                NgayThanhToan = row["NgayThanhToan"] != DBNull.Value ? row["NgayThanhToan"] : null,
                HinhThuc = row["HinhThuc"] != DBNull.Value ? row["HinhThuc"] : "",
                MaHocSinh = row["MaHocSinh"]
            });
        }

        // 3. Tạo phiếu thu mới (Phiên bản không check lỗi)
        // URL: POST /api/HocPhi/create
        [HttpPost("create")]
        public IActionResult Create([FromBody] HocPhi model)
        {
            try
            {
                // BỎ HẾT CÁC ĐOẠN IF KIỂM TRA
                // Cứ có dữ liệu là đẩy thẳng xuống Database luôn

                string msg = _bll.Them(model);

                return Ok(msg);
            }
            catch (Exception ex)
            {
                // Vẫn nên giữ try-catch này.
                // Vì nếu bạn nhập "linh tinh" quá (ví dụ Mã bị trùng) thì SQL sẽ báo lỗi.
                // Hàm này giúp hiện lỗi đó ra cho bạn biết thay vì sập web.
                return StatusCode(500, "Lỗi SQL: " + ex.Message);
            }
        }

        // 4. Xác nhận đóng tiền
        // URL: POST /api/HocPhi/pay
        [HttpPost("pay")]
        public IActionResult Pay([FromBody] HocPhi model)
        {
            // Logic tự động điền dữ liệu nếu thiếu
            if (string.IsNullOrEmpty(model.TrangThai))
                model.TrangThai = "Đã đóng";

            if (model.NgayThanhToan == null)
                model.NgayThanhToan = DateTime.Now;

            string msg = _bll.CapNhat(model);
            // Trả về trực tiếp chuỗi thông báo
            return Ok(msg);
        }

        // 5. Xóa
        // URL: POST /api/HocPhi/delete?id=HP01
        [HttpPost("delete")]
        public IActionResult Delete(string id)
        {
            string msg = _bll.Xoa(id);
            // Trả về trực tiếp chuỗi thông báo
            return Ok(msg);
        }
    }
}