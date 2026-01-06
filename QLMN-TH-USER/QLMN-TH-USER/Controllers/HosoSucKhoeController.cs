using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using System.Data;
using System.Collections.Generic;
using System;

namespace QLMN_TH_ADMIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HosoSucKhoeController : ControllerBase
    {
        private readonly HosoSucKhoeBLL _bll = new HosoSucKhoeBLL();

        // 1. API Xem toàn bộ danh sách
        // URL: GET /api/HosoSucKhoe/get-all
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            try
            {
                // KHÔNG DÙNG VAR
                DataTable dt = _bll.GetAll();
                List<object> listResult = new List<object>();

                foreach (DataRow row in dt.Rows)
                {
                    listResult.Add(new
                    {
                        MaPhieuKham = Convert.ToString(row["MaPhieuKham"]),

                        // Format ngày: dd/MM/yyyy (Ví dụ: 25/10/2023)
                        NgayKham = row["NgayKham"] != DBNull.Value ? Convert.ToDateTime(row["NgayKham"]).ToString("dd/MM/yyyy") : "",

                        // Xử lý số thực
                        ChieuCao = row["ChieuCao"] != DBNull.Value ? Convert.ToDouble(row["ChieuCao"]) : 0.0,
                        CanNang = row["CanNang"] != DBNull.Value ? Convert.ToDouble(row["CanNang"]) : 0.0,

                        TinhTrangSucKhoe = Convert.ToString(row["TinhTrangSucKhoe"]),
                        GhiChu = Convert.ToString(row["GhiChu"]),
                        MaHocSinh = Convert.ToString(row["MaHocSinh"]),
                        TenHocSinh = row.Table.Columns.Contains("TenHocSinh") ? Convert.ToString(row["TenHocSinh"]) : ""
                    });
                }
                return Ok(listResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi: " + ex.Message);
            }
        }

        // 2. API Xem theo Mã Học Sinh
        // URL: GET /api/HosoSucKhoe/get-by-hs?maHS=HS01
        [HttpGet("get-by-hs")]
        public IActionResult GetByHocSinh(string maHS)
        {
            try
            {
                if (string.IsNullOrEmpty(maHS))
                    return BadRequest("Vui lòng truyền Mã học sinh");

                DataTable dt = _bll.GetByHocSinh(maHS);
                List<object> listResult = new List<object>();

                foreach (DataRow row in dt.Rows)
                {
                    listResult.Add(new
                    {
                        MaPhieuKham = Convert.ToString(row["MaPhieuKham"]),
                        NgayKham = row["NgayKham"] != DBNull.Value ? Convert.ToDateTime(row["NgayKham"]).ToString("dd/MM/yyyy") : "",
                        ChieuCao = row["ChieuCao"] != DBNull.Value ? Convert.ToDouble(row["ChieuCao"]) : 0.0,
                        CanNang = row["CanNang"] != DBNull.Value ? Convert.ToDouble(row["CanNang"]) : 0.0,
                        TinhTrangSucKhoe = Convert.ToString(row["TinhTrangSucKhoe"]),
                        GhiChu = Convert.ToString(row["GhiChu"]),
                        MaHocSinh = Convert.ToString(row["MaHocSinh"]),
                        TenHocSinh = row.Table.Columns.Contains("TenHocSinh") ? Convert.ToString(row["TenHocSinh"]) : ""
                    });
                }
                return Ok(listResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi: " + ex.Message);
            }
        }

        // 3. API Xem chi tiết 1 phiếu
        // URL: GET /api/HosoSucKhoe/get-by-id?id=SK01
        [HttpGet("get-by-id")]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Mã phiếu khám trống");

            DataTable dt = _bll.GetById(id);
            if (dt == null || dt.Rows.Count == 0)
                return NotFound("Không tìm thấy phiếu khám");

            DataRow row = dt.Rows[0];
            return Ok(new
            {
                MaPhieuKham = Convert.ToString(row["MaPhieuKham"]),
                NgayKham = row["NgayKham"] != DBNull.Value ? Convert.ToDateTime(row["NgayKham"]).ToString("dd/MM/yyyy") : "",
                ChieuCao = row["ChieuCao"] != DBNull.Value ? Convert.ToDouble(row["ChieuCao"]) : 0.0,
                CanNang = row["CanNang"] != DBNull.Value ? Convert.ToDouble(row["CanNang"]) : 0.0,
                TinhTrangSucKhoe = Convert.ToString(row["TinhTrangSucKhoe"]),
                GhiChu = Convert.ToString(row["GhiChu"]),
                MaHocSinh = Convert.ToString(row["MaHocSinh"])
            });
        }
    }
}