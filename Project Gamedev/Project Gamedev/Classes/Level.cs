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
        public List<Texture2D> Texture;

        public Level()
        {
            Texture = new List<Texture2D>();
        }

        public void AddTextures(Texture2D newTexture)
        {
            Texture.Add(newTexture);
        }

        public byte[,] LevelArray = new byte[,]
        {
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0 },
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0 }, //Max hoogte scherm
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,11,11,11 },
            {1,0,0,0,0,0,0,0,0,0,0,0,4,5,6,0,0,0,4,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,8,8,8 },
            {1,0,0,4,6,0,0,0,0,4,6,0,0,0,0,0,4,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,6,0,0,0,0,0 },
            {1,6,0,0,0,0,0,4,6,0,0,0,0,0,4,6,0,0,0,0,4,5,5,5,6,0,0,0,0,0,4,6,0,0,0,0,1,2,2,2 },
            {1,0,0,0,0,0,0,0,0,0,0,0,4,6,0,0,0,0,0,0,0,0,0,0,0,0,0,4,6,0,0,0,0,0,0,0,10,11,11,11 },
            {1,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,4,6,0,0,0,0,0,0,0,0,0,10,0,0,0 },
            {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0 },
        };

        public Tile[,] TileArray = new Tile[9, 40];

        public void CreateWorld()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 40; y++)
                {
                    switch (LevelArray[x,y])
                    {
                        case 0:
                            //0 is een lege tile
                            break;
                        case 1:
                            TileArray[x, y] = new Tile(Texture[0], new Vector2(y * 64, x * 64));
                            break;
                        case 2:
                            TileArray[x, y] = new Tile(Texture[1], new Vector2(y * 64, x * 64));
                            break;
                        case 3:
                            TileArray[x, y] = new Tile(Texture[2], new Vector2(y * 64, x * 64));
                            break;
                        case 4:
                            TileArray[x, y] = new Tile(Texture[3], new Vector2(y * 64, x * 64));
                            break;
                        case 5:
                            TileArray[x, y] = new Tile(Texture[4], new Vector2(y * 64, x * 64));
                            break;
                        case 6:
                            TileArray[x, y] = new Tile(Texture[5], new Vector2(y * 64, x * 64));
                            break;
                        case 7:
                            TileArray[x, y] = new Tile(Texture[6], new Vector2(y * 64, x * 64));
                            break;
                        case 8:
                            TileArray[x, y] = new Tile(Texture[7], new Vector2(y * 64, x * 64));
                            break;
                        case 9:
                            TileArray[x, y] = new Tile(Texture[8], new Vector2(y * 64, x * 64));
                            break;
                        case 10:
                            TileArray[x, y] = new Tile(Texture[9], new Vector2(y * 64, x * 64));
                            break;
                        case 11:
                            TileArray[x, y] = new Tile(Texture[10], new Vector2(y * 64, x * 64));
                            break;
                        case 12:
                            TileArray[x, y] = new Tile(Texture[11], new Vector2(y * 64, x * 64));
                            break;
                    }
                }
            }
        }

        public void DrawLevel(SpriteBatch spritebatch)
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 40; y++)
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
