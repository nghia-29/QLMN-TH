using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class ThongBaoDAL
    {
        // 1. Lấy danh sách
        public DataTable GetAll()
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_ThongBao_GetAll", DatabaseConnect.Conn);
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
                SqlCommand cmd = new SqlCommand("sp_ThongBao_GetById", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaThongBao", id);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 3. Gửi thông báo
        public string Them(ThongBao obj)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_ThongBao_Them", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaThongBao", obj.MaThongBao);
                cmd.Parameters.AddWithValue("@TieuDe", obj.TieuDe ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NoiDung", obj.NoiDung ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NguoiGui", obj.NguoiGui ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NguoiNhan", obj.NguoiNhan ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiThongBao", obj.LoaiThongBao ?? (object)DBNull.Value);

                // Khóa ngoại (Cho phép null nếu thông báo chung)
                cmd.Parameters.AddWithValue("@MaGiaoVien", obj.MaGiaoVien ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaHocSinh", obj.MaHocSinh ?? (object)DBNull.Value);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Thành công";
            }
            catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 4. Xóa
        public string Xoa(string id)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_ThongBao_Xoa", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaThongBao", id);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Xóa thành công";
            }
            catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }
    }
}