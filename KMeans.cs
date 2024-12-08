﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace KmeansColorClustering
{
    internal class KMeans
    {
        //Parameters dont show in summary I'm not really sure as to why though ...
        /// <summary>
        /// Clusters an Image using K-Means Clustering Algorithm
        /// </summary>
        /// <param name="image"><see cref="Image"/> image to be clustered</param>
        /// <param name="k"><see cref="int"/> number of colors</param>
        /// <param name="iterations"><see cref="int"/> number of maximum iterations</param>
        /// <param name="runs"><see cref="int"/> number of runs</param>
        /// <param name="updateProgress"> <see cref="Action{Byte}"/> binding to progressbar</param>
        /// <returns> Clustered <see cref="Image"/> image</returns>
        internal static Image Cluster(Image image, int k, int iterations, int runs, Action<byte> updateProgress)
        {
            var input_image = ConvertToByteArray(image); // Convert Image to Byte array so we can alter pixel directly and efficiently!
            byte[,,] output_image = new byte[0,0,0];

            int progress = 0; // Variable for progressbar progress

            List<Pixel> pixels = GetPixelsFromImage(input_image); // Get a list of pixel from our byte array
            List<List<Centroid>> clusters = []; // Centroids for each run saveed in a lisst of lists


            for (int r = 1; r <= runs; r++) // Get all the centroids for all the runs
            {
                List<Centroid> centroids = ClusterImage(pixels, k, iterations, runs, ref progress, updateProgress);

                clusters.Add(centroids);
            }


            // Calculate all variance for each run in order to find the smallest
            List<double> dists = [];
            foreach (var centroid in clusters)
                dists.Add(CalculateVariance(centroid));

            List<Centroid> finalCentroids = clusters.OrderBy(CalculateVariance).First(); // Order by shortest distance and take the first elemnt



            // Assign each pixel the color of the centroid (for generating the output image)

            foreach (var centroid in finalCentroids)
            {
                foreach (var pixel in centroid.Pixels)
                {
                    pixel.Color = centroid.Color;
                }

            }


            output_image = GetImageFromPixels(pixels); // Convert list of pixels back to Byte[,,]

            return ConvertToImage(output_image); // Turn the byte[,,] back to type System.Drawing.Image
        }

        /// <summary>
        /// Clustering of singular image without selecting the optimal run
        /// </summary>
        /// <param name="image"><see cref="Image"/> image to be clustered</param>
        /// <param name="k"><see cref="int"/> number of colors</param>
        /// <param name="iterations"><see cref="int"/> number of maximum iterations</param>
        /// <param name="runs"><see cref="int"/> number of runs</param>
        /// <param name="progress"><see cref="ref int"/> the current progress of the algorithm</param>
        /// <param name="updateProgress"> <see cref="Action{Byte}"/> binding to progressbar</param>
        /// <param name="convergenceThreshold"> minimum amount of change for the algorithm to exit</param>
        /// <returns> Clustered <see cref="Image"/> image</returns>
        private static List<Centroid> ClusterImage(List<Pixel> pixels, int k, int iterations, int runs, ref int progress, Action<byte> updateProgress, double convergenceThreshold = 0.1)
        {
            List<Centroid> centroids = GetCentroids(k);
            for (int i = 1; i <= iterations; i++)
            {
                // Update the progressbar
                updateProgress((byte)(((progress++) * 100) / (runs * iterations)));


                // Remember the previous centroids to check for convergence
                List<Centroid> prevCentroids = centroids.Select(c => new Centroid(c.Color)).ToList();


                // Empty the HashSet of pixels in the centroid, since some pixels might not be assigned to the centroid
                // it used to be assigned to anymore
                foreach (var centroid in centroids)
                    centroid.Pixels.Clear();

                // Find the closest centroid foreach pixel
                Parallel.ForEach(pixels, pixel =>
                {
                    Centroid closestCentroid = Pixel.FindClosestCentroid(centroids, pixel);
                    lock (closestCentroid.Pixels)
                        closestCentroid.Pixels.Add(pixel);

                });

                // Calculate the center for each centroid 
                Parallel.ForEach(centroids, centroid =>
                {
                    CalculateCenter(centroid);
                });

                
                // Stop the algorithm if centroids converged
                if (CheckCentroidMovement(centroids, prevCentroids, convergenceThreshold)) // If centroids didn't move its safe to say that the algorithm can stop
                    break;


            }
            return centroids;
        }

        /// <summary>
        /// Finds out wether or not centroids moved more than the threshold
        /// </summary>
        /// <param name="centroids"><see cref="List{Centroid}"/>Current Centroids</param>
        /// <param name="prevCentroids"><see cref="List{Centroid}"/>Previous Centroids</param>
        /// <param name="threshold"><see cref="double">The convergence threshold</param>
        /// <returns><see cref="bool"/>Returns True when all centroids moved LESS than the specified threshold</returns>
        private static bool CheckCentroidMovement(List<Centroid> centroids, List<Centroid> prevCentroids, double threshold)
        {
            var distances = centroids.Zip(prevCentroids, (current, previous) => Distance(current.Color, previous.Color)).ToList();
            // So what this does is: it fuses centroids and prevCentroids into a new list where each element is the distance between the two centroids
            // We can do this because no new centroids are added as the algorithm runs. 
            // After that all the distances are compared with the threshold to check wether the centroids moved or not
            // .All returns true if ALL elements of the collection fulfull the condition in this case the minimum moved distance.
            // Never using LINQ again ...
            return distances.All(distance => distance < threshold);
        }

        /// <summary>
        /// Turns a 32bpp (bits per pixel [R;G,B;A]) Image into one with 24bpp [R;G;B]
        /// </summary>
        /// <param name="img"><see cref="Bitmap"/>Image to be converted (32bpp)</param>
        /// <returns><see cref="Bitmap"/>The converted (24bpp) Image</returns>
        private static Bitmap ConvertTo24bpp(Bitmap img)
        {
            // Create a new Bitmap with 24bpp format and the same size as the original image
            Bitmap newImage = new(img.Width, img.Height, PixelFormat.Format24bppRgb);

            // Use Graphics to draw the 32bpp image onto the 24bpp Bitmap
            // This "Deletes" the alpha color channel
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            }

            return newImage;
        }


        /// <summary>
        /// Calculates the sum of the variances of all centroids in a list
        /// </summary>
        /// <param name="centroids"><see cref="List{Centroid}"/>The List of centroids</param>
        /// <returns>The sum of all variances as <see cref="double"/></returns>
        private static double CalculateVariance(List<Centroid> centroids) => centroids.Sum(c => CalculateVariance(c));

        /// <summary>
        /// Calculates the sum of the variances of a centroid to its pixels
        /// </summary>
        /// <param name="centroids"><see cref="Centroid"/>The centroid</param>
        /// <returns>The variances as <see cref="double"/></returns>
        private static double CalculateVariance(Centroid centroid) 
            => centroid.Pixels.Count == 0 ? 0 : centroid.Pixels.Sum(pixel => Distance(centroid.Color, pixel.Color));
            // Must check the centroid contains pixels else the Sum statement will throw exception and break everything



        /// <summary>
        /// Calculates the position of the Centroid according to its pixels (average)
        /// </summary>
        /// <param name="centroid"><see cref="Centroid"/>Centroid for repositioning</param>
        private static void CalculateCenter(Centroid centroid)
        {
            if (centroid.Pixels.Count == 0) return; 
            // Again here for the next statement to execute properly we need to confirm that the centroid contains atleast a pixel
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


        internal static Image GenerateDifferenceImage(Image a, Image b)
        {
            byte[,,] img1 = ConvertToByteArray(a);
            byte[,,] img2 = ConvertToByteArray(b);

            byte[,,] result = new byte[img1.GetLength(0), img1.GetLength(1), 3];

            for(int i = 0; i < result.GetLength(0); i++)
            {
                for(int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j, 0] = (byte)Math.Abs(img1[i, j, 0] - img2[i, j, 0]);
                    result[i, j, 1] = (byte)Math.Abs(img1[i, j, 1] - img2[i, j, 1]);
                    result[i, j, 2] = (byte)Math.Abs(img1[i, j, 2] - img2[i, j, 2]);
                }
            }
            return ConvertToImage(result);
        }


        /// <summary>
        /// Converts an image to a 3D byte array
        /// </summary>
        /// <param name="image">The image to be converted</param>
        /// <returns>byte[,,] the result as [Width,Height,[R,G,B]]</returns>
        internal static byte[,,] ConvertToByteArray(Image image) 
        // turns out GetPixel is stupid slow so he is gonna get replaced by LockBits
        {
            using Bitmap bmp = ConvertTo24bpp(new(image));
            Rectangle rect = new(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat); // Lock the bits so other code can't mess with it
            IntPtr ptr = bmpData.Scan0; // Get the pointer to the first pixel this works because our pixels are saved in an array (after one another in memory)
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height; // Gives us the amount of bytes we need to store the image in 1d array
            // Stride is the amount of bytes in one row of the image, is negative if the image is upside down 
            byte[] rgbValues = new byte[bytes];
            Marshal.Copy(ptr, rgbValues, 0, bytes); // Copy the bytes from the image to our array
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
        /// Converts a 3D byte array to an Image
        /// </summary>
        /// <param name="image"><see cref="byte[,,]"/>The image as 3D byte array</param>
        /// <returns><see cref="Image"/> The byte array as Image</returns>
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
