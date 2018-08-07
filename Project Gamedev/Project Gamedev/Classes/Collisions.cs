using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev.Classes
{
    public class Collisions
    {
        List<ICollide> collisionObjecten;

        public Collisions()
        {
            collisionObjecten = new List<ICollide>();
        }

        public void AddCollisionsObject(ICollide collisionRectangle)
        {
            collisionObjecten.Add(collisionRectangle);
        }

        public bool CheckCollisions()
        {
            for (int i = 0; i < collisionObjecten.Count; i++)
            {
                for (int j = i + 1; j < collisionObjecten.Count; j++)
                {
                    if (collisionObjecten[i].GetCollisionRectangle().Intersects(collisionObjecten[j].GetCollisionRectangle()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
