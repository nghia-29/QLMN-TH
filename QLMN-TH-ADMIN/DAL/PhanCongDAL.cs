using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class PhanCongDAL
    {
        // 1. Lấy danh sách (Giữ nguyên)
        public DataTable GetAll()
        {
            DataTable dt = new DataTable();
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_PhanCong_GetAll", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch { }
            finally { DatabaseConnect.CloseDatabase(); }
            return dt;
        }

        // 2. Lấy chi tiết theo ID (Thêm hàm này vì BLL đang thiếu)
        public DataTable GetById(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_PhanCong_GetById", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaPhanCong", id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch { }
            finally { DatabaseConnect.CloseDatabase(); }
            return dt;
        }

        // 3. Thêm mới (Đổi tên Insert -> Them)
        public string Them(PhanCong model)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_PhanCong_Them", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaPhanCong", model.MaPhanCong);
                cmd.Parameters.AddWithValue("@MaLop", model.MaLop ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaMonHoc", model.MaMonHoc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NamHoc", model.NamHoc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HocKy", model.HocKy);
                cmd.Parameters.AddWithValue("@MaGiaoVien", model.MaGiaoVien ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaHocSinh", model.MaHocSinh ?? (object)DBNull.Value);

                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Lỗi không xác định";
            }
            catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 4. Cập nhật (Đổi tên Update -> Sua)
        public string Sua(PhanCong model)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_PhanCong_Sua", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaPhanCong", model.MaPhanCong);
                cmd.Parameters.AddWithValue("@MaLop", model.MaLop ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaMonHoc", model.MaMonHoc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NamHoc", model.NamHoc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HocKy", model.HocKy);
                cmd.Parameters.AddWithValue("@MaGiaoVien", model.MaGiaoVien ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaHocSinh", model.MaHocSinh ?? (object)DBNull.Value);

                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Lỗi không xác định";
            }
            catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 5. Xóa (Đổi tên Delete -> Xoa)
        public string Xoa(string id)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_PhanCong_Xoa", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaPhanCong", id);

                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Lỗi không xác định";
            }
            catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }
    }
}