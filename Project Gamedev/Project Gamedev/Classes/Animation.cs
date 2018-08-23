using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev
{
    public class Animation
    {
        private List<AnimationFrame> _frames;
        public AnimationFrame CurrentFrame { get; set; }
        public int AantalBewegingenPerSeconde { get; set; }

        private int _counter = 0;

        private double _x = 0;
        public double Offset {get; set;}
        private int _totalWidth = 0;

        public Animation()
        {
            _frames = new List<AnimationFrame>();
            AantalBewegingenPerSeconde = 1;
        }

        public void AddFrame(Rectangle rectangle)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
            };
            _frames.Add(newFrame);
            CurrentFrame = _frames[0];
            Offset = CurrentFrame.SourceRectangle.Width;
            foreach (AnimationFrame f in _frames)
            {
                _totalWidth += f.SourceRectangle.Width;
            }
        }

        public void Update(GameTime gameTime)
        {
            double temp = CurrentFrame.SourceRectangle.Width * ((double)gameTime.ElapsedGameTime.Milliseconds / 1000);

            _x += temp;
            if (_x >= CurrentFrame.SourceRectangle.Width / AantalBewegingenPerSeconde)
            {
                Console.WriteLine(_x);
                _x = 0;
                _counter++;
                if (_counter >= _frames.Count)
                {
                    _counter = 0;
                }
                CurrentFrame = _frames[_counter];
                Offset += CurrentFrame.SourceRectangle.Width;
            }
            if (Offset >= _totalWidth)
            {
                Offset = 0;
            }
        }
    }
}
