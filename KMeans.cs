using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
            MessageBox.Show("Centroids generated");
            StringBuilder sb = new();
            foreach (var c in centroids)
            {
                sb.Append($"Centroid:  R: { c.R.ToString()} G: {c.G.ToString()} B: {c.B.ToString()}\n");
            }
            MessageBox.Show(sb.ToString());
            
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
        // turns out GePixel is stupid slow so he is gonna get replaced by LockBits
        {
            using Bitmap bmp = new(image);
            Rectangle rect = new(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat); // Lock the bits so other code can't mess with it
            IntPtr ptr = bmpData.Scan0; // Get the pointer to the first pixel this works because our pixels are saved in an array (after one another in memory)
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height; // Gives us the amount of bytes we need to store the image in 1d array
            // Stride is the amount of bytes in one row of the image, is negative if the image is upside down 
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes); // Copy the bytes from the image to our array
            // this stuff so cool why we not learn it in school xD

            byte[,,] result = new byte[bmp.Width, bmp.Height, 3]; // [Width, Height, [R,G,B]]

            int index = 0; // Index for the 1D array
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++) // This made me lose my mind why is it not stored as RGB but BGR ???????
                {
                    result[x, y, 0] = rgbValues[index + 2]; // R
                    result[x, y, 1] = rgbValues[index + 1]; // G
                    result[x, y, 2] = rgbValues[index]; // B
                    index += 3;
                }
                index += bmpData.Stride - (bmp.Width * 3); // Padding bytes 
            }

            bmp.UnlockBits(bmpData); // Unlock the bits so other code can access
            return result;

            // Note after the fact: For a full HD image the change from GetPixel to LockBits reduced the runtuime from <= 0.48 to <= 0.2 seconds which is a huge improvement 
            // Keep in mind these numbers are built in debug and not release so it will be even faster later on !! ^^
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
