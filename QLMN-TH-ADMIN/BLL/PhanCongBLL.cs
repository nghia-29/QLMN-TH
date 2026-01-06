using System;
using System.Data;
using DAL;   // Namespace chứa PhanCongDAL
using Model; // Namespace chứa Model PhanCong

namespace BLL;

public class PhanCongBLL
{
    private PhanCongDAL _dal = new PhanCongDAL();

    // 1. Lấy danh sách
    public DataTable GetAll()
    {
        return _dal.GetAll();
    }

    // 2. Lấy chi tiết theo ID
    public DataTable GetById(string id)
    {
        return _dal.GetById(id);
    }

    // 3. Thêm mới
    public string Them(PhanCong obj)
    {
        // Kiểm tra dữ liệu đầu vào
        if (string.IsNullOrEmpty(obj.MaPhanCong))
        {
            return "Lỗi: Mã phân công không được để trống";
        }

        if (string.IsNullOrEmpty(obj.MaGiaoVien) || string.IsNullOrEmpty(obj.MaHocSinh))
        {
            return "Lỗi: Phải chọn Giáo viên và Học sinh";
        }

        return _dal.Them(obj);
    }

    // 4. Cập nhật
    public string Sua(PhanCong obj)
    {
        if (string.IsNullOrEmpty(obj.MaPhanCong))
        {
            return "Lỗi: Mã phân công không hợp lệ";
        }
        return _dal.Sua(obj);
    }

    // 5. Xóa
    public string Xoa(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return "Lỗi: Mã phân công không được để trống";
        }
        return _dal.Xoa(id);
    }
}