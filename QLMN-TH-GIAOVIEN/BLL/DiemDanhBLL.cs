using DAL;
using Model;
using System.Data;
using System;

namespace BLL
{
    public class DiemDanhBLL
    {
        private DiemDanhDAL _dal = new DiemDanhDAL();

        public DataTable GetAll(string maLop, string ngayStr)
        {
            DateTime ngay;
            // Nếu không truyền ngày -> Lấy ngày hiện tại
            if (string.IsNullOrEmpty(ngayStr))
                ngay = DateTime.Now;
            else
            {
                // Cố gắng parse ngày, nếu lỗi thì lấy ngày hiện tại
                if (!DateTime.TryParse(ngayStr, out ngay))
                    ngay = DateTime.Now;
            }

            return _dal.GetAll(maLop, ngay);
        }

        public string Save(DiemDanh obj)
        {
            return _dal.Save(obj);
        }

        public string Delete(string id)
        {
            return _dal.Delete(id);
        }
    }
}