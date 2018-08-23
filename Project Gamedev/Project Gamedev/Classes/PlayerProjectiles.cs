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

        public PlayerProjectiles()
        {

        }

        public void AddPlayerProjectile(Texture2D _texture, Vector2 _positie, bool lookingLeft)
        {
            if (playerProjectileList.Count < 10)
            {
                playerProjectileList.Add(new Projectile(_texture, _positie, lookingLeft));
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
            foreach (var projectile in playerProjectileList)
            {
                projectile.Update(gametime);
            }
        }

        public void RemovePlayerProjectile(int collideObject)
        {

            playerProjectileList.RemoveAt(collideObject);

        }
    }
}
