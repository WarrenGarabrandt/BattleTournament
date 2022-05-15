using BattleTournament.Script;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace BattleTournament
{
    public class Game
    {
        public void Load(KeyboardState key, MouseState mouse)
        {
            ScriptMain.Load(key, mouse);
        }

        public void Unload()
        {
            //unload graphics and turn off game music
        }

        string DisplayText = "";

        private void ProcessMouse(MouseState mouse)
        {
            if ((mouse.Buttons & MouseButtons.Left) == MouseButtons.Left)
            {
                if (!mouse.LeftHeld)
                {
                    mouse.LeftClicked = true;
                }
                mouse.LeftHeld = true;
            }
            else
            {
                mouse.LeftHeld = false;
            }

            if ((mouse.Buttons & MouseButtons.Right) == MouseButtons.Right)
            {
                if (!mouse.RightHeld)
                {
                    mouse.RightClicked = true;
                }
                mouse.RightHeld = true;
            }
            else
            {
                mouse.RightHeld = false;
            }
        }

        private void FinalizeMouse(MouseState mouse)
        {
            mouse.LastButtons = mouse.Buttons;
            mouse.Clicks = 0;
            mouse.WheelTicks = 0;
            mouse.LeftClicked = false;
            mouse.RightClicked = false;
            mouse.dX = 0;
            mouse.dY = 0;
        }

        private void ProcessKeyboard(KeyboardState key)
        {
            if ((Keyboard.GetKeyStates(Key.Escape) & KeyStates.Down) > 0)
            {
                if (!key.EscHeld)
                {
                    key.EscPressed = true;
                }
                key.EscHeld = true;
            }
            else if (key.EscHeld)
            {
                key.EscHeld = false;
            }
            if ((Keyboard.GetKeyStates(Key.Enter) & KeyStates.Down) > 0)
            {
                if (!key.EnterHeld)
                {
                    key.EnterPressed = true;
                }
                key.EnterHeld = true;
            }
            else if (key.EnterHeld)
            {
                key.EnterHeld = false;
            }
            if ((Keyboard.GetKeyStates(Key.RightAlt) & KeyStates.Down) > 0)
            {
                key.RightAltHeld = true;
            }
            else if (key.RightAltHeld)
            {
                key.RightAltHeld = false;
            }
            if ((Keyboard.GetKeyStates(Key.LeftShift) & KeyStates.Down) > 0)
            {
                key.LeftShiftHeld = true;
            }
            else
            {
                key.LeftShiftHeld = false;
            }
            if ((Keyboard.GetKeyStates(Key.RightShift) & KeyStates.Down) > 0)
            {
                key.RightShiftHeld = true;
            }
            else
            {
                key.RightShiftHeld = false;
            }
        }

        private void FinalizeKeyboard(KeyboardState key)
        {
            key.EscPressed = false;
            key.EnterPressed = false;
            //key.RightAltPressed = false;
        }

        public void Update(TimeSpan gameTime, KeyboardState key, MouseState mouse)
        {
            // get time elapsed in decimal of a second
            double gameTimeElapsed = gameTime.TotalMilliseconds / 1000f;
            DisplayText = string.Format("FPS: {0}", (int)(1f / gameTimeElapsed));

            ProcessMouse(mouse);
            ProcessKeyboard(key);

            ScriptMain.Update(gameTimeElapsed, key, mouse);

            FinalizeKeyboard(key);
            FinalizeMouse(mouse);
        }

        Font fontFPS = new Font("Consolas", 16);
        Font fontPause = new Font("Consolas", 92);

        public void Draw(Graphics g)
        {
            if (GameState.IsPaused)
            {
                g.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(0, 0, GameState.GameResolution.Width, GameState.GameResolution.Height));
                SizeF pauseSize = g.MeasureString("GAME PAUSED", fontPause);

                g.DrawString("GAME PAUSED", fontPause, Brushes.Red, 
                    new PointF((GameState.GameResolution.Width - pauseSize.Width) / 2 , (GameState.GameResolution.Height - pauseSize.Height) / 2));
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, GameState.GameResolution.Width, GameState.GameResolution.Height));

                // draw the player
                GameState.PlayerObject.Sprite.Draw(g);

                // draw the player's bullets
                foreach (var bullet in GameState.PlayerBullets)
                {
                    bullet.Sprite.Draw(g);
                }

            }

            g.DrawString(DisplayText, fontFPS, Brushes.White, new PointF(5, 5));

        }


    }
}
