using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_ban_may_bay.Enum;

namespace Game_ban_may_bay.Model
{
    internal class Aircraft
    {
        public Bitmap Image { get; set; }
        public PointF Position { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }
        public int Healthy { get; set; }
        public AirType Type { get; set; }
        public Size Size { get; set; }
        public Aircraft(Bitmap image, PointF position, float speedX, float speedY, int healthy, AirType type = AirType.normal)
        {
            Image = image;
            Position = position;
            SpeedX = speedX;
            SpeedY = speedY;
            Healthy = healthy;
            Type = type;
            Size = new Size(image.Width, image.Height);
        }

        public void Move() 
        {
            Position = new PointF(Position.X + SpeedX, Position.Y + SpeedY);
        }

        public RectangleF GetBounds()
        {
            return new RectangleF(Position.X, Position.Y, Size.Width, Size.Height);
        }
    }
}
