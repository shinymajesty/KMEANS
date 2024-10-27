using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmeansColorClustering
{
    internal class KMeans
    {
        // TODO: Implement KMeans algorithm for clustering
        // Generate DIFF function


        internal static Image ClusterImage(Image image, int k, int iterations, int runs)
        {
            var input_image = ConvertToByteArray(image);

            List<Color> centroids =[];

            for(int i = 0; i < k; i++)
            {
                centroids.Add(GenerateCentroid(input_image, centroids));
            }
            return new Bitmap(1, 1);
        }
        


        internal static Image GeneratDifferentialImage(Image a, Image b)
        {
            return new Bitmap(1, 1);
        }


        /// <summary>
        /// Converts an image to a 3D byte array
        /// </summary>
        /// <param name="image">The image to be converted</param>
        /// <returns>byte[,,] the result as [Width,Height,[R,G,B]]</returns>
        internal static byte[,,] ConvertToByteArray(Image image) // I made this myself im so proud haha :D no dependencies for me!!!!!!
        // but seriously, i don't know if it's just alot less efficient but all others do it with a DataStream or something this just so easy why not?
        {
            using Bitmap bmp = new(image); // Very practical to use using here, as it disposes the bitmap after the block, also fancy .NET 8 syntax 
            byte[,,] result = new byte[bmp.Width, bmp.Height, 3]; // [Widht, Height, [R,G,B]] 
            // Populate the array
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color color = bmp.GetPixel(x, y);
                    result[x, y, 0] = color.R;
                    result[x, y, 1] = color.G;
                    result[x, y, 2] = color.B;
                }
            }
            return result;
        }
        
        /// <summary>
        /// Calculates the squared euclidean distance between two colors
        /// </summary>
        /// <param name="a">Color a</param>
        /// <param name="b">Color b</param>
        /// <returns>Returns the squared euclidean as <see cref="int"/></returns>
        internal static int Distance(byte[] a, byte[] b)
        {
            return
                (int) (
                  Math.Pow(a[0] - b[0], 2) 
                + Math.Pow(a[1] - b[1], 2) 
                + Math.Pow(a[2] - b[2], 2)
                );
        }
        
        private static Color GenerateCentroid(byte[,,] image, List<Color> centroids)
        {
            Color color = GenerateRandomColor(); // This method is just here to improve readability. You can thank me later 

            return (centroids.Contains(color)) ? GenerateCentroid(image, centroids) : color; // Avoid centroids spawning on top of each other
        }


        /// <summary>
        /// Generates a random color
        /// </summary>
        /// <returns>Returns a <see cref="Color"/> with random RGB values [0, 255].</returns>

        private static Color GenerateRandomColor()
        {
            Random random = new();

            int red = random.Next(0, 256);
            int green = random.Next(0, 256);
            int blue = random.Next(0, 256);

            return Color.FromArgb(red, green, blue);
        }
    }
}
