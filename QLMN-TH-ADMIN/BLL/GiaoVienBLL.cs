using System;
using System.Data;
using DAL; // Namespace chứa GiaoVienDAL
using Model; // Namespace chứa GiaoVien

namespace BLL; // Hoặc AdminApi.BLL

public class GiaoVienBLL
{
    private GiaoVienDAL _dal = new GiaoVienDAL();

    public DataTable GetAll() => _dal.GetAll();

    public DataTable GetById(string id) => _dal.GetById(id);

    public string Them(GiaoVien obj)
    {
        // Kiểm tra dữ liệu đầu vào
        if (string.IsNullOrEmpty(obj.MaGiaoVien)) return "Lỗi: Mã giáo viên không được để trống";
        if (string.IsNullOrEmpty(obj.HoTen)) return "Lỗi: Họ tên không được để trống";

        return _dal.Them(obj);
    }

    public string Sua(GiaoVien obj)
    {
        if (string.IsNullOrEmpty(obj.MaGiaoVien)) return "Lỗi: Mã giáo viên không được để trống";
        return _dal.Sua(obj);
    }

    public string Xoa(string id)
    {
        if (string.IsNullOrEmpty(id)) return "Lỗi: Mã không hợp lệ";
        return _dal.Xoa(id);
    }
}