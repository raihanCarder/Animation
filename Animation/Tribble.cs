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
        private int _tribbleWidth, _tribbleHeight;
        private SoundEffect _bounce;
        private Color _color;
        private bool _tribbleVertical = false, _tribbleHorizontal = false, _tribbleMixed = false, _tribbleCorner = false, _mute = false;
        private int _uniqueTribble;
        

        public Tribble(Texture2D texture, SoundEffect bounce, GraphicsDeviceManager graphics)
        {
            _uniqueTribble = _generator.Next(1, 5);

            _tribbleWidth = _generator.Next(10, 100);
            _tribbleHeight = _generator.Next(10, 100);

            _rectangle = new Rectangle(_generator.Next(graphics.PreferredBackBufferWidth - _tribbleWidth), _generator.Next(graphics.PreferredBackBufferHeight - _tribbleHeight), _tribbleWidth, _tribbleHeight);
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
                _tribbleCorner = true;
                _speed = new Vector2(sameDistance, sameDistance);
            }

            _color = Color.White;
            
        }

        public Tribble (Texture2D texture)  // Challenge, Made it So if you use this contructor there's no bounce sound instead of the normal which includes the bounce.
        {
            _texture = texture;

            _mute = true;
            _uniqueTribble = _generator.Next(1, 5);

            _tribbleHeight = _generator.Next(10, 100);
            _tribbleWidth = _generator.Next(10, 100);

            _rectangle = new Rectangle(_generator.Next(900- _tribbleWidth), _generator.Next(500- _tribbleHeight), _tribbleWidth, _tribbleHeight);

            if (_uniqueTribble == 1)
            {
                _tribbleVertical = true;
                _speed = new Vector2(0, _generator.Next(1, 10));
            }
            else if (_uniqueTribble == 2)
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
                _tribbleCorner = true;
                _speed = new Vector2(sameDistance, sameDistance);
            }

            _color = Color.White;
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
        public Vector2 Speed
        {
            get { return _speed; }
        }
        public Color Color
        { 
            get { return _color; }
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

                if (!_mute)
                {
                    _bounce.Play();
                }

                if (_tribbleMixed)
                {
                    _color = new Color(Convert.ToByte(_generator.Next(255)), Convert.ToByte(_generator.Next(255)), Convert.ToByte(_generator.Next(255)));
                    _rectangle.X = _generator.Next(graphics.PreferredBackBufferWidth - _tribbleWidth);
                    _rectangle.Y = _generator.Next(graphics.PreferredBackBufferWidth - _tribbleHeight);
                    _speed.X = _generator.Next(1, 10);
                    _speed.Y = _generator.Next(1, 10);
                }
                else if (_tribbleCorner)
                {
                    _color = new Color(Convert.ToByte(_generator.Next(255)), Convert.ToByte(_generator.Next(255)), Convert.ToByte(_generator.Next(255)));                 
                }
            }

            if (_rectangle.Bottom > graphics.PreferredBackBufferHeight || _rectangle.Top < 0)
            {
                _speed.Y *= -1;

                if (_tribbleMixed)
                {

                    _color = new Color(Convert.ToByte(_generator.Next(255)), Convert.ToByte(_generator.Next(255)), Convert.ToByte(_generator.Next(255)));
                    _rectangle.X = _generator.Next(graphics.PreferredBackBufferWidth - _tribbleWidth);
                    _rectangle.Y = _generator.Next(graphics.PreferredBackBufferWidth - _tribbleHeight);
                    _speed.X = _generator.Next(1, 10);
                    _speed.Y = _generator.Next(1, 10);
                }
                else if (_tribbleCorner)
                {
                    _color = new Color(Convert.ToByte(_generator.Next(255)), Convert.ToByte(_generator.Next(255)), Convert.ToByte(_generator.Next(255)));                
                }

                _hits++;

                if (!_mute)
                {
                    _bounce.Play();
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, _color);
        }

    }

}
