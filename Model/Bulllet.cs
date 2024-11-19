using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_ban_may_bay.Enum;

namespace Game_ban_may_bay.Model
{
    internal class Bullet
    {
        public Bitmap Image { get; set; }
        public PointF Position { get; set; }
        public float Speed { get; set; }
        public BulletType Type { get; set; }

        public int Left { get; set; }

        public Bullet(Bitmap image, PointF position, float speed, BulletType type, int left = 0)
        {
            Image = image;
            Position = position;
            Speed = speed;
            Type = type;
            Left = left;
        }
        public void Move()
        {
            // Di chuyển đạn theo hướng lên trên
            Position = new PointF(Position.X - Left, Position.Y - Speed);
        }
        public RectangleF GetBounds()
        {
            return new RectangleF(Position.X, Position.Y, Image.Width, Image.Height);
        }
    }
}
