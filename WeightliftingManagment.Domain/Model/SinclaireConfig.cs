using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightliftingManagment.Domain.Model
{
    public class SinclaireConfig
    {
        public SinclaireConfig()
        {

        }
        public SinclaireConfig(double menA, double menB, double womenA, double womenB)
        {
            MenA = menA;
            MenB = menB;
            WomenA = womenA;
            WomenB = womenB;
        }

        public double MenA { get; set; }
        public double MenB { get; set; }
        public double WomenA { get; set; }
        public double WomenB { get; set; }
    }
}
