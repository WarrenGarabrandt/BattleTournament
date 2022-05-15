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
            GameState.PlayerObject.Sprite.SpriteArray = new Bitmap[16];
            GameState.PlayerObject.Sprite.SpriteArray[0] = Properties.Resources.ship1_0;
            GameState.PlayerObject.Sprite.SpriteArray[1] = Properties.Resources.ship1_1;
            GameState.PlayerObject.Sprite.SpriteArray[2] = Properties.Resources.ship1_2;
            GameState.PlayerObject.Sprite.SpriteArray[3] = Properties.Resources.ship1_3;
            GameState.PlayerObject.Sprite.SpriteArray[4] = Properties.Resources.ship1_4;
            GameState.PlayerObject.Sprite.SpriteArray[5] = Properties.Resources.ship1_5;
            GameState.PlayerObject.Sprite.SpriteArray[6] = Properties.Resources.ship1_6;
            GameState.PlayerObject.Sprite.SpriteArray[7] = Properties.Resources.ship1_7;
            GameState.PlayerObject.Sprite.SpriteArray[8] = Properties.Resources.ship1_8;
            GameState.PlayerObject.Sprite.SpriteArray[9] = Properties.Resources.ship1_9;
            GameState.PlayerObject.Sprite.SpriteArray[10] = Properties.Resources.ship1_10;
            GameState.PlayerObject.Sprite.SpriteArray[11] = Properties.Resources.ship1_11;
            GameState.PlayerObject.Sprite.SpriteArray[12] = Properties.Resources.ship1_12;
            GameState.PlayerObject.Sprite.SpriteArray[13] = Properties.Resources.ship1_13;
            GameState.PlayerObject.Sprite.SpriteArray[14] = Properties.Resources.ship1_14;
            GameState.PlayerObject.Sprite.SpriteArray[15] = Properties.Resources.ship1_15;

            GameState.PlayerObject.Sprite.SpriteImage = GameState.PlayerObject.Sprite.SpriteArray[4];

            GameState.PlayerObject.Sprite.Width = GameState.PlayerObject.Sprite.SpriteImage.Width;
            GameState.PlayerObject.Sprite.Height = GameState.PlayerObject.Sprite.SpriteImage.Height;
            GameState.PlayerObject.Sprite.X = (GameState.GameResolution.Width - GameState.PlayerObject.Sprite.Width) / 2;
            GameState.PlayerObject.Sprite.Y = (GameState.GameResolution.Height - GameState.PlayerObject.Sprite.Height) / 2;
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

            // calculate the direction the ship should face
            double theta = Math.Atan2(GameState.PlayerObject.Kimematic.dY * -1, GameState.PlayerObject.Kimematic.dX) * (180 / Math.PI);
            if (theta < 0)
            {
                theta += 360;
            }
            int ShipIndex = ThetaToShipIndex(theta);
            // now override that actual angle if the ship is against a wall. We want to point the ship into the field, and the speed
            // determines how far it leans in the direction of travel.

            // Ship pinned on left side
            if (GameState.PlayerObject.Sprite.X < 10)
            {
                if (GameState.PlayerObject.Kimematic.dY >= -1.5 &&
                    GameState.PlayerObject.Kimematic.dY <= 1.5)
                {
                    ShipIndex = 0;
                }
                else if (GameState.PlayerObject.Kimematic.dY < -5)
                {
                    ShipIndex = 4;
                }
                else if (GameState.PlayerObject.Kimematic.dY > 5)
                {
                    ShipIndex = 12;
                }
                else if (GameState.PlayerObject.Kimematic.dY < -4)
                {
                    ShipIndex = 3;
                }
                else if (GameState.PlayerObject.Kimematic.dY > 4)
                {
                    ShipIndex = 13;
                }
                else if (GameState.PlayerObject.Kimematic.dY < -3)
                {
                    ShipIndex = 2;
                }
                else if (GameState.PlayerObject.Kimematic.dY > 3)
                {
                    ShipIndex = 14;
                }
                else if (GameState.PlayerObject.Kimematic.dY < -1.5)
                {
                    ShipIndex = 1;
                }
                else if (GameState.PlayerObject.Kimematic.dY > 1.5)
                {
                    ShipIndex = 15;
                }
            }

            // Ship pinned to the right
            if (GameState.GameResolution.Width - GameState.PlayerObject.Sprite.X - GameState.PlayerObject.Sprite.Width < 10)
            {
                if (GameState.PlayerObject.Kimematic.dY >= -1.5 &&
                    GameState.PlayerObject.Kimematic.dY <= 1.5)
                {
                    ShipIndex = 8;
                }
                else if (GameState.PlayerObject.Kimematic.dY < -5)
                {
                    ShipIndex = 4;
                }
                else if (GameState.PlayerObject.Kimematic.dY > 5)
                {
                    ShipIndex = 12;
                }
                else if (GameState.PlayerObject.Kimematic.dY < -4)
                {
                    ShipIndex = 5;
                }
                else if (GameState.PlayerObject.Kimematic.dY > 4)
                {
                    ShipIndex = 11;
                }
                else if (GameState.PlayerObject.Kimematic.dY < -3)
                {
                    ShipIndex = 6;
                }
                else if (GameState.PlayerObject.Kimematic.dY > 3)
                {
                    ShipIndex = 10;
                }
                else if (GameState.PlayerObject.Kimematic.dY < -1.5)
                {
                    ShipIndex = 7;
                }
                else if (GameState.PlayerObject.Kimematic.dY > 1.5)
                {
                    ShipIndex = 9;
                }
            }

            // Ship pinned to the top
            if (GameState.PlayerObject.Sprite.Y < 10)
            {
                if (GameState.PlayerObject.Kimematic.dX >= -1.5 &&
                    GameState.PlayerObject.Kimematic.dX <= 1.5)
                {
                    ShipIndex = 12;
                }
                else if (GameState.PlayerObject.Kimematic.dX < -5)
                {
                    ShipIndex = 8;
                }
                else if (GameState.PlayerObject.Kimematic.dX > 5)
                {
                    ShipIndex = 0;
                }
                else if (GameState.PlayerObject.Kimematic.dX < -4)
                {
                    ShipIndex = 9;
                }
                else if (GameState.PlayerObject.Kimematic.dX > 4)
                {
                    ShipIndex = 15;
                }
                else if (GameState.PlayerObject.Kimematic.dX < -3)
                {
                    ShipIndex = 10;
                }
                else if (GameState.PlayerObject.Kimematic.dX > 3)
                {
                    ShipIndex = 14;
                }
                else if (GameState.PlayerObject.Kimematic.dX < -1.5)
                {
                    ShipIndex = 11;
                }
                else if (GameState.PlayerObject.Kimematic.dX > 1.5)
                {
                    ShipIndex = 13;
                }
            }

            // Ship pinned to the bottom
            if (GameState.GameResolution.Height - GameState.PlayerObject.Sprite.Y - GameState.PlayerObject.Sprite.Height < 10)
            {
                if (GameState.PlayerObject.Kimematic.dX >= -1.5 &&
                    GameState.PlayerObject.Kimematic.dX <= 1.5)
                {
                    ShipIndex = 4;
                }
                else if (GameState.PlayerObject.Kimematic.dX < -5)
                {
                    ShipIndex = 8;
                }
                else if (GameState.PlayerObject.Kimematic.dX > 5)
                {
                    ShipIndex = 0;
                }
                else if (GameState.PlayerObject.Kimematic.dX < -4)
                {
                    ShipIndex = 7;
                }
                else if (GameState.PlayerObject.Kimematic.dX > 4)
                {
                    ShipIndex = 1;
                }
                else if (GameState.PlayerObject.Kimematic.dX < -3)
                {
                    ShipIndex = 6;
                }
                else if (GameState.PlayerObject.Kimematic.dX > 3)
                {
                    ShipIndex = 2;
                }
                else if (GameState.PlayerObject.Kimematic.dX < -1.5)
                {
                    ShipIndex = 5;
                }
                else if (GameState.PlayerObject.Kimematic.dX > 1.5)
                {
                    ShipIndex = 3;
                }
            }



            //Apply the ship angle override
            GameState.PlayerObject.Sprite.SpriteImage = GameState.PlayerObject.Sprite.SpriteArray[ShipIndex];

            //System.Diagnostics.Debug.WriteLine("Ship: {0}, Delta X: {1}, Y: {2}", ShipIndex, GameState.PlayerObject.Kimematic.dX, GameState.PlayerObject.Kimematic.dY);
        }

        public static int ThetaToShipIndex(double theta)
        {
            if (theta >= 348.75 || theta < 11.25)
            {
                return 0;
            }
            else if (theta < 33.75)
            {
                return 1;
            }
            else if (theta < 56.25)
            {
                return 2;
            }
            else if (theta < 78.75)
            {
                return 3;
            }
            else if (theta < 101.25)
            {
                return 4;
            }
            else if (theta < 123.75)
            {
                return 5;
            }
            else if (theta < 146.25)
            {
                return 6;
            }
            else if (theta < 168.75)
            {
                return 7;
            }
            else if (theta < 191.25)
            {
                return 8;
            }
            else if (theta < 213.75)
            {
                return 9;
            }
            else if (theta < 236.25)
            {
                return 10;
            }
            else if (theta < 258.75)
            {
                return 11;
            }
            else if (theta < 281.25)
            {
                return 12;
            }
            else if (theta < 303.75)
            {
                return 13;
            }
            else if (theta < 326.25)
            {
                return 14;
            }
            else if (theta < 348.75)
            {
                return 15;
            }
            else
            {
                return 4;
            }
        }

        public static double ShipIndexToTheta(int index)
        {
            switch (index)
            {
                case 0:
                    return 0.0;
                case 1:
                    return 22.5;
                case 2:
                    return 45.0;
                case 3:
                    return 67.5;
                case 4:
                    return 90.0;
                case 5:
                    return 112.5;
                case 6:
                    return 135.0;
                case 7:
                    return 157.5;
                case 8:
                    return 180.0;
                case 9:
                    return 202.5;
                case 10:
                    return 225.0;
                case 11:
                    return 247.5;
                case 12:
                    return 270.0;
                case 13:
                    return 292.5;
                case 14:
                    return 315.0;
                case 15:
                    return 337.5;
                default:
                    return 90.0;
            }
        }
    }
}
