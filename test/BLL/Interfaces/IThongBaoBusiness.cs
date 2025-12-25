using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IThongBaoBusiness
    {
        List<ThongBao> GetAll();
        ThongBao GetById(string id);
        bool Create(ThongBao model);
        bool Update(ThongBao model);
        bool Delete(string id);
    }
}