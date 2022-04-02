using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedCollection
{
    public class Seed
    {

        public String seed;
        public String biomes;
        public String description;

        public Seed(String seed,String biomes,String description)
        {
            this.seed = seed;
            this.biomes = biomes;
            this.description = description;
        }

    }
}
