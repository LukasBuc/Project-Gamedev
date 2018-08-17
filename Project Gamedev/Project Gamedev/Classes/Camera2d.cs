using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev.Classes
{
    public class Camera2d
    {
        private readonly Viewport _viewport;
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }

        public Camera2d(Viewport viewport)
        {
            _viewport = viewport;

            Rotation = 0;
            Zoom = 1;
            Position = Vector2.Zero;
        }

        public Matrix GetViewMatrix()
        {
            Matrix m = Matrix.CreateTranslation(new Vector3(-Position, 0.0f));

            return m;
        }
    }
}
