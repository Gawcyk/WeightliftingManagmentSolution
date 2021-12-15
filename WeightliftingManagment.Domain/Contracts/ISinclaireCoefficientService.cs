using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Domain.Contracts
{
    public interface ISinclaireCoefficientService
    {
        double Count(double bodyWeight, Gender gender);
    }
}
