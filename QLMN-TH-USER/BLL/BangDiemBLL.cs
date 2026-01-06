using System.Data;
using DAL;
using Model;

namespace BLL
{
    public class BangDiemBLL
    {
        private BangDiemDAL _dal = new BangDiemDAL();

        // 1. Lấy tất cả
        public DataTable GetAll()
        {
            return _dal.GetAll();
        }

        // 2. Tìm kiếm theo học sinh
        public DataTable GetByHocSinh(string maHS)
        {
            if (string.IsNullOrEmpty(maHS))
            {
                // Nếu không truyền mã, trả về bảng rỗng hoặc xử lý tùy ý
                return new DataTable();
            }
            return _dal.GetByHocSinh(maHS);
        }

        // 3. Lấy chi tiết
        public DataTable GetById(string id)
        {
            return _dal.GetById(id);
        }
    }
}