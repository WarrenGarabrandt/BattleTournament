using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BattleTournament.Script
{
    public class SpaceShipTest
    {
        public static void Load()
        {
            GameState.IsPaused = true;
            GameState.PlayerObject = new GameObject();
            GameState.PlayerObject.Sprite = new GameSprite();
            GameState.PlayerObject.Sprite.SpriteImage = Properties.Resources.bomber_sprite;
            GameState.PlayerObject.Sprite.Width = GameState.PlayerObject.Sprite.SpriteImage.Width;
            GameState.PlayerObject.Sprite.Height = GameState.PlayerObject.Sprite.SpriteImage.Height;
            GameState.PlayerObject.Sprite.X = 300;
            GameState.PlayerObject.Sprite.Y = 300;
            GameState.PlayerObject.Kimematic = new KinematicObject();
            GameState.PlayerObject.Kimematic.dX = 0;
            GameState.PlayerObject.Kimematic.dY = 0;
        }

        public static void Update(double gameTime, MouseState mouse, Rectangle rect)
        {
            float force = (float)(GameState.PlayerObject.Kimematic.force * gameTime);


            GameState.PlayerObject.Kimematic.dX += mouse.dX * force * 0.3f;
            GameState.PlayerObject.Kimematic.dY += mouse.dY * force * 0.3f;
            
            if ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0)
            {
                GameState.PlayerObject.Kimematic.dX += force;
                if (GameState.PlayerObject.Kimematic.dX > GameState.PlayerObject.Kimematic.dXMax)
                {
                    GameState.PlayerObject.Kimematic.dX = GameState.PlayerObject.Kimematic.dXMax;
                }
            }
            if ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0)
            {
                GameState.PlayerObject.Kimematic.dX -= force;
                if (GameState.PlayerObject.Kimematic.dX < -1 * GameState.PlayerObject.Kimematic.dXMax)
                {
                    GameState.PlayerObject.Kimematic.dX = -1 * GameState.PlayerObject.Kimematic.dXMax;
                }
            }
            if ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
            {
                GameState.PlayerObject.Kimematic.dY -= force;
                if (GameState.PlayerObject.Kimematic.dY < -1 * GameState.PlayerObject.Kimematic.dYMax)
                {
                    GameState.PlayerObject.Kimematic.dY = -1 * GameState.PlayerObject.Kimematic.dYMax;
                }
            }
            if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
            {
                GameState.PlayerObject.Kimematic.dY += force;
                if (GameState.PlayerObject.Kimematic.dY > GameState.PlayerObject.Kimematic.dYMax)
                {
                    GameState.PlayerObject.Kimematic.dY = GameState.PlayerObject.Kimematic.dYMax;
                }
            }

            GameState.PlayerObject.Sprite.X += GameState.PlayerObject.Kimematic.dX;
            GameState.PlayerObject.Sprite.Y += GameState.PlayerObject.Kimematic.dY;

            // Keep ship in screen bounds
            if (GameState.PlayerObject.Sprite.X + GameState.PlayerObject.Sprite.Width > 
                GameState.GameResolution.Width)
            {
                GameState.PlayerObject.Sprite.X = GameState.GameResolution.Width - GameState.PlayerObject.Sprite.Width;
                GameState.PlayerObject.Kimematic.dX = 0;
            }
            if (GameState.PlayerObject.Sprite.X < 0)
            {
                GameState.PlayerObject.Sprite.X = 0;
                GameState.PlayerObject.Kimematic.dX = 0;
            }
            if (GameState.PlayerObject.Sprite.Y + GameState.PlayerObject.Sprite.Height >
                GameState.GameResolution.Height)
            {
                GameState.PlayerObject.Sprite.Y = GameState.GameResolution.Height - GameState.PlayerObject.Sprite.Height;
                GameState.PlayerObject.Kimematic.dY = 0;
            }
            if (GameState.PlayerObject.Sprite.Y < 0)
            {
                GameState.PlayerObject.Sprite.Y = 0;
                GameState.PlayerObject.Kimematic.dY = 0;
            }
        }
    }
}
