using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL // Đảm bảo namespace đúng với project của bạn
{
    public class DatabaseConnect
    {
        // CHUỖI KẾT NỐI ĐÃ CẤU HÌNH THEO THÔNG TIN BẠN GỬI
        private static string strCon = @"Data Source=laptop-o2gr7ipf\may1;Initial Catalog=QLMN_TH_V2;Integrated Security=True;TrustServerCertificate=True";

        private static SqlConnection conn = null;

        public static SqlConnection Conn
        {
            get
            {
                if (conn == null) conn = new SqlConnection(strCon);
                return conn;
            }
        }

        public static void OpenDatabase()
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
        }

        public static void CloseDatabase()
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
}