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
        public Texture2D Texture;
        public List<Projectile> PlayerProjectileList = new List<Projectile>();

        private List<Projectile> _projectilesToRemove;

        public PlayerProjectiles()
        {

        }

        public void AddPlayerProjectile(Texture2D _texture, Vector2 _positie, bool lookingLeft, double currentTime)
        {
            if (PlayerProjectileList.Count < 10)
            {
                PlayerProjectileList.Add(new Projectile(_texture, _positie, lookingLeft, currentTime));
            }
        }

        public void DrawPlayerProjectiles(SpriteBatch spritebatch)
        {
            foreach (var projectile in PlayerProjectileList)
            {
                projectile.Draw(spritebatch);
            }
        }

        public void UpdatePlayerProjectiles(GameTime gametime)
        {
            _projectilesToRemove = new List<Projectile>();
            foreach (var projectile in PlayerProjectileList)
            {
                projectile.Update(gametime);

                //Projectiles die langer dan 1.5 seconden vliegen worden aan een lijst toegevoegd die later wordt verwijderd
                if ((gametime.TotalGameTime.TotalSeconds - projectile.timeToLive) > 1.5)
                {
                    _projectilesToRemove.Add(projectile);
                }
            }

            //Lijst projectiles die moeten worden verwijderd, worden hier verwijderd
            for (int i = 0; i < _projectilesToRemove.Count; i++)
            {
                PlayerProjectileList.Remove(_projectilesToRemove[i]);
            }
        }
        public void RemovePlayerProjectile(int collideObject)
        {
            if (PlayerProjectileList.Count > collideObject)
            {
                PlayerProjectileList.RemoveAt(collideObject);
            }
            
        }
    }
}
