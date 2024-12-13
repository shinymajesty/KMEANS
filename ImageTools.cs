using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KmeansColorClustering
{
    public static class ImageTools
    {
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
        /// Converts an image to a 3D byte array
        /// </summary>
        /// <param name="image">The image to be converted</param>
        /// <returns>byte[,,] the result as [Width,Height,[R,G,B]]</returns>
        internal static byte[,,] ConvertToByteArray(this Image image)
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


        internal static Image GenerateDifferenceImage(Image a, Image b)
        {
            byte[,,] img1 = ConvertToByteArray(a);
            byte[,,] img2 = ConvertToByteArray(b);

            byte[,,] result = new byte[img1.GetLength(0), img1.GetLength(1), 3];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j, 0] = (byte)Math.Abs(img1[i, j, 0] - img2[i, j, 0]);
                    result[i, j, 1] = (byte)Math.Abs(img1[i, j, 1] - img2[i, j, 1]);
                    result[i, j, 2] = (byte)Math.Abs(img1[i, j, 2] - img2[i, j, 2]);
                }
            }
            return result.ConvertToImage();
        }

        /// <summary>
        /// Converts a 3D byte array to an Image
        /// </summary>
        /// <param name="image"><see cref="byte[,,]"/>The image as 3D byte array</param>
        /// <returns><see cref="Image"/> The byte array as Image</returns>
        internal static Image ConvertToImage(this byte[,,] image)
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


        /// <summary>
        /// Scales down an Image to the specified width and height
        /// </summary>
        /// <param name="image">The image to be downscaled</param>
        /// <param name="width">The desired width of the downscaled Image</param>
        /// <param name="height">The desired height of the downscaled Image</param>
        /// <returns>The downscaled <see cref="Image"/> </returns>
        public static Image Downscale (this Image image, int width, int height)
        {
            if(width > image.Width || height > image.Height) throw new ArgumentException("The desired width and height must be smaller than the original image");
            Bitmap bmp = new(image, width, height);
            return bmp;
        }
    }
}
