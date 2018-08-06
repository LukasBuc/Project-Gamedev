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

        //TODO DEES IS OM TE TESTEN
        List<ICollide> collideObjecten;

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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tileTexture = Content.Load<Texture2D>("Sprites\\Tiles\\Tile (1)");
            level1 = new Level();
            level1.texture = tileTexture;
            level1.CreateWorld();

            _playerTexture = Content.Load<Texture2D>("Sprites\\Characters\\Player\\Player sprite");
            _player = new Player(_playerTexture, new Vector2(100, 200));

            //TODO DEES IS OM TE TESTEN
            collideObjecten = new List<ICollide>();
            collideObjecten.Add(_player);

            foreach (var item in level1.tileArray)
            {
                if (item != null)
                {
                    collideObjecten.Add(item);
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

        private bool CheckCollision()
        {
            for (int i = 0; i < collideObjecten.Count; i++)
            {
                for (int j = i + 1; j < collideObjecten.Count; j++)
                {
                    if (collideObjecten[i].GetCollisionRectangle().Intersects(collideObjecten[j].GetCollisionRectangle()))
                    {
                        return true;
                    }
                }
            }
            return false;
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

            if (CheckCollision())
            {
                System.Console.WriteLine("AAAAAAA");
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

            spriteBatch.Begin();
            level1.DrawLevel(spriteBatch);
            _player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
