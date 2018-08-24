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
        public bool FireProjectile = false;

        //Collisions
        public Rectangle CollisionRectangle;

        //Animations
        private Animation _animationWalkRight;
        private Animation _animationWalkLeft;
        private Animation _animationIdleRight;
        private Animation _animationIdleLeft;
        public Vector2 VelocityX = new Vector2(2, 0);

        public bool PlayerWalkedLeft = false;

        //Controls
        public Controls PlayerControls { get; set; }

        //Collisions
        public bool IsGrounded { get; set; }
        public bool CollisionRight { get; set; }
        public bool CollisionLeft { get; set; }
        public bool CollisionTop { get; set; }

        public Vector2 VelocityY = new Vector2(0, 2);
        public Vector2 JumpVelocity = new Vector2(0, 7);

        private float _fallspeed = (float)0.5;
        private bool _jumping = false;
        private int _jumpCounter = 0;

        bool fired = false;
        bool jumped = false;

        //Player info
        const int playerHeight = 28;
        const int playerwidth = 20;

        public bool MovingLeft = false;
        public bool MovingRight = false;

        private Vector2 _totaalFallSpeed;

        public Player(Texture2D _texture, Vector2 _positie)
        {
            Texture = _texture;
            Positie = _positie;

            IsGrounded = false;
            CollisionRight = false;
            CollisionLeft = false;
            CollisionTop = false;

            CollisionRectangle = new Rectangle((int)Positie.X, (int)Positie.Y, playerwidth, playerHeight);

            PlayerControls = new ControlArrows();

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
            PlayerControls.Update();
            
            //Schieten
            if (PlayerControls.Fire && !fired)
            {
                FireProjectile = true;
                fired = true;
            }
            else
            {
                FireProjectile = false;              
            }

            //zorgt ervoor dat je firekey moet loslaten voor je opnieuw kan schieten
            if (!PlayerControls.Fire)
            {               
                fired = false;
            }

            //Bewegen links
            if (PlayerControls.Left)
            {
                if (!CollisionLeft)
                {
                    Positie -= VelocityX;
                    MovingLeft = true;

                    //Animation naar links wandelen
                    _animationWalkLeft.Update(gameTime);

                    //Walked left op true zetten zodat de idle animatie naar de juiste kant staat
                    PlayerWalkedLeft = true;
                }
                else
                {
                    MovingLeft = false;
                }
            }
            else
            {
                MovingLeft = false;
            }

            //Bewegen rechts
            if (PlayerControls.Right)
            {
                if (!CollisionRight)
                {
                    Positie += VelocityX;
                    MovingRight = true;
                    
                    //Animation naar rechts wandelen
                    _animationWalkRight.Update(gameTime);

                    //Walked left op false zetten zodat de idle animatie naar de juiste kant staat
                    PlayerWalkedLeft = false;
                }
                else
                {
                    MovingRight = false;
                }
            }
            else
            {
                MovingRight = false;
            }

            //Idle links/rechts
            if (!PlayerControls.Left && !PlayerControls.Right && !PlayerControls.Jump)
            {
                if (PlayerWalkedLeft)
                {
                    _animationIdleLeft.Update(gameTime);
                }
                else
                {
                    _animationIdleRight.Update(gameTime);
                }
            }

            //Jump 
            if (PlayerControls.Jump && IsGrounded && !jumped)
            {
                _jumping = true;
                jumped = true;
            }

            //zorgt ervoor dat je jumpkey moet loslaten voor je opnieuw springt
            if (!PlayerControls.Jump)
            {
                jumped = false;
            }

            //Als we vanboven een collision raken
            if (CollisionTop)
            {
                _jumping = false;
                _jumpCounter = 0;
            }

            //Jump mechanics & velocity
            if (_jumping)
            {
                if (_jumpCounter > 11) //5
                {
                    _jumping = false;
                    _jumpCounter = 0;
                }
                else
                {
                    Positie -= JumpVelocity;
                    _jumpCounter++;
                }
            }

            //Zwaartekracht en valsnelheid
            if (!IsGrounded && !_jumping)
            {
                //Zorgt ervoor dat we sneller vallen als we langer in de lucht blijven
                _totaalFallSpeed = VelocityY * _fallspeed;
                Positie += _totaalFallSpeed;
                _fallspeed += (float)0.09;
            }
            else
            {
                //Val snelheid resetten
                _fallspeed = (float)0.5;
                _totaalFallSpeed = VelocityY * _fallspeed;

            }

            //CollisionRectangle player updaten
            CollisionRectangle.X = (int)Positie.X;
            CollisionRectangle.Y = (int)Positie.Y;
        }

        //Hoogte van player juist zetten zodat die niet in voor een klein deel in objecten zit
        public void SetCorrectHeight(int collisionHeight)
        {
            Vector2 newPosition = new Vector2(Positie.X, collisionHeight - playerHeight);
            Positie = newPosition;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (PlayerControls.Left)
            {                
                spritebatch.Draw(Texture, Positie, _animationWalkLeft.CurrentFrame.SourceRectangle, Color.AliceBlue);
            }
            else if (PlayerControls.Right)
            {
                spritebatch.Draw(Texture, Positie, _animationWalkRight.CurrentFrame.SourceRectangle, Color.AliceBlue);
            }
            else
            {
                if (PlayerWalkedLeft)
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
