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
    public class BangDiemController : ControllerBase
    {
        private readonly BangDiemBLL _bll = new BangDiemBLL();

        // 1. Lấy danh sách toàn bộ
        // URL: GET /api/BangDiem/get-all
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
                        MaBangDiem = Convert.ToString(row["MaBangDiem"]),
                        MaMonHoc = Convert.ToString(row["MaMonHoc"]),
                        NamHoc = Convert.ToString(row["NamHoc"]),
                        HocKy = row["HocKy"] != DBNull.Value ? Convert.ToInt32(row["HocKy"]) : 0,
                        DiemSo = row["DiemSo"] != DBNull.Value ? Convert.ToDouble(row["DiemSo"]) : 0.0,
                        NhanXet = Convert.ToString(row["NhanXet"]),
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

        // 2. Tìm kiếm điểm theo học sinh
        // URL: GET /api/BangDiem/get-by-hs?maHS=HS01
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
                        MaBangDiem = Convert.ToString(row["MaBangDiem"]),
                        MaMonHoc = Convert.ToString(row["MaMonHoc"]),
                        NamHoc = Convert.ToString(row["NamHoc"]),
                        HocKy = row["HocKy"] != DBNull.Value ? Convert.ToInt32(row["HocKy"]) : 0,
                        DiemSo = row["DiemSo"] != DBNull.Value ? Convert.ToDouble(row["DiemSo"]) : 0.0,
                        NhanXet = Convert.ToString(row["NhanXet"]),
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

        // 3. Lấy chi tiết bảng điểm
        // URL: GET /api/BangDiem/get-by-id?id=BD01
        [HttpGet("get-by-id")]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Mã bảng điểm trống");

            DataTable dt = _bll.GetById(id);
            if (dt == null || dt.Rows.Count == 0)
                return NotFound("Không tìm thấy bảng điểm");

            DataRow row = dt.Rows[0];
            return Ok(new
            {
                MaBangDiem = Convert.ToString(row["MaBangDiem"]),
                MaMonHoc = Convert.ToString(row["MaMonHoc"]),
                NamHoc = Convert.ToString(row["NamHoc"]),
                HocKy = row["HocKy"] != DBNull.Value ? Convert.ToInt32(row["HocKy"]) : 0,
                DiemSo = row["DiemSo"] != DBNull.Value ? Convert.ToDouble(row["DiemSo"]) : 0.0,
                NhanXet = Convert.ToString(row["NhanXet"]),
                MaHocSinh = Convert.ToString(row["MaHocSinh"])
            });
        }
    }
}