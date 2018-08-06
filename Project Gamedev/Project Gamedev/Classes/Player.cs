using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Gamedev.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev
{
    public class Player : ICollide
    {
        //Positie
        public Vector2 Positie { get; set; }

        //Tonen van player
        private Texture2D Texture { get; set; }
        private Rectangle _ShowRect;

        //Collisions
        public Rectangle CollisionRectangle;

        //Animations
        private Animation _animation;
        public Vector2 VelocityX = new Vector2(2, 0);

        //Controls
        public Controls _controls { get; set; }

        public Player(Texture2D _texture, Vector2 _positie)
        {
            Texture = _texture;
            Positie = _positie;
            _ShowRect = new Rectangle(0, 0, 20, 28);

            CollisionRectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 20, 28);

            _controls = new ControlArrows();

            _animation = new Animation();
            _animation.AddFrame(new Rectangle(0, 0, 20, 28));
            _animation.AddFrame(new Rectangle(21, 0, 20, 28));
            _animation.AddFrame(new Rectangle(42, 0, 20, 28));
            _animation.AddFrame(new Rectangle(63, 0, 23, 28));
            _animation.AddFrame(new Rectangle(87, 0, 20, 28));
            _animation.AddFrame(new Rectangle(108, 0, 20, 28));
            _animation.AantalBewegingenPerSeconde = 8;
        }

        public void Update(GameTime gameTime)
        {
            _controls.Update();

            if (_controls.left || _controls.right)
            {
                _animation.Update(gameTime);
            }
                
            if (_controls.left)
            {
                Positie -= VelocityX;
            }
            if (_controls.right)
            {
                Positie += VelocityX;
            }

            CollisionRectangle.X = (int)Positie.X;
            CollisionRectangle.Y = (int)Positie.Y;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Positie, _animation.CurrentFrame.SourceRectangle, Color.AliceBlue);
        }

        public Rectangle GetCollisionRectangle()
        {
            return CollisionRectangle;
        }
    }
}
