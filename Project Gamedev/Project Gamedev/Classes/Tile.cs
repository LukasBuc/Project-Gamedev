using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev.Classes
{
    public class Tile : ICollide
    {
        private Texture2D _texture;
        public Vector2 Position { get; set; }
        public Rectangle CollisionRectangle;

        public Tile(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
            CollisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, 64, 64); //128
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(_texture, Position, Color.AliceBlue);
        }

        public Rectangle GetCollisionRectangle()
        {
            return CollisionRectangle;
        }
    }
}
