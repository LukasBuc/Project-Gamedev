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

        enum GameState
        {
            MainMenu,
            Controls,
            Level1,
            Level2
        }
        GameState CurrentGameState = GameState.MainMenu;

        //Main menu
        Button _mainStartButton;
        Button _mainControlsButton;
        Button _controlsBackButton;

        //Textures
        Texture2D _playerTexture;
        Texture2D _groundTileLeft;
        Texture2D _groundTileMiddle;
        Texture2D _groundTileRight;
        Texture2D _islandTileLeft;
        Texture2D _islandTileMiddle;
        Texture2D _islandTileRight;
        Texture2D _dirtTileLeft;
        Texture2D _dirtTileMiddle;
        Texture2D _dirtTileRight;
        Texture2D _dirtTileWallLeft;
        Texture2D _dirtTileWallMiddle;
        Texture2D _dirtTileWallRight;
        Texture2D _playerProjectileTexture;
        Texture2D _enemyMinotaurTexture;

        //Images
        Image _controlsLayout;

        //Objecten
        Player _player;
        Level _level1;
        Collisions myCollisions;
        PlayerProjectiles myProjectiles;
        Camera2d camera;
        List<Enemy> _enemyMinotaurList;

        Vector2 campos;

        MouseState mouse;

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
            IsMouseVisible = true;
            
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

            _mainStartButton = new Button(Content.Load<Texture2D>("Sprites\\Buttons\\ButtonPlay"), Content.Load<Texture2D>("Sprites\\Buttons\\ButtonPlayHover"), new Vector2(200, 100));
            _mainControlsButton = new Button(Content.Load<Texture2D>("Sprites\\Buttons\\ButtonControls"), Content.Load<Texture2D>("Sprites\\Buttons\\ButtonControlsHover"), new Vector2(200, 300));
            _controlsBackButton = new Button(Content.Load<Texture2D>("Sprites\\Buttons\\ButtonBack"), Content.Load<Texture2D>("Sprites\\Buttons\\ButtonBackHover"), new Vector2(200, 300));

            _controlsLayout = new Image(Content.Load<Texture2D>("Sprites\\Extra\\ControlsLayout"), new Vector2(150, 50));

            _playerProjectileTexture = Content.Load<Texture2D>("Sprites\\Projectiles\\fireball");
            _playerTexture = Content.Load<Texture2D>("Sprites\\Characters\\Player\\Player sprite");
            _enemyMinotaurTexture = Content.Load<Texture2D>("Sprites\\Characters\\Enemy\\Minotaur sprite");

            #region Tile textures inladen
            _groundTileLeft = Content.Load<Texture2D>("Sprites\\Tiles\\GroundTileLeft");
            _groundTileMiddle = Content.Load<Texture2D>("Sprites\\Tiles\\GroundTileMiddle");
            _groundTileRight = Content.Load<Texture2D>("Sprites\\Tiles\\GroundTileRight");
            _islandTileLeft = Content.Load<Texture2D>("Sprites\\Tiles\\IslandTileLeft");
            _islandTileMiddle = Content.Load<Texture2D>("Sprites\\Tiles\\IslandTileMiddle");
            _islandTileRight = Content.Load<Texture2D>("Sprites\\Tiles\\IslandTileRight");
            _dirtTileLeft = Content.Load<Texture2D>("Sprites\\Tiles\\DirtTileLeft");
            _dirtTileMiddle = Content.Load<Texture2D>("Sprites\\Tiles\\DirtTileMiddle");
            _dirtTileRight = Content.Load<Texture2D>("Sprites\\Tiles\\DirtTileRight");
            _dirtTileWallLeft = Content.Load<Texture2D>("Sprites\\Tiles\\DirtTileWallLeft");
            _dirtTileWallMiddle = Content.Load<Texture2D>("Sprites\\Tiles\\DirtTileWallMiddle");
            _dirtTileWallRight = Content.Load<Texture2D>("Sprites\\Tiles\\DirtTileWallRight");
           
            #endregion

            //Level inladen
            #region Level inladen
            _level1 = new Level();

            _level1.AddTextures(_groundTileLeft);       // 0
            _level1.AddTextures(_groundTileMiddle);     // 1
            _level1.AddTextures(_groundTileRight);      // 2
            _level1.AddTextures(_islandTileLeft);       // 3
            _level1.AddTextures(_islandTileMiddle);     // 4
            _level1.AddTextures(_islandTileRight);      // 5
            _level1.AddTextures(_dirtTileLeft);         // 6
            _level1.AddTextures(_dirtTileMiddle);       // 7
            _level1.AddTextures(_dirtTileRight);        // 8
            _level1.AddTextures(_dirtTileWallLeft);     // 9
            _level1.AddTextures(_dirtTileWallMiddle);   // 10
            _level1.AddTextures(_dirtTileWallRight);    // 11
            _level1.CreateWorld();
            #endregion


            //Player projectile object aanmaken en texture geven
            myProjectiles = new PlayerProjectiles();
            myProjectiles.Texture = _playerProjectileTexture;

            //Player object aanmaken 
            _player = new Player(_playerTexture, new Vector2(90, 280));

            //Enemy objects aanmaken
            _enemyMinotaurList = new List<Enemy>
            {
                new Enemy(_enemyMinotaurTexture, new Vector2(100, 480)),
                new Enemy(_enemyMinotaurTexture, new Vector2(230, 480)),
                new Enemy(_enemyMinotaurTexture, new Vector2(580, 480)),
                new Enemy(_enemyMinotaurTexture, new Vector2(730, 480)),
                new Enemy(_enemyMinotaurTexture, new Vector2(850, 480)),
                new Enemy(_enemyMinotaurTexture, new Vector2(950, 480))
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
            campos = new Vector2(-150, 90);
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

            switch (CurrentGameState)
            {
                #region Main menu
                case GameState.MainMenu:
                    mouse = Mouse.GetState();

                    if (_mainStartButton.buttonClicked)
                    {
                        CurrentGameState = GameState.Level1;
                        IsMouseVisible = false;
                        _mainStartButton.buttonClicked = false;
                    }

                    if (_mainControlsButton.buttonClicked)
                    {
                        CurrentGameState = GameState.Controls;
                        _mainControlsButton.buttonClicked = false;
                    }
                    _mainStartButton.Update(mouse);
                    _mainControlsButton.Update(mouse);
                    break;
                #endregion

                #region Controls
                case GameState.Controls:
                    mouse = Mouse.GetState();

                    if (_controlsBackButton.buttonClicked)
                    {
                        CurrentGameState = GameState.MainMenu;
                        _controlsBackButton.buttonClicked = false;
                    }
                    _controlsBackButton.Update(mouse);
                    break;
                #endregion

                #region Level1
                case GameState.Level1:
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
                    //if (myCollisions.TopCollisions.Count > 0)
                    //{
                    //    Console.WriteLine("TOP COLLISION");
                    //    _player.CollisionTop = true;
                    //}
                    //else
                    //{
                    //    _player.CollisionTop = false;
                    //}
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
                                //LoadContent();
                                _player.playerKilled = true;
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

                    //Checken of player dood is
                    if (_player.playerKilled)
                    {
                        LoadContent();
                        _player.playerKilled = false;
                    }
                    break;
                #endregion

                #region Level2
                case GameState.Level2:
                    break;
                #endregion
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
            
            switch (CurrentGameState)
            {
                #region Main menu
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    _mainStartButton.Draw(spriteBatch);
                    _mainControlsButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                #endregion

                #region Controls
                case GameState.Controls:
                    spriteBatch.Begin();
                    _controlsLayout.Draw(spriteBatch);
                    _controlsBackButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                #endregion

                #region Level1
                case GameState.Level1:
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
                    break;
                #endregion

                #region Level2
                case GameState.Level2:
                    break;
                #endregion                
            }
            
            base.Draw(gameTime);
        }
    }
}
