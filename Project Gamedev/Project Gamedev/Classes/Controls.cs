﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev
{
    public abstract class Controls
    {
        public bool left { get; set; }
        public bool right { get; set; }
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
                left = true;
            }
            if (stateKey.IsKeyUp(Keys.Left))
            {
                left = false;
            }

            //Check arrow rechts ingedrukt of niet
            if (stateKey.IsKeyDown(Keys.Right))
            {
                right = true;
            }
            if (stateKey.IsKeyUp(Keys.Right))
            {
                right = false;
            }
        }
    }
}