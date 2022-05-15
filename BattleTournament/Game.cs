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
        private GameObject playerObject;
        
        public Size Resolution { get; set; }

        public void Load()
        {
            playerObject = new GameObject();
            playerObject.sprite = new GameSprite();
            playerObject.sprite.SpriteImage = Properties.Resources.bomber_sprite;
            playerObject.sprite.Width = playerObject.sprite.SpriteImage.Width;
            playerObject.sprite.Height = playerObject.sprite.SpriteImage.Height;
            playerObject.sprite.X = 300;
            playerObject.sprite.Y = 300;
            playerObject.kimematic = new KinematicObject();
            playerObject.kimematic.dX = 0;
            playerObject.kimematic.dY = 0;
        }

        public void Unload()
        {
            //unload graphics and turn off game music
        }

        string DisplayText = "";

        private void UpdateMouse(MouseState mouse)
        {
            mouse.dX = mouse.Location.X - mouse.LastLocation.X;
            mouse.dY = mouse.Location.Y - mouse.LastLocation.Y;
            if (mouse.Buttons == MouseButtons.None && mouse.LastButtons == MouseButtons.Left)
            {
                mouse.LeftClicked = true;
            }
            if (mouse.Buttons == MouseButtons.None && mouse.LastButtons == MouseButtons.Right)
            {
                mouse.RightClicked = true;
            }
        }

        private void FinalizeMouse(MouseState mouse)
        {
            mouse.LastLocation = mouse.Location;
            mouse.LastButtons = mouse.Buttons;
            mouse.Clicks = 0;
            mouse.WheelTicks = 0;
            mouse.LeftClicked = false;
            mouse.RightClicked = false;
        }
        public void Update(TimeSpan gameTime, MouseState mouse)
        {
            // get time elapsed in decimal of a second
            double gameTimeElapsed = gameTime.TotalMilliseconds / 1000f;
            DisplayText = string.Format("FPS: {0}", (int)(1f / gameTimeElapsed));
            float force = (float)(playerObject.kimematic.force * gameTimeElapsed);

            UpdateMouse(mouse);
            if (mouse.LeftClicked)
            {
                mouse.Tracking = !mouse.Tracking;
            }
            if (mouse.Tracking)
            {
                playerObject.kimematic.dX += mouse.dX * force * 0.1f;
                playerObject.kimematic.dY += mouse.dY * force * 0.1f;
            }

            if ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0)
            {
                playerObject.kimematic.dX += force;
                if (playerObject.kimematic.dX > playerObject.kimematic.dXMax)
                {
                    playerObject.kimematic.dX = playerObject.kimematic.dXMax;
                }
            }
            if ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0)
            {
                playerObject.kimematic.dX -= force;
                if (playerObject.kimematic.dX < -1 * playerObject.kimematic.dXMax)
                {
                    playerObject.kimematic.dX = -1 * playerObject.kimematic.dXMax;
                }
            }
            if ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
            {
                playerObject.kimematic.dY -= force;
                if (playerObject.kimematic.dY < -1 * playerObject.kimematic.dYMax)
                {
                    playerObject.kimematic.dY = -1 * playerObject.kimematic.dYMax;
                }
            }
            if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
            {
                playerObject.kimematic.dY += force;
                if (playerObject.kimematic.dY > playerObject.kimematic.dYMax)
                {
                    playerObject.kimematic.dY = playerObject.kimematic.dYMax;
                }
            }

            playerObject.sprite.X += playerObject.kimematic.dX;
            playerObject.sprite.Y += playerObject.kimematic.dY;

            FinalizeMouse(mouse);
        }

        Font f = new Font("Consolas", 16);

        public void Draw(Graphics gfx)
        {
            gfx.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(0, 0, Resolution.Width, Resolution.Height));
            gfx.DrawString(DisplayText, f, Brushes.Black, new PointF(5, 5));
            playerObject.sprite.Draw(gfx);
        }


    }
}
