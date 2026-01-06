using System.Data;
using DAL;

namespace BLL
{
    public class HosoSucKhoeBLL
    {
        private HosoSucKhoeDAL _dal = new HosoSucKhoeDAL();

        public DataTable GetAll()
        {
            return _dal.GetAll();
        }

        public DataTable GetByHocSinh(string maHS)
        {
            if (string.IsNullOrEmpty(maHS)) return new DataTable();
            return _dal.GetByHocSinh(maHS);
        }

        public DataTable GetById(string id)
        {
            if (string.IsNullOrEmpty(id)) return new DataTable();
            return _dal.GetById(id);
        }
    }
}