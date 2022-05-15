using System.Drawing;

namespace BattleTournament
{
    public class GameSprite
    {
        public Bitmap SpriteImage { get; set; }
        public Bitmap[] SpriteArray { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public GameSprite()
        {

        }
 
        public GameSprite(Bitmap spriteImage, float x, float y)
        {
            SpriteImage = spriteImage;
            SpriteArray = new Bitmap[0];
            X = x;
            Y = y;
            Width = spriteImage.Width;
            Height = spriteImage.Height;
        }

        public void Draw(Graphics gfx)
        {
            gfx.DrawImage(SpriteImage, new RectangleF(X, Y, Width, Height));
        }

    }
}
