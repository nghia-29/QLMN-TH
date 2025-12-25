using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class ThongBaoBusiness : IThongBaoBusiness
    {
        private readonly IThongBaoRepository _res;

        public ThongBaoBusiness(IThongBaoRepository res)
        {
            _res = res;
        }

        public List<ThongBao> GetAll() { return _res.GetAll(); }
        public ThongBao GetById(string id) { return _res.GetById(id); }

        public bool Create(ThongBao model)
        {
            return _res.Create(model);
        }

        public bool Update(ThongBao model) { return _res.Update(model); }
        public bool Delete(string id) { return _res.Delete(id); }
    }
}