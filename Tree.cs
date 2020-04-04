using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_From_The_Woods
{
    public class Tree
    {
        #region atr
        public int treeID { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public bool Bezet { get => bezet; set => bezet = value; }
        //Of er een aap op de boom zit
        private Boolean bezet = false;
        #endregion

        //Boom constr
        public Tree(int treeID, int x, int y)
        {
            this.treeID = treeID;
            this.x = x;
            this.y = y;
        }

        //is er momenteel al een aap op?
        public Boolean IsBoomBezet()
        {
            if (bezet)
            {
                return true;
            }
            else return false;
        }
    }
}
