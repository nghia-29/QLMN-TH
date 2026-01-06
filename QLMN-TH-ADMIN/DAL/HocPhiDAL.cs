using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class HocPhiDAL
    {
        // 1. Lấy danh sách
        public DataTable GetAll()
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_HocPhi_GetAll", DatabaseConnect.Conn);
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
                SqlCommand cmd = new SqlCommand("sp_HocPhi_GetById", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHocPhi", id);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 3. Tạo mới (Chỉ cần tạo công nợ, chưa cần ngày thanh toán)
        public string Them(HocPhi obj)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_HocPhi_Them", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaHocPhi", obj.MaHocPhi);
                cmd.Parameters.AddWithValue("@ThangThu", obj.ThangThu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TongTien", obj.TongTien ?? 0);
                cmd.Parameters.AddWithValue("@TrangThai", obj.TrangThai ?? "Chưa đóng");
                cmd.Parameters.AddWithValue("@MaHocSinh", obj.MaHocSinh ?? (object)DBNull.Value);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Thêm thành công";
            }
            catch (Exception ex) { return "Lỗi hệ thống: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 4. Cập nhật (Xác nhận đóng tiền)
        public string CapNhat(HocPhi obj)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_HocPhi_CapNhat", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaHocPhi", obj.MaHocPhi);
                cmd.Parameters.AddWithValue("@ThangThu", obj.ThangThu ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TongTien", obj.TongTien ?? 0);
                cmd.Parameters.AddWithValue("@TrangThai", obj.TrangThai ?? (object)DBNull.Value);
                // Xử lý ngày thanh toán
                cmd.Parameters.AddWithValue("@NgayThanhToan", obj.NgayThanhToan ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HinhThuc", obj.HinhThuc ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaHocSinh", obj.MaHocSinh ?? (object)DBNull.Value);

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
                SqlCommand cmd = new SqlCommand("sp_HocPhi_Xoa", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHocPhi", id);
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Xóa thành công";
            }
            catch (Exception ex) { return "Lỗi: " + ex.Message; }
            finally { DatabaseConnect.CloseDatabase(); }
        }
    }
}