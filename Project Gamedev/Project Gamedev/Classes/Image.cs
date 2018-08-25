using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev.Classes
{
    class Image
    {
        private Texture2D Texture { get; set; }
        public Vector2 Positie { get; set; }

        public Image(Texture2D _texture, Vector2 _positie)
        {
            Texture = _texture;
            Positie = _positie;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Positie, Color.AliceBlue);
        }
    }
}
