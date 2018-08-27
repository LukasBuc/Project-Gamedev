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

        SpriteFont scoreFont;
        SpriteFont endScreenFont;

        enum GameState
        {
            MainMenu,
            Controls,
            Level1,
            Level2,
            EndScreen
        }
        GameState CurrentGameState = GameState.MainMenu;

        int playerPoints = 0, level1Points;
        bool level1Cleared = false;

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
        Texture2D _DirtToGrassLeft;
        Texture2D _DirtToGrassRight;
        Texture2D _GrassTileLeft;
        Texture2D _GrassTileRight;
        Texture2D _playerProjectileTexture;
        Texture2D _enemyMinotaurTexture;

        //Images
        Image _controlsLayout;
        Image _victoryAchieved;

        //Objecten
        Player _player;
        Level _level1;
        Level _level2;
        Collisions myCollisions;
        PlayerProjectiles myProjectiles;
        Camera2d camera;
        List<Enemy> _enemyMinotaurList;

        Vector2 campos;

        MouseState mouse;


        byte[,] Level1Array, Level2Array;
        int currentLevel;

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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scoreFont = Content.Load<SpriteFont>("Sprites\\Fonts\\ScoreFont");
            endScreenFont = Content.Load<SpriteFont>("Sprites\\Fonts\\EndScreenFont");

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    _mainStartButton = new Button(Content.Load<Texture2D>("Sprites\\Buttons\\ButtonPlay"), Content.Load<Texture2D>("Sprites\\Buttons\\ButtonPlayHover"), new Vector2(200, 100));
                    _mainControlsButton = new Button(Content.Load<Texture2D>("Sprites\\Buttons\\ButtonControls"), Content.Load<Texture2D>("Sprites\\Buttons\\ButtonControlsHover"), new Vector2(200, 300));
                    break;
                case GameState.Controls:
                    _controlsBackButton = new Button(Content.Load<Texture2D>("Sprites\\Buttons\\ButtonBack"), Content.Load<Texture2D>("Sprites\\Buttons\\ButtonBackHover"), new Vector2(200, 300));
                    _controlsLayout = new Image(Content.Load<Texture2D>("Sprites\\Extra\\ControlsLayout"), new Vector2(150, 50));
                    break;
                case GameState.Level1:

                    LoadTextures();

                    #region Level1 inladen
                    Level1Array = new byte[,]
                    {
                        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,11,11,11,11,11,11,11,11,11,11,11,11,11 },
                        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,11,11,11,11,11,11,11,11,11,11,11,11,11 },
                        {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,11,11,11,11,11,11,11,11,11,11,11,11,11 },
                        {1,0,0,0,0,0,0,0,0,0,0,0,4,5,6,0,0,0,4,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,8,8,8,8,8,8,8,8,8,8,8,8,8 },
                        {1,0,0,4,6,0,0,0,0,4,6,0,0,0,0,0,4,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                        {1,6,0,0,0,0,0,4,6,0,0,0,0,0,4,6,0,0,0,0,4,5,5,5,6,0,0,0,0,0,4,6,0,0,0,0,1,2,2,2,2,2,2,2,2,2,2,2,2,2 },
                        {1,0,0,0,0,0,0,0,0,0,0,0,4,6,0,0,0,0,0,0,0,0,0,0,0,0,0,4,6,0,0,0,0,0,0,0,10,11,11,11,11,11,11,11,11,11,11,11,11,11 },
                        {1,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,4,6,0,0,0,0,0,0,0,0,0,10,11,11,11,11,11,11,11,11,11,11,11,11,11 },
                        {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,10,11,11,11,11,11,11,11,11,11,11,11,11,11 },
                    };

                    _level1 = new Level(Level1Array);
                    AddTexturesToLevel(_level1);
                    _level1.CreateWorld();

                    //Player projectile object aanmaken en texture geven
                    myProjectiles = new PlayerProjectiles
                    {
                        Texture = _playerProjectileTexture
                    };
                    
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

                    myCollisions = new Collisions();

                    //Ophalen welke tiles worden gebruikt en in lijst zetten
                    foreach (var item in _level1.TileArray)
                    {
                        if (item != null)
                        {
                            myCollisions.AddCollisionsObject(item);
                        }
                    }
                    #endregion

                    //Camera start positie
                    campos = new Vector2(-150, 90);
                    break;
                case GameState.Level2:
                    LoadTextures();

                    #region Level2 inladen
                    Level2Array = new byte[,]
                    {
                        { 12,0,0,0,0,0,0,0,0,0,10 },
                        { 12,0,0,0,0,0,0,0,0,0,10 },
                        { 12,0,0,0,0,0,0,0,0,0,10 },
                        { 12,4,5,6,0,0,0,0,4,6,10 },
                        { 12,4,5,5,6,0,0,4,5,6,10 },
                        { 12,0,0,0,0,2,2,0,0,0,10 },
                        { 12,0,0,0,1,0,0,6,0,0,10 },
                        { 12,0,0,1,0,0,0,0,0,4,10 },
                        { 12,0,4,0,0,0,0,4,6,0,10 },
                        { 12,1,0,0,0,1,0,0,0,4,10 },
                        { 12,1,1,1,1,11,6,0,4,0,10 },
                        { 12,0,0,0,0,0,0,4,6,0,10 },
                        { 12,0,0,0,0,4,6,0,0,0,10 },
                        { 12,0,0,0,4,6,0,0,0,0,10 },
                        { 12,0,4,5,6,0,0,0,0,0,10 },
                        { 12,1,0,0,0,0,1,6,0,0,10 },
                        { 12,2,2,2,2,2,9,0,0,1,10 },
                        { 12,11,11,11,11,12,0,0,1,11,10 },
                        { 12,8,8,8,8,8,6,0,0,0,10 },
                        { 12,0,0,0,0,0,0,1,0,0,10 },
                        { 13,15,2,2,2,2,2,2,2,16,14 }
                    };

                    //Level 2 aanmaken en textures inladen
                    _level2 = new Level(Level2Array);
                    AddTexturesToLevel(_level2);
                    _level2.CreateWorld();

                    //Player projectile object aanmaken en texture geven
                    myProjectiles = new PlayerProjectiles
                    {
                        Texture = _playerProjectileTexture
                    };

                    //Player object aanmaken 
                    _player = new Player(_playerTexture, new Vector2(90, 1250));

                    //Enemy objects aanmaken
                    _enemyMinotaurList = new List<Enemy>
                    {
                        new Enemy(_enemyMinotaurTexture, new Vector2(80, 1000)),
                        new Enemy(_enemyMinotaurTexture, new Vector2(80, 600))
                    };

                    //Collision object aanmaken
                    myCollisions = new Collisions();

                    //Ophalen welke tiles worden gebruikt en in lijst zetten
                    foreach (var item in _level2.TileArray)
                    {
                        if (item != null)
                        {
                            myCollisions.AddCollisionsObject(item);
                        }
                    }
                    #endregion

                    //Camera start positie
                    campos = new Vector2(-50, 1050);
                    break;
                case GameState.EndScreen:
                    _victoryAchieved = new Image(Content.Load<Texture2D>("Sprites\\Extra\\VictoryAchieved"), new Vector2(-80, 150));
                    break;
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
                        LoadContent();
                    }

                    if (_mainControlsButton.buttonClicked)
                    {
                        CurrentGameState = GameState.Controls;
                        _mainControlsButton.buttonClicked = false;
                        LoadContent();
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
                        LoadContent();
                    }
                    _controlsBackButton.Update(mouse);
                    break;
                #endregion

                case GameState.Level1:
                case GameState.Level2:
                    #region Update characters
                    _player.Update(gameTime, campos, currentLevel);

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
                        myProjectiles.AddPlayerProjectile(_playerProjectileTexture, new Vector2(_player.Positie.X, _player.Positie.Y + 5), _player.PlayerWalkedLeft, gameTime.TotalGameTime.TotalSeconds);
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
                        _player.IsGrounded = true;

                        //Y positie juist zetten zodat player niet in de grond zit
                        _player.SetCorrectHeight(myCollisions.BottomCollisionHeight);
                    }
                    else
                    {
                        _player.IsGrounded = false;
                    }

                    //Collisions right controlleren
                    if (myCollisions.RightCollisions.Count > 0)
                    {
                        _player.CollisionRight = true;
                    }
                    else
                    {
                        _player.CollisionRight = false;
                    }

                    //Collisions left controleren
                    if (myCollisions.LeftCollisions.Count > 0)
                    {
                        _player.CollisionLeft = true;
                    }
                    else
                    {
                        _player.CollisionLeft = false;
                    }

                    //Collision top controleren
                    if (myCollisions.TopCollisions.Count > 0)
                    {
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
                                enemy.SetCorrectHeight(myCollisions.BottomCollisionHeight);
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
                                playerPoints += 100;
                                break;
                            }

                            //Player enemy collision
                            if (myCollisions.CheckEnemyPlayerCollision(_player, enemy))
                            {
                                _player.playerKilled = true;
                            }
                        }
                    }
                    #endregion

                    //Checken of player dood is
                    if (_player.playerKilled)
                    {                        
                        _player.playerKilled = false;
                        playerPoints = 0;
                        if (level1Cleared)
                        {
                            playerPoints = level1Points;
                        }
                        LoadContent();
                    }

                    if (_player.level1Cleared)
                    {
                        CurrentGameState = GameState.Level2;
                        level1Points = playerPoints;
                        level1Cleared = true;
                        LoadContent();
                    }

                    if (_player.level2Cleared)
                    {
                        CurrentGameState = GameState.EndScreen;
                        LoadContent();
                    }
                    Console.WriteLine(playerPoints);
                    break;
            }

            if (CurrentGameState == GameState.Level1)
            {
                currentLevel = 1;
                //Camera positie
                if (_player.MovingRight)
                {
                    campos += _player.VelocityX;
                }

                if (_player.MovingLeft)
                {
                    campos -= _player.VelocityX;
                }
            }
            else if (CurrentGameState == GameState.Level2)
            {
                currentLevel = 2;
                //Camera automatisch laten stijgen
                campos -= new Vector2(0, 1);
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

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    _mainStartButton.Draw(spriteBatch);
                    _mainControlsButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.Controls:
                    spriteBatch.Begin();
                    _controlsLayout.Draw(spriteBatch);
                    _controlsBackButton.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case GameState.Level1:
                    DrawLevel(_level1, viewMatrix);
                    break;
                case GameState.Level2:
                    DrawLevel(_level2, viewMatrix);
                    break;
                case GameState.EndScreen:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    spriteBatch.DrawString(endScreenFont, "Totale score: " + playerPoints.ToString(), new Vector2(300, 300), Color.White);
                    _victoryAchieved.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
            }
            base.Draw(gameTime);
        }

        public void LoadTextures()
        {
            _playerProjectileTexture = Content.Load<Texture2D>("Sprites\\Projectiles\\fireball");
            _playerTexture = Content.Load<Texture2D>("Sprites\\Characters\\Player\\Player sprite");
            _enemyMinotaurTexture = Content.Load<Texture2D>("Sprites\\Characters\\Enemy\\Minotaur sprite");

            //Tile textures
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
            _DirtToGrassLeft = Content.Load<Texture2D>("Sprites\\Tiles\\DirtToGrassLeft");
            _DirtToGrassRight = Content.Load<Texture2D>("Sprites\\Tiles\\DirtToGrassRight");
            _GrassTileLeft = Content.Load<Texture2D>("Sprites\\Tiles\\GrassTileLeft");
            _GrassTileRight = Content.Load<Texture2D>("Sprites\\Tiles\\GrassTileRight");
        }

        public void AddTexturesToLevel(Level level)
        {
            level.AddTextures(_groundTileLeft);       // 0
            level.AddTextures(_groundTileMiddle);     // 1
            level.AddTextures(_groundTileRight);      // 2
            level.AddTextures(_islandTileLeft);       // 3
            level.AddTextures(_islandTileMiddle);     // 4
            level.AddTextures(_islandTileRight);      // 5
            level.AddTextures(_dirtTileLeft);         // 6
            level.AddTextures(_dirtTileMiddle);       // 7
            level.AddTextures(_dirtTileRight);        // 8
            level.AddTextures(_dirtTileWallLeft);     // 9
            level.AddTextures(_dirtTileWallMiddle);   // 10
            level.AddTextures(_dirtTileWallRight);    // 11
            level.AddTextures(_DirtToGrassLeft);      // 12
            level.AddTextures(_DirtToGrassRight);     // 13
            level.AddTextures(_GrassTileLeft);        // 14
            level.AddTextures(_GrassTileRight);       // 15
            
        }

        public void DrawLevel(Level level, Matrix viewMatrix)
        {
            camera.Position = campos;
            spriteBatch.Begin(transformMatrix: viewMatrix);

            level.DrawLevel(spriteBatch);
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
            //Score tonen
            spriteBatch.DrawString(scoreFont, "Score: " + playerPoints.ToString(), new Vector2(campos.X, campos.Y), Color.White);
            spriteBatch.End();
        }
    }
}
