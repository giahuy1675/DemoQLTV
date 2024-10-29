using System.Collections.Generic;
using System.Linq;
using DAL;

namespace BLL
{
    public class NguoiDungBLL
    {
        private Model1 dbContext;

        public NguoiDungBLL()
        {
            dbContext = new Model1(); // Khởi tạo DbContext để kết nối cơ sở dữ liệu
        }
        

        public List<Quyen> GetAllQuyen()
        {
            return dbContext.Quyen.ToList(); // Trả về danh sách tất cả quyền
        }
        public NguoiDung Login(string username, string password)
        {
            // Tìm người dùng trong cơ sở dữ liệu theo tên đăng nhập và mật khẩu
            return dbContext.NguoiDung.FirstOrDefault(nguoiDung => nguoiDung.TenDangNhap == username && nguoiDung.MatKhau == password);
        }

        // Phương thức lấy danh sách tất cả người dùng
        public List<NguoiDung> GetAllNguoiDung()
        {
            return dbContext.NguoiDung.Include("Quyen").ToList(); // Lấy cả quyền
        }

        // Phương thức thêm người dùng mới
        public void AddNguoiDung(NguoiDung nguoiDung)
        {
            dbContext.NguoiDung.Add(nguoiDung);
            dbContext.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Phương thức sửa thông tin người dùng
        public void UpdateNguoiDung(NguoiDung nguoiDung)
        {
            var existingNguoiDung = dbContext.NguoiDung.Find(nguoiDung.MaNguoiDung);
            if (existingNguoiDung != null)
            {
                existingNguoiDung.TenDangNhap = nguoiDung.TenDangNhap;
                existingNguoiDung.MatKhau = nguoiDung.MatKhau;
                existingNguoiDung.Email = nguoiDung.Email;
                existingNguoiDung.MaQuyen = nguoiDung.MaQuyen;
                existingNguoiDung.TinhTrangTaiKhoan = nguoiDung.TinhTrangTaiKhoan;
                existingNguoiDung.NgayTaoTaiKhoan = nguoiDung.NgayTaoTaiKhoan;
                dbContext.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        // Phương thức xóa người dùng
        public void DeleteNguoiDung(int maNguoiDung)
        {
            var nguoiDung = dbContext.NguoiDung.Find(maNguoiDung);
            if (nguoiDung != null)
            {
                dbContext.NguoiDung.Remove(nguoiDung);
                dbContext.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }
    }
}
