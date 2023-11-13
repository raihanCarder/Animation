using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Animation
{   
    // Raihan Carder

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D tribbleGreyTexture;
        Texture2D tribbleBrownTexture;
        Texture2D tribbleCreamTexture;
        Texture2D tribbleOrangeTexture;
        Texture2D mcdonaldsTexture;
        Rectangle tribbleGreyRect;  // make own rectangle for each tribble
        Rectangle tribbleCreamRect;
        Rectangle tribbleBrownRect;
        Rectangle tribbleOrangeRect;
        Rectangle mcdonaldsRect;
        Vector2 tribbleGreySpeed;
        Vector2 tribbleCreamSpeed;
        Vector2 tribbleBrownSpeed;
        Vector2 tribbleOrangeSpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            this.Window.Title = "Mcdonalds Outbreak";
 
        }

        protected override void Initialize()
        {
            Random generator = new Random();

            int tribbleWidth = 100;

            int xLocationGrey = generator.Next(_graphics.PreferredBackBufferWidth - tribbleWidth);
            int xLocationCream = generator.Next(_graphics.PreferredBackBufferWidth - tribbleWidth);
            int yLocationGrey = generator.Next(_graphics.PreferredBackBufferHeight - tribbleWidth);
            int yLocationCream = generator.Next(_graphics.PreferredBackBufferHeight - tribbleWidth);

            tribbleGreySpeed = new Vector2(2, 0); // horizontal speed, vertical speed.
            tribbleCreamSpeed = new Vector2(0, 5);
            tribbleBrownSpeed = new Vector2(10, 9);
            tribbleOrangeSpeed = new Vector2(5, 5);
            tribbleGreyRect = new Rectangle(xLocationGrey, yLocationGrey,100,100);
            mcdonaldsRect = new Rectangle(0, 0, 900, 500);
            tribbleCreamRect = new Rectangle(xLocationCream, yLocationCream, 100, 100);
            tribbleBrownRect = new Rectangle(10, 10, 100, 100);
            tribbleOrangeRect = new Rectangle(100, 40, 100, 100);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
            mcdonaldsTexture = Content.Load<Texture2D>("Mcdonalds");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            tribbleGreyRect.X += (int)tribbleGreySpeed.X; // Needs to be int 
            tribbleGreyRect.Y += (int)tribbleGreySpeed.Y;
            tribbleCreamRect.X += (int)tribbleCreamSpeed.X;
            tribbleCreamRect.Y += (int)tribbleCreamSpeed.Y;
            tribbleBrownRect.X += (int)tribbleBrownSpeed.X;
            tribbleBrownRect.Y += (int)tribbleBrownSpeed.Y;
            tribbleOrangeRect.X += (int)tribbleOrangeSpeed.X;
            tribbleOrangeRect.Y += (int)tribbleOrangeSpeed.Y;

            if (tribbleGreyRect.Right >= _graphics.PreferredBackBufferWidth || tribbleGreyRect.Left <= 0)
                tribbleGreySpeed.X *= -1;
            if (tribbleGreyRect.Top <= 0 || tribbleGreyRect.Bottom >= _graphics.PreferredBackBufferHeight)
                tribbleGreySpeed.Y *= -1;

            if (tribbleCreamRect.Right >= _graphics.PreferredBackBufferWidth || tribbleCreamRect.Left <= 0)
                tribbleCreamSpeed.X *= -1;
            if (tribbleCreamRect.Top <= 0 || tribbleCreamRect.Bottom >= _graphics.PreferredBackBufferHeight)
                tribbleCreamSpeed.Y *= -1;

            if (tribbleBrownRect.Right >= _graphics.PreferredBackBufferWidth || tribbleBrownRect.Left <= 0)
                tribbleBrownSpeed.X *= -1;
            if (tribbleBrownRect.Top <= 0 || tribbleBrownRect.Bottom >= _graphics.PreferredBackBufferHeight)
                tribbleBrownSpeed.Y *= -1;

            if (tribbleOrangeRect.Right >= _graphics.PreferredBackBufferWidth || tribbleOrangeRect.Left <= 0)
                tribbleOrangeSpeed.X *= -1;
            if (tribbleOrangeRect.Top <= 0 || tribbleOrangeRect.Bottom >= _graphics.PreferredBackBufferHeight)
                tribbleOrangeSpeed.Y *= -1;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(mcdonaldsTexture, mcdonaldsRect, Color.White);
            _spriteBatch.Draw(tribbleGreyTexture, tribbleGreyRect, Color.White);
            _spriteBatch.Draw(tribbleCreamTexture, tribbleCreamRect, Color.White);
            _spriteBatch.Draw(tribbleBrownTexture, tribbleBrownRect, Color.White);
            _spriteBatch.Draw(tribbleOrangeTexture, tribbleOrangeRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}