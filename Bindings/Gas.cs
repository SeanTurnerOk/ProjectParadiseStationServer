using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Gas
    {
        private string type;
        private List<string> tags = new List<string>();
        private int amount;
        public Gas(string type, List<string> tags, int amount)
        {
            this.type = type;
            this.tags = tags;
            this.amount = amount;
        }
        public int getAmount()
        {
            return this.amount;
        }
        public void setAmount(int amount)
        {
            this.amount = amount;
        }
        public void increaseAmount(int amount)
        {
            this.amount += amount;
        }
        public string getType()
        {
            return this.type;
        }
        public void setType(string type)
        {
            this.type = type;
        }
        public void spreadToTiles(Tile tile1, Tile tile2, Tile tile3, Tile tile4)
        {
            //TODO:: Clean this the heck up, mate. There's GOT to be a better way of doing this.
            Gas tempGas = new Gas(null, null, 0), tempGas1 = tempGas, tempGas2 = tempGas, tempGas3 = tempGas, tempGas4 = tempGas;
            List<Gas> temp;
            int total = 0, num = 0;
            if (tile1 != null)
            {
                //you might be riiiiiiiight
                //I might be crazyyyyyyyyy
                //But it just may be a LOOONATIC you're looking foooooooooor
                temp = tile1.getGasContents();
                foreach (Gas i in temp)
                {
                    if (i.type == this.type)
                    {
                        tempGas1 = i;
                        num++;
                    }
                }
            }
            if (tile2 != null)
            {
                temp = tile2.getGasContents();
                foreach (Gas i in temp)
                {
                    if (i.type == this.type)
                    {
                        tempGas2 = i;
                        num++;
                    }
                }
            }
            if (tile3 != null)
            {
                temp = tile3.getGasContents();
                foreach (Gas i in temp)
                {
                    if (i.type == this.type)
                    {
                        tempGas3 = i;
                        num++;
                    }
                }
            }

            if (tile4 != null)
            {
                temp = tile4.getGasContents();
                foreach (Gas i in temp)
                {
                    if (i.type == this.type)
                    {
                        tempGas4 = i;
                        num++;
                    }
                }
            }
            total = tempGas1.getAmount() + tempGas2.getAmount() + tempGas3.getAmount() + tempGas4.getAmount();
            total = total / num;
            tempGas = new Gas(this.type, this.tags, total);
            if (tile1 != null)
            {
                tile1.addNextGas(tempGas);
            }
            if (tile2 != null)
            {
                tile2.addNextGas(tempGas);
            }
            if (tile3 != null)
            {
                tile3.addNextGas(tempGas);
            }
            if (tile4 != null)
            {
                tile4.addNextGas(tempGas);
            }
        }
    }
}
