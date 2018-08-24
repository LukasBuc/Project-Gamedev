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

        //Textures
        Texture2D _playerTexture;
        Texture2D _tileTexture;
        Texture2D _playerProjectileTexture;
        Texture2D _enemyMinotaurTexture;

        //Objecten
        Player _player;
        Level _level1;
        Collisions myCollisions;
        PlayerProjectiles myProjectiles;
        Camera2d camera;
        List<Enemy> _enemyMinotaurList;

        Vector2 campos;

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

            //Textures inladen
            _tileTexture = Content.Load<Texture2D>("Sprites\\Tiles\\TileSmall (1)");
            _playerProjectileTexture = Content.Load<Texture2D>("Sprites\\Projectiles\\fireball");
            _playerTexture = Content.Load<Texture2D>("Sprites\\Characters\\Player\\Player sprite");
            _enemyMinotaurTexture = Content.Load<Texture2D>("Sprites\\Characters\\Enemy\\Minotaur sprite");

            //Level inladen
            _level1 = new Level
            {
                Texture = _tileTexture
            };
            _level1.CreateWorld();

            //Player projectile object aanmaken en texture geven
            myProjectiles = new PlayerProjectiles();
            myProjectiles.Texture = _playerProjectileTexture;

            //Player object aanmaken 
            _player = new Player(_playerTexture, new Vector2(150, 100));

            //Enemy objects aanmaken
            _enemyMinotaurList = new List<Enemy>
            {
                new Enemy(_enemyMinotaurTexture, new Vector2(100, 300)),
                new Enemy(_enemyMinotaurTexture, new Vector2(150, 300)),
                new Enemy(_enemyMinotaurTexture, new Vector2(200, 300)),
                new Enemy(_enemyMinotaurTexture, new Vector2(460, 100))
            };

            //Collision object aanmaken
            myCollisions = new Collisions();

            //Ophalen welke tiles worden gebruikt en in lijst zetten
            foreach (var item in _level1.TileArray)
            {
                if (item != null)
                {
                    myCollisions.AddCollisionsObject(item);
                }
            }

            //Camera start positie
            campos = new Vector2(-150, 100);
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

            #region Update characters
            _player.Update(gameTime);

            if (_enemyMinotaurList != null)
            {
                foreach (var enemy in _enemyMinotaurList)
                {
                    enemy.Update(gameTime);
                }
            }
            #endregion

            #region player schieten
            if (_player.FireProjectile)
            {
                myProjectiles.AddPlayerProjectile(_playerProjectileTexture, new Vector2(_player.Positie.X, _player.Positie.Y), _player.PlayerWalkedLeft, gameTime.TotalGameTime.TotalSeconds);
            }

            //projectile lijst wordt elke update opnieuw ingeladen
            myCollisions.ClearProjectileCollisions();

            //Projectiles toevoegen aan lijst voor collisions
            foreach (var item in myProjectiles.PlayerProjectileList)
            {
                myCollisions.AddProjectileCollisionObjects(item);
            }
            #endregion

            #region Collisions player
            //Controleren waar de collisions plaatsvinden
            myCollisions.CheckCollisions(_player);

            //Collisions bottom controleren
            if (myCollisions.BottomCollisions.Count > 0)
            {
                Console.WriteLine("GROUNDED");
                _player.IsGrounded = true;

                //Y positie juist zetten zodat player niet in de grond zit
                _player.SetCorrectHeight(myCollisions.BottomCollisionHeight);
            }
            else
            {
                _player.IsGrounded = false;
            }

            //Collisions rechts controlleren
            if (myCollisions.RightCollisions.Count > 0)
            {
                Console.WriteLine("RIGHT COLLISION");
                _player.CollisionRight = true;
            }
            else
            {
                _player.CollisionRight = false;
            }

            //Collisions left controleren
            if (myCollisions.LeftCollisions.Count > 0)
            {
                Console.WriteLine("LEFT COLLISION");
                _player.CollisionLeft = true;
            }
            else
            {
                _player.CollisionLeft = false;
            }

            //Collision top controleren
            if (myCollisions.TopCollisions.Count > 0)
            {
                Console.WriteLine("TOP COLLISION");
                _player.CollisionTop = true;
            }
            else
            {
                _player.CollisionTop = false;
            }
            #endregion

            #region player projectiles updaten & controleren
            myProjectiles.UpdatePlayerProjectiles(gameTime);

            //Controleren of er een collision is met de level tiles
            int collisionIndex = myCollisions.CheckProjectileCollisions();
            if (collisionIndex != -1)
            {
                myProjectiles.RemovePlayerProjectile(collisionIndex);
            }
            #endregion

            #region Collisions enemies controleren
            if (_enemyMinotaurList != null)
            {
                foreach (var enemy in _enemyMinotaurList)
                {
                    myCollisions.CheckCollisions(enemy);
                    if (myCollisions.BottomCollisions.Count > 0)
                    {
                        enemy.IsGrounded = true;
                        enemy.setCorrectHeight(myCollisions.BottomCollisionHeight);
                    }
                    else
                    {
                        enemy.IsGrounded = false;
                    }

                    if (myCollisions.LeftCollisions.Count > 0)
                    {
                        enemy.CollisionLeft = true;
                    }
                    else
                    {
                        enemy.CollisionLeft = false;
                    }

                    if (myCollisions.RightCollisions.Count > 0)
                    {
                        enemy.CollisionRight = true;
                    }
                    else
                    {
                        enemy.CollisionRight = false;
                    }
                    //enemy playerprojectile collision
                    collisionIndex = myCollisions.CheckEnemyPlayerProjectileHit(enemy);
                    if (collisionIndex != -1)
                    {
                        myProjectiles.RemovePlayerProjectile(collisionIndex);
                        _enemyMinotaurList.Remove(enemy);
                        break;
                    }

                    //Player enemy collision
                    if (myCollisions.CheckEnemyPlayerCollision(_player, enemy))
                    {
                        //_player.Reset();
                        //campos = new Vector2(-150, 100);
                        LoadContent();
                    }
                }
            }
            #endregion

            //Camera positie
            if (_player.MovingRight)
            {
                campos += _player.VelocityX;
            }

            if (_player.MovingLeft)
            {
                campos -= _player.VelocityX;
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

            _level1.DrawLevel(spriteBatch);
            _player.Draw(spriteBatch);

            //Als er enemy minotaurs zijn ze tonen
            if (_enemyMinotaurList != null)
            {
                foreach (var enemy in _enemyMinotaurList)
                {
                    enemy.Draw(spriteBatch);
                }
            }

            myProjectiles.DrawPlayerProjectiles(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
