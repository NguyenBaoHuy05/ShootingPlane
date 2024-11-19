using Game_ban_may_bay.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_ban_may_bay.Controller
{
    internal class Create_Bitmap
    {
        public Bitmap airbulletBitmap {  get; set; }
        public Bitmap bulletBitmap { get; set; }

        public Bitmap bulletBossBitmap { get; set; }
        public Bitmap playerBitmap { get; set; }
        public Bitmap boss1Bitmap { get; set; }
        public Bitmap starBitmap { get; set; }
        public Bitmap explosion3Bitmap { get; set; }
        public Bitmap bossbom1Bitmap { get; set; }
        public Bitmap heartBitmap { get; set; }
        public Bitmap lightingBitmap { get; set; }
        public Bitmap hackBitmap { get; set; }
        public Bitmap gameplayBitmap { get; set; }

        public List<Bitmap> plane;

        public Create_Bitmap()
        {
            starBitmap = CreateBulletImage(Brushes.DarkGray, 2, 2);
            bulletBitmap = CreateBulletImage(Brushes.Red, 3, 8);
            bulletBossBitmap = CreateBulletImage(Brushes.Yellow, 8, 8);
            airbulletBitmap = CreateBulletImage(Brushes.Blue, 3, 9);
            playerBitmap = ByteArrayToBitmap(Properties.Resources.player);
            boss1Bitmap = ByteArrayToBitmap(Properties.Resources.munition);
            bossbom1Bitmap = ByteArrayToBitmap(Properties.Resources.bossbom1);
            explosion3Bitmap = ByteArrayToBitmap(Properties.Resources.explosion3);
            heartBitmap = ByteArrayToBitmap(Properties.Resources.heart);
            lightingBitmap = ByteArrayToBitmap(Properties.Resources.lighting);
            hackBitmap = ByteArrayToBitmap(Properties.Resources.hack);
            gameplayBitmap = ByteArrayToBitmap(Properties.Resources.earth);



            plane = new List<Bitmap>
            {
                ByteArrayToBitmap(Properties.Resources.plane1),
                ByteArrayToBitmap(Properties.Resources.plane2),
                ByteArrayToBitmap(Properties.Resources.plane3),
                ByteArrayToBitmap(Properties.Resources.plane4)
            };
        }
        private Bitmap ByteArrayToBitmap(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return new Bitmap(ms);
            }
        }

        private Bitmap CreateBulletImage(Brush color, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.FillEllipse(color, 0, 0, width, height);
            }
            return bitmap;
        }
        public static void DrawHealthBar(Graphics g, int health, RectangleF bounds, Brush healthyBrush, Brush damagedBrush, bool IsEnemy = true)
        {
            // Vẽ thanh sức khỏe dựa trên giá trị health
            float percent = (float)health / 100;
            g.FillRectangle(healthyBrush, bounds.X, bounds.Y, bounds.Width * percent, bounds.Height);
            g.DrawRectangle(Pens.Black, bounds.X, bounds.Y, bounds.Width, bounds.Height);

            if (!IsEnemy) g.DrawString(health.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Yellow, bounds.X, bounds.Y + (bounds.Height - 20));
        }
    }
}
