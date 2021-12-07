using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AlgoLab3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Solver solver = new Solver();
            
            for (int i = 1; i < 3; i++)
            {
                List<Graph> graphs = solver.GenerateUsingGreedyAlgo(i);
                var s = solver.BeesAlgo(5,graphs,10,100).First();
                Console.WriteLine("The chromatic number is : " + s.UsedColors.Count);
            }
            
            
                //graphs[0].DrawGraph();
            
            
            Console.WriteLine(1/2);
        }
    }
}