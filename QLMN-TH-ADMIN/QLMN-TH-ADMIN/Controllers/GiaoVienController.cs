using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using System.Data;
using System.Linq; // Cần thiết để dùng LINQ
using System;

namespace QLMN_TH_ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaoVienController : ControllerBase
    {
        private readonly GiaoVienBLL _bll = new GiaoVienBLL();

        // 1. Lấy danh sách
        // URL: GET /api/GiaoVien/get-all
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                var dt = _bll.GetAll();

                // Sử dụng LINQ để map dữ liệu nhanh và gọn
                var data = dt.AsEnumerable().Select(row => new
                {
                    MaGiaoVien = row["MaGiaoVien"],
                    HoTen = row["HoTen"] != DBNull.Value ? row["HoTen"] : "",
                    NgaySinh = row["NgaySinh"] != DBNull.Value ? row["NgaySinh"] : null,
                    Sdt = row["SDT"] != DBNull.Value ? row["SDT"] : "",
                    ChuyenMon = row["ChuyenMon"] != DBNull.Value ? row["ChuyenMon"] : "",
                    BangCap = row["BangCap"] != DBNull.Value ? row["BangCap"] : "",
                    Email = row["Email"] != DBNull.Value ? row["Email"] : ""
                }).ToList();

                // Trả về trực tiếp mảng JSON
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi hệ thống: " + ex.Message);
            }
        }

        // 2. Lấy chi tiết
        // URL: GET /api/GiaoVien/get-by-id?id=GV01
        [HttpGet("get-by-id")]
        public IActionResult GetById(string id)
        {
            var dt = _bll.GetById(id);
            if (dt == null || dt.Rows.Count == 0)
                return NotFound("Không tìm thấy giáo viên");

            DataRow row = dt.Rows[0];

            // Trả về trực tiếp Object
            return Ok(new
            {
                MaGiaoVien = row["MaGiaoVien"],
                HoTen = row["HoTen"] != DBNull.Value ? row["HoTen"] : "",
                NgaySinh = row["NgaySinh"] != DBNull.Value ? row["NgaySinh"] : null,
                Sdt = row["SDT"] != DBNull.Value ? row["SDT"] : "",
                ChuyenMon = row["ChuyenMon"] != DBNull.Value ? row["ChuyenMon"] : "",
                BangCap = row["BangCap"] != DBNull.Value ? row["BangCap"] : "",
                Email = row["Email"] != DBNull.Value ? row["Email"] : ""
            });
        }

        // 3. Thêm mới
        // URL: POST /api/GiaoVien/create
        [HttpPost("create")]
        public IActionResult Create([FromBody] GiaoVien model)
        {
            string msg = _bll.Them(model);
            // Trả về trực tiếp chuỗi thông báo
            return Ok(msg);
        }

        // 4. Cập nhật
        // URL: POST /api/GiaoVien/update
        [HttpPost("update")]
        public IActionResult Update([FromBody] GiaoVien model)
        {
            string msg = _bll.Sua(model);
            // Trả về trực tiếp chuỗi thông báo
            return Ok(msg);
        }

        // 5. Xóa
        // URL: POST /api/GiaoVien/delete?id=GV01
        [HttpPost("delete")]
        public IActionResult Delete(string id)
        {
            string msg = _bll.Xoa(id);
            // Trả về trực tiếp chuỗi thông báo
            return Ok(msg);
        }
    }
}