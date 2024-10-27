using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmeansColorClustering
{
    internal class Pixel(int x, int y, Color color)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public Color Color { get; set; } = color;

        public static Centroid FindClosestCentroid(List<Centroid> centroids, Pixel pixel)
        {
            // Performance difference between static and non static very minor here as this method is not overriden 
            // Would be different with polymorphism as there would be a vtable lookup for the method in runtime 
            Centroid closest = centroids[0];
            int minDistance = int.MaxValue;
            foreach (var centroid in centroids)
            {
                int distance = KMeans.Distance(pixel.Color, centroid.Color);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = centroid;
                }
            }
            return closest;
        }
    }
}
