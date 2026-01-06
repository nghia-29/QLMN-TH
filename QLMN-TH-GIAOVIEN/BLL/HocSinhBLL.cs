using System.Data;
using DAL;
using Model;

namespace BLL;

public class HocSinhBLL
{
    private HocSinhDAL _dal = new HocSinhDAL();

    public DataTable GetAll() => _dal.GetAll();

    public DataTable GetById(string id) => _dal.GetById(id);

    public string Them(HocSinh obj)
    {
        if (string.IsNullOrEmpty(obj.MaHocSinh)) return "Lỗi: Mã học sinh không được để trống";
        if (string.IsNullOrEmpty(obj.HoTen)) return "Lỗi: Họ tên không được để trống";

        return _dal.Them(obj);
    }

    public string Sua(HocSinh obj)
    {
        if (string.IsNullOrEmpty(obj.MaHocSinh)) return "Lỗi: Mã học sinh không hợp lệ";
        return _dal.Sua(obj);
    }

    public string Xoa(string id)
    {
        if (string.IsNullOrEmpty(id)) return "Lỗi: Mã không hợp lệ";
        return _dal.Xoa(id);
    }
}