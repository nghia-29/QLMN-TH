using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using System.Data;
using System.Linq; // Cần dòng này cho LINQ
using System;
using System.Collections.Generic; // Cần dòng này cho List<>

namespace QLMN_TH_ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanCongController : ControllerBase
    {
        private readonly PhanCongBLL _bll = new PhanCongBLL();

        // 1. Lấy danh sách
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                var dt = _bll.GetAll();
                var list = new List<object>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new
                        {
                            MaPhanCong = row["MaPhanCong"],
                            // Lấy tên cột chuẩn theo SQL, dùng DBNull để tránh lỗi null
                            MaLop = row["MaLop"] != DBNull.Value ? row["MaLop"] : "",
                            MaMonHoc = row.Table.Columns.Contains("MaMonHoc") && row["MaMonHoc"] != DBNull.Value ? row["MaMonHoc"] : "",
                            NamHoc = row["NamHoc"] != DBNull.Value ? row["NamHoc"] : "",
                            HocKy = row["HocKy"] != DBNull.Value ? row["HocKy"] : "",
                            MaGiaoVien = row["MaGiaoVien"] != DBNull.Value ? row["MaGiaoVien"] : "",
                            MaHocSinh = row["MaHocSinh"] != DBNull.Value ? row["MaHocSinh"] : ""
                        });
                    }
                }
                return Ok(list);
            }
            catch (Exception ex) { return StatusCode(500, "Lỗi Server: " + ex.Message); }
        }

        // 2. Lấy chi tiết
        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var dt = _bll.GetById(id);
                if (dt == null || dt.Rows.Count == 0)
                    return NotFound("Không tìm thấy dữ liệu");

                DataRow row = dt.Rows[0];
                return Ok(new
                {
                    MaPhanCong = row["MaPhanCong"],
                    MaLop = row["MaLop"],
                    MaMonHoc = row.Table.Columns.Contains("MaMonHoc") ? row["MaMonHoc"] : "",
                    NamHoc = row["NamHoc"],
                    HocKy = row["HocKy"],
                    MaGiaoVien = row["MaGiaoVien"],
                    MaHocSinh = row.Table.Columns.Contains("MaHocSinh") ? row["MaHocSinh"] : ""
                });
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        // 3. Thêm mới (Áp dụng mô hình check msg)
        [HttpPost("create")]
        public IActionResult Create([FromBody] PhanCong model)
        {
            string msg = _bll.Them(model);
            // Kiểm tra xem chuỗi trả về có chữ "thành công" không
            if (msg.ToLower().Contains("thành công"))
                return Ok(msg);
            else
                return BadRequest(msg); // Trả về lỗi 400 để Frontend hiện màu đỏ
        }

        // 4. Cập nhật
        [HttpPost("update")]
        public IActionResult Update([FromBody] PhanCong model)
        {
            string msg = _bll.Sua(model);
            if (msg.ToLower().Contains("thành công"))
                return Ok(msg);
            else
                return BadRequest(msg);
        }

        // 5. Xóa
        [HttpPost("delete/{id}")]
        public IActionResult Delete(string id)
        {
            string msg = _bll.Xoa(id);
            if (msg.ToLower().Contains("thành công"))
                return Ok(msg);
            else
                return BadRequest(msg);
        }
    }
}