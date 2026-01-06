using System;
using System.Data;
using DAL;
using Model;

namespace BLL
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL _dal = new TaiKhoanDAL();

        // --- ĐÃ XÓA HÀM LOGIN ---

        // 1. Lấy danh sách tài khoản (Để hiển thị lên bảng quản lý)
        public DataTable GetAll()
        {
            return _dal.GetAll();
        }

        // 2. Lấy chi tiết (Để hiển thị lên form sửa)
        public DataTable GetById(string id)
        {
            return _dal.GetById(id);
        }

        // 3. Thêm tài khoản mới
        public string Them(TaiKhoan obj)
        {
            // Validate dữ liệu đầu vào
            if (string.IsNullOrEmpty(obj.IDTaiKhoan) || string.IsNullOrEmpty(obj.TenTaiKhoan))
            {
                return "Lỗi: ID và Tên tài khoản không được để trống";
            }

            if (string.IsNullOrEmpty(obj.MatKhau) || obj.MatKhau.Length < 3)
            {
                return "Lỗi: Mật khẩu phải từ 3 ký tự trở lên";
            }

            return _dal.Them(obj);
        }

        // 4. Sửa tài khoản (Đổi mật khẩu, đổi quyền...)
        public string Sua(TaiKhoan obj)
        {
            if (string.IsNullOrEmpty(obj.IDTaiKhoan))
            {
                return "Lỗi: Không tìm thấy ID tài khoản cần sửa";
            }

            return _dal.Sua(obj);
        }

        // 5. Xóa tài khoản
        public string Xoa(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "Lỗi: ID không hợp lệ";
            }
            return _dal.Xoa(id);
        }
    }
}