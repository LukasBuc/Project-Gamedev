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
        public List<ICollide> collisionObjecten;
        public List<ICollide> bottomCollisions { get; set; }
        public List<ICollide> rightCollisions { get; set; }
        public List<ICollide> leftCollisions { get; set; }
        public List<ICollide> topCollisions { get; set; }

        public int bottomCollisionHeight { get; set; }

        public Collisions()
        {
            mogelijkeCollisionObjecten = new List<ICollide>();
        }

        public void AddCollisionsObject(ICollide collisionRectangle)
        {
            mogelijkeCollisionObjecten.Add(collisionRectangle);
        }

        //Kijkt of er collisions zijn met de player en steekt deze in een lijst
        public void getCollisionObjects(ICollide playerCollisionRectangle)
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
        public void checkCollisions(ICollide playerCollisionRectangle)
        {
            bottomCollisions = new List<ICollide>();
            rightCollisions = new List<ICollide>();
            leftCollisions = new List<ICollide>();
            topCollisions = new List<ICollide>();

            for (int i = 0; i < collisionObjecten.Count; i++)
            {
                //Check bottom
                if (playerCollisionRectangle.GetCollisionRectangle().Bottom >= collisionObjecten[i].GetCollisionRectangle().Top - 5 &&
                    playerCollisionRectangle.GetCollisionRectangle().Bottom <= collisionObjecten[i].GetCollisionRectangle().Top + (collisionObjecten[i].GetCollisionRectangle().Height /2) &&
                    playerCollisionRectangle.GetCollisionRectangle().Right >= collisionObjecten[i].GetCollisionRectangle().Left + 5 &&
                    playerCollisionRectangle.GetCollisionRectangle().Left <= collisionObjecten[i].GetCollisionRectangle().Right - 5)
                {
                    bottomCollisions.Add(collisionObjecten[i]);
                    bottomCollisionHeight = collisionObjecten[i].GetCollisionRectangle().Top;
                }

                //Check right
                if (playerCollisionRectangle.GetCollisionRectangle().Right >= collisionObjecten[i].GetCollisionRectangle().Left - 2 &&
                    playerCollisionRectangle.GetCollisionRectangle().Right <= collisionObjecten[i].GetCollisionRectangle().Left + 2 &&
                    playerCollisionRectangle.GetCollisionRectangle().Bottom >= collisionObjecten[i].GetCollisionRectangle().Top + 8 &&
                    playerCollisionRectangle.GetCollisionRectangle().Top <= collisionObjecten[i].GetCollisionRectangle().Bottom - 8)
                {
                    rightCollisions.Add(collisionObjecten[i]);
                }

                // Check left
                if (playerCollisionRectangle.GetCollisionRectangle().Left >= collisionObjecten[i].GetCollisionRectangle().Right - 2 &&
                    playerCollisionRectangle.GetCollisionRectangle().Left <= collisionObjecten[i].GetCollisionRectangle().Right + 2 &&
                    playerCollisionRectangle.GetCollisionRectangle().Bottom >= collisionObjecten[i].GetCollisionRectangle().Top + 8 &&
                    playerCollisionRectangle.GetCollisionRectangle().Top <= collisionObjecten[i].GetCollisionRectangle().Bottom - 8)
                {
                    leftCollisions.Add(collisionObjecten[i]);
                }

                //Check top
                if (playerCollisionRectangle.GetCollisionRectangle().Top <= collisionObjecten[i].GetCollisionRectangle().Bottom + 5 &&
                    playerCollisionRectangle.GetCollisionRectangle().Top >= collisionObjecten[i].GetCollisionRectangle().Bottom - (collisionObjecten[i].GetCollisionRectangle().Height /2) &&
                    playerCollisionRectangle.GetCollisionRectangle().Right >= collisionObjecten[i].GetCollisionRectangle().Left + 5 &&
                    playerCollisionRectangle.GetCollisionRectangle().Left <= collisionObjecten[i].GetCollisionRectangle().Right - 5)
                {
                    topCollisions.Add(collisionObjecten[i]);
                }
            }
        }
    }
}
