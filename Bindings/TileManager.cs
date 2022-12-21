using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Server
{
    public class TileManager
    {
        const int height=1000, width=1000;
        private List<List<Tile>> tiles = new List<List<Tile>>();
        private float timer;

        //awake called before start, needed because RenderManager runs off of Start and requires the Tiles be baked at that point
        void Awake()
        {
            //need to make all of the tiles
            for (int x = 0; x < width; x++)
            {
                List<Tile> tempCol = new List<Tile>();
                for (int y = 0; y < height; y++)
                {
                    //for the moment, creating air filled tiles, except for outside edge, as a way to test air flow.
                    Gas airContent;
                    if (y > 0 && y < height && x > 0 && x < width)
                    {
                        airContent = GasPresets.Air(100);
                    }
                    else
                    {
                        airContent = GasPresets.Air(0);
                    }
                    Tile tempTile = new Tile(x, y, airContent);
                    tempCol.Add(tempTile);
                }
                tiles.Add(tempCol);
            }
            //need to start the timer
            timer = 0F;
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= .1)
            {
                //tick is sixth of a second
                //all update logic goes here

                //iterate through tiles, since this IS tilemanager
                foreach (List<Tile> j in tiles)
                {
                    foreach (Tile i in j)
                    {
                        //checks if active to save on compute cycles, since we are likely to have quite a few tiles going.
                        if (i.isActive())
                        {
                            //maybe compile this into a function? Maybe toss it into i.updateTile
                            (int xCoord, int yCoord) = i.getCoords();
                            foreach (Gas e in i.getGasContents())
                            {
                                //spread gas
                                e.spreadToTiles(findTile(xCoord - 1, yCoord), findTile(xCoord + 1, yCoord), findTile(xCoord, yCoord - 1), findTile(xCoord, yCoord + 1));
                            }
                            i.updateTile();
                        }
                    }
                }
                //reset timer if it's over .1 seconds
                timer = 0;
            }
        }
        public Tile findTile(int x, int y)
        {
            return tiles[x][y];
        }
        public List<Tile> renderTileSubset(Vector3 camLoc)
        {
            List<Tile> tempList = new List<Tile>();
            //checks for all tiles a certain radius around the camera location, and returns them.
            for (int i = -20; i <= 20; i++)
            {
                for (int j = -20; j <= 20; j++)
                {
                    if (camLoc.x + i >= 0 && camLoc.y + j >= 0)
                    {
                        tempList.Add(findTile((int)camLoc.x + i, (int)camLoc.y + j));
                    }
                }
            }
            return tempList;
        }
        public List<List<Tile>> getTiles()
        {
            return tiles;
        }
    }
}
