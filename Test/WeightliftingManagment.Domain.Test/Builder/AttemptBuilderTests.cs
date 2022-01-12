using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using FluentAssertions;

using WeightliftingManagment.Domain.Builder;
using WeightliftingManagment.Domain.Model;
using Xunit;

namespace WeightliftingManagment.Domain.Test.Builder
{
    public class AttemptBuilderTests
    {
        private AttemptBuilder _builder;

        public AttemptBuilderTests()
        {
            //Given
            _builder = new AttemptBuilder();
        }

        [Theory, AutoData]
        public void WhenAttemptBuilderWithValueBuild_ThenAttemptValueIsSet(int value)
        {
            // When
            var attempt = _builder.WithValue(value).Build();
            // Then
            attempt.Value.Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenAttemptBuilderWithStatusBuild_ThenAttemptStatusIsSet(AttemptStatus status)
        {
            // When
            var attempt = _builder.WithStatus(status).Build();
            // Then
            attempt.Status.Should().Be(status);
        }
    }
}
