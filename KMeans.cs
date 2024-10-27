using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
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

            

            List<Centroid> centroids = GetCentroids(k);

            List<Pixel> pixels = GetPixelsFromImage(input_image);

            for (int i = 0; i < 10; i++)
            {
                foreach (var centroid in centroids)
                    centroid.Pixels.Clear();

                Parallel.ForEach(pixels, pixel =>
                {
                    Centroid closestCentroid = Pixel.FindClosestCentroid(centroids, pixel);
                    lock (closestCentroid.Pixels)
                        closestCentroid.Pixels.Add(pixel);
                    
                });

                Parallel.ForEach(centroids, centroid =>
                {
                    CalculateCenter(centroid);
                });


            }

            foreach (var centroid in centroids)
            {
                foreach (var pixel in centroid.Pixels)
                {
                    pixel.Color = centroid.Color;
                }
                
            }


            var output_image = GetImageFromPixels(pixels);
           
            return ConvertToImage(output_image);
        }

        private static void CalculateCenter(Centroid centroid)
        {
            if (centroid.Pixels.Count == 0) return;
            centroid.Color = Color.FromArgb(
                               (int)centroid.Pixels.Average(p => p.Color.R),
                               (int)centroid.Pixels.Average(p => p.Color.G),
                               (int)centroid.Pixels.Average(p => p.Color.B)
                               );
        }

        
        private static List<Centroid> GetCentroids(int k)
        {
            List<Centroid> centroids = [];
            for (int i = 0; i < k; i++)
            {
                centroids.Add(GenerateCentroid(centroids));
            }
            return centroids;
        }


        private static List<Pixel> GetPixelsFromImage(byte[,,] image)
        {
            List<Pixel> pixels = [];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    pixels.Add(new(i, j, Color.FromArgb(image[i, j, 0], image[i, j, 1], image[i, j, 2]))); // Couldnt have put into conversion method beacuse of the x y thingy
                }
            }
            return pixels;
        }


        private static byte[,,] GetImageFromPixels(List<Pixel> pixels)
        {
            byte[,,] image = new byte[pixels.Max(p => p.X) + 1, pixels.Max(p => p.Y) + 1, 3];
            foreach (var pixel in pixels)
            {
                image[pixel.X, pixel.Y, 0] = pixel.Color.R;
                image[pixel.X, pixel.Y, 1] = pixel.Color.G;
                image[pixel.X, pixel.Y, 2] = pixel.Color.B;
            }
            return image;
        }


        internal static Image GeneratDifferentialImage(Image a, Image b)
        {
            if (a == b) { }
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


        internal static Image ConvertToImage(byte[,,] image)
        {
            // Exactly the same as ConvertToByteArray but in reverse 
            int width = image.GetLength(0);
            int height = image.GetLength(1);
            Bitmap bmp = new(width, height, PixelFormat.Format24bppRgb);

            // Lock the bits so we can copy the pixel data
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp.PixelFormat); 

            int bytesPerPixel = 3; // 1 Channel per color = 3 bytes per pixel (1 Byte -> 256 Values)
            int stride = bmpData.Stride;
            byte[] pixelData = new byte[stride * height]; // Stride is width with padding

            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int arrayIndex = (y * stride) + (x * bytesPerPixel);
                    pixelData[arrayIndex + 2] = (byte)image[x, y, 0]; // Red
                    pixelData[arrayIndex + 1] = (byte)image[x, y, 1]; // Green
                    pixelData[arrayIndex] = (byte)image[x, y, 2];     // Blue
                }
            }

            // Copy the byte array back to the bitmap
            Marshal.Copy(pixelData, 0, bmpData.Scan0, pixelData.Length);

            // Unlock the bits
            bmp.UnlockBits(bmpData);

            return bmp;
        }
        

        internal static int Distance(Color a, Color b)
        {
            return Distance([a.R, a.G, a.B], [b.R, b.G, b.B]);
        }


        /// <summary>
        /// Calculates the squared euclidean distance between two colors
        /// </summary>
        /// <param name="a">Color a</param>
        /// <param name="b">Color b</param>
        /// <returns>Returns the squared euclidean as <see cref="int"/></returns>
        internal static int Distance(byte[] a, byte[] b)
        {
            // I reject Math.Pow because I don't like unnecessary overhead!!
            return (int)(
                (a[0] - b[0]) * (a[0] - b[0]) +
                (a[1] - b[1]) * (a[1] - b[1]) +
                (a[2] - b[2]) * (a[2] - b[2])
            );
        }


        private static Centroid GenerateCentroid(List<Centroid> centroids)
        {
            Centroid c = new(GenerateRandomColor()); // This method is just here to improve readability. You can thank me later 

            return (centroids.Contains(c)) ? GenerateCentroid(centroids) : c; // Avoid centroids spawning on top of each other
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
