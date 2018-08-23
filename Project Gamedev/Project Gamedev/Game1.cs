using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_Gamedev.Classes;
using System.Collections.Generic;

namespace Project_Gamedev
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D _playerTexture;
        Player _player;
        Texture2D tileTexture;
        Level level1;
        Collisions myCollisions;
        Vector2 campos = new Vector2();

        Texture2D _projectileTexture;
        Projectile playerProjectile;
        bool projectileCreated = false;

        Camera2d camera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            camera = new Camera2d(GraphicsDevice.Viewport);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tileTexture = Content.Load<Texture2D>("Sprites\\Tiles\\TileSmall (1)"); //"Sprites\\Tiles\\Tile (1)"
            level1 = new Level();
            level1.texture = tileTexture;
            level1.CreateWorld();

            _projectileTexture = Content.Load<Texture2D>("Sprites\\Projectiles\\fireball");
            playerProjectile = new Projectile();


            _playerTexture = Content.Load<Texture2D>("Sprites\\Characters\\Player\\Player sprite");
            _player = new Player(_playerTexture, new Vector2(150, 100));

            

            myCollisions = new Collisions();

            //TODO DIT IS OM TE TESTEN
            //Collisionobjecten toevoegen aan lijst
            //myCollisions.AddCollisionsObject(_player);

            //Ophalen welke tiles worden gebruikt en in lijst zetten
            foreach (var item in level1.tileArray)
            {
                if (item != null)
                {
                    myCollisions.AddCollisionsObject(item);
                }             
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime);

            myCollisions.getCollisionObjects(_player);
            myCollisions.checkCollisions(_player);

            //Controleren of er collisions zijn met de player
            //if (myCollisions.checkCollisions(_player))
            //{
            //    System.Console.WriteLine("AAAAAAA");
            //}

            //Collisions bottom controleren
            if (myCollisions.bottomCollisions.Count > 0)
            {
                Console.WriteLine("GROUNDED");
                _player.isGrounded = true;

                //Y positie juist zetten zodat player niet in de grond zit
                _player.setCorrectHeigt(myCollisions.bottomCollisionHeight);
            }
            else
            {
                _player.isGrounded = false;
            }

            //Collisions rechts controlleren
            if (myCollisions.rightCollisions.Count > 0)
            {
                Console.WriteLine("RIGHT COLLISION");
                _player.collisionRight = true;
            }
            else
            {
                _player.collisionRight = false;
            }

            //Collisions left controleren
            if (myCollisions.leftCollisions.Count > 0)
            {
                Console.WriteLine("LEFT COLLISION");
                _player.collisionLeft = true;
            }
            else
            {
                _player.collisionLeft = false;
            }

            //Collision top controleren
            if (myCollisions.topCollisions.Count > 0)
            {
                Console.WriteLine("TOP COLLISION");
                _player.collisionTop = true;
            }
            else
            {
                _player.collisionTop = false;
            }

            //Camera positie
            if (_player.movingRight)
            {
                campos += _player.VelocityX;
            }

            if (_player.movingLeft)
            {
                campos -= _player.VelocityX;
            }

            //Schieten
            if (_player.fireProjectile)
            {
                playerProjectile.CreateProjectile(_projectileTexture, new Vector2(_player.Positie.X, _player.Positie.Y), _player.walkedleft);
                projectileCreated = true;
            }

            if (projectileCreated)
            {
                playerProjectile.Update(gameTime);            
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var viewMatrix = camera.GetViewMatrix();
            camera.Position = campos;


            spriteBatch.Begin(transformMatrix: viewMatrix);

            level1.DrawLevel(spriteBatch);
            _player.Draw(spriteBatch);

            if (projectileCreated)
            {
                playerProjectile.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
