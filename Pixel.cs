using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmeansColorClustering
{
    internal class Pixel(int x, int y, Color color)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public byte R { get; set; } = color.R;
        public byte G { get; set; } = color.G;
        public byte B { get; set; } = color.B;
        public Centroid? Centroid { get; set; }
    }
}
