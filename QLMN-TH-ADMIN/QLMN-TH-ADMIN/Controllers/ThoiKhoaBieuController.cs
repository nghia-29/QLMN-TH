using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using System.Data;
using System.Collections.Generic; // Để dùng List
using System;

namespace QLMN_TH_ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThoiKhoaBieuController : ControllerBase
    {
        private readonly ThoiKhoaBieuBLL _bll = new ThoiKhoaBieuBLL();
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                // Gọi hàm GetAll từ BLL (cần bổ sung bên dưới)
                DataTable dt = _bll.GetAll();
                List<object> list = new List<object>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new
                    {
                        MaTkb = row["MaTKB"],
                        MaLop = row["MaLop"],
                        MaMonHoc = row["MaMonHoc"],
                        TenGiaoVien = row["TenGiaoVien"],
                        Thu = row["Thu"],
                        TietHoc = row["TietHoc"],
                        MaPhanCong = row["MaPhanCong"]
                    });
                }
                return Ok(list);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        // 2. API Lấy chi tiết
        [HttpGet("get-by-id")]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Mã TKB không hợp lệ");

            DataTable dt = _bll.GetById(id);
            if (dt == null || dt.Rows.Count == 0)
                return NotFound("Không tìm thấy thời khóa biểu");

            DataRow row = dt.Rows[0];

            // Trả về Object chi tiết
            return Ok(new
            {
                MaTkb = Convert.ToString(row["MaTKB"]),
                MaLop = Convert.ToString(row["MaLop"]),
                MaMonHoc = Convert.ToString(row["MaMonHoc"]),
                TenGiaoVien = Convert.ToString(row["TenGiaoVien"]),
                Thu = Convert.ToString(row["Thu"]),
                MaPhanCong = Convert.ToString(row["MaPhanCong"]),
                TietHoc = row["TietHoc"] != DBNull.Value ? Convert.ToInt32(row["TietHoc"]) : 0
            });
        }

        // 3. API Thêm mới
        [HttpPost("create")]
        public IActionResult Create([FromBody] ThoiKhoaBieu model)
        {
            if (model == null) return BadRequest("Dữ liệu không hợp lệ");

            // Gọi BLL để kiểm tra điều kiện và thêm
            string msg = _bll.Them(model);

            // Trả về chuỗi thông báo trực tiếp
            return Ok(msg);
        }

        // 4. API Cập nhật
        [HttpPost("update")]
        public IActionResult Update([FromBody] ThoiKhoaBieu model)
        {
            if (model == null) return BadRequest("Dữ liệu không hợp lệ");

            // Gọi BLL để kiểm tra điều kiện và sửa
            string msg = _bll.Sua(model);
            return Ok(msg);
        }

        // 5. API Xóa
        [HttpPost("delete")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Mã xóa trống");

            string msg = _bll.Xoa(id);
            return Ok(msg);
        }
    }
}