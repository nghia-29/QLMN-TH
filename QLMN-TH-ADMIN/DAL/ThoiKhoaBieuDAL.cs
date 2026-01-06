using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class ThoiKhoaBieuDAL
    {
        // 1. Lấy tất cả danh sách
        public DataTable GetAll()
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_ThoiKhoaBieu_GetAll", DatabaseConnect.Conn);
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
                SqlCommand cmd = new SqlCommand("sp_ThoiKhoaBieu_GetById", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTKB", id);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 3. Thêm mới
        public string Them(ThoiKhoaBieu obj)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_ThoiKhoaBieu_Them", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Map dữ liệu khớp với Controller của bạn
                cmd.Parameters.AddWithValue("@MaTKB", obj.MaTkb);
                cmd.Parameters.AddWithValue("@MaLop", obj.MaLop ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaMonHoc", obj.MaMonHoc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TenGiaoVien", obj.TenGiaoVien ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Thu", obj.Thu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TietHoc", obj.TietHoc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaPhanCong", obj.MaPhanCong ?? (object)DBNull.Value);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Thêm thành công";
            }
            catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 4. Cập nhật
        public string Sua(ThoiKhoaBieu obj)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_ThoiKhoaBieu_Sua", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaTKB", obj.MaTkb);
                cmd.Parameters.AddWithValue("@MaLop", obj.MaLop ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaMonHoc", obj.MaMonHoc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TenGiaoVien", obj.TenGiaoVien ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Thu", obj.Thu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TietHoc", obj.TietHoc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaPhanCong", obj.MaPhanCong ?? (object)DBNull.Value);

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
                SqlCommand cmd = new SqlCommand("sp_ThoiKhoaBieu_Xoa", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaTKB", id);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Xóa thành công";
            }
            catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }
    }
}