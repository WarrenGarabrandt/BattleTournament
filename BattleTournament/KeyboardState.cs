using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleTournament
{
    public class KeyboardState
    {
        public bool EscHeld { get; set; }
        public bool EscPressed { get; set; }

        public bool EnterHeld { get; set; }
        public bool EnterPressed { get; set; }

        public bool RightAltHeld { get; set; }
        //public bool RightAltPressed { get; set; }

        public bool LeftShiftHeld { get; set; }
        public bool RightShiftHeld { get; set; }
    }
}
