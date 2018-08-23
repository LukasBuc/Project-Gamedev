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
        //private Rectangle _ShowRect;

        //Schieten
        public bool fireProjectile = false;


        //Collisions
        public Rectangle CollisionRectangle;

        //Animations
        private Animation _animationWalkRight;
        private Animation _animationWalkLeft;
        private Animation _animationIdleRight;
        private Animation _animationIdleLeft;
        public Vector2 VelocityX = new Vector2(2, 0);

        public bool walkedleft = false;

        //Controls
        public Controls _controls { get; set; }

        public bool isGrounded { get; set; }
        public bool collisionRight { get; set; }
        public bool collisionLeft { get; set; }
        public bool collisionTop { get; set; }

        public Vector2 VelocityY = new Vector2(0, 2);
        public Vector2 jumpVelocity = new Vector2(0, 7);

        private float fallspeed = (float)0.5;
        private bool jumping = false;
        private int jumpCounter = 0;

        //Player info
        const int playerHeight = 28;
        const int playerwidth = 20;

        public bool movingLeft = false;
        public bool movingRight = false;

        private Vector2 totaalFallSpeed;

        public Player(Texture2D _texture, Vector2 _positie)
        {
            Texture = _texture;
            Positie = _positie;

            isGrounded = false;
            collisionRight = false;
            collisionLeft = false;
            collisionTop = false;

            //_ShowRect = new Rectangle(0, 0, playerwidth, playerHeight);

            CollisionRectangle = new Rectangle((int)Positie.X, (int)Positie.Y, playerwidth, playerHeight);

            _controls = new ControlArrows();

            _animationWalkRight = new Animation();
            _animationWalkLeft = new Animation();
            _animationIdleRight = new Animation();
            _animationIdleLeft = new Animation();

            _animationWalkRight.AddFrame(new Rectangle(0, 0, 20, playerHeight));
            _animationWalkRight.AddFrame(new Rectangle(21, 0, 20, playerHeight));
            _animationWalkRight.AddFrame(new Rectangle(42, 0, 20, playerHeight));
            _animationWalkRight.AddFrame(new Rectangle(63, 0, 23, playerHeight));
            _animationWalkRight.AddFrame(new Rectangle(87, 0, 20, playerHeight));
            _animationWalkRight.AddFrame(new Rectangle(108, 0, 20, playerHeight));
            _animationWalkRight.AantalBewegingenPerSeconde = 8;
            
            _animationWalkLeft.AddFrame(new Rectangle(0, 96, 20, playerHeight));
            _animationWalkLeft.AddFrame(new Rectangle(21, 96, 20, playerHeight));
            _animationWalkLeft.AddFrame(new Rectangle(42, 96, 20, playerHeight));
            _animationWalkLeft.AddFrame(new Rectangle(63, 96, 20, playerHeight));
            _animationWalkLeft.AddFrame(new Rectangle(87, 96, 20, playerHeight));
            _animationWalkLeft.AddFrame(new Rectangle(108, 96, 20, playerHeight));
            _animationWalkLeft.AantalBewegingenPerSeconde = 8;

            _animationIdleRight.AddFrame(new Rectangle(0, 34, 19, playerHeight));
            _animationIdleRight.AddFrame(new Rectangle(20, 34, 17, playerHeight));
            _animationIdleRight.AddFrame(new Rectangle(38, 34, 19, playerHeight));
            _animationIdleRight.AantalBewegingenPerSeconde = 3;

            _animationIdleLeft.AddFrame(new Rectangle(0, 128, 19, playerHeight));
            _animationIdleLeft.AddFrame(new Rectangle(20, 128, 19, playerHeight));
            _animationIdleLeft.AddFrame(new Rectangle(40, 128, 18, playerHeight));
            _animationIdleLeft.AantalBewegingenPerSeconde = 3;
        }

        public void Update(GameTime gameTime)
        {
            _controls.Update();

            //Schieten
            if (_controls.fire)
            {
                fireProjectile = true;
            }
            else
            {
                fireProjectile = false;
            }

            //Bewegen links
            if (_controls.left)
            {
                if (!collisionLeft)
                {
                    Positie -= VelocityX;
                    movingLeft = true;

                    //Animation naar links wandelen
                    _animationWalkLeft.Update(gameTime);

                    //Walked left op true zetten zodat de idle animatie naar de juiste kant staat
                    walkedleft = true;
                }
                else
                {
                    movingLeft = false;
                }
            }
            else
            {
                movingLeft = false;
            }

            //Bewegen rechts
            if (_controls.right)
            {
                if (!collisionRight)
                {
                    Positie += VelocityX;
                    movingRight = true;
                    
                    //Animation naar rechts wandelen
                    _animationWalkRight.Update(gameTime);

                    //Walked left op false zetten zodat de idle animatie naar de juiste kant staat
                    walkedleft = false;
                }
                else
                {
                    movingRight = false;
                }
            }
            else
            {
                movingRight = false;
            }

            //Idle links/rechts
            if (!_controls.left && !_controls.right && !_controls.jump)
            {
                if (walkedleft)
                {
                    _animationIdleLeft.Update(gameTime);
                }
                else
                {
                    _animationIdleRight.Update(gameTime);
                }
            }

            //Jump 
            if (_controls.jump && isGrounded)
            {
                jumping = true;
            }


            //Als we vanboven een collision raken
            if (collisionTop)
            {
                jumping = false;
                jumpCounter = 0;
            }

            //Jump mechanics & velocity
            if (jumping)
            {
                if (jumpCounter > 11) //5
                {
                    jumping = false;
                    jumpCounter = 0;
                }
                else
                {
                    Positie -= jumpVelocity;
                    jumpCounter++;
                }
            }

            //Zwaartekracht en valsnelheid
            if (!isGrounded && !jumping)
            {
                //Zorgt ervoor dat we sneller vallen als we langer in de lucht blijven
                totaalFallSpeed = VelocityY * fallspeed;
                Positie += totaalFallSpeed;
                fallspeed += (float)0.09;
            }
            else
            {
                //Val snelheid resetten
                fallspeed = (float)0.5;
                totaalFallSpeed = VelocityY * fallspeed;

            }

            CollisionRectangle.X = (int)Positie.X;
            CollisionRectangle.Y = (int)Positie.Y;
        }

        public void setCorrectHeigt(int collisionHeight)
        {
            Vector2 newPosition = new Vector2(Positie.X, collisionHeight - playerHeight);
            Positie = newPosition;
        }

        public void Draw(SpriteBatch spritebatch)
        {

            if (_controls.left)
            {                
                spritebatch.Draw(Texture, Positie, _animationWalkLeft.CurrentFrame.SourceRectangle, Color.AliceBlue);
            }
            else if (_controls.right)
            {
                spritebatch.Draw(Texture, Positie, _animationWalkRight.CurrentFrame.SourceRectangle, Color.AliceBlue);
            }
            else
            {
                if (walkedleft)
                {
                    spritebatch.Draw(Texture, Positie, _animationIdleLeft.CurrentFrame.SourceRectangle, Color.AliceBlue);
                }
                else
                {
                    spritebatch.Draw(Texture, Positie, _animationIdleRight.CurrentFrame.SourceRectangle, Color.AliceBlue);
                }
            }
        }

        public Rectangle GetCollisionRectangle()
        {
            return CollisionRectangle;
        }
    }
}
