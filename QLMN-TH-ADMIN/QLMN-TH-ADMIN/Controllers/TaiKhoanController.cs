using BLL;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Data;
using System.Linq;
using System; // Thêm namespace này để dùng DBNull

namespace QLMN_TH_ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly TaiKhoanBLL _bll = new TaiKhoanBLL();

        // 1. Lấy danh sách
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                var dt = _bll.GetAll();

                var data = dt.AsEnumerable().Select(row => new
                {
                    IDTaiKhoan = row["IDTaiKhoan"],
                    TenTaiKhoan = row["TenTaiKhoan"],
                    MaLoai = row["MaLoai"],
                    TenLoai = row.Table.Columns.Contains("TenLoai") && row["TenLoai"] != DBNull.Value
                              ? row["TenLoai"] : ""
                }).ToList();

                return Ok(data);
            }
            catch
            {
                return StatusCode(500, "Lỗi Server");
            }
        }

        // 2. Lấy chi tiết
        [HttpGet("get-by-id")]
        public IActionResult GetById(string id)
        {
            var dt = _bll.GetById(id);

            if (dt == null || dt.Rows.Count == 0)
                return NotFound();

            var row = dt.Rows[0];

            return Ok(new
            {
                IDTaiKhoan = row["IDTaiKhoan"],
                TenTaiKhoan = row["TenTaiKhoan"],
                MatKhau = row["MatKhau"],
                MaLoai = row["MaLoai"]
            });
        }

        // --- ĐÃ XÓA API LOGIN ---

        // 3. Các hàm Thêm/Sửa/Xóa
        [HttpPost("create")]
        public IActionResult Create([FromBody] TaiKhoan model)
        {
            return Ok(_bll.Them(model));
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] TaiKhoan model)
        {
            return Ok(_bll.Sua(model));
        }

        [HttpPost("delete")]
        public IActionResult Delete(string id)
        {
            return Ok(_bll.Xoa(id));
        }
    }
}