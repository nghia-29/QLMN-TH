using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongBaoController : ControllerBase
    {
        private readonly IThongBaoBusiness _bus;

        public ThongBaoController(IThongBaoBusiness bus)
        {
            _bus = bus;
        }

        // 1. Lấy danh sách (Giữ nguyên GET)
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_bus.GetAll());
        }

        // 2. Thêm mới (Giữ nguyên POST)
        [HttpPost("create")]
        public IActionResult Create([FromBody] ThongBao model)
        {
            var result = _bus.Create(model);
            return Ok(new { success = result, message = result ? "Thành công" : "Thất bại" });
        }

        // 3. Cập nhật (Đổi từ PUT sang POST)
        [HttpPost("update")]
        public IActionResult Update([FromBody] ThongBao model)
        {
            var result = _bus.Update(model);
            return Ok(new { success = result, message = result ? "Sửa thành công" : "Không tìm thấy ID" });
        }

        // 4. Xóa (Đổi từ DELETE sang POST)
        // Client sẽ gọi: POST /api/ThongBao/delete/TB001
        [HttpPost("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _bus.Delete(id);
            return Ok(new { success = result, message = result ? "Xóa thành công" : "Không tìm thấy ID" });
        }
    }
}