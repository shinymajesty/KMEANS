using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmeansColorClustering
{
    public class KNN
    {
        public static Color FindKNNColor(Color[] colors, Color targetColor, int k)
        {
            // Calculate the distance between the target color and all other colors
            var distances = colors.Select(c => ColorHelper.Distance(c, targetColor)).ToArray();

            // Sort the colors by distance
            Array.Sort(distances, colors);

            // Take the first k colors
            var k_nearest_colors = colors.Take(k).ToArray();

            // Calculate the average color
            return ColorHelper.CalculateAverageColor(k_nearest_colors);
        }
    }
}
