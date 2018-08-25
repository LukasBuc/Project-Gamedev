using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev.Classes
{
    class Button
    {
        private Texture2D Texture { get; set; }
        private Texture2D TextureHover { get; set; }
        public Vector2 Positie { get; set; }

        private Rectangle ButtonRectangle;
        private Rectangle MouseRectangle;
        public bool buttonClicked;
        public bool mouseReleased;
        public bool mouseHover;

        Color buttonKleur = new Color(255, 255, 255, 255);

        public Button(Texture2D _texture, Texture2D _textureHover, Vector2 _positie)
        {
            Texture = _texture;
            TextureHover = _textureHover;
            Positie = _positie;
            ButtonRectangle = new Rectangle((int)Positie.X, (int)Positie.Y, Texture.Width, Texture.Height);
        }

        public void Update(MouseState mouse)
        {
            MouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (MouseRectangle.Intersects(ButtonRectangle))
            {
                mouseHover = true;
                if (mouse.LeftButton == ButtonState.Pressed && mouseReleased)
                {
                    buttonClicked = true;
                    mouseReleased = false;
                }

                if (mouse.LeftButton == ButtonState.Released)
                {
                    mouseReleased = true;
                }
            }
            else
            {
                mouseHover = false;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (mouseHover)
            {
                spritebatch.Draw(TextureHover, Positie, Color.AliceBlue);
            }
            else
            {
                spritebatch.Draw(Texture, Positie, Color.AliceBlue);
            }
            
        }
    }
}
