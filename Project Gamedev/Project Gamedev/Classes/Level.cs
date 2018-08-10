using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Gamedev.Classes
{
    public class Level
    {
        public Texture2D texture;

        public Level()
        {
            
        }

        public byte[,] levelArray = new byte[,]
        {
            {0,0,0,0,1,0,0,0,0,1 },
            {1,0,0,1,0,0,0,0,0,0 },
            {1,1,1,1,1,0,0,1,0,0 },
        };

        public Tile[,] tileArray = new Tile[3, 10];

        public void CreateWorld()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (levelArray[x,y] == 1)
                    {
                        tileArray[x, y] = new Tile(texture, new Vector2(y * 128, x * 128));
                    }
                }
            }
        }

        public void DrawLevel(SpriteBatch spritebatch)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (tileArray[x, y] != null)
                    {
                        tileArray[x, y].Draw(spritebatch);
                    }
                }
            }
        }
    }
}
