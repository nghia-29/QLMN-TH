using DAL.Interfaces;
using Model;
using Microsoft.EntityFrameworkCore; // Để dùng .Include nếu cần
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class ThongBaoRepository : IThongBaoRepository
    {
        private readonly QLMNContext _context;

        public ThongBaoRepository(QLMNContext context)
        {
            _context = context;
        }

        public List<ThongBao> GetAll()
        {
            // Lấy danh sách thông báo, sắp xếp ngày gửi mới nhất lên đầu
            return _context.ThongBaos.OrderByDescending(x => x.NgayGui).ToList();
        }

        public ThongBao GetById(string id)
        {
            return _context.ThongBaos.FirstOrDefault(x => x.MaThongBao == id);
        }

        public bool Create(ThongBao model)
        {
            try
            {
                // Nếu chưa có ngày gửi, tự động lấy ngày hiện tại
                if (model.NgayGui == null) model.NgayGui = DateTime.Now;

                _context.ThongBaos.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool Update(ThongBao model)
        {
            try
            {
                var item = _context.ThongBaos.FirstOrDefault(x => x.MaThongBao == model.MaThongBao);
                if (item != null)
                {
                    item.TieuDe = model.TieuDe;
                    item.NoiDung = model.NoiDung;
                    item.LoaiThongBao = model.LoaiThongBao;
                    item.NguoiNhan = model.NguoiNhan;
                    // Không cập nhật Người gửi và Ngày gửi để giữ nguyên lịch sử

                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception) { return false; }
        }

        public bool Delete(string id)
        {
            try
            {
                var item = _context.ThongBaos.FirstOrDefault(x => x.MaThongBao == id);
                if (item != null)
                {
                    _context.ThongBaos.Remove(item);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception) { return false; }
        }
    }
}