using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTournament
{
    public class GameObject
    {
        public GameSprite Sprite { get; set; }
        public KinematicObject Kimematic { get; set; }
        public double Lifetime { get; set; }
    }
}
