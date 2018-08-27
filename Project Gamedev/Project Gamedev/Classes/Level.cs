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
        public byte[,] LevelArray;
        public Tile[,] TileArray;

        const int Width = 64;
        const int Height = 64;
        private readonly int levelWidth;
        private readonly int levelHeight;

        public Level(byte[,] _levelArray)
        {
            Texture = new List<Texture2D>();
            LevelArray = _levelArray;
            levelHeight = LevelArray.GetLength(0);
            levelWidth = LevelArray.GetLength(1);            
            TileArray = new Tile[levelHeight, levelWidth];
        }

        public void AddTextures(Texture2D newTexture)
        {
            Texture.Add(newTexture);
        }

        public void CreateWorld()
        {
            for (int x = 0; x < levelHeight; x++) //9
            {
                for (int y = 0; y < levelWidth; y++) //40
                {
                    switch (LevelArray[x,y])
                    {
                        case 0:
                            //0 is een lege tile
                            break;
                        case 1:
                            TileArray[x, y] = new Tile(Texture[0], new Vector2(y * Height, x * Width));
                            break;
                        case 2:
                            TileArray[x, y] = new Tile(Texture[1], new Vector2(y * Height, x * Width));
                            break;
                        case 3:
                            TileArray[x, y] = new Tile(Texture[2], new Vector2(y * Height, x * Width));
                            break;
                        case 4:
                            TileArray[x, y] = new Tile(Texture[3], new Vector2(y * Height, x * Width));
                            break;
                        case 5:
                            TileArray[x, y] = new Tile(Texture[4], new Vector2(y * Height, x * Width));
                            break;
                        case 6:
                            TileArray[x, y] = new Tile(Texture[5], new Vector2(y * Height, x * Width));
                            break;
                        case 7:
                            TileArray[x, y] = new Tile(Texture[6], new Vector2(y * Height, x * Width));
                            break;
                        case 8:
                            TileArray[x, y] = new Tile(Texture[7], new Vector2(y * Height, x * Width));
                            break;
                        case 9:
                            TileArray[x, y] = new Tile(Texture[8], new Vector2(y * Height, x * Width));
                            break;
                        case 10:
                            TileArray[x, y] = new Tile(Texture[9], new Vector2(y * Height, x * Width));
                            break;
                        case 11:
                            TileArray[x, y] = new Tile(Texture[10], new Vector2(y * Height, x * Width));
                            break;
                        case 12:
                            TileArray[x, y] = new Tile(Texture[11], new Vector2(y * Height, x * Width));
                            break;
                        case 13:
                            TileArray[x, y] = new Tile(Texture[12], new Vector2(y * Height, x * Width));
                            break;
                        case 14:
                            TileArray[x, y] = new Tile(Texture[13], new Vector2(y * Height, x * Width));
                            break;
                        case 15:
                            TileArray[x, y] = new Tile(Texture[14], new Vector2(y * Height, x * Width));
                            break;
                        case 16:
                            TileArray[x, y] = new Tile(Texture[15], new Vector2(y * Height, x * Width));
                            break;
                    }
                }
            }
        }

        public void DrawLevel(SpriteBatch spritebatch)
        {
            for (int x = 0; x < levelHeight; x++) //9
            {
                for (int y = 0; y < levelWidth; y++) //40
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
