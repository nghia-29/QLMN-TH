using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class HocSinhDAL
    {
        // 1. Lấy danh sách
        public DataTable GetAll()
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_HocSinh_GetAll", DatabaseConnect.Conn);
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
                SqlCommand cmd = new SqlCommand("sp_HocSinh_GetById", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHocSinh", id);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 3. Thêm mới
        public string Them(HocSinh obj)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_HocSinh_Them", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Map tham số và xử lý NULL
                cmd.Parameters.AddWithValue("@MaHocSinh", obj.MaHocSinh);
                cmd.Parameters.AddWithValue("@HoTen", obj.HoTen ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgaySinh", obj.NgaySinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiTinh", obj.GioiTinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", obj.DiaChi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaLop", obj.MaLop ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PhuHuynh", obj.PhuHuynh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SDTLienHe", obj.SdtlienHe ?? (object)DBNull.Value);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Thêm thành công";
            }
            catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 4. Sửa
        public string Sua(HocSinh obj)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_HocSinh_Sua", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaHocSinh", obj.MaHocSinh);
                cmd.Parameters.AddWithValue("@HoTen", obj.HoTen ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgaySinh", obj.NgaySinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiTinh", obj.GioiTinh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", obj.DiaChi ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaLop", obj.MaLop ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PhuHuynh", obj.PhuHuynh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SDTLienHe", obj.SdtlienHe ?? (object)DBNull.Value);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Cập nhật thành công";
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
                SqlCommand cmd = new SqlCommand("sp_HocSinh_Xoa", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHocSinh", id);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Xóa thành công";
            }
            catch (Exception ex) { return "Lỗi (Có thể do ràng buộc dữ liệu): " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }
    }
}