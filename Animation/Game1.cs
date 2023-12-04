using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace Animation
{   
    // Raihan Carder

    public class Game1 : Game
    {
        Random generator = new Random();

        List<Tribble> loopedTribbles = new List<Tribble>();
        List<Texture2D> tribbleTextures = new List<Texture2D>();

        private SoundEffect bounce, introSong, endSong, teleport;
        SoundEffectInstance introInstance, endInstance;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Tribble tribbleGrey;
        Tribble tribbleCream;
        Tribble tribbleBrown;
        Tribble tribbleOrange;

        Texture2D tribbleGreyTexture;
        Texture2D tribbleBrownTexture;
        Texture2D tribbleCreamTexture;
        Texture2D tribbleOrangeTexture;
        Texture2D mcdonaldsTexture;
        Texture2D spaceTexture;
        Texture2D parisTexture;
        Texture2D tribbleIntroTexture;
        Texture2D blownTribbleTexture;

        Rectangle spaceRect;
        Rectangle parisRect;
        Rectangle mcdonaldsRect;
        
        MouseState mouseState;
        Screen screen;

        SpriteFont introText;
        SpriteFont attackText;
        SpriteFont victoryText;

        int amount = 10;
        bool mcdonaldsWorld, parisWorld, moonWorld;


        enum Screen
        {
            Intro,
            TribbleYard,
            Ending
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

            mcdonaldsRect = new Rectangle(0, 0, 900, 500);
            spaceRect = new Rectangle(0, 0, 900, 500);
            parisRect = new Rectangle(0,0,900, 500);

            base.Initialize();

            for (int i = 0; i < amount; i++)
            {
                loopedTribbles.Add(new Tribble(tribbleTextures[generator.Next(tribbleTextures.Count)], bounce, _graphics));
            }


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
            tribbleTextures.Add(tribbleGreyTexture);
            tribbleTextures.Add(tribbleOrangeTexture);
            tribbleTextures.Add(tribbleBrownTexture);
            tribbleTextures.Add(tribbleCreamTexture);


            mcdonaldsTexture = Content.Load<Texture2D>("Mcdonalds");
            spaceTexture = Content.Load<Texture2D>("Space");
            parisTexture = Content.Load<Texture2D>("Paris");
            teleport = Content.Load<SoundEffect>("TeleportWav");
            bounce = Content.Load<SoundEffect>("BounceWave");
            tribbleIntroTexture = Content.Load<Texture2D>("Area51");
            introText = Content.Load<SpriteFont>("IntroText");
            attackText = Content.Load<SpriteFont>("Attack");
            blownTribbleTexture = Content.Load<Texture2D>("Explosion-8");
            introSong = Content.Load<SoundEffect>("Intro");
            introInstance = introSong.CreateInstance();
            introInstance.IsLooped = false;   // The sound will only play once
            victoryText = Content.Load<SpriteFont>("Victory");
            endSong = Content.Load<SoundEffect>("DiscoFever");
            endInstance = endSong.CreateInstance();
            endInstance.IsLooped = false;
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.TribbleYard;

                   introInstance.Play();

            }
            else if (screen == Screen.TribbleYard)
            {
                introInstance.Stop();

                if (mouseState.RightButton == ButtonState.Pressed)
                    screen = Screen.Ending;

                foreach (Tribble tribble in loopedTribbles)
                    tribble.Move(_graphics);
                    
            }
            else if (screen == Screen.Ending)
            {
                endInstance.Play();
            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
                endInstance.Stop();
                introInstance.Stop();
            }

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


                if (loopedTribbles[1].Hits < 15)
                {
                    _spriteBatch.Draw(mcdonaldsTexture, mcdonaldsRect, Color.White);
                    _spriteBatch.DrawString(attackText, "'Right Click' to End the Tribble Race!", new Vector2(0, 0), Color.Red);
                    mcdonaldsWorld = true;
                    moonWorld = false;
                    parisWorld = false;
                }
                else if (loopedTribbles[1].Hits >= 15 && loopedTribbles[1].Hits <= 20)
                {
                    _spriteBatch.Draw(parisTexture, parisRect, Color.White);
                    _spriteBatch.DrawString(attackText, "'Right Click' to End the Tribble Race!", new Vector2(0, 0), Color.Red);
                    parisWorld = true;
                    moonWorld = false;
                    mcdonaldsWorld = false;
                }
                else
                {
                    _spriteBatch.Draw(spaceTexture, spaceRect, Color.White);
                    _spriteBatch.DrawString(attackText, "'Right Click' to End the Tribble Race!", new Vector2(0, 0), Color.Red);
                    moonWorld = true;
                    parisWorld = false;
                    mcdonaldsWorld = false;
                }

              

                foreach (Tribble tribble in loopedTribbles)
                    tribble.Draw(_spriteBatch);

               


            }
            else if (screen == Screen.Ending)
            {
                if (parisWorld == true)
                {
                    _spriteBatch.Draw(parisTexture, parisRect, Color.White);
                }
                else if (mcdonaldsWorld == true)
                {
                    _spriteBatch.Draw(mcdonaldsTexture, mcdonaldsRect, Color.White);
                }
                else if (moonWorld == true)
                {
                    _spriteBatch.Draw(spaceTexture, spaceRect, Color.White);
                }

                _spriteBatch.DrawString(victoryText, "The Tribbles are Extinct!", new Vector2(30, 30), Color.Blue);

               for (int i = 0; i < amount; i++)
               {
                    loopedTribbles[i].Texture = blownTribbleTexture;
               }
                foreach (Tribble tribble in loopedTribbles)
                    tribble.Draw(_spriteBatch);

            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}