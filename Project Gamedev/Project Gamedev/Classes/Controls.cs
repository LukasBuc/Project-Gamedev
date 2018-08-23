using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev
{
    public abstract class Controls
    {
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Jump { get; set; }
        public bool Fire { get; set; }
        public abstract void Update();
    }

    public class ControlArrows : Controls
    {
        public override void Update()
        {
            KeyboardState stateKey = Keyboard.GetState();

            //Check arrow links ingedrukt of niet
            if (stateKey.IsKeyDown(Keys.Left))
            {
                Left = true;
            }
            if (stateKey.IsKeyUp(Keys.Left))
            {
                Left = false;
            }

            //Check arrow rechts ingedrukt of niet
            if (stateKey.IsKeyDown(Keys.Right))
            {
                Right = true;
            }
            if (stateKey.IsKeyUp(Keys.Right))
            {
                Right = false;
            }

            //Check of spatie is ingedrukt of niet
            if (stateKey.IsKeyDown(Keys.Space))
            {
                Jump = true;
            }
            if (stateKey.IsKeyUp(Keys.Space))
            {
                Jump = false;
            }

            //Check of schieten is ingedrukt of niet
            if (stateKey.IsKeyDown(Keys.C))
            {
                Fire = true;
            }
            if (stateKey.IsKeyUp(Keys.C))
            {
                Fire = false;
            }
        }
    }
}
