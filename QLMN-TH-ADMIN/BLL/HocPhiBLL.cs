using System;
using System.Data;
using DAL;
using Model;

namespace BLL
{
    public class HocPhiBLL
    {
        private HocPhiDAL _dal = new HocPhiDAL();

        public DataTable GetAll()
        {
            return _dal.GetAll();
        }

        public DataTable GetById(string id)
        {
            return _dal.GetById(id);
        }

        // --- 1. Thêm mới (Validation chặt chẽ) ---
        public string Them(HocPhi obj)
        {
            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(obj.MaHocPhi))
                return "Lỗi: Mã học phí không được để trống";

            if (string.IsNullOrEmpty(obj.MaHocSinh))
                return "Lỗi: Vui lòng chọn học sinh cần thu phí";

            // Kiểm tra logic Tháng
            if (obj.ThangThu == null || obj.ThangThu < 1 || obj.ThangThu > 12)
                return "Lỗi: Tháng thu không hợp lệ (Phải từ 1 đến 12)";

            // Kiểm tra Tiền (Không được âm)
            if (obj.TongTien == null || obj.TongTien < 0)
                return "Lỗi: Tổng tiền không hợp lệ (Phải lớn hơn hoặc bằng 0)";

            return _dal.Them(obj);
        }

        // --- 2. Cập nhật / Thanh toán (Validation chặt chẽ) ---
        public string CapNhat(HocPhi obj)
        {
            // Kiểm tra khóa chính
            if (string.IsNullOrEmpty(obj.MaHocPhi))
                return "Lỗi: Không tìm thấy Mã học phí cần cập nhật";

            // Kiểm tra lại tiền khi sửa (đề phòng sửa thành số âm)
            if (obj.TongTien != null && obj.TongTien < 0)
                return "Lỗi: Tổng tiền không được là số âm";

            // Kiểm tra trạng thái
            if (string.IsNullOrEmpty(obj.TrangThai))
                return "Lỗi: Trạng thái không được để trống (Ví dụ: Đã đóng/Chưa đóng)";

            return _dal.CapNhat(obj);
        }

        // --- 3. Xóa ---
        public string Xoa(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Lỗi: Mã học phí cần xóa không hợp lệ";

            return _dal.Xoa(id);
        }
    }
}