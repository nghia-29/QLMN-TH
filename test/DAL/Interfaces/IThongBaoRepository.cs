using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IThongBaoRepository
    {
        List<ThongBao> GetAll();
        ThongBao GetById(string id);
        bool Create(ThongBao model);
        bool Update(ThongBao model);
        bool Delete(string id);
    }
}