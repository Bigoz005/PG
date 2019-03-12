using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Texture2D playerBoundsTex, enemyBoundsTex, rect, rect2;
        Rectangle playerBounds, enemyBounds2;
        Vector2 smallPosition2;
        Vector2 ballPosition;
        float ballSpeed;
        float enemySpeed;

        Vector2 coor;
        Vector2 coor2;

        private String napis;
        private String napis2;
        private String napis3;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            ballPosition = new Vector2(200, 150);
            smallPosition2 = new Vector2(450, 350);
            ballSpeed = 50f;
            enemySpeed = 2f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("File");
            playerBoundsTex = Content.Load<Texture2D>("ball");
            enemyBoundsTex = Content.Load<Texture2D>("ball_small");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            //rzeczy do rysowania kwadratow reprezentujacych hitboxy
            GraphicsDevice.Clear(Color.CornflowerBlue);
            rect = new Texture2D(graphics.GraphicsDevice, playerBoundsTex.Width, playerBoundsTex.Height);
            rect2 = new Texture2D(graphics.GraphicsDevice, enemyBoundsTex.Width, enemyBoundsTex.Height);

            Color[] data = new Color[playerBoundsTex.Width * playerBoundsTex.Height];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);

            Color[] data2 = new Color[enemyBoundsTex.Width * enemyBoundsTex.Height];
            for (int i = 0; i < data2.Length; ++i) data2[i] = Color.Chocolate;
            rect2.SetData(data2);

            coor = new Vector2(ballPosition.X - 32, ballPosition.Y - 32);
            coor2 = new Vector2(smallPosition2.X - 2, smallPosition2.Y);

            //wylaczanie gry
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();
            //czas miedzy wywolaniami update nie zawsze jest ten sam wiec, aby uzysac plynny ruch trzeba pomnozyc predkosc przez czas od ostatniego wywolania metody update
            if (kstate.IsKeyDown(Keys.Up))
                ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
                ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Right))
                ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //ustawienie granic okna
            if (ballPosition.X > Window.ClientBounds.Width - playerBoundsTex.Width / 2)
                ballPosition.X = Window.ClientBounds.Width - playerBoundsTex.Width / 2;
            if (ballPosition.Y > Window.ClientBounds.Height - playerBoundsTex.Height / 2)
                ballPosition.Y = Window.ClientBounds.Height - playerBoundsTex.Height / 2;

            if (ballPosition.X < playerBoundsTex.Width / 2)
                ballPosition.X = playerBoundsTex.Width / 2;
            if (ballPosition.Y < playerBoundsTex.Height / 2)
                ballPosition.Y = playerBoundsTex.Height / 2;

            //ballPosition.X = Math.Min(Math.Max(playerBoundsTex.Width / 2, ballPosition.X), graphics.PreferredBackBufferWidth - playerBoundsTex.Width / 2);
            //ballPosition.Y = Math.Min(Math.Max(playerBoundsTex.Height / 2, ballPosition.Y), graphics.PreferredBackBufferHeight - playerBoundsTex.Height / 2);

            playerBounds = new Rectangle((int)ballPosition.X - 32, (int)ballPosition.Y - 32, playerBoundsTex.Width, playerBoundsTex.Height);
            enemyBounds2 = new Rectangle((int)smallPosition2.X, (int)smallPosition2.Y, enemyBoundsTex.Width, enemyBoundsTex.Height);

            smallPosition2.X -= enemySpeed;
            if (smallPosition2.X < -20)
            {
                smallPosition2.X = 800;
            }

            if (playerBounds.Intersects(enemyBounds2))
            {
                napis3 = "Kolizja";
                napis = "Gracz X:" + ballPosition.X + "Y:" + ballPosition.Y;
            }
            else
            {
                napis3 = "Brak Kolizji";
                napis = "Gracz X:" + ballPosition.X + "Y:" + ballPosition.Y;
                napis2 = "NPC X:" + smallPosition2.X + "Y:" + smallPosition2.Y;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(rect, coor, Color.White);
            spriteBatch.Draw(rect2, coor2, Color.White);
            spriteBatch.Draw(playerBoundsTex, ballPosition, null, Color.White, 0f, new Vector2(playerBoundsTex.Width / 2, playerBoundsTex.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            spriteBatch.Draw(enemyBoundsTex, smallPosition2, Color.White);
            spriteBatch.DrawString(font, napis, new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(font, napis2, new Vector2(10, 50), Color.Black);
            spriteBatch.DrawString(font, napis3, new Vector2(500, 10), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

