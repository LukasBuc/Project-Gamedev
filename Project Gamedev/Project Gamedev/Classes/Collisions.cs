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
        //List<ICollide> mogelijkeCollisionObjecten;
        List<ICollide> mogelijkeCollisionObjecten;
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


        //TODO margin variabele toevoegen en cijfers proberen weghalen uit code
        public void CheckCollisions(ICollide myCollisionRectangle)
        {
            BottomCollisions = new List<ICollide>();
            RightCollisions = new List<ICollide>();
            LeftCollisions = new List<ICollide>();
            TopCollisions = new List<ICollide>();

            for (int i = 0; i < mogelijkeCollisionObjecten.Count; i++)
            {
                //Check bottom
                if (myCollisionRectangle.GetCollisionRectangle().Bottom >= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Top - 5 &&
                    myCollisionRectangle.GetCollisionRectangle().Bottom <= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Top + (mogelijkeCollisionObjecten[i].GetCollisionRectangle().Height /2) &&
                    myCollisionRectangle.GetCollisionRectangle().Right >= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Left + 5 &&
                    myCollisionRectangle.GetCollisionRectangle().Left <= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Right - 5)
                {
                    BottomCollisions.Add(mogelijkeCollisionObjecten[i]);
                    BottomCollisionHeight = mogelijkeCollisionObjecten[i].GetCollisionRectangle().Top;
                }

                //Check right
                if (myCollisionRectangle.GetCollisionRectangle().Right >= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Left - 2 &&
                    myCollisionRectangle.GetCollisionRectangle().Right <= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Left + 2 &&
                    myCollisionRectangle.GetCollisionRectangle().Bottom >= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Top + 8 &&
                    myCollisionRectangle.GetCollisionRectangle().Top <= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Bottom - 8)
                {
                    RightCollisions.Add(mogelijkeCollisionObjecten[i]);
                }

                // Check left
                if (myCollisionRectangle.GetCollisionRectangle().Left >= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Right - 2 &&
                    myCollisionRectangle.GetCollisionRectangle().Left <= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Right + 2 &&
                    myCollisionRectangle.GetCollisionRectangle().Bottom >= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Top + 8 &&
                    myCollisionRectangle.GetCollisionRectangle().Top <= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Bottom - 8)
                {
                    LeftCollisions.Add(mogelijkeCollisionObjecten[i]);
                }

                //Check top
                if (myCollisionRectangle.GetCollisionRectangle().Top <= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Bottom + 5 &&
                    myCollisionRectangle.GetCollisionRectangle().Top >= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Bottom - (mogelijkeCollisionObjecten[i].GetCollisionRectangle().Height /2) &&
                    myCollisionRectangle.GetCollisionRectangle().Right >= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Left + 5 &&
                    myCollisionRectangle.GetCollisionRectangle().Left <= mogelijkeCollisionObjecten[i].GetCollisionRectangle().Right - 5)
                {
                    TopCollisions.Add(mogelijkeCollisionObjecten[i]);
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
                        //Er is collision tussen een tile en een projectiel
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
