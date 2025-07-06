using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_ban_may_bay.Model
{
    internal class Player
    {
        public Bitmap Image { get; set; }
        public PointF Position { get; set; }
        public int Healthy {  get; set; }
        public int Level {  get; set; }
        public int Speed { get; set; }

        public Player(Bitmap image, PointF position)
        {
            Image = image;
            this.Healthy = 100;
            this.Level = 1;
            Position = position;
            Speed = 8;
        }

        public void Move(PointF newPosition)
        {
            Position = newPosition;
        }

        public RectangleF GetBounds()
        {
            return new RectangleF(Position.X, Position.Y, Image.Width, Image.Height);
        }
    }
}
