using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev
{
    public class Player
    {
        public Vector2 Positie { get; set; }
        private Texture2D Texture { get; set; }
        private Rectangle _ShowRect;

        private Animation _animation;
        public Vector2 VelocityX = new Vector2(2, 0);
        public Controls _controls { get; set; }

        public Player(Texture2D _texture, Vector2 _positie)
        {
            Texture = _texture;
            Positie = _positie;
            _ShowRect = new Rectangle(0, 0, 64, 205);

            _animation = new Animation();
            _animation.AddFrame(new Rectangle(0, 0, 64, 205));
            _animation.AddFrame(new Rectangle(64, 0, 64, 205));
            _animation.AddFrame(new Rectangle(128, 0, 64, 205));
            _animation.AddFrame(new Rectangle(192, 0, 64, 205));
            _animation.AantalBewegingenPerSeconde = 8;
        }

        public void Update(GameTime gameTime)
        {
            _controls.Update();

            if (_controls.left || _controls.right)
                _animation.Update(gameTime);
            if (_controls.left)
            {
                Positie -= VelocityX;
            }
            if (_controls.right)
            {
                Positie += VelocityX;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Positie, _animation.CurrentFrame.SourceRectangle, Color.AliceBlue);
        }
    }
}
