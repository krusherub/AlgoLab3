using System.Collections.Generic;

namespace AlgoLab3
{
    public class Vertex
    {
        public int Number { get; }
        public int? Color { get; set; } = null;
        public List<Vertex> connections; // Pow is a connections.Count

        public Vertex(int number)
        {
            Number = number;
            connections = new List<Vertex>(20);
        }
    }
}