using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class HosoSucKhoeDAL
    {
        // 1. Lấy toàn bộ danh sách (Gọi sp_HosoSucKhoe_GetAll)
        public DataTable GetAll()
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_HosoSucKhoe_GetAll", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }
        // 2. Lấy lịch sử theo Học sinh (Gọi sp_HosoSucKhoe_GetByHocSinh)
        public DataTable GetByHocSinh(string maHS)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_HosoSucKhoe_GetByHocSinh", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaHocSinh", maHS);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }

        // 3. Lấy chi tiết 1 phiếu (Gọi sp_HosoSucKhoe_GetById)
        public DataTable GetById(string id)
        {
            try
            {
                DatabaseConnect.OpenDatabase();
                SqlCommand cmd = new SqlCommand("sp_HosoSucKhoe_GetById", DatabaseConnect.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaPhieuKham", id);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex) { throw new Exception("Lỗi DAL: " + ex.Message); }
            finally { DatabaseConnect.CloseDatabase(); }
        }
    }
}