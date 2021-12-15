using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.Domain.Contracts;
using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Domain.Services
{
    public class SinclaireCoefficientService : ISinclaireCoefficientService
    {
        private readonly SinclaireConfig _sinclaireConfig;
        public SinclaireCoefficientService(SinclaireConfig sinclaireConfig)
        {
            _sinclaireConfig = sinclaireConfig;
        }

        public double Count(double bodyWeight, Gender gender) 
            => gender == Gender.Women ? CountByGender(bodyWeight, _sinclaireConfig.WomenA, _sinclaireConfig.WomenB)
                : CountByGender(bodyWeight, _sinclaireConfig.MenA, _sinclaireConfig.MenB);
        private static double CountByGender(double bodyWeight, double paramA, double paramB)
        {
            if (bodyWeight > paramB)
                return 1;

            return Math.Round(Math.Pow(10, (paramA * Math.Pow((Math.Log10(bodyWeight / paramB)), 2))), 6);
        }
    }
}
