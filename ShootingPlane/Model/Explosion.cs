using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_ban_may_bay.Model
{
    internal class Explosion
    {
        public PointF Position { get; set; }
        public DateTime StartTime { get; set; }
        public Bitmap Image { get; set; }

        public Explosion(PointF position, Bitmap image)
        {
            Position = position;
            Image = image;
            StartTime = DateTime.Now;
        }
    }

}
