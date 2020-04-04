using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_From_The_Woods
{
    public class Program
    {
        public static List<Map> mappen;
        public static List<Monkey> monkeys;
        public static Tree dichtsteBoom;

        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string connectionString = "Data Source=./SQLEXPRESS;Initial Catalog=WoodEscape;Integrated Security=True";

            DataBeheer db = new DataBeheer(connectionString);


            mappen = new List<Map>();
            monkeys = new List<Monkey>();
            var random = new Random();

            //maak een map(pen) aan
            Map map1 = new Map(0, 200, 0, 250, 80);
            map1.woodID = 0;

            Map map2 = new Map(0, 100, 0, 150, 15);
            map1.woodID = 1;

            mappen.Add(map1);
            mappen.Add(map2);

            #region MapTesten_voor_OLENA

            //Kijk of 20 bomen gemaakt
            Console.WriteLine("Map has " + map1.TreesInMap + " trees");

            //Kijk of bomen in de map staan
            //map1.trees.ForEach(t => Console.WriteLine("Boom met Id: " + t.treeID +
            //    " staat op locatie x,y: (" + t.x + "," + t.y + ")")
            //    );

            #endregion MapTesten_voor_OLENA

            //Voeg aap toe aan map
            map1.VoegAapToe("Nina");
            map1.VoegAapToe("Annabele");
            map1.VoegAapToe("Elize");
            map2.VoegAapToe("Frank");
            map2.VoegAapToe("Michiel");

            #region Nina_en_afstandTesten_voor_OLENA

            //var nina = map1.monkeys.FirstOrDefault(m => m.naam == "Nina");
            //waar is Nina test
            //   Console.WriteLine("Nina bevind zich op boom met Id: " + nina.isOpBoom().treeID + " staat op locatie: (" + nina.isOpBoom().x + "," + nina.isOpBoom().y + ")");
            //afstand test

            //double x = map1.AfstandBerekenen2Bomen(nina.isOpBoom(), map1.trees.FirstOrDefault(t => t.treeID == (nina.isOpBoom().treeID + 1)));
            //string round = x.ToString();
            //Console.WriteLine("afstand tussen bomen is {0}", round);

            //DichtsteBoom bij Nina
            //dichtsteBoom = map1.KortsteafstandBoomBerekenenAap1(nina);
            //Console.WriteLine(dichtsteBoom.treeID);

            ////Dichtste Boom of bij Nina?
            //double d1 = map1.AfstandBerekenen2Bomen(nina.isOpBoom(), dichtsteBoom);
            //double d2 = map1.AfstandBerekenenBorder(nina.isOpBoom());

            //Console.WriteLine("Naar dichtst boom: {0}", d1);
            //Console.WriteLine("Naar dichtst rand: {0}", d2);

            #endregion Nina_en_afstandTesten_voor_OLENA

            //Laat de apen maar springen, tot ze eruit zijn!
            //  map1.Spring();

            //while (map1.monkeys.Any()) {
            //    int index = random.Next(map1.monkeys.Count);
            //    await map1.ASCSpring(map1.monkeys[index]);

            //}
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (Map m in mappen)
            {
                while (m.monkeys.Any())
                {

                    for (int i = 0; i <= m.monkeys.Count - 1; i++)
                    {
                        await m.ASCSpring(m.monkeys[i]);

                    }

                }

            }

            stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            Console.WriteLine("end");

        }

    }
}
