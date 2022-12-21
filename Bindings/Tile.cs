using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class Tile
    {
        private int xCoord, yCoord;
        private bool active = false;
        private List<Liquid> liquids = new List<Liquid>();
        private List<Gas> gasses = new List<Gas>();
        private List<Gas> nextGas = new List<Gas>();
        private List<Item> items = new List<Item>();
        private Station station;
        private Critter critter;
        private List<Effect> effects = new List<Effect>();

        public Tile(int xPos, int yPos, Gas airContent)
        {
            this.xCoord = xPos;
            this.yCoord = yPos;
            this.gasses.Add(airContent);
        }
        public List<Gas> getGasContents()
        {
            return this.gasses;
        }
        public void setGasContents(List<Gas> gasses)
        {
            this.gasses = gasses;
        }
        public (Gas, int) getGas(string type)
        {
            for (int i = 0; i < gasses.Count; i++)
            {
                if (gasses[i].getType() == type)
                {
                    return (gasses[i], i);
                }
            }
            return (null, 0);
        }
        public void setGas(Gas gasItem)
        {
            for (int i = 0; i < gasses.Count; i++)
            {
                if (gasses[i].getType() == gasItem.getType())
                {
                    gasses[i] = gasItem;
                    return;
                }
            }
            gasses.Add(gasItem);
        }
        public (int, int) getCoords()
        {
            return (this.xCoord, this.yCoord);
        }
        public bool isActive()
        {
            return this.active;
        }
        public void addNextGas(Gas gas)
        {
            nextGas.Add(gas);
        }
        public void updateTile()
        {
            //consider throwing this into a different function when you add other things to get updated.
            //using a gas for nextgas means I can couple together the amount into the unused thing. Since I'm not passing by reference, creating a new Gas shouldn't take any more space
            foreach (Gas i in nextGas)
            {
                bool found = false;
                foreach (Gas k in this.gasses)
                {
                    if (i.getType() == k.getType())
                    {
                        k.increaseAmount(i.getAmount());
                        found = true;
                    }
                }
                if (!found)
                {
                    this.gasses.Add(i);
                }
                nextGas.Remove(i);
            }
        }
    }
}
