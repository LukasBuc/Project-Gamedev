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
        public bool CollisionRight { get; set; }
        public bool CollisionLeft { get; set; }

        public Vector2 VelocityY = new Vector2(0, 2);
        public Vector2 VelocityX = new Vector2(3, 0);

        private float _fallspeed = (float)0.5;
        private Vector2 _totalFallSpeed;

        //Enemy info
        const int enemyHeight = 35;
        const int enemyWidth = 35;
        bool walkingLeft = false;

        //Animations
        private Animation _animationWalkRight;
        private Animation _animationWalkLeft;

        static Random r = new Random();

        public Enemy(Texture2D _texture, Vector2 _positie)
        {
            //Random keuze voor welke kant hij begint met wandelen
            if (r.Next(0, 2) == 0)
            {
                walkingLeft = true;
            }
            else
            {
                walkingLeft = false;
            }

            Texture = _texture;
            Positie = _positie;

            IsGrounded = false;
            CollisionLeft = false;
            CollisionRight = false;

            CollisionRectangle = new Rectangle((int)Positie.X, (int)Positie.Y, enemyWidth, enemyHeight);

            _animationWalkRight = new Animation();
            _animationWalkLeft = new Animation();

            _animationWalkRight.AddFrame(new Rectangle(0, 0, 33, 35));
            _animationWalkRight.AddFrame(new Rectangle(34, 0, 36, 35));
            _animationWalkRight.AddFrame(new Rectangle(71, 0, 40, 35));
            _animationWalkRight.AddFrame(new Rectangle(113, 0, 35, 35));
            _animationWalkRight.AantalBewegingenPerSeconde = 8;

            _animationWalkLeft.AddFrame(new Rectangle(0, 90, 33, 35));
            _animationWalkLeft.AddFrame(new Rectangle(33, 90, 37, 35));
            _animationWalkLeft.AddFrame(new Rectangle(71, 90, 40, 35));
            _animationWalkLeft.AddFrame(new Rectangle(113, 90, 35, 35));
            _animationWalkLeft.AantalBewegingenPerSeconde = 8;
        }

        public void Update(GameTime gameTime)
        {
            //Wandelen en collisions links, rechts
            EnemyWalk(gameTime);

            //Zwaartekracht
            if (!IsGrounded)
            {
                _totalFallSpeed = VelocityY * _fallspeed;
                Positie += _totalFallSpeed;
                _fallspeed += (float)0.09;
            }
            else
            {
                _fallspeed = (float)0.5;
                _totalFallSpeed = VelocityY * _fallspeed;
            }

            //CollisionRectangle enemy updaten
            CollisionRectangle.X = (int)Positie.X;
            CollisionRectangle.Y = (int)Positie.Y;
        }

        public void EnemyWalk(GameTime gameTime)
        {
            if (walkingLeft)
            {
                if (!CollisionLeft)
                {
                    Positie -= VelocityX;
                    _animationWalkLeft.Update(gameTime);
                }
                else
                {
                    walkingLeft = false;
                }
            }
            else
            {
                if (!CollisionRight)
                {
                    Positie += VelocityX;
                    _animationWalkRight.Update(gameTime);
                }
                else
                {
                    walkingLeft = true;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (walkingLeft)
            {
                spritebatch.Draw(Texture, Positie, _animationWalkLeft.CurrentFrame.SourceRectangle, Color.AliceBlue);
            }
            else
            {
                spritebatch.Draw(Texture, Positie, _animationWalkRight.CurrentFrame.SourceRectangle, Color.AliceBlue);
            }           
        }

        public void SetCorrectHeight(int collisionHeight)
        {
            Vector2 newPosition = new Vector2(Positie.X, collisionHeight - enemyHeight);
            Positie = newPosition;
        }

        public Rectangle GetCollisionRectangle()
        {
            return CollisionRectangle;
        }
    }
}
