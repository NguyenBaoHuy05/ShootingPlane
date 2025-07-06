using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game_ban_may_bay.Controller;
using WMPLib;

namespace Game_ban_may_bay
{
    public partial class Gameover : Form
    {
        Create_music music;
        public Gameover(int _point)
        {
            music = new Create_music();
            InitializeComponent();
            lblPoint.Text = "POINT: " + _point.ToString();
            music.gameOverMusic.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Music", "amThanhGameOver.wav");
            music.gameOverMusic.controls.play();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
