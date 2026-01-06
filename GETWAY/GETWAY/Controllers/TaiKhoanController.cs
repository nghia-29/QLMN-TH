using Microsoft.AspNetCore.Mvc;
using BLL;
using Model;
using System.Data;
using System;

namespace GETWAY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly TaiKhoanBLL _bll = new TaiKhoanBLL();

        [HttpPost("login")]
        public IActionResult Login([FromBody] Dangnhap model)
        {
            try
            {
                // ===> CHÈN ĐOẠN DEBUG NÀY VÀO <===
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("🔴 CHECK SERVER NHẬN DỮ LIỆU:");
                Console.WriteLine("- Tài khoản nhận được: " + model.TenTaiKhoan);
                Console.WriteLine("- Mật khẩu nhận được:  " + model.MatKhau);
                Console.WriteLine("---------------------------------------------");

                // Code cũ của bạn vẫn giữ nguyên ở dưới
                var dt = _bll.CheckLogin(model.TenTaiKhoan, model.MatKhau);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return Ok(new
                    {
                        IDTaiKhoan = row["IDTaiKhoan"].ToString(),
                        TenTaiKhoan = row["TenTaiKhoan"].ToString(),
                        MaLoai = row["MaLoai"].ToString(),
                        TenLoai = row["TenLoai"].ToString()
                    });
                }

                // In ra để biết nếu bị sai pass
                Console.WriteLine("🔴 KẾT QUẢ SQL: Không tìm thấy dòng nào (Sai TK hoặc MK)");
                return Ok(new { Success = false, Message = "Sai tài khoản hoặc mật khẩu" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔴 LỖI CODE: " + ex.Message);
                return StatusCode(500, "Lỗi Server: " + ex.Message);
            }
        }
    }
}