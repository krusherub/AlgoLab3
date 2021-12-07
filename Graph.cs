using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLab3
{
    public class Graph : ICloneable
    {
        public List<Vertex> vertexes;
        public bool[,] _graph;
        public List<int> UsedColors { get; set; }
        public Graph()
        {
            Init();
        }

        private void TestInit()
        {
            vertexes = new List<Vertex>(10);
            _graph = new bool[10, 10];
            UsedColors = new List<int>(10);
            
            //Vertexes
            for (var i = 0; i < 10; i++)
            {
                vertexes.Add(new Vertex(i));
            }
            //Graph
            Random generator = new Random();
            for (int i = 0; i < _graph.GetLength(0); i++)
            {
               // int j = i;
                for (int k = 0, j = 0; j < _graph.GetLength(1); j++)
                {
                    if(j == i)
                        continue;
                    if (k >= 4)
                        break;
                    bool res = generator.Next(100) < 40 ? true : false;
                    if (j < i)
                    {
                        if (_graph[i, j] == true || _graph[j,i] == true)
                            k++;
                        continue;
                    }
                    if (res == true && j > i)
                    { 
                        k++;
                       // vertexes[i].connections.Add(vertexes[j]);
                        _graph[i, j] = true;
                        _graph[j, i] = true;
                    }
                    
                }
            }
            
            for (int i = 0; i < _graph.GetLength(0); i++)
            {
                for (int j = 0; j < _graph.GetLength(1); j++)
                {
                    if(_graph[i,j])
                        vertexes[i].connections.Add(vertexes[j]);
                }
            }
        }
        //rebuild
        private void Init()
        {
            vertexes = new List<Vertex>(200);
            _graph = new bool[200, 200];
            UsedColors = new List<int>(200);
            
            
            //Vertexes
            for (var i = 0; i < 200; i++)
            {
                vertexes.Add(new Vertex(i));
            }
            //Graph
            Random generator = new Random();
            for (int i = 0; i < _graph.GetLength(0); i++)
            {
               // int j = i;
                for (int k = 0, j = 0; j < _graph.GetLength(1); j++)
                {
                    if(j == i)
                        continue;
                    if (k >= 20)
                        break;
                    bool res = generator.Next(100) < 40 ? true : false;
                    if (j < i)
                    {
                        if (_graph[i, j] == true || _graph[j,i] == true)
                            k++;
                        continue;
                    }
                    if (res == true && j > i)
                    { 
                        k++;
                        // vertexes[i].connections.Add(vertexes[j]);
                        _graph[i, j] = true;
                        _graph[j, i] = true;
                    }
                    
                }
            }
            for (int i = 0; i < _graph.GetLength(0); i++)
            {
                for (int j = 0; j < _graph.GetLength(1); j++)
                {
                    if(_graph[i,j])
                        vertexes[i].connections.Add(vertexes[j]);
                }
            }
        }

        public object Clone()
        {
            var graph = new Graph();
            Array.Copy(this._graph,graph._graph,this._graph.Length);
            graph.UsedColors = this.UsedColors.ToList();
            int count = this.vertexes.Count;
            graph.vertexes.Clear();
            for (var i = 0; i < count; i++)
            {
                graph.vertexes.Add(new Vertex(i));
            }
            for (int i = 0; i < graph._graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph._graph.GetLength(1); j++)
                {
                    if(graph._graph[i,j])
                        graph.vertexes[i].connections.Add(graph.vertexes[j]);
                }
            }

            return graph;
        }

        public void DrawGraph()
        {
            for(int row = 0; row < _graph.GetLength(0); row++)
            {
                for(int column = 0; column < _graph.GetLength(1); column++)
                    Console.Write((_graph[row, column] ? "1" : "0") + " ");
                Console.WriteLine();
            }
        }
    }
}