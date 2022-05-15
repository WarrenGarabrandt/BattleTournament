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


    }
}
