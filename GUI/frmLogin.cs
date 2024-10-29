using BLL;
using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmLogin : Form
    {
        // Lưu kích thước ban đầu của form và các điều khiển
        private Size formOriginalSize;
        private Rectangle recUsername, recPassword, recButtonLogin, recLabelUsername, recLabelPassword;

        public frmLogin()
        {
            InitializeComponent();
            this.Load += frmLogin_Load; // Đăng ký sự kiện Load
            this.Resize += frmLogin_Resize; // Đăng ký sự kiện Resize
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Lưu kích thước và vị trí ban đầu của form và các điều khiển
            formOriginalSize = this.Size;
            recUsername = new Rectangle(txtUsername.Location, txtUsername.Size);
            recPassword = new Rectangle(txtPassword.Location, txtPassword.Size);
            recButtonLogin = new Rectangle(btnLogin.Location, btnLogin.Size);
        
        }

        private void frmLogin_Resize(object sender, EventArgs e)
        {
            // Cập nhật vị trí và kích thước các điều khiển
            ResizeControl(txtUsername, recUsername);
            ResizeControl(txtPassword, recPassword);
            ResizeControl(btnLogin, recButtonLogin);
        
        }

        private void ResizeControl(Control c, Rectangle r)
        {
            // Tính toán tỷ lệ kích thước mới dựa trên kích thước gốc của form
            float xRatio = (float)this.Width / (float)formOriginalSize.Width;
            float yRatio = (float)this.Height / (float)formOriginalSize.Height;

            // Tính toán vị trí mới
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);

            // Tính toán kích thước mới
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            // Cập nhật vị trí và kích thước cho điều khiển
            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }

        private bool Login(string username, string password)
        {
            NguoiDungBLL nguoiDungBLL = new NguoiDungBLL();
            NguoiDung user = nguoiDungBLL.Login(username, password);

            if (user != null)
            {
                // Kiểm tra quyền của người dùng
                if (user.MaQuyen == 1) // Quyền Admin
                {
                    frmMuonSach adminForm = new frmMuonSach(); // Form dành cho Admin
                    adminForm.Show();
                }
                else if (user.MaQuyen == 2) // Quyền Người dùng
                {
                    frmUser userForm = new frmUser(user); // Truyền đối tượng user sang form User
                    userForm.Show();
                }
                return true;
            }
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Gọi hàm kiểm tra đăng nhập
            if (Login(username, password))
            {
                this.Hide(); // Ẩn form đăng nhập nếu thành công
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
            }
        }
    }
}
