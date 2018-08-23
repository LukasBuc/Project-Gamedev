using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Gamedev.Classes
{
    class Projectile : ICollide
    {
        //Positie
        public Vector2 Positie { get; set; }
        
        //Snelheid projectiel
        private Vector2 VelocityX = new Vector2(5, 0);

        //Richting die wordt geschoten
        private bool shootLeft;

        //Projectiel tonen
        private Texture2D Texture { get; set; }
        private Animation _animationProjectileLeft;
        private Animation _animationProjectileRight;
        
        //Collision
        public Rectangle CollisionRectangle;

        //Tijd dat projectiel leeft
        public double timeToLive;

        public Projectile(Texture2D _texture, Vector2 _positie, bool lookingLeft, double currentTime)
        {
            Texture = _texture;
            Positie = _positie;
            _animationProjectileLeft = new Animation();
            _animationProjectileRight = new Animation();

            //Huidige tijd bijhouden zodat we later hiermee kunne rekenen
            timeToLive = currentTime;

            _animationProjectileRight.AddFrame(new Rectangle(0, 0, 15, 7));
            _animationProjectileRight.AantalBewegingenPerSeconde = 0;

            _animationProjectileLeft.AddFrame(new Rectangle(0, 7, 15, 7));
            _animationProjectileLeft.AantalBewegingenPerSeconde = 0;

            //Checken naar welke kant het projectiel moet vliegen
            if (lookingLeft)
            {
                shootLeft = true;
            }
            else
            {
                shootLeft = false;
            }

            CollisionRectangle = new Rectangle((int)Positie.X, (int)Positie.Y, 15, 7);
        }

        public void Update(GameTime gameTime)
        {
            if (shootLeft)
            {
                Positie -= VelocityX;             
            }
            else
            {
                Positie += VelocityX;
            }
            CollisionRectangle.X = (int)Positie.X;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (shootLeft)
            {
                spritebatch.Draw(Texture, Positie, _animationProjectileLeft.CurrentFrame.SourceRectangle, Color.AliceBlue);
                
            }
            else
            {
                spritebatch.Draw(Texture, Positie, _animationProjectileRight.CurrentFrame.SourceRectangle, Color.AliceBlue);
            }
        }

        public Rectangle GetCollisionRectangle()
        {
            return CollisionRectangle;
        }
    }
}
