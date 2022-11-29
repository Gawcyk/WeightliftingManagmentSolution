using Xunit;
using WeightliftingManagment.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using WeightliftingManagment.Domain.Model;
using FluentAssertions;
using WeightliftingManagment.Core.Models.Config;

namespace WeightliftingManagment.Core.Services.Tests
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
        public void WhenBodyWeightIsGreaterByMenParamB_ThenResultShouldBe1(SinclaireConfig sinclaireConfig)
        {
            //Given
            var bodyWeight = sinclaireConfig.Men.ParamB + 1;
            var service = new SinclaireCoefficientService(sinclaireConfig);
            //When
            var result = service.Count(bodyWeight, Gender.Men);
            //Then
            result.Should().Be(1);
        }

        [Theory, AutoData]
        public void WhenBodyWeightIsGreaterByWomenParamB_ThenResultShouldBe1(SinclaireConfig sinclaireConfig)
        {
            //Given
            var bodyWeight = sinclaireConfig.Women.ParamB + 1;
            var service = new SinclaireCoefficientService(sinclaireConfig);
            //When
            var result = service.Count(bodyWeight, Gender.Women);
            //Then
            result.Should().Be(1);
        }

        [Theory, AutoData]
        public void WhenCountForMenWeightSmallerParamB_ThenResultShouldBeGreaterBy1(SinclaireConfig sinclaireConfig)
        {
            //Given
            var bodyWeight = new Random().Next(0, (int)sinclaireConfig.Men.ParamB);
            var service = new SinclaireCoefficientService(sinclaireConfig);
            //When
            var result = service.Count(bodyWeight, Gender.Men);
            //Then
            result.Should().BeGreaterThan(1);
        }

        [Theory, AutoData]
        public void WhenCountForWomenWeightSmallerParamB_ThenResultShouldBeGreaterBy1(SinclaireConfig sinclaireConfig)
        {
            //Given
            var bodyWeight = new Random().Next(0, (int)sinclaireConfig.Women.ParamB);
            var service = new SinclaireCoefficientService(sinclaireConfig);
            //When
            var result = service.Count(bodyWeight, Gender.Women);
            //Then
            result.Should().BeGreaterThan(1);
        }

        [Theory, AutoData]
        public void WhenCountTwoTimesResult_ThenResultMustBeTheSame(SinclaireConfig config, double bodyWeight,Gender gender)
        {
            //Given
            var sinclaireCoefficient = new SinclaireCoefficientService(config);
            //When
            var count1 = sinclaireCoefficient.Count(bodyWeight, gender);
            var count2 = sinclaireCoefficient.Count(bodyWeight, gender);
            //Then
            count1.Should().Be(count2);
        }
    }
}