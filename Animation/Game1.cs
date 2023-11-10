using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Animation
{   // Raihan Carder
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D tribbleGreyTexture;
        Rectangle tribbleGretRect;  // make own rectangle for each tribble
        Vector2 tribbleGreySpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 500;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            tribbleGreySpeed = new Vector2(2, 2); // horizontal speed, vertical speed.
            tribbleGretRect = new Rectangle(300,10,100,100);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            tribbleGretRect.X += (int)tribbleGreySpeed.X; // Needs to be int 
            tribbleGretRect.Y += (int)tribbleGreySpeed.Y;

            if (tribbleGretRect.Right >= _graphics.PreferredBackBufferWidth || tribbleGretRect.Left <= 0)
                tribbleGreySpeed.X *= -1;
            if (tribbleGretRect.Top <= 0 || tribbleGretRect.Bottom >= _graphics.PreferredBackBufferHeight)
                tribbleGreySpeed.Y *= -1;   

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(tribbleGreyTexture, tribbleGretRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}