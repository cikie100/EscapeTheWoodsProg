using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_From_The_Woods
{
    public class Monkey
    {
        #region atr
        public String naam { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public List<Tree> BeszochteBomen { get; set; }
        #endregion

        public Monkey(string naam, int mx, int my)
        {
            this.naam = naam;
            this.x = mx;
            this.x = my;
            BeszochteBomen = new List<Tree>();
        }

        public Boolean HeeftAap_BoomAlBezocht(Tree tree)
        {
            return BeszochteBomen.Any(Tr => Tr.x == tree.x
                                            && Tr.y == tree.y);
        }

        //Aap springt naar volgende boom
        public void aapBezektNu(Tree tree)
        {
            //onthouden dat hij er al eens op zat
            BeszochteBomen.Add(tree);
            this.x = tree.x;
            this.y = tree.y;
        }

        //Op welke boom bevind deze aap?
        public Tree isOpBoom()
        {
            return BeszochteBomen.Last();
        }
    }
}
