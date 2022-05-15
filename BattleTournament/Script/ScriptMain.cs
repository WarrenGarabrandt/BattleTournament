using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleTournament.Script
{
    public static class ScriptMain
    {
        public static void Load(KeyboardState key, MouseState mouse)
        {
            SpaceShipTest.Load();
            mouse.MouseMode = MouseState.MouseModes.RelativeCoords;
        }

        public static void Update(double gameTime, KeyboardState key, MouseState mouse)
        {
            if (GameState.IsPaused)
            {
                if (key.EscPressed)
                {
                    GameState.IsPaused = false;
                    mouse.MouseMode = MouseState.MouseModes.Delta;
                    mouse.MouseHidden = true;
                }
                else
                {
                    // option to toggle full screen/windowed
                    if (key.RightAltHeld && key.EnterPressed)
                    {
                        // switch between full screen and windowed mode
                    }
                }
            }
            else
            {
                if (key.EscPressed)
                {
                    GameState.IsPaused = true;
                    mouse.MouseMode = MouseState.MouseModes.RelativeCoords;
                    mouse.MouseHidden = false;
                }
            }

            if (!GameState.IsPaused)
            {
                SpaceShipTest.Update(gameTime, mouse, GameState.GameResolution);
            }
        }
    }
}
