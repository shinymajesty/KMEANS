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
    }
}
