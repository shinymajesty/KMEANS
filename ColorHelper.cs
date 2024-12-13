using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KmeansColorClustering
{
    internal class ColorHelper
    {
        public static int Distance(Color a, Color b)
        {
            Vector3 color1 = new(a.R, a.G, a.B);
            Vector3 color2 = new(b.R, b.G, b.B);
            Vector3 diff = color1 - color2;
            return (int)Vector3.Dot(diff, diff); // Squared distance
        }



        /// <summary>
        /// Calculates the squared euclidean distance between two colors
        /// </summary>
        /// <param name="a">Color a</param>
        /// <param name="b">Color b</param>
        /// <returns>Returns the squared euclidean as <see cref="int"/></returns>
        public static int Distance(byte[] a, byte[] b)
        {
            // I reject Math.Pow because I don't like unnecessary overhead!!
            return (int)(
                (a[0] - b[0]) * (a[0] - b[0]) +
                (a[1] - b[1]) * (a[1] - b[1]) +
                (a[2] - b[2]) * (a[2] - b[2])
            );
        }

        /// <summary>
        /// Generates a random color
        /// </summary>
        /// <returns>Returns a <see cref="Color"/> with random RGB values [0, 255].</returns>
        public static Color GenerateRandomColor()
        {
            Random random = new();

            int red = random.Next(0, 256);
            int green = random.Next(0, 256);
            int blue = random.Next(0, 256);

            return Color.FromArgb(red, green, blue);
        }


        public static Color CalculateAverageColor(IEnumerable<Color> colors) => CalculateAverageColor(colors.Select(c => new Pixel(0,0, c)));

        /// <summary>
        /// Calculates the position of the Centroid according to its pixels (average)
        /// </summary>
        /// <param name="centroid"><see cref="Centroid"/>Centroid for repositioning</param>
        public static Color CalculateAverageColor(IEnumerable<Pixel> pixels)
        {
            if (pixels.Count() == 0) throw new ArgumentException("Pixels can not be empty");

            // Again here for the next statement to execute properly we need to confirm that the centroid contains atleast a pixel
            return Color.FromArgb(
                               (int)pixels.Average(p => p.Color.R),
                               (int)pixels.Average(p => p.Color.G),
                               (int)pixels.Average(p => p.Color.B)
                               );
        }
    }
}
