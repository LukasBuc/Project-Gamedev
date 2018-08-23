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
        List<ICollide> mogelijkeCollisionObjecten;
        List<ICollide> collisionObjecten;
        List<ICollide> projectileCollisionObjecten;
        public List<ICollide> BottomCollisions { get; set; }
        public List<ICollide> RightCollisions { get; set; }
        public List<ICollide> LeftCollisions { get; set; }
        public List<ICollide> TopCollisions { get; set; }

        public int BottomCollisionHeight { get; set; }

        public Collisions()
        {
            mogelijkeCollisionObjecten = new List<ICollide>();

            projectileCollisionObjecten = new List<ICollide>();
        }

        public void AddCollisionsObject(ICollide collisionRectangle)
        {
            mogelijkeCollisionObjecten.Add(collisionRectangle);
        }

        //Kijkt of er collisions zijn met de player en steekt deze in een lijst
        public void GetCollisionObjects(ICollide playerCollisionRectangle)
        {
            collisionObjecten = new List<ICollide>();

            for (int i = 0; i < mogelijkeCollisionObjecten.Count; i++)
            {
                if (mogelijkeCollisionObjecten[i].GetCollisionRectangle().Intersects(playerCollisionRectangle.GetCollisionRectangle()))
                {
                    collisionObjecten.Add(mogelijkeCollisionObjecten[i]);
                }
            }
        }

        //TODO margin variabele toevoegen en cijfers proberen weghalen uit code
        public void CheckCollisions(ICollide playerCollisionRectangle)
        {
            BottomCollisions = new List<ICollide>();
            RightCollisions = new List<ICollide>();
            LeftCollisions = new List<ICollide>();
            TopCollisions = new List<ICollide>();

            for (int i = 0; i < collisionObjecten.Count; i++)
            {
                //Check bottom
                if (playerCollisionRectangle.GetCollisionRectangle().Bottom >= collisionObjecten[i].GetCollisionRectangle().Top - 5 &&
                    playerCollisionRectangle.GetCollisionRectangle().Bottom <= collisionObjecten[i].GetCollisionRectangle().Top + (collisionObjecten[i].GetCollisionRectangle().Height /2) &&
                    playerCollisionRectangle.GetCollisionRectangle().Right >= collisionObjecten[i].GetCollisionRectangle().Left + 5 &&
                    playerCollisionRectangle.GetCollisionRectangle().Left <= collisionObjecten[i].GetCollisionRectangle().Right - 5)
                {
                    BottomCollisions.Add(collisionObjecten[i]);
                    BottomCollisionHeight = collisionObjecten[i].GetCollisionRectangle().Top;
                }

                //Check right
                if (playerCollisionRectangle.GetCollisionRectangle().Right >= collisionObjecten[i].GetCollisionRectangle().Left - 2 &&
                    playerCollisionRectangle.GetCollisionRectangle().Right <= collisionObjecten[i].GetCollisionRectangle().Left + 2 &&
                    playerCollisionRectangle.GetCollisionRectangle().Bottom >= collisionObjecten[i].GetCollisionRectangle().Top + 8 &&
                    playerCollisionRectangle.GetCollisionRectangle().Top <= collisionObjecten[i].GetCollisionRectangle().Bottom - 8)
                {
                    RightCollisions.Add(collisionObjecten[i]);
                }

                // Check left
                if (playerCollisionRectangle.GetCollisionRectangle().Left >= collisionObjecten[i].GetCollisionRectangle().Right - 2 &&
                    playerCollisionRectangle.GetCollisionRectangle().Left <= collisionObjecten[i].GetCollisionRectangle().Right + 2 &&
                    playerCollisionRectangle.GetCollisionRectangle().Bottom >= collisionObjecten[i].GetCollisionRectangle().Top + 8 &&
                    playerCollisionRectangle.GetCollisionRectangle().Top <= collisionObjecten[i].GetCollisionRectangle().Bottom - 8)
                {
                    LeftCollisions.Add(collisionObjecten[i]);
                }

                //Check top
                if (playerCollisionRectangle.GetCollisionRectangle().Top <= collisionObjecten[i].GetCollisionRectangle().Bottom + 5 &&
                    playerCollisionRectangle.GetCollisionRectangle().Top >= collisionObjecten[i].GetCollisionRectangle().Bottom - (collisionObjecten[i].GetCollisionRectangle().Height /2) &&
                    playerCollisionRectangle.GetCollisionRectangle().Right >= collisionObjecten[i].GetCollisionRectangle().Left + 5 &&
                    playerCollisionRectangle.GetCollisionRectangle().Left <= collisionObjecten[i].GetCollisionRectangle().Right - 5)
                {
                    TopCollisions.Add(collisionObjecten[i]);
                }
            }
        }

        //Projectile collisionobjecten toevoegen aan lijst
        public void AddProjectileCollisionObjects(ICollide collisionRectangle)
        {
            projectileCollisionObjecten.Add(collisionRectangle);
        }

        public int CheckProjectileCollisions()
        {
            for (int i = 0; i < mogelijkeCollisionObjecten.Count; i++)
            {
                for (int j = 0; j < projectileCollisionObjecten.Count; j++)
                {
                    if (mogelijkeCollisionObjecten[i].GetCollisionRectangle().Intersects(projectileCollisionObjecten[j].GetCollisionRectangle()))
                    {
                        //Er is een collision tussen een tile en een projectiel
                        return j;
                    }
                }
                
            }
            return -1;
        }

        public void ClearProjectileCollisions()
        {
            projectileCollisionObjecten.Clear();
        }
    }
}
