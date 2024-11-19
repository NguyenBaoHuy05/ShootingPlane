using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_ban_may_bay
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDemo_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form demo = new Demo();
            demo.ShowDialog();
            this.Show();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form author = new Author();
            author.ShowDialog();
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show(
            "Bạn có chắc chắn muốn thoát",
            "Exit",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );
            if (rs == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        //private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    this.DialogResult = DialogResult.Yes;
        //}
    }
}
