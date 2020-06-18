using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModel
{
    public class WalkerDetailsViewModel
    {
        public Walker Walker { get; set; }

        public Neighborhood Neighborhood { get; set; }

        public List<Walk> Walk { get; set; }

        public Owner Owner { get; set; }

        public List<Dog> Dogs { get; set; }
    }
}
