using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class BangDiemDAL
    {
        // 1. Lấy tất cả bảng điểm
        public DataTable GetAll()
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_BangDiem_GetAll", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 2. Lấy bảng điểm theo học sinh
        public DataTable GetByHocSinh(string maHS)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_BangDiem_GetByHocSinh", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHocSinh", maHS);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 3. Lấy chi tiết
        public DataTable GetById(string id)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_BangDiem_GetById", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaBangDiem", id);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }
    }
}