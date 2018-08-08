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

        public bool CheckCollisions(ICollide playerCollisionRectangle)
        {
            for (int i = 0; i < collisionObjecten.Count; i++)
            {
                if (collisionObjecten[i].GetCollisionRectangle().Intersects(playerCollisionRectangle.GetCollisionRectangle()))
                {
                    return true;
                }
            }
            return false;
        }

        //Kijkt of de player grounded is en geeft de hoogte van collision object terug
        //Geeft null terug indien geen collision
        public int? CheckPlayerGrounded(ICollide playerCollisionRectangle)
        {
            for (int i = 0; i < collisionObjecten.Count; i++)
            {
                if (playerCollisionRectangle.GetCollisionRectangle().Intersects(collisionObjecten[i].GetCollisionRectangle()))
                {
                    if(playerCollisionRectangle.GetCollisionRectangle().Bottom >= collisionObjecten[i].GetCollisionRectangle().Top)
                    {
                        return collisionObjecten[i].GetCollisionRectangle().Y;
                    }                 
                }
            }
            return null;
        }
    }
}
