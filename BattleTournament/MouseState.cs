using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleTournament
{
    public class MouseState
    {
        public bool Tracking { get; set; }
        public bool FormFocused { get; set; }
        public Point LastLocation { get; set; }
        public int dX { get; set; }
        public int dY { get; set; }
        public Point Location { get; set; }
        public MouseButtons Buttons { get; set; }
        public MouseButtons LastButtons { get; set; }
        public int Clicks { get; set; }
        public int WheelTicks { get; set; }
        public bool LeftClicked { get; set; }
        public bool RightClicked { get; set; }

    }
}
