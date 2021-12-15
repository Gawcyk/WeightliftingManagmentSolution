using Xunit;
using WeightliftingManagment.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using WeightliftingManagment.Domain.Model;
using FluentAssertions;

namespace WeightliftingManagment.Domain.Services.Tests
{
    public class SinclaireCoefficientServiceTests
    { 
        [Theory, AutoData]
        public void SinclaireCoefficientServiceTest(SinclaireConfig sinclaireConfig)
        {
            var model = new SinclaireCoefficientService(sinclaireConfig);
            model.Should().NotBeNull();
        }

        [Theory, AutoData]
        public void CountForMenWeightGreaterParamBTest(SinclaireConfig sinclaireConfig)
        {
            var bodyWeight = sinclaireConfig.MenB + 1;
            var service = new SinclaireCoefficientService(sinclaireConfig);
            service.Count(bodyWeight, Gender.Men).Should().Be(1);
        }

        [Theory, AutoData]
        public void CountForWomenWeightGreaterParamBTest(SinclaireConfig sinclaireConfig)
        {
            var bodyWeight = sinclaireConfig.WomenB + 1;
            var service = new SinclaireCoefficientService(sinclaireConfig);
            service.Count(bodyWeight, Gender.Women).Should().Be(1);
        }

        [Theory, AutoData]
        public void CountForMenWeightSmallerParamBTest(SinclaireConfig sinclaireConfig)
        {
            var bodyWeight = new Random().Next(0, (int)sinclaireConfig.MenB);
            var service = new SinclaireCoefficientService(sinclaireConfig);
            var firstCount = service.Count(bodyWeight, Gender.Men);
            service.Count(bodyWeight, Gender.Men).Should().Be(firstCount);
        }

        [Theory, AutoData]
        public void CountForWomenWeightSmallerParamBTest(SinclaireConfig sinclaireConfig)
        {
            var bodyWeight = new Random().Next(0, (int)sinclaireConfig.WomenB);
            var service = new SinclaireCoefficientService(sinclaireConfig);
            var firstCount = service.Count(bodyWeight, Gender.Men);
            service.Count(bodyWeight, Gender.Men).Should().Be(firstCount);
        }
    }
}