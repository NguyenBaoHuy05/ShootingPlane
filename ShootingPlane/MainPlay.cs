using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Game_ban_may_bay.Model;
using Game_ban_may_bay.Controller;
using Game_ban_may_bay.Properties;
using WMPLib;

namespace Game_ban_may_bay
{
    public partial class MainPlay : Form
    {
        #region Variable
        Create_Bitmap bitmap;
        Create_music music;
        private Player player;
        private int diem = 0;
        private int levelup = 10;
        private Aircraft boss1;
        private int times = 0;
        private long distance = 0;
        private int hack = 0;

        private int difficult = 97;
        private int count = 1;

        private List<Bullet> bullets = new List<Bullet>();
        private List<Aircraft> aircrafts;
        private List<Explosion> explosions = new List<Explosion>();
        private List<Bullet> stars = new List<Bullet>();

        private Timer mainTimer;
        private Timer aircraftTimer;
        private Timer mouseTimer;
        private Timer distanceTimer;

        private bool isBoss1Active = false;
        Random rd = new Random();
        #endregion

        public MainPlay()
        {
            InitializeComponent();
        }
        private void MainPlay_Load(object sender, EventArgs e)
        {
            bitmap = new Create_Bitmap();
            music = new Create_music();
            this.DoubleBuffered = true;
            // Khởi tạo danh sách máy bay và người chơi

            mainTimer = new Timer();
            aircraftTimer = new Timer();
            mouseTimer = new Timer();
            distanceTimer = new Timer();

            mainTimer.Interval = 16;
            mainTimer.Tick += Timer_Tick;
            mainTimer.Tick += Timer_Tick_Star;
            mainTimer.Tick += Timer_Tick_Bullet;
            aircraftTimer.Interval = 30;
            aircraftTimer.Tick += Timer_Tick_Air;
            mouseTimer.Interval = 200;
            mouseTimer.Tick += Timer_Tick_Bullet_Player;
            distanceTimer.Interval = 1;
            distanceTimer.Tick += Timer_Tick_Distance;

            aircrafts = new List<Aircraft>();
            bullets = new List<Bullet>();
            stars = new List<Bullet>();
            explosions = new List<Explosion>();

            music.backgroundMusic.settings.setMode("loop", true);

            music.shootingMusic.controls.stop();
            music.boomMusic.controls.stop();
            music.backgroundMusic.controls.stop();

            CreateTimer();
            Create();
        }
        #region Create
        private void Create()
        {
            player = new Player(bitmap.playerBitmap, new PointF(ClientSize.Width / 2, ClientSize.Height / 2));
            for (int i = 0; i < 4; i++)
            {
                aircrafts.Add(new Aircraft(bitmap.plane[rd.Next(0, bitmap.plane.Count)], new PointF(rd.Next(0, ClientSize.Width), 0), rd.Next(-4, 4), rd.Next(1, 2), rd.Next(1, 4) * 10));
            }
            for (int i = 0; i < 30; i++)
            {
                stars.Add(new Bullet(bitmap.starBitmap, new PointF(rd.Next(0, ClientSize.Width), rd.Next(0, ClientSize.Height)), rd.Next(-5, 5), Enum.BulletType.Star, rd.Next(-3, -1)));
            }
            music.backgroundMusic.controls.play();
        }
        private void CreateTimer()
        {
            mainTimer.Start();
            aircraftTimer.Start();
            mouseTimer.Start();
            distanceTimer.Start();
        }
        #endregion
        #region Timer
        private void Timer_Tick_Distance(object sender, EventArgs e)
        {
            distance++;
            label4.Text = "Distance: " + distance;
            Invalidate(Region);
        }
        private void Timer_Tick_Star(object sender, EventArgs e)
        {
            foreach (Bullet star in stars.ToList())
            {
                star.Move();
                if (star.Position.Y > ClientSize.Height)
                {
                    stars.Remove(star);
                    stars.Add(new Bullet(bitmap.starBitmap, new PointF(rd.Next(0, ClientSize.Width), rd.Next(0, ClientSize.Height)), rd.Next(-5, -1), Enum.BulletType.Star, rd.Next(-3, -1)));
                }
            }
            }
        private void Timer_Tick_Bullet_Player(object sender, EventArgs e)
        {
            // Tạo đạn mới và thêm vào danh sách
            Bullet bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 4, player.Position.Y - 3), player.Speed, Enum.BulletType.Player);
            bullets.Add(bullet);
            switch (player.Level / 4)
            {
                case 0:
                    break;
                case 1:
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 5, player.Position.Y - 3), player.Speed, Enum.BulletType.Player);
                    bullets.Add(bullet);
                    break;
                case 2:
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 5, player.Position.Y - 3), player.Speed, Enum.BulletType.Player);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 3, player.Position.Y - 3), player.Speed, Enum.BulletType.Player);
                    bullets.Add(bullet);
                    break;
                case 3:
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 5, player.Position.Y - 3), player.Speed, Enum.BulletType.Player);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 3, player.Position.Y - 3), player.Speed, Enum.BulletType.Player, 1);
                    bullets.Add(bullet);
                    break;
                case 4:
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 5, player.Position.Y - 3), player.Speed, Enum.BulletType.Player);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 3, player.Position.Y - 3), player.Speed, Enum.BulletType.Player, 5);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 3, player.Position.Y - 3), player.Speed, Enum.BulletType.Player, -5);
                    bullets.Add(bullet);
                    break;
                default:
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 5, player.Position.Y - 3), player.Speed, Enum.BulletType.Player);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 4, player.Position.Y - 3), player.Speed, Enum.BulletType.Player, -5);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 4, player.Position.Y - 3), player.Speed, Enum.BulletType.Player, 5);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 4, player.Position.Y - 3), player.Speed, Enum.BulletType.Player, -7);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 4, player.Position.Y - 3), player.Speed, Enum.BulletType.Player, 7);
                    bullets.Add(bullet);
                    break;
            }
        }
        private void Timer_Tick_Bullet(object sender, EventArgs e)
        {
            foreach (var aircraft in aircrafts.ToList())
            {
                // Tạo đạn cho máy bay với tỉ lệ xác suất
                if (rd.Next(0, 100) > 98 && aircraft.Type == Enum.AirType.normal)
                {
                    Bullet bullet = new Bullet(bitmap.airbulletBitmap, new PointF(aircraft.Position.X + aircraft.Image.Width / 6, aircraft.Position.Y + aircraft.Image.Height / 3), -2, Enum.BulletType.Enemy);
                    bullets.Add(bullet);
                }
                if (rd.Next(0, 100) > 98 && aircraft.Type == Enum.AirType.boss1)
                {
                    Bullet bullet = new Bullet(bitmap.bulletBossBitmap, new PointF(aircraft.Position.X + aircraft.Image.Width / 6, aircraft.Position.Y + aircraft.Image.Height / 3), 2, Enum.BulletType.Enemy,2);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBossBitmap, new PointF(aircraft.Position.X + aircraft.Image.Width / 6, aircraft.Position.Y + aircraft.Image.Height / 3), -2, Enum.BulletType.Enemy, -2);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBossBitmap, new PointF(aircraft.Position.X + aircraft.Image.Width / 6, aircraft.Position.Y + aircraft.Image.Height / 3), 2, Enum.BulletType.Enemy, 3);
                    bullets.Add(bullet);
                    bullet = new Bullet(bitmap.bulletBossBitmap,  new PointF(aircraft.Position.X + aircraft.Image.Width / 6, aircraft.Position.Y + aircraft.Image.Height / 3), -1, Enum.BulletType.Enemy, -3);
                    bullets.Add(bullet);
                }
            }
        }
        private void Timer_Tick_Air(object sender, EventArgs e)
        {
            foreach (var aircraft in aircrafts.ToList())
            {
                aircraft.Move();
                    if (aircraft.Position.X < 0 || aircraft.Position.X > ClientSize.Width ||
                        aircraft.Position.Y < 0 || aircraft.Position.Y > ClientSize.Height )
                    {
                        if(aircraft.Type == Enum.AirType.normal) aircraft.Position = new PointF(rd.Next(0, ClientSize.Width), 0);
                        if (aircraft.Type == Enum.AirType.boss1) isBoss1Active = false;
                    }   
                
            }
            if(aircrafts.Count < count) aircrafts.Add(new Aircraft(bitmap.plane[rd.Next(0, bitmap.plane.Count)], new PointF(rd.Next(0, ClientSize.Width), 0), rd.Next(-2, 2), rd.Next(1, 2), rd.Next(1, (100 - difficult) / 12 + 4) * 10));
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            label1.Text = "Point: " + diem;
            label2.Text = "Level: " + player.Level;
            label5.Text = "    " + times;
            label6.Text = "    " + hack;

            if(!isBoss1Active) music.BossMusic.controls.stop();
            foreach (Bullet bullet in bullets.ToList())
            {
                bullet.Move();
                if (bullet.Position.Y > ClientSize.Height || bullet.Position.Y < 0)
                {
                    bullets.Remove(bullet);                        
                }

                foreach(var aircraft in aircrafts.ToList())
                {
                    if(bullet.Type == Enum.BulletType.Player && (bullet.GetBounds().IntersectsWith(aircraft.GetBounds())))
                    {
                        ShowExplosion(aircraft.Position, aircraft.Healthy);
                        bullets.Remove(bullet);
                        aircraft.Healthy -= 10;
                        music.boomMusic.controls.play();
                        if (aircraft.Healthy <= 0)
                        {
                            diem += 10;
                            if(diem > levelup )
                            {
                                player.Level++;
                                if(player.Speed < 15 ) player.Speed++;
                                levelup = player.Level * 100;
                                if (difficult > 8) difficult -= 5;
                                count = (100 - difficult) / 4 + 1;
                            }
                            if (!isBoss1Active && diem % 1000 == 0 && diem != 0)
                            {
                                boss1 = new Aircraft(bitmap.boss1Bitmap, new PointF(rd.Next(ClientSize.Width/2-50, ClientSize.Width/2 + 50), rd.Next(30, 130)), rd.Next(-2, 2), rd.Next(1, 2), 200, Enum.AirType.boss1);
                                aircrafts.Add(boss1);
                                isBoss1Active = true;
                                if(diem!= 0) music.BossMusic.controls.play();
                            }
                            if (aircraft.Type == Enum.AirType.boss1)
                            {
                                isBoss1Active = false;
                            }
                            else
                            {                      
                                aircrafts.Add(new Aircraft(bitmap.plane[rd.Next(0, bitmap.plane.Count)], new PointF(rd.Next(0, ClientSize.Width), 0), rd.Next(-2, 2), rd.Next(1, 2), rd.Next(1, (100 - difficult) / 12 + 4) * 10));
                            }
                            if(aircraft.Type == Enum.AirType.boss1)
                            {
                                Bullet hack = new Bullet(bitmap.hackBitmap, new PointF(aircraft.Position.X + aircraft.Image.Width / 6, aircraft.Position.Y + aircraft.Image.Height / 3), -1, Enum.BulletType.Hack, 2);
                                bullets.Add(hack);
                            }    
                             if (rd.NextDouble() < 0.02)
                            {
                                Bullet heart = new Bullet(bitmap.heartBitmap, new PointF(aircraft.Position.X + aircraft.Image.Width / 6, aircraft.Position.Y + aircraft.Image.Height / 3), -1, Enum.BulletType.Heart, 2);
                                bullets.Add(heart);
                            }
                            else if (rd.NextDouble() < 0.05)
                            {
                                Bullet lighting = new Bullet(bitmap.lightingBitmap, new PointF(aircraft.Position.X + aircraft.Image.Width / 6, aircraft.Position.Y + aircraft.Image.Height / 3), -1, Enum.BulletType.Lighting, 2);
                                bullets.Add(lighting);
                            }
                            aircrafts.Remove(aircraft);
                        }
                        break;
                    }    
                }    

                 if (bullet.GetBounds().IntersectsWith(player.GetBounds()) && (bullet.Type == Enum.BulletType.Heart))
                {
                    if(player.Healthy < 100) player.Healthy += 10;
                    bullets.Remove(bullet);
                }

                if (bullet.GetBounds().IntersectsWith(player.GetBounds()) && (bullet.Type == Enum.BulletType.Lighting))
                {
                    times++;
                    bullets.Remove(bullet);
                }
                if (bullet.GetBounds().IntersectsWith(player.GetBounds()) && (bullet.Type == Enum.BulletType.Hack))
                {
                    hack++;
                    bullets.Remove(bullet);
                }
                if (bullet.GetBounds().IntersectsWith(player.GetBounds()) && (bullet.Type == Enum.BulletType.Enemy ))
                {
                    // Xử lý khi đạn chạm người chơi
                    ShowExplosion(player.Position, player.Healthy);
                    bullets.Remove(bullet);
                    music.boomMusic.controls.play();
                    player.Healthy -= 10;
                  
                    if (player.Healthy <= 0)
                    {
                        Clear();
                    }
                }
            }
        }
        #endregion
        private void ShowExplosion(PointF position, int hea, int count = 50)
        {
            Bitmap explosionImage = new Bitmap(20, 20);
            using (Graphics g = Graphics.FromImage(explosionImage))
            {
                if (hea > 30)
                {
                    g.FillEllipse(Brushes.Yellow, 0, 0, 20, 20);
                    g.DrawEllipse(Pens.Orange, 0, 0, 20, 20);
                }
                else if (hea > 20)
                {
                    g.FillEllipse(Brushes.Orange, 0, 0, 20, 20);
                    g.DrawEllipse(Pens.Orange, 0, 0, 20, 20);
                }
                else explosionImage = bitmap.explosion3Bitmap;
            }

            explosions.Add(new Explosion(position, explosionImage));

            // Đặt lại timer để xóa vụ nổ sau 0.5 giây
            Timer explosionTimer = new Timer
            {
                Interval = count
            };
            explosionTimer.Tick += (s, e) =>
            {
                explosions.RemoveAll(exp => (DateTime.Now - exp.StartTime).TotalMilliseconds > count);
                explosionTimer.Stop();
                explosionTimer.Dispose();
                Invalidate();
            };
            explosionTimer.Start();
        }
        #region Form_event
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            //g.DrawImage(gameplayBitmap, this.ClientRectangle);
            // Vẽ máy bay
            foreach (Aircraft aircraft in aircrafts)
            {
                g.DrawImage(aircraft.Image, aircraft.Position);
                Create_Bitmap.DrawHealthBar(g, aircraft.Healthy, new RectangleF(aircraft.Position.X, aircraft.Position.Y - 10, aircraft.Image.Width / 2, 5), Brushes.Red, Brushes.Blue);
            }

            // Vẽ người chơi
            g.DrawImage(player.Image, player.Position);
            // Vẽ đạn
            foreach (Bullet bullet in bullets)
            {
                g.DrawImage(bullet.Image, bullet.Position);
            }
            foreach (Bullet star in stars)
            {
                g.DrawImage(star.Image, star.Position);
            }
            Create_Bitmap.DrawHealthBar(g, player.Healthy, new RectangleF(player.Position.X, player.Position.Y - 10, player.Image.Width / 2, 5), Brushes.Green, Brushes.Red, false);
            // Vẽ các vụ nổ
            foreach (var explosion in explosions)
            {
                g.DrawImage(explosion.Image, explosion.Position);
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            PointF pos = e.Location;
            // Điều chỉnh vị trí của người chơi để không ra ngoài giới hạn
            if (pos.X < 0) pos.X = 0;
            if (pos.Y < 0) pos.Y = 0;
            if (pos.X > ClientSize.Width) pos.X = ClientSize.Width;
            if (pos.Y > ClientSize.Height - player.Image.Height) pos.Y = ClientSize.Height - player.Image.Height;


            player.Move(pos);
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            if (times > 0)
            {
                music.shootingMusic.controls.play();
                times--;
                int sum = 20 + player.Level * 2;
                for (int i = 0; i <= sum; i++)
                {
                    Bullet light = new Bullet(bitmap.bulletBitmap, new PointF(player.Position.X + player.Image.Width / 3, player.Position.Y - 3), player.Speed, Enum.BulletType.Player, i - sum / 2);
                    bullets.Add(light);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (hack > 0 && e.KeyCode == Keys.A)
            {
                hack--;
                music.boomBossMusic.controls.play();
                foreach (Aircraft aircraft in aircrafts.ToList())
                {
                    if (rd.NextDouble() < 0.5)
                    {
                        ShowExplosion(aircraft.Position, 10, 1000);
                        aircrafts.Remove(aircraft);
                    }

                }

            }
        }

        #endregion
        private void Clear()
        {
            mainTimer.Stop();
            aircraftTimer.Stop();
            mouseTimer.Stop();
            distanceTimer.Stop();
            music.backgroundMusic.controls.stop();
            stars.Clear();
            bullets.Clear();
            aircrafts.Clear();
            music.backgroundMusic.controls.stop();
            music.backgroundMusic.close();
            this.Hide();
            Form end = new Gameover(diem);
            DialogResult rend = end.ShowDialog();
            if (rend == DialogResult.Cancel)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
