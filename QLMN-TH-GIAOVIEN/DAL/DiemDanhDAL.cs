using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class DiemDanhDAL
    {
        // 1. Load danh sách (Gọi sp_DiemDanh_GetAll)
        public DataTable GetAll(string maLop, DateTime ngay)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_DiemDanh_GetAll", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaLop", maLop);
                cmd.Parameters.AddWithValue("@NgayDiemDanh", ngay);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL GetAll: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 2. Lưu - Thêm/Sửa (Gọi sp_DiemDanh_Save)
        public string Save(DiemDanh obj)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_DiemDanh_Save", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Map tham số
                cmd.Parameters.AddWithValue("@MaDiemDanh", obj.MaDiemDanh ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayDiemDanh", obj.NgayDiemDanh);
                cmd.Parameters.AddWithValue("@TrangThai", obj.TrangThai ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GhiChu", obj.GhiChu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaHocSinh", obj.MaHocSinh);
                cmd.Parameters.AddWithValue("@MaGiaoVien", obj.MaGiaoVien ?? (object)DBNull.Value);

                // Xử lý ngày giờ (Cho phép null)
                cmd.Parameters.AddWithValue("@GioDen", obj.GioDen ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GioVe", obj.GioVe ?? (object)DBNull.Value);

                // Dùng ExecuteScalar để lấy câu thông báo từ SQL (SELECT N'...')
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Lưu thành công";
            }
            catch (Exception ex) { return "Lỗi DAL Save: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 3. Xóa (Gọi sp_DiemDanh_Delete)
        public string Delete(string maDiemDanh)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_DiemDanh_Delete", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaDiemDanh", maDiemDanh);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Xóa thành công";
            }
            catch (Exception ex) { return "Lỗi DAL Delete: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }
    }
}