using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Game_ban_may_bay.Controller
{
    internal class Create_music
    {
        private string basePath = AppDomain.CurrentDomain.BaseDirectory;
        public WindowsMediaPlayer backgroundMusic;
        public WindowsMediaPlayer shootingMusic;
        public WindowsMediaPlayer boomMusic;
        public WindowsMediaPlayer boomBossMusic;
        public WindowsMediaPlayer BossMusic;
        public WindowsMediaPlayer gameOverMusic;
        public Create_music() 
        {
            backgroundMusic = new WindowsMediaPlayer();
            shootingMusic = new WindowsMediaPlayer();
            boomMusic = new WindowsMediaPlayer();
            BossMusic = new WindowsMediaPlayer();
            boomBossMusic = new WindowsMediaPlayer();
            gameOverMusic = new WindowsMediaPlayer();

            backgroundMusic.URL = Path.Combine(basePath, "Music", "GameSong.wav");
            shootingMusic.URL = Path.Combine(basePath, "Music", "shoot.wav");
            boomMusic.URL = Path.Combine(basePath, "Music", "boom.wav");
            boomBossMusic.URL = Path.Combine(basePath, "Music", "amThanhNo.wav");
            BossMusic.URL = Path.Combine(basePath, "Music", "warn.mp3");

            BossMusic.controls.stop();
        }
    }
}
