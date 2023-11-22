using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Animation
{   
    // Raihan Carder

    public class Game1 : Game
    {
        Random generator = new Random();
        private SoundEffect teleport;
        private SoundEffect bounce;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D tribbleGreyTexture;
        Texture2D tribbleBrownTexture;
        Texture2D tribbleCreamTexture;
        Texture2D tribbleOrangeTexture;
        Texture2D mcdonaldsTexture;
        Texture2D spaceTexture;
        Texture2D parisTexture;
        Texture2D tribbleIntroTexture; 
        Rectangle spaceRect;
        Rectangle parisRect;
        Rectangle tribbleGreyRect;  // make own rectangle for each tribble
        Rectangle tribbleCreamRect;
        Rectangle tribbleBrownRect;
        Rectangle tribbleOrangeRect;
        Rectangle mcdonaldsRect;
        Vector2 tribbleGreySpeed;
        Vector2 tribbleCreamSpeed;
        Vector2 tribbleBrownSpeed;
        Vector2 tribbleOrangeSpeed;
        int hits = 0;
        MouseState mouseState;
        Screen screen;
        SpriteFont introText;

        enum Screen
        {
            Intro,
            TribbleYard            
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            this.Window.Title = "Tribble Outbreak";
 
        }

        protected override void Initialize()
        {
            screen = Screen.Intro;


            int tribbleWidth = 100;

            int xLocationGrey = generator.Next(_graphics.PreferredBackBufferWidth - tribbleWidth);
            int xLocationCream = generator.Next(_graphics.PreferredBackBufferWidth - tribbleWidth);
            int xLocationBrown = generator.Next(_graphics.PreferredBackBufferWidth - tribbleWidth);
            int xLocationOrange = generator.Next(_graphics.PreferredBackBufferWidth - tribbleWidth);
            int yLocationGrey = generator.Next(_graphics.PreferredBackBufferHeight - tribbleWidth);
            int yLocationCream = generator.Next(_graphics.PreferredBackBufferHeight - tribbleWidth);
            int yLocationBrown = generator.Next(_graphics.PreferredBackBufferHeight - tribbleWidth);
            int yLocationOrange = generator.Next(_graphics.PreferredBackBufferHeight - tribbleWidth);
            int randomYSpeed = generator.Next(5, 10);
            int randomXSpeed = generator.Next(5, 10);

            tribbleGreySpeed = new Vector2(randomXSpeed, 0); // horizontal speed, vertical speed.
            tribbleCreamSpeed = new Vector2(0, randomYSpeed);
            tribbleBrownSpeed = new Vector2(10, 9);
            tribbleOrangeSpeed = new Vector2(3, 9);
            tribbleGreyRect = new Rectangle(xLocationGrey, yLocationGrey,100,100);
            mcdonaldsRect = new Rectangle(0, 0, 900, 500);
            spaceRect = new Rectangle(0, 0, 900, 500);
            parisRect = new Rectangle(0,0,900, 500);
            tribbleCreamRect = new Rectangle(xLocationCream, yLocationCream, 100, 100);
            tribbleBrownRect = new Rectangle(xLocationBrown, yLocationBrown, 100, 100);
            tribbleOrangeRect = new Rectangle(xLocationOrange, yLocationOrange, 100, 100);
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
            spaceTexture = Content.Load<Texture2D>("Space");
            parisTexture = Content.Load<Texture2D>("Paris");
            teleport = Content.Load<SoundEffect>("TeleportWav");
            bounce = Content.Load<SoundEffect>("BounceWave");
            tribbleIntroTexture = Content.Load<Texture2D>("Area51");
            introText = Content.Load<SpriteFont>("IntroText");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            int randomSizeWidth = generator.Next(50, 200);
            int randomSizeHeight = generator.Next(50, 200);
            int randomX = generator.Next(_graphics.PreferredBackBufferWidth - randomSizeWidth);
            int randomY = generator.Next(_graphics.PreferredBackBufferHeight - randomSizeHeight);

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.TribbleYard;


            }
            else if (screen == Screen.TribbleYard)
            {
                // Your previous tribble moving code should go here
                tribbleGreyRect.X += (int)tribbleGreySpeed.X; // Needs to be int 
                tribbleGreyRect.Y += (int)tribbleGreySpeed.Y;
                tribbleCreamRect.X += (int)tribbleCreamSpeed.X;
                tribbleCreamRect.Y += (int)tribbleCreamSpeed.Y;
                tribbleBrownRect.X += (int)tribbleBrownSpeed.X;
                tribbleBrownRect.Y += (int)tribbleBrownSpeed.Y;
                tribbleOrangeRect.X += (int)tribbleOrangeSpeed.X;
                tribbleOrangeRect.Y += (int)tribbleOrangeSpeed.Y;

                if (tribbleGreyRect.Right >= _graphics.PreferredBackBufferWidth || tribbleGreyRect.Left <= 0)
                {
                    tribbleGreySpeed.X *= -1;
                    bounce.Play();
                }
                if (tribbleGreyRect.Top <= 0 || tribbleGreyRect.Bottom >= _graphics.PreferredBackBufferHeight)
                {
                    tribbleGreySpeed.Y *= -1;
                    bounce.Play();
                }

                if (tribbleCreamRect.Right >= _graphics.PreferredBackBufferWidth || tribbleCreamRect.Left <= 0)
                {
                    tribbleCreamSpeed.X *= -1;
                    hits++;

                    bounce.Play();
                }

                if (tribbleCreamRect.Top <= 0 || tribbleCreamRect.Bottom >= _graphics.PreferredBackBufferHeight)
                {
                    tribbleCreamSpeed.Y *= -1;
                    hits++;
                    bounce.Play();
                }

                if (tribbleBrownRect.Right >= _graphics.PreferredBackBufferWidth || tribbleBrownRect.Left <= 0)
                {
                    tribbleBrownSpeed.X *= -1;
                    tribbleBrownRect.X = randomX;
                    tribbleBrownRect.Y = randomY;
                    tribbleBrownRect.Width = randomSizeWidth;
                    tribbleBrownRect.Height = randomSizeHeight;
                    teleport.Play();
                }

                if (tribbleBrownRect.Top <= 0 || tribbleBrownRect.Bottom >= _graphics.PreferredBackBufferHeight)
                {
                    tribbleBrownSpeed.Y *= -1;
                    tribbleBrownRect.X = randomX;
                    tribbleBrownRect.Y = randomY;
                    tribbleBrownRect.Width = randomSizeWidth;
                    tribbleBrownRect.Height = randomSizeHeight;
                    teleport.Play();
                }

                if (tribbleOrangeRect.Right >= _graphics.PreferredBackBufferWidth || tribbleOrangeRect.Left <= 0)
                {
                    if (tribbleOrangeRect.Right >= _graphics.PreferredBackBufferWidth)
                    {
                        tribbleOrangeSpeed.X = -1 * generator.Next(5, 13);
                    }
                    else
                    {
                        tribbleOrangeSpeed.X = generator.Next(5, 13);
                    }

                    bounce.Play();
                }

                if (tribbleOrangeRect.Top <= 0 || tribbleOrangeRect.Bottom >= _graphics.PreferredBackBufferHeight)
                {
                    if (tribbleOrangeRect.Top <= 0)
                    {
                        tribbleOrangeSpeed.Y = generator.Next(5, 13);
                    }
                    else
                    {
                        tribbleOrangeSpeed.Y = -1 * generator.Next(5, 13);
                    }

                    bounce.Play();

                }


            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

          
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 900, 500), Color.White);
                _spriteBatch.DrawString(introText, "WARNING: LEFT CLICK TO RELEASE THE TRIBBLES", new Vector2(30,0), Color.Red);
            }
            else if (screen == Screen.TribbleYard)
            {
                // Your previous tribble drawing code should go here

                if (hits < 15)
                {
                    _spriteBatch.Draw(mcdonaldsTexture, mcdonaldsRect, Color.White);
                    _spriteBatch.Draw(tribbleCreamTexture, tribbleCreamRect, Color.White);
                }
                else if (hits >= 15 && hits <= 20)
                {
                    _spriteBatch.Draw(parisTexture, parisRect, Color.White);
                    _spriteBatch.Draw(tribbleCreamTexture, tribbleCreamRect, Color.Red);
                }
                else
                {
                    _spriteBatch.Draw(spaceTexture, spaceRect, Color.White);
                    _spriteBatch.Draw(tribbleCreamTexture, tribbleCreamRect, Color.Blue);
                }

                _spriteBatch.Draw(tribbleGreyTexture, tribbleGreyRect, Color.White);
                _spriteBatch.Draw(tribbleBrownTexture, tribbleBrownRect, Color.White);
                _spriteBatch.Draw(tribbleOrangeTexture, tribbleOrangeRect, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}