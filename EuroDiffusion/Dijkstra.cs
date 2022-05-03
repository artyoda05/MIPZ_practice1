using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    internal class Dijkstra
    {
        public static (int, City)[] dijkstra((int, City)[][] graph, int source)
        {
            int count = graph.Length;
            bool[] visitedVertex = new bool[count];
            int[] distance = new int[count];
            for (int i = 0; i < count; i++)
            {
                visitedVertex[i] = false;
                distance[i] = int.MaxValue;
            }

            // Distance of self loop is zero
            distance[source] = 0;
            for (int i = 0; i < count; i++)
            {

                // Update the distance between neighbouring vertex and source vertex
                int u = findMinDistance(distance, visitedVertex);
                visitedVertex[u] = true;

                // Update all the neighbouring vertex distances
                for (int v = 0; v < count; v++)
                {
                    if (!visitedVertex[v] && graph[u][v].Item1 != 0 && (distance[u] + graph[u][v].Item1 < distance[v]))
                    {
                        distance[v] = distance[u] + graph[u][v].Item1;
                    }
                }
            }

            var result = new List<(int, City)>();
            
            for (int i = 0; i < distance.Length; i++)
            {
                result.Add((distance[i], graph[i][i].Item2));
            }

            return result.ToArray();

        }

        // Finding the minimum distance
        private static int findMinDistance(int[] distance, bool[] visitedVertex)
        {
            int minDistance = int.MaxValue;
            int minDistanceVertex = -1;
            for (int i = 0; i < distance.Length; i++)
            {
                if (!visitedVertex[i] && distance[i] < minDistance)
                {
                    minDistance = distance[i];
                    minDistanceVertex = i;
                }
            }
            return minDistanceVertex;
        }
    }
}
