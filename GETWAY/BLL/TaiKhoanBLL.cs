using System.Data;
using DAL;

namespace BLL
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL _dal = new TaiKhoanDAL();

        public DataTable CheckLogin(string u, string p)
        {
            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p)) return new DataTable();
            return _dal.Login(u, p);
        }
    }
}