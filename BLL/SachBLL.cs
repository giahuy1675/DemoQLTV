using DAL; // Tham chiếu đến lớp DAL
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class SachBLL
    {
        private Model1 dbContext;

        public SachBLL()
        {
            dbContext = new Model1(); // Khởi tạo đối tượng DbContext
        }

        // Phương thức lấy tất cả sách từ cơ sở dữ liệu
        public List<Sach> GetAllSach()
        {
            return dbContext.Sach.ToList();
        }

        // Phương thức lấy tất cả tác giả
        public List<TacGia> GetAllTacGia()
        {
            return dbContext.TacGia.ToList(); // Truy vấn tất cả tác giả từ cơ sở dữ liệu
        }

        // Phương thức lấy tất cả thể loại
        public List<TheLoai> GetAllTheLoai()
        {
            return dbContext.TheLoai.ToList(); // Truy vấn tất cả thể loại từ cơ sở dữ liệu
        }

        // Thêm sách mới
        public void AddSach(Sach sach)
        {
            dbContext.Sach.Add(sach);
            dbContext.SaveChanges();
        }

        // Sửa sách
        public void UpdateSach(Sach sach)
        {
            var sachToUpdate = dbContext.Sach.Find(sach.MaSach);
            if (sachToUpdate != null)
            {
                sachToUpdate.TieuDe = sach.TieuDe;
                sachToUpdate.MaTacGia = sach.MaTacGia;
                sachToUpdate.MaTheLoai = sach.MaTheLoai;
                sachToUpdate.NamXuatBan = sach.NamXuatBan;
                sachToUpdate.SoLuong = sach.SoLuong;
                sachToUpdate.MoTa = sach.MoTa;
                dbContext.SaveChanges();
            }
        }

        // Xóa sách
        public void DeleteSach(int maSach)
        {
            var sachToDelete = dbContext.Sach.Find(maSach);
            if (sachToDelete != null)
            {
                dbContext.Sach.Remove(sachToDelete);
                dbContext.SaveChanges();
            }
        }
    }
}
