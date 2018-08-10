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

        //TODO DIT IS OM TE TESTEN
        public bool isGrounded { get; set; }
        public bool collisionRight { get; set; }
        public bool collisionLeft { get; set; }
        public bool collisionTop { get; set; }

        public Vector2 VelocityY = new Vector2(0, 2);
        public Vector2 jumpVelocity = new Vector2(0, 5); //5
        public Vector2 jumpVelocityStart = new Vector2(0, 90); //30

        float fallspeed = (float)0.5;
        bool jumping = false;
        int jumpCounter = 0;

        //Player info
        const int playerHeight = 28;
        const int playerwidth = 20;

        public bool movingLeft = false;
        public bool movingRight = false;
        public bool movingUp = false;
        public bool movingDown = false;
        public Vector2 totaalFallSpeed;
        public Vector2 jumpSpeed;

        bool firstTick = false;

        public Player(Texture2D _texture, Vector2 _positie)
        {
            Texture = _texture;
            Positie = _positie;

            //TODO DIT IS OM TE TESTEN
            isGrounded = false;
            collisionRight = false;
            collisionLeft = false;
            collisionTop = false;

            _ShowRect = new Rectangle(0, 0, playerwidth, playerHeight);

            CollisionRectangle = new Rectangle((int)Positie.X, (int)Positie.Y, playerwidth, playerHeight);

            _controls = new ControlArrows();

            _animation = new Animation();
            _animation.AddFrame(new Rectangle(0, 0, 20, playerHeight));
            _animation.AddFrame(new Rectangle(21, 0, 20, playerHeight));
            _animation.AddFrame(new Rectangle(42, 0, 20, playerHeight));
            _animation.AddFrame(new Rectangle(63, 0, 23, playerHeight));
            _animation.AddFrame(new Rectangle(87, 0, 20, playerHeight));
            _animation.AddFrame(new Rectangle(108, 0, 20, playerHeight));
            _animation.AantalBewegingenPerSeconde = 8;
        }

        public void Update(GameTime gameTime)
        {
            _controls.Update();

            //Bewegen links
            if (_controls.left || _controls.right)
            {
                _animation.Update(gameTime);
            }

            if (_controls.left)
            {
                if (!collisionLeft)
                {
                    Positie -= VelocityX;
                    movingLeft = true;
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

            //Code om te springen
            if (_controls.jump && isGrounded)
            {
                Positie -= jumpVelocityStart;
                jumpSpeed = jumpVelocityStart;
                jumping = true;
                movingUp = true;
                firstTick = true;
            }

            //Als we vanboven een collision raken
            if (collisionTop)
            {
                jumping = false;
                jumpCounter = 0;
                movingUp = false;
            }

            //Jump mechanics & velocity
            if (jumping)
            {
                if (jumpCounter > 10)
                {
                    jumping = false;
                    jumpCounter = 0;
                    movingUp = false;
                    movingDown = true;

                    
                }
                else
                {
                    Positie -= jumpVelocity;
                    if (firstTick)
                    {
                        firstTick = false;
                    }
                    else
                    {
                        jumpSpeed = jumpVelocity;
                    }
                    
                    jumpCounter++;
                    
                }
            }

            //Zwaartekracht en valsnelheid
            if (!isGrounded && !jumping)
            {
                //Zorgt ervoor dat we sneller vallen als we langer in de lucht blijven
                totaalFallSpeed = VelocityY * fallspeed;
                Positie += totaalFallSpeed;
                fallspeed += (float)0.2;
            }
            else
            {
                //Val snelheid resetten
                fallspeed = (float)0.5;
                totaalFallSpeed = VelocityY * fallspeed;
                movingDown = false;
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
            spritebatch.Draw(Texture, Positie, _animation.CurrentFrame.SourceRectangle, Color.AliceBlue);
        }

        public Rectangle GetCollisionRectangle()
        {
            return CollisionRectangle;
        }
    }
}
