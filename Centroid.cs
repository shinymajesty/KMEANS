using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmeansColorClustering
{
    internal class Centroid(Color color, HashSet<Pixel>? pixels = null)
    {
        public Color Color { get; set; } = color;
        public HashSet<Pixel> Pixels { get; set; } = pixels ?? []; // cant put pixels=[] in constructor as its not compile time constant
    }

}
