using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmeansColorClustering
{
    public class KNN
    {
        /// <summary>
        /// Finds the k-nearest neighbor color to the target color
        /// </summary>
        /// <param name="colors">All of the labels</param>
        /// <param name="targetColor">The label to find the neighbors</param>
        /// <param name="k">The number of nearest neighbors to find the color from</param>
        /// <returns>The resulting <see cref="Color"/> based on the KNN-Algorithm</returns>
        public static Color FindKNNColor(Color[] colors, Color targetColor, int k)
        {
            // Calculate the distance between the target color and all other colors
            var distances = colors.Select(c => ColorHelper.Distance(c, targetColor)).ToArray();

            // Sort the colors by distance
            Array.Sort(distances, colors);

            // Approach 1: Take the first k colors and calculate the average color 
            // Take the first k colors
            var k_nearest_color = ColorHelper.CalculateAverageColor(colors.Take(k).ToArray());

            // Approach 2: Take the first k colors and calculate the most common color
            // var k_nearest_color = colors.Take(k).GroupBy(c => c).OrderByDescending(g => g.Count()).First();


            // Calculate the average color
            return k_nearest_color;
        }
    }
}
