using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphModel
{  
    // this short test program shows performance of two diffrent graph representations for big sparse graphs
    class Program
    {
        static void Main(string[] args)
        {
            int start, stop;

            Graph_l g = new Graph_l(1000, false); 
            g.insert_edge(0, 4);
            g.insert_edge(4, 3);
            g.insert_edge(3, 5);
            g.insert_edge(3, 6);
            g.insert_edge(4, 2);
            g.insert_edge(4, 1);
            
            Graph_m g1 = new Graph_m(1000, false);
            g1.insert_edge(0, 4);
            g1.insert_edge(4, 3);
            g1.insert_edge(3, 5);
            g1.insert_edge(3, 6);
            g1.insert_edge(4, 2);
            g1.insert_edge(4, 1);

            
            int[] path = g1.shortest_path(g1.find_vertex_by_label("label0"), g1.find_vertex_by_label("label6"));

            start = Environment.TickCount;
            for(int i=0; i<100000;i++)
                g.shortest_path(0, 6);
            stop = Environment.TickCount;
            Console.WriteLine("Czas pracy dla reprezentacji listowej: " + (stop - start)+"\n");

            start = Environment.TickCount;
            for (int i = 0; i < 100000; i++)
             g1.shortest_path(0, 6);
            stop = Environment.TickCount;
            Console.WriteLine("Czas pracy dla reprezentacji macierzowej: " + (stop - start));

            Console.WriteLine("Najkrótsza ścieżka z wierzckołka 0 do wierzchołka 6: ");
            foreach (int n in path)
            {
                Console.Write(n);
               if(n!=0) Console.Write("<-");
            }
            Console.Read();

        }
    }
}
