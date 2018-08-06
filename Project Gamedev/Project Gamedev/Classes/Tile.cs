using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev.Classes
{
    public class Tile
    {
        private Texture2D _texture;
        public Vector2 Position { get; set; }
        public Tile(Texture2D texture, Vector2 pos)
        {
            _texture = texture;
            Position = pos;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, Position, Color.AliceBlue);
        }
    }
}
