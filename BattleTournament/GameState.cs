using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTournament
{
    public static class GameState
    {

        public static Rectangle GameResolution { get; set; }
        public static bool IsPaused { get; set; }

        public static GameObject PlayerObject { get; set; }
        public static float PlayerMaxSpeed = 8f;
        public static float PlayerAcceleration = 2f;
        public static double BulletSpeed = 12.0;
        public static double PlayerBulletsPerMinute = 600.0;
        public static double BulletFireCooldown = 0.0;

        public static Dictionary<int, GameSprite> PlayerBulletSprites { get; set; }

        public static HashSet<GameObject> PlayerBullets { get; set; }
    }
}
