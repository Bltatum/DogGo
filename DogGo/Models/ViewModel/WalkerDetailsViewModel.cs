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

        public int TotalWalkTime()
        {
            if(Walk != null)
            { 
            int totalTime = 0;
            Walk.ForEach(w => totalTime += w.Duration);
            return totalTime / 60;
            
            } else
            {
                return 0;
            }
        }
    }
}
