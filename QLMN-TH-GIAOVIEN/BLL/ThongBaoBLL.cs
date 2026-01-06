using System.Data;
using DAL;
using Model;
using System;

namespace BLL
{
    public class ThongBaoBLL
    {
        private ThongBaoDAL _dal = new ThongBaoDAL();

        public DataTable GetAll() => _dal.GetAll();
        public DataTable GetById(string id) => _dal.GetById(id);

        public string Them(ThongBao obj)
        {
            // --- VALIDATION ---
            if (string.IsNullOrEmpty(obj.MaThongBao))
                return "Lỗi: Mã thông báo không được để trống";

            if (string.IsNullOrEmpty(obj.TieuDe))
                return "Lỗi: Tiêu đề không được để trống";

            if (string.IsNullOrEmpty(obj.NoiDung))
                return "Lỗi: Nội dung thông báo không được để trống";

            if (string.IsNullOrEmpty(obj.NguoiGui))
                return "Lỗi: Chưa xác định người gửi";

            return _dal.Them(obj);
        }

        public string Xoa(string id)
        {
            if (string.IsNullOrEmpty(id)) return "Lỗi: Mã không hợp lệ";
            return _dal.Xoa(id);
        }
    }
}