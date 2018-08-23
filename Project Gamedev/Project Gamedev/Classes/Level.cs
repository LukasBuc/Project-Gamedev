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
        public Texture2D Texture;

        public Level()
        {
            
        }

        public byte[,] LevelArray = new byte[,]
        {
            {1,0,0,0,0,0,0,0,0,0 },
            {1,0,0,0,0,0,0,0,0,0 },
            {1,0,0,0,0,0,0,0,0,0 },
            {1,0,0,1,0,0,0,1,0,1 },
            {1,1,0,0,0,0,1,0,0,0 },
            {1,0,0,0,0,1,1,0,0,0 },
            {1,0,0,0,1,0,0,0,0,0 },
            {1,1,1,1,1,1,1,1,1,1 },
        };

        public Tile[,] TileArray = new Tile[8, 10];

        public void CreateWorld()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (LevelArray[x,y] == 1)
                    {
                        TileArray[x, y] = new Tile(Texture, new Vector2(y * 64, x * 64)); // 128
                    }
                }
            }
        }

        public void DrawLevel(SpriteBatch spritebatch)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (TileArray[x, y] != null)
                    {
                        TileArray[x, y].Draw(spritebatch);
                    }
                }
            }
        }
    }
}
