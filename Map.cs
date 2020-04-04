using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_From_The_Woods
{
    public class Map
    {
        #region atrr

        public int woodID { get; set; }
        public int TreesInMap { get; set; }
        public List<Tree> trees = new List<Tree>();
        public List<Monkey> monkeys = new List<Monkey>();

        public int x_min { get; set; }
        public int x_max { get; set; }
        public int y_min { get; set; }
        public int y_max { get; set; }

        #endregion atrr

        //Random aanmaken voor bomen
        private Random r = new Random();

        //Maak de map aan.
        public Map(int x_min, int x_max, int y_min, int y_max, int bomen)
        {
            this.x_min = x_min;
            this.x_max = x_max;
            this.y_min = y_min;
            this.y_max = y_max;
            this.TreesInMap = bomen;

            // Bomen aanmaken  op willekeurige locatie
            for (int i = 0; i < TreesInMap; i++)
            {
                // nieuwe boom genereren op willekeurige locatie
                Tree t = new Tree(i,
                            r.Next(x_min, x_max),
                            r.Next(y_min, y_max));
                trees.Add(t);
            };
        }

        //Voeg aap toe methode
        public void VoegAapToe(String naam)
        {
            //kies een random startboom van bomen lijst
            var index = r.Next(trees.Count);
            Tree startBoom = trees[index];

            //kijk of boom bezet is, bezet -> andere random boom
            while (startBoom.Bezet)
            {
                index = r.Next(trees.Count);
                startBoom = trees[index];
            }

            //Als boom niet bezet is, plaats dan aap
            if (!startBoom.Bezet)
            {
                Monkey m = new Monkey(naam, startBoom.x, startBoom.y);
                monkeys.Add(m);
                //Geef aan dat boom bezet is
                startBoom.Bezet = true;
                //Voegt boom toe aan bezochteBomenLijst
                m.aapBezektNu(startBoom);

                Console.WriteLine("Start calculating route for woods : {0} for monkey : {1}", woodID, naam);
            };
        }

        //Berekent afstanden tussen 2 bomen
        private double AfstandBerekenen2Bomen(Tree t1, Tree t2)
        {
            double d = Math.Sqrt(Math.Pow(t1.x - t2.x, 2) + Math.Pow(t1.y - t2.y, 2));
            return d;
        }

        //Berekent afstanden tussen boom en randen
        private double AfstandBerekenenBorder(Tree t)
        {
            double d = (new List<double>()
            {
                y_max - t.y,
                x_max - t.x,
                t.y - y_min,
                t.x - x_min
            }.Min());

            return d;
        }
        //Berekent dichste boom voor aap 
        private Tree KortsteafstandBoomBerekenenAap1(Monkey m)
        {
            List<Tree> ZonderBezochteBomen = trees.Except(m.BeszochteBomen).ToList();
            Tree dichtsteBoom = ZonderBezochteBomen.OrderBy(t => AfstandBerekenen2Bomen(m.isOpBoom(), t)).FirstOrDefault();
            return dichtsteBoom;
        }




        //go monkeys go
        public void Spring()
        {
            if (!monkeys.Any())
            {
                Console.WriteLine("The woods are now monkey-less ");
            }
            monkeys.ToList().ForEach(m =>
            {
                Tree boomWaarAapOpZat = m.isOpBoom();

                Tree dichtsteBoom = KortsteafstandBoomBerekenenAap1(m);
                double d_v_Boom = AfstandBerekenen2Bomen(m.isOpBoom(), dichtsteBoom);
                double d_v_Rand = AfstandBerekenenBorder(m.isOpBoom());

                //zolang aap nog in de map is
                while (monkeys.Contains(m))
                {
                    //is rand dichter dan dichtsbijzijnde boom?
                    if (d_v_Rand < d_v_Boom)
                    {   //huidige boom gaat niet meer bezet zijn
                        m.isOpBoom().Bezet = false;
                        //Console.WriteLine("The woods only has "+ (monkeys.Count()-1) +" monkeys. {0} has left us :c", m.naam);
                        Console.WriteLine("{0}  left. The woods have {1} monkeys left.", m.naam, (monkeys.Count() - 1));
                        monkeys.Remove(m);
                    }
                    else
                    {
                        //huidige boom gaat niet meer bezet zijn
                        m.isOpBoom().Bezet = false;

                        //aap springt naar dichtse boom
                        m.aapBezektNu(dichtsteBoom);

                        //nieuwe huidige boom gaat bezet zijn
                        m.isOpBoom().Bezet = true;
                        Console.WriteLine("{0} is in tree {1} at ({2},{3})", m.naam, m.isOpBoom().treeID, m.isOpBoom().x, m.isOpBoom().y);
                        Spring();
                    }
                }
            });
        }

        //go monkeys go ASYNC
        public async Task ASCSpring(Monkey m)
        {
            Tree boomWaarAapOpZat = m.isOpBoom();

            Tree dichtsteBoom = KortsteafstandBoomBerekenenAap1(m);
            double d_v_Boom = AfstandBerekenen2Bomen(m.isOpBoom(), dichtsteBoom);
            double d_v_Rand = AfstandBerekenenBorder(m.isOpBoom());

            //zolang aap nog in de map is

            //is rand dichter dan dichtsbijzijnde boom?
            if (d_v_Rand < d_v_Boom)
            {   //huidige boom gaat niet meer bezet zijn
                m.isOpBoom().Bezet = false;
                //  Console.WriteLine("The woods only has "+ (monkeys.Count()-1) +" monkeys. {0} has left us :c", m.naam);
                Console.WriteLine("end calculating escape route for woods : {0} for monkey : {1}", woodID, m.naam); ;
                monkeys.Remove(m);

            }
            else
            {
                //huidige boom gaat niet meer bezet zijn
                m.isOpBoom().Bezet = false;

                //aap springt naar dichtse boom
                m.aapBezektNu(dichtsteBoom);

                //nieuwe huidige boom gaat bezet zijn
                m.isOpBoom().Bezet = true;
                Console.WriteLine("{0} is in tree {1} at ({2},{3})", m.naam, m.isOpBoom().treeID, m.isOpBoom().x, m.isOpBoom().y);

            }

            ;
        }

    }
}
