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
    public partial class Flow : Form
    {
        public Flow()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.Visible = false;
        }

        private bool IsActive = true;
        private void Flow_Load(object sender, EventArgs e)
        {
            while (IsActive)
            {
                Form dangnhap = new DangNhap();
                DialogResult fdangnhap = dangnhap.ShowDialog();
                if (fdangnhap == DialogResult.Yes) break;

                Form mainplay = new MainPlay();
                DialogResult fmainplay = mainplay.ShowDialog();
                if (fmainplay == DialogResult.Cancel) IsActive = false;

            }
            this.Close();
        }
    }
}
