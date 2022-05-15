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
        public enum MouseModes
        {
            /// <summary>
            /// Get mouse position in display coordinates
            /// </summary>
            AbsoluteCoords,
            /// <summary>
            /// Get mouse position on the game window
            /// </summary>
            RelativeCoords,
            /// <summary>
            /// Keep mouse in middle and report dX and dY movements.
            /// </summary>
            Delta,
        }

        private MouseModes _mouseMode;
        public MouseModes MouseMode
        {
            get
            {
                return _mouseMode;
            }
            set
            {
                _mouseMode = value;
                ChangeMouseMode = true;
            }
        }
        public bool ChangeMouseMode { get; set; }
        public bool MouseHidden { get; set; }
        public bool MouseIsHidden { get; set; }
        public int dX { get; set; }
        public int dY { get; set; }
        public Point Location { get; set; }
        public MouseButtons Buttons { get; set; }
        public MouseButtons LastButtons { get; set; }
        public int Clicks { get; set; }
        public int WheelTicks { get; set; }
        public bool LeftHeld { get; set; }
        public bool RightHeld { get; set; }
        public bool LeftClicked { get; set; }
        public bool RightClicked { get; set; }

    }
}
