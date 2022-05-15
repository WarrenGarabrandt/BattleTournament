using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTournament
{
    public class KinematicObject
    {
        public float dX { get; set; }
        public float dY { get; set; }

        public float dXMax = 2f;
        public float dYMax = 2f;
        public float force = 8f;
    }
}
