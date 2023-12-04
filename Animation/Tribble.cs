using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Animation
{
    class Tribble
    {
        private Random _generator = new Random();
       
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        private int _hits = 0;
        private int tribbleWidth = 100;
        private SoundEffect _bounce;
        private Color _color;
        private bool _tribbleVertical = false, _tribbleHorizontal = false, _tribbleMixed = false, _tribbleCorner = false;
        private int _uniqueTribble;
        

        public Tribble(Texture2D texture, SoundEffect bounce, GraphicsDeviceManager graphics)
        {
            _uniqueTribble = _generator.Next(1, 5);

            _rectangle = new Rectangle(_generator.Next(graphics.PreferredBackBufferWidth - tribbleWidth), _generator.Next(graphics.PreferredBackBufferHeight - tribbleWidth), _generator.Next(10,tribbleWidth), _generator.Next(10, tribbleWidth));
            _texture = texture;
            _bounce = bounce;

            if (_uniqueTribble == 1)
            {
                _tribbleVertical = true;
                _speed = new Vector2(0, _generator.Next(1, 10));
            }
            else if ( _uniqueTribble == 2)
            {
                _tribbleHorizontal = true;
                _speed = new Vector2(_generator.Next(1, 10), 0);
            }
            else if (_uniqueTribble == 3)
            {
                _tribbleMixed = true;
                _speed = new Vector2(_generator.Next(1, 10), _generator.Next(1, 10));
            }
            else if (_uniqueTribble == 4)
            {
                int sameDistance = _generator.Next(1, 10);
                _speed = new Vector2(sameDistance, sameDistance);
            }

        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value; }

        }
        public int Hits
        {
            get { return _hits; }
        }
        public void Move(GraphicsDeviceManager graphics)
        {
            _rectangle.Offset(_speed);

            if (_rectangle.Right > graphics.PreferredBackBufferWidth || _rectangle.Left < 0)
            {
                _speed.X *= -1;
                _hits++;
                _bounce.Play();

                if (_tribbleHorizontal)
                {
                                 
                }
                else if (_tribbleMixed)
                {  
                 
                }
                else if (_tribbleCorner)
                {
                
                }
            }

            if (_rectangle.Bottom > graphics.PreferredBackBufferHeight || _rectangle.Top < 0)
            {
                _speed.Y *= -1;
                _hits++;
                _bounce.Play();

         
                if (_tribbleMixed)
                {
                    
                }
                else if (_tribbleVertical)
                {

                }
                else if (_tribbleCorner)
                {

                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, Color.White);
        }

    }

}
