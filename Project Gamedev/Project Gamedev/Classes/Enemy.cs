using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev.Classes
{
    class Enemy : ICollide
    {
        //Positie
        public Vector2 Positie { get; set; }

        //Tonen enemy
        private Texture2D Texture { get; set; }

        //Collisions
        public Rectangle CollisionRectangle;

        public bool IsGrounded { get; set; }

        public Vector2 VelocityY = new Vector2(0, 2);

        private float _fallspeed = (float)0.5;
        private Vector2 _totalFallSpeed;

        //Enemy info
        const int enemyHeight = 35;
        const int enemyWidth = 35;

        //TODO ANIMATIONS TOEVOEGEN
        private Animation _animationIdleRight;
        
        public Enemy(Texture2D _texture, Vector2 _positie)
        {
            Texture = _texture;
            Positie = _positie;

            CollisionRectangle = new Rectangle((int)Positie.X, (int)Positie.Y, enemyWidth, enemyHeight);

            _animationIdleRight = new Animation();

            _animationIdleRight.AddFrame(new Rectangle(0, 47, 36, 35));
            _animationIdleRight.AddFrame(new Rectangle(37, 47, 36, 35));
            _animationIdleRight.AantalBewegingenPerSeconde = 8;

        }

        public void Update(GameTime gameTime)
        {
            _animationIdleRight.Update(gameTime);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Positie, _animationIdleRight.CurrentFrame.SourceRectangle, Color.AliceBlue);
        }
        

        public Rectangle GetCollisionRectangle()
        {
            return CollisionRectangle;
        }
    }
}
