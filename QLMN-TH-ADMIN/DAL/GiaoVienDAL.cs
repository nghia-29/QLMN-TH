using System;
using System.Data;
using System.Data.SqlClient;
using Model; // Nhớ sửa thành namespace AdminApi.Model nếu cần

namespace DAL; // Hoặc namespace AdminApi.DAL

public class GiaoVienDAL
{
    // 1. Lấy danh sách
    public DataTable GetAll()
    {
        try
        {
            DatabaseConnect.OpenDatabase();
            SqlCommand cmd = new SqlCommand("sp_GiaoVien_DanhSach", DatabaseConnect.Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
        finally { DatabaseConnect.CloseDatabase(); }
    }

    // 2. Lấy chi tiết
    public DataTable GetById(string id)
    {
        try
        {
            DatabaseConnect.OpenDatabase();
            SqlCommand cmd = new SqlCommand("sp_GiaoVien_ChiTiet", DatabaseConnect.Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaGiaoVien", id);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
        finally { DatabaseConnect.CloseDatabase(); }
    }

    // 3. Thêm mới
    public string Them(GiaoVien obj)
    {
        try
        {
            DatabaseConnect.OpenDatabase();
            SqlCommand cmd = new SqlCommand("sp_GiaoVien_Them", DatabaseConnect.Conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Truyền tham số (Xử lý null để không bị lỗi SQL)
            cmd.Parameters.AddWithValue("@MaGiaoVien", obj.MaGiaoVien);
            cmd.Parameters.AddWithValue("@HoTen", obj.HoTen ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NgaySinh", obj.NgaySinh ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SDT", obj.Sdt ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ChuyenMon", obj.ChuyenMon ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@BangCap", obj.BangCap ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", obj.Email ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery(); // Thực thi lệnh Insert
            return "Thêm thành công";
        }
        catch (SqlException ex)
        {
            if (ex.Message.Contains("PRIMARY KEY")) return "Lỗi: Mã giáo viên đã tồn tại";
            return "Lỗi SQL: " + ex.Message;
        }
        catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
        finally { DatabaseConnect.CloseDatabase(); }
    }

    // 4. Sửa
    public string Sua(GiaoVien obj)
    {
        try
        {
            DatabaseConnect.OpenDatabase();
            SqlCommand cmd = new SqlCommand("sp_GiaoVien_Sua", DatabaseConnect.Conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaGiaoVien", obj.MaGiaoVien);
            cmd.Parameters.AddWithValue("@HoTen", obj.HoTen ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NgaySinh", obj.NgaySinh ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SDT", obj.Sdt ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ChuyenMon", obj.ChuyenMon ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@BangCap", obj.BangCap ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", obj.Email ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();
            return "Cập nhật thành công";
        }
        catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
        finally { DatabaseConnect.CloseDatabase(); }
    }

    // 5. Xóa
    public string Xoa(string id)
    {
        try
        {
            DatabaseConnect.OpenDatabase();
            SqlCommand cmd = new SqlCommand("sp_GiaoVien_Xoa", DatabaseConnect.Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaGiaoVien", id);

            cmd.ExecuteNonQuery();
            return "Xóa thành công";
        }
        catch (Exception ex) { return "Lỗi (Có thể do ràng buộc dữ liệu): " + ex.Message; }
        finally { DatabaseConnect.CloseDatabase(); }
    }
}