using System.Data;
using DAL;
using Model;
using System;

namespace BLL
{
    public class ThoiKhoaBieuBLL
    {
        private ThoiKhoaBieuDAL _dal = new ThoiKhoaBieuDAL();

        // 1. Lấy tất cả
        public DataTable GetAll()
        {
            return _dal.GetAll();
        }

        // 2. Lấy chi tiết
        public DataTable GetById(string id)
        {
            return _dal.GetById(id);
        }

        // 3. Thêm mới
        public string Them(ThoiKhoaBieu obj)
        {
            // Kiểm tra mã
            if (string.IsNullOrEmpty(obj.MaTkb))
                return "Lỗi: Mã TKB không được để trống";

            return _dal.Them(obj);
        }

        // 4. Cập nhật
        public string Sua(ThoiKhoaBieu obj)
        {
            if (string.IsNullOrEmpty(obj.MaTkb))
                return "Lỗi: Không tìm thấy Mã TKB cần sửa";

            return _dal.Sua(obj);
        }

        // 5. Xóa
        public string Xoa(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Lỗi: Mã cần xóa không hợp lệ";

            return _dal.Xoa(id);
        }
    }
}