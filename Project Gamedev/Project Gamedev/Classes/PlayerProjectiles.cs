using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev.Classes
{
    class PlayerProjectiles
    {
        public Texture2D texture;
        public List<Projectile> playerProjectileList = new List<Projectile>();

        private List<Projectile> projectilesToRemove;

        public PlayerProjectiles()
        {

        }

        public void AddPlayerProjectile(Texture2D _texture, Vector2 _positie, bool lookingLeft, double currentTime)
        {
            if (playerProjectileList.Count < 10)
            {
                playerProjectileList.Add(new Projectile(_texture, _positie, lookingLeft, currentTime));
            }
        }

        public void DrawPlayerProjectiles(SpriteBatch spritebatch)
        {
            foreach (var projectile in playerProjectileList)
            {
                projectile.Draw(spritebatch);
            }
        }

        public void UpdatePlayerProjectiles(GameTime gametime)
        {
            projectilesToRemove = new List<Projectile>();
            foreach (var projectile in playerProjectileList)
            {
                projectile.Update(gametime);

                //Projectiles die langer dan 1.5 seconden vliegen worden aan een lijst toegevoegd die later wordt verwijderd
                if ((gametime.TotalGameTime.TotalSeconds - projectile.timeToLive) > 1.5)
                {
                    projectilesToRemove.Add(projectile);
                }
            }

            //Lijst projectiles die moeten worden verwijderd, worden hier verwijderd
            for (int i = 0; i < projectilesToRemove.Count; i++)
            {
                playerProjectileList.Remove(projectilesToRemove[i]);
            }
        }

        public void RemovePlayerProjectile(int collideObject)
        {
            playerProjectileList.RemoveAt(collideObject);
        }
    }
}
