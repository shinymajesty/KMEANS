using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmeansColorClustering
{
    internal class Centroid(Color color)
    {
        public Color Color { get; set; } = color;
        public ConcurrentBag<Pixel> Pixels { get; set; } = [];
    }

}
