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
                }
            }
            else
            {
                if (key.EscPressed)
                {
                    GameState.IsPaused = true;
                    mouse.MouseMode = MouseState.MouseModes.RelativeCoords;
                }
            }

            if (!GameState.IsPaused)
            {
                SpaceShipTest.Update(gameTime, mouse, GameState.GameResolution);
            }
        }
    }
}
