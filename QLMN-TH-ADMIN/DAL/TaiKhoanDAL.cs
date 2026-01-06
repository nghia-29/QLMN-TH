using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class TaiKhoanDAL
    {
        // --- ĐÃ XÓA HÀM DANGNHAP ---

        // 1. Hàm Lấy danh sách
        public DataTable GetAll()
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_GetAll", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DAL: " + ex.Message);
            }
            finally
            {
                DatabaseConnect.CloseDatabase();
            }
        }

        // 2. Hàm Lấy chi tiết theo ID
        public DataTable GetById(string id)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_GetById", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDTaiKhoan", id);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DAL: " + ex.Message);
            }
            finally
            {
                DatabaseConnect.CloseDatabase();
            }
        }

        // 3. Hàm Thêm mới
        public string Them(TaiKhoan obj)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_Insert", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDTaiKhoan", obj.IDTaiKhoan);
                cmd.Parameters.AddWithValue("@TenTaiKhoan", obj.TenTaiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", obj.MatKhau);
                cmd.Parameters.AddWithValue("@MaLoai", obj.MaLoai);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Thêm thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
            finally
            {
                DatabaseConnect.CloseDatabase();
            }
        }

        // 4. Hàm Sửa (Cập nhật)
        public string Sua(TaiKhoan obj)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_Update", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDTaiKhoan", obj.IDTaiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", obj.MatKhau);
                cmd.Parameters.AddWithValue("@MaLoai", obj.MaLoai);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
            finally
            {
                DatabaseConnect.CloseDatabase();
            }
        }

        // 5. Hàm Xóa
        public string Xoa(string id)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_Delete", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDTaiKhoan", id);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "Xóa thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
            finally
            {
                DatabaseConnect.CloseDatabase();
            }
        }
    }
}