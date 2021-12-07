using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AlgoLab3
{
    public class Solver
    {
        private int[] Colors { get; } // Colors type from 0 to 199

        public Solver()
        {
            Colors = new int[200];
            Init();
        }

        private void Init()
        {
            //Init Colors from 0 to 199
            for (var i = 0; i < Colors.Length; i++)
            {
                Colors[i] = i;
            }
        }

        public List<Graph> BeesAlgo(int iterations, List<Graph> graphs, int scoutBees = 2, int foragerBees = 28)
        {
           // List<Graph> graphs = GenerateUsingGreedyAlgo();
            List<Graph> newGraphs = new List<Graph>();
            for (int i = 0; i < iterations; i++)
            {
                //graphs = graphs.Distinct().ToList();
                
                for (int j = 0; j < scoutBees; j++)
                {
                    Graph graphClone;
                    
                    //new graph
                    
                    graphClone = (Graph)graphs[j];
                    
                    
                    for (var k = 0; k < graphClone.vertexes.Count; k++)
                    {
                        //Try to swap each vertex
                        var vertex = graphClone.vertexes[k];
                        // vertex.connections = vertex.connections.OrderByDescending(vertex1 => vertex1.connections.Count).ToList();
                        int count = foragerBees / scoutBees;
                        for (int l = 0; l < count && l < vertex.connections.Count; l++)
                        {
                            //Try to swap each vertex not more times than foragerbees
                            int tempColor = (int)vertex.Color;
                            int tempColor2 = (int)vertex.connections[l].Color;
                            //Swap with vertex.connections[i]
                            for (int m = 0; m < vertex.connections.Count; m++)
                            {
                                if (vertex.connections[m].Color == tempColor2)
                                {
                                    //leave circle
                                    l = vertex.connections.Count + 1;
                                    break;
                                }
                            }
                            graphs = graphs.OrderBy(graph => graph.UsedColors.Count).ToList();
                            if (l == vertex.connections.Count + 1)
                                break;
                            List<int> otherUsedColors = new List<int>();
                            
                            //Check other vertex connections
                            for (int m = 0; m < vertex.connections[l].connections.Count; m++)
                            {
                                otherUsedColors.Add((int)vertex.connections[l].connections[m].Color);
                                if (vertex.connections[l].connections[m].Color == tempColor)
                                {
                                    //leave circle
                                    l = vertex.connections.Count + 1;
                                }
                            }

                            otherUsedColors = otherUsedColors.Distinct().ToList();
                            if (l == vertex.connections.Count + 1 || otherUsedColors.Count >= graphClone.UsedColors.Count-1)
                                break;
                            
                            List<int> s = graphClone.UsedColors.Except(otherUsedColors).ToList();
                            
                            for (int m = 0; m < s.Count; m++)
                            {
                                if (s[m] != tempColor2)
                                {
                                    vertex.connections[l].Color = s[m];
                                    graphClone.UsedColors.Remove(tempColor);
                                    vertex.Color = tempColor2;
                                    break;
                                }
                            }
                            
                        }
                        
                    }
                    graphs.Insert(0,graphClone);
                    
                }
            }
            
            return graphs;
        }
        public List<Graph> GenerateUsingGreedyAlgo(int number)
        {
            //Init
            List<Graph> graphs = new List<Graph>(number); // 10 graphs
            var mainGraph = new Graph();
            for (var i = 0; i < number; i++)
            {
                graphs.Add((Graph)mainGraph.Clone());
            }
            Console.WriteLine("Graph pow: " + mainGraph.vertexes.Count);
            //Algorithm
            for (int i = 0, b = graphs.Count; i < graphs.Count; i++,b--)
            {
                //Sort graph vertexes
                Random random = new Random();
                if (random.Next(100) > 60)
                {
                    graphs[i].vertexes = graphs[i].vertexes.OrderBy(vertex => vertex.connections.Count).ToList();
                }
                else
                {
                    graphs[i].vertexes = graphs[i].vertexes.OrderByDescending(vertex => vertex.connections.Count).ToList();
                }
                
                (graphs[i].vertexes[0], graphs[i].vertexes[b]) = (graphs[i].vertexes[b], graphs[i].vertexes[0]);


                //Color vertexes
                int k = 0;
                while (true)
                {
                    
                    if (graphs[i].vertexes.Find(vertex => vertex.Color == null) == null)
                        break;
                    graphs[i].vertexes.Find(vertex => vertex.Color == null).Color = k;
                    
                    
                    for (int j = 1; j < graphs[i].vertexes.Count; j++)
                    {
                        var vertex = graphs[i].vertexes[j];
                        
                        if(vertex.Color != null)
                            continue;
                        
                        for (var l = 0; l < j; l++)
                        {
                            var vertex1 = graphs[i].vertexes[l];
                            if(vertex1.Color != k)
                                continue;
                            if (vertex.connections.Contains(vertex1))
                            {
                                vertex.Color = null;
                                break;
                            }

                            vertex.Color = k;
                        }
                    }
                    graphs[i].UsedColors.Add(k);
                    k++;
                }
            }

            return graphs.OrderByDescending(graph => graph.UsedColors.Count ).ToList();
        }
    }
}