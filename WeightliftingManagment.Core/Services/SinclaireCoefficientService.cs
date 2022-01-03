
using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.Models.Config;
using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Core.Services
{
    public class SinclaireCoefficientService : ISinclaireCoefficientService
    {
        private readonly SinclaireConfig _sinclaireConfig;

        public SinclaireCoefficientService(SinclaireConfig sinclaireConfig)
        {
            _sinclaireConfig = sinclaireConfig;
        }
        public double Count(double bodyWeight, Gender gender) => gender == Gender.Women
                ? CountCoefificient(bodyWeight,_sinclaireConfig.Women.ParamA,_sinclaireConfig.Women.ParamB)
            : CountCoefificient(bodyWeight,_sinclaireConfig.Men.ParamA,_sinclaireConfig.Men.ParamB);


        private static double CountCoefificient(double bodyWeight, double paramA, double paramB) => bodyWeight > paramB ? 1 : Math.Round(Math.Pow(10, paramA * Math.Pow(Math.Log10(bodyWeight / paramB), 2)), 6);
    }
}
