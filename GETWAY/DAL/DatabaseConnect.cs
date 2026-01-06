using System.Data.SqlClient;

namespace DAL
{
    public class DatabaseConnect
    {
        // --- SỬA LẠI DÒNG NÀY ---
        // Đã thay dấu . bằng tên máy của bạn: LAPTOP-O2GR7IPF\MAY1
        public static string strCon = @"Data Source=LAPTOP-O2GR7IPF\MAY1;Initial Catalog=QLMN_TH_V2;Integrated Security=True";

        // ... (Các phần dưới giữ nguyên)
        public static SqlConnection? Conn = null;

        public static void OpenDatabase()
        {
            if (Conn == null) Conn = new SqlConnection(strCon);
            if (Conn.State == System.Data.ConnectionState.Closed) Conn.Open();
        }

        public static void CloseDatabase()
        {
            if (Conn != null && Conn.State == System.Data.ConnectionState.Open) Conn.Close();
        }
    }
}