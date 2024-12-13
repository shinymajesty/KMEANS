using System;
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
        internal static Image Cluster(Image image, int k, int iterations, int runs, Action<byte> updateProgress, int downscaleFactor)
        {
            var downscaledImage = image.Downscale(image.Width / downscaleFactor, image.Height / downscaleFactor);
            var input_image = downscaledImage.ConvertToByteArray(); // Convert Image to Byte array so we can alter pixel directly and efficiently!
            var input_image_full = image.ConvertToByteArray();
            byte[,,] output_image = new byte[0,0,0];

            int progress = 0; // Variable for progressbar progress

            List<Pixel> pixels = GetPixelsFromImage(input_image); // Get a list of pixel from our byte array
            List<Pixel> pixels_full = GetPixelsFromImage(input_image_full);
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

            Parallel.ForEach(pixels_full, p =>
            {
                p.Color = KNN.FindKNNColor(finalCentroids.Select(c => c.Color).ToArray(), p.Color, 1);
            });


            output_image = GetImageFromPixels(pixels_full); // Convert list of pixels back to Byte[,,]
            
            


            return output_image.ConvertToImage(); // Turn the byte[,,] back to type System.Drawing.Image
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
                    try
                    {
                        centroid.Color = ColorHelper.CalculateAverageColor(centroid.Pixels);
                    }
                    catch (Exception e)
                    {
                        centroid.Color = centroid.Color; // Doesn't do anything just for readability :)
                    }
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
            var distances = centroids.Zip(prevCentroids, (current, previous) => ColorHelper.Distance(current.Color, previous.Color)).ToList();
            // So what this does is: it fuses centroids and prevCentroids into a new list where each element is the distance between the two centroids
            // We can do this because no new centroids are added as the algorithm runs. 
            // After that all the distances are compared with the threshold to check wether the centroids moved or not
            // .All returns true if ALL elements of the collection fulfull the condition in this case the minimum moved distance.
            // Never using LINQ again ...
            return distances.All(distance => distance < threshold);
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
            => centroid.Pixels.Count == 0 ? 0 : centroid.Pixels.Sum(pixel => (double)ColorHelper.Distance(centroid.Color, pixel.Color));
            // Must check the centroid contains pixels else the Sum statement will throw exception and break everything

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

        private static Centroid GenerateCentroid(List<Centroid> centroids)
        {
            Centroid c = new(ColorHelper.GenerateRandomColor()); // This method is just here to improve readability. You can thank me later 

            return (centroids.Contains(c)) ? GenerateCentroid(centroids) : c; // Avoid centroids spawning on top of each other
        }
    }
}
