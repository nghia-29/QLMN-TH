using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class TaiKhoanDAL
    {
        public DataTable Login(string u, string p)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                // Gọi Stored Procedure sp_TaiKhoan_Login
                SqlCommand cmd = new SqlCommand("sp_TaiKhoan_Login", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenTaiKhoan", u);
                cmd.Parameters.AddWithValue("@MatKhau", p);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }
    }
}