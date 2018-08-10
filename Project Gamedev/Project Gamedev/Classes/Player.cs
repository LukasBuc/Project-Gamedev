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

        public Player(Texture2D _texture, Vector2 _positie)
        {
            Texture = _texture;
            Positie = _positie;

            //TODO DIT IS OM TE TESTEN
            isGrounded = false;
            collisionRight = false;
            collisionLeft = false;
            collisionTop = false;

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
                if (!collisionLeft)
                {                    
                    Positie -= VelocityX;
                }
            }
            if (_controls.right)
            {
                if (!collisionRight)
                {                    
                    Positie += VelocityX;
                }
            }
            //TODO ZWAARTEKRACHT BETER MAKEN
            //Code om te springen
            if (_controls.jump && isGrounded)
            {
                Positie -= jumpVelocityStart;
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
                if (jumpCounter > 10)
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
                Positie += VelocityY * fallspeed;
                fallspeed += (float)0.2;
            }
            else
            {
                //Val snelheid resetten
                fallspeed = (float)0.5;           
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
