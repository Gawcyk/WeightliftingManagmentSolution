using Xunit;
using WeightliftingManagment.Domain.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Domain.Builder.Tests
{
    public class ParticipantBuilderTests
    {
        private readonly ParticipantBuilder _builder;

        public ParticipantBuilderTests()
        {
            //Given
            _builder = new ParticipantBuilder();
        }

        [Fact()]
        public void WhenParticipantBuilderBuild_ThenParticipant_IsCreated()
        {
            //When
            var participant = _builder.Build();
            //Then
            participant.Should().NotBeNull();
        }


        [Theory, AutoData]
        public void WhenPartcipantBuilderBuildWithStartNumber_ThenStartNumber_IsSet(string value)
        {
            // When
            var participant = _builder.WithLicenseNumber(value).Build();
            // Then
            participant.LicenseNumber.Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenPartcipantBuilderBuildWithFirstAndLastName_ThenPersonalData_IsSet(string first, string last)
        {
            // When
            var participant = _builder.WithFirstAndLastName(first, last).Build();
            // Then
            participant.PersonalData.FirstName.Should().Be(first);
            participant.PersonalData.LastName.Should().Be(last);
        }

        [Theory, AutoData]
        public void WhenPartcipantBuilderBuildWithClub_ThenClub_IsSet(string value)
        {
            // When
            var participant = _builder.WithClub(value).Build();
            // Then
            participant.Club.Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenPartcipantBuilderBuildWithBodyWeight_ThenBodyWeight_IsSet(double value)
        {
            // When
            var participant = _builder.WithBodyWeight(value).Build();
            // Then
            participant.BodyWeight.Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenParticipantBuilderBuildWithYearOfBirth_ThenYearOfBirth_IsSet(int value)
        {
            //When
            var participant = _builder.WithYearOfBirth(value).Build();
            //Then
            participant.YearOfBirth.Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenParticipantBuilderBuildWithGender_ThenGender_IsSet(Gender value)
        {
            //When
            var participant = _builder.WithGender(value).Build();
            //Then
            participant.Gender.Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenParticipantBuilderBuildWithSnatchAttempt_ThenSnatch_IsSet(Attempt value)
        {
            //When
            var participant = _builder.WithSnatch(value).Build();
            //Then
            participant.Snatchs[0].Should().Be(value);
        }

        [Fact]
        public void WhenParticipantBuilderBuildWithSnatchCollection_ThenSnachCollection_IsSet()
        {
            var attempts = new AttemptCollection(
                new (120,AttemptStatus.GoodLift),
                new(130,AttemptStatus.GoodLift),
                new(140,AttemptStatus.GoodLift));
            //When
            var participant = _builder.WithSnatchs(attempts).Build();
            //Then
            participant.Snatchs.Should().Be(attempts);
        }

        [Fact]
        public void WhenParticipantBuilderBuildWithCleanJerkCollection_ThenCleanJerkCollection_IsSet()
        {
            var attempts = new AttemptCollection(
                new(120, AttemptStatus.GoodLift),
                new(130, AttemptStatus.GoodLift),
                new(140, AttemptStatus.GoodLift));
            //When
            var participant = _builder.WithCleanJerks(attempts).Build();
            //Then
            participant.CleanJerks.Should().Be(attempts);
        }

        [Theory, AutoData]
        public void WhenParticipantBuilderBuildWithSnatch_ThenSnatch_IsSet(int value)
        {
            //When
            var participant = _builder.WithSnatch(value).Build();
            //Then
            participant.Snatchs[0].Value.Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenParticipantBuilderBuildWithCleanJerkAttempt_ThenCleanJerk_IsSet(Attempt value)
        {
            //When
            var participant = _builder.WithCleanJerk(value).Build();
            //Then
            participant.CleanJerks[0].Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenParticipantBuilderBuildWithCleanJerk_ThenCleanJerk_IsSet(int value)
        {
            //When
            var participant = _builder.WithCleanJerk(value).Build();
            //Then
            participant.CleanJerks[0].Value.Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenParticipantBuilderBuildWithGroup_ThenGroup_IsSet(string value)
        {
            //When
            var participant = _builder.WithGroup(value).Build();
            //Then
            participant.Group.Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenParticipantBuilderBuildWithSinclaireCoefficient_ThenSinclaireCoefficient_IsSet(double value)
        {
            //When
            var participant = _builder.WithSinclaireCoefficient(value).Build();
            //Then
            participant.SinclairCoefficient.Should().Be(value);
        }

        [Theory, AutoData]
        public void WhenParticipantBuilderBuildWithLicenseNumber_ThenLicenseNumber_IsSet(string value)
        {
            //When
            var participant = _builder.WithLicenseNumber(value).Build();
            //Then
            participant.LicenseNumber.Should().Be(value);
        }


        [Theory, AutoData]
        public void WhenParticipantBuilderBuildWithCategory_ThenCategory_IsSet(Category value)
        {
            //When
            var participant = _builder.WithCategory(value).Build();
            //Then
            participant.Category.Should().Be(value);
        }
    }
}