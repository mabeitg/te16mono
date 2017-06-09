using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace te16mono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player, player2;
        SpriteFont font;
        Song music;

        Block testblock;


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
            testblock = new Block();
            testblock.position.X = 200;
            testblock.position.Y = 310;
            testblock.isAlive = true;
            testblock.type = TypeOfBlock.teleporter;

            // TODO: Add your initialization logic here
            player = new Player();
            player.up = Keys.W;
            player.down = Keys.S;
            player.left = Keys.A;
            player.right = Keys.D;
            player.attack = Keys.LeftControl;

            player2 = new Player();
            player2.Initialize();
            player2.up = Keys.Up;
            player2.down = Keys.Down;
            player2.left = Keys.Left;
            player2.right = Keys.Right;
            player2.attack = Keys.RightControl;

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
            player.texture = Content.Load<Texture2D>("square");
            player2.texture = Content.Load<Texture2D>("square");

            testblock.texture= Content.Load<Texture2D>("square");

            font = Content.Load<SpriteFont>("Font");

            music = Content.Load<Song>("megaman2");
            MediaPlayer.Play(music);
            
            // TODO: use this.Content to load your game content here
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

            if(player.Hitbox.Intersects(testblock.Hitbox))
            {
                if (testblock.type == TypeOfBlock.teleporter)
                    player.position = Vector2.Zero;
            }

            if (player2.Hitbox.Intersects(testblock.Hitbox))
            {
                if (testblock.type == TypeOfBlock.teleporter)
                    player2.position = Vector2.Zero;
            }

            for (int i = 0; i < player.shots.Count;)
            {
                if (player.shots[i].isAlive == false)
                    player.shots.RemoveAt(i);
                else
                    i++;
            }

            for (int i = 0; i < player2.shots.Count;)
            {
                if (player2.shots[i].isAlive == false)
                    player2.shots.RemoveAt(i);
                else
                    i++;
            }


            player.Update();
            player2.Update();

            foreach (Shot shot in player.shots)
            {
                shot.Update();
                if (player2.Hitbox.Intersects(shot.Hitbox))
                {
                    player.points++;
                    shot.isAlive = false;
                }
            }

            foreach (Shot shot in player2.shots)
            {
                shot.Update();
                if (player.Hitbox.Intersects(shot.Hitbox))
                {
                    player2.points++;
                    shot.isAlive = false;
                }
            }

           
            // TODO: Add your update logic here

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

            foreach (Shot shot in player.shots)
            {
                shot.Draw(spriteBatch);
            }
            foreach (Shot shot in player2.shots)
            {
                shot.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            testblock.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Poang: " + player.points.ToString() + " - " + player2.points.ToString(), Vector2.Zero, Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
