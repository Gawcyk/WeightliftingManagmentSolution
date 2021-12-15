using System;

using AutoFixture.Xunit2;

using FluentAssertions;

using Xunit;

namespace WeightliftingManagment.Domain.Model.Tests
{
    public class ParticipantCollectionTests
    {
        [Fact()]
        public void ParticipantCollectionTest()
        {
            var model = new ParticipantCollection();

            model.Should().BeEmpty();
        }

        [Theory, AutoData]
        public void AddNewParticipantTest(Participant participant)
        {
            var model = new ParticipantCollection();

            model.AddNewParticipant(participant);
            model.Count.Should().Be(1);
            model.Contains(participant).Should().BeTrue();
        }

        [Theory, AutoData]
        public void AddTwoTimesOneParticipantTest(Participant participant)
        {
            var model = new ParticipantCollection();

            model.AddNewParticipant(participant);
            Action action = () => model.AddNewParticipant(participant);
            action.Should().Throw<ArgumentException>();
        }

        [Theory, AutoData]
        public void GetParticipantByIDTest(Participant participant)
        {
            var model = new ParticipantCollection();

            model.AddNewParticipant(participant);

            model.GetParticipantByID(participant.ParticipantId).Should().Be(participant);
        }

        [Theory, AutoData]
        public void GetParticipantWithComesUpTest(Participant participant)
        {
            var model = new ParticipantCollection();
            participant.Snatchs[0].SetStatus(AttemptStatus.ComesUp);
            participant.SetComesUp(true);
            model.AddNewParticipant(participant);
            model.GetParticipantWithComesUp().Should().Be(participant);
        }

        [Theory, AutoData]
        public void GetParticipantWithNextTest(Participant participant)
        {
            var model = new ParticipantCollection();
            participant.Snatchs[0].SetStatus(AttemptStatus.Next);
            participant.SetNext(true);
            model.AddNewParticipant(participant);
            model.GetParticipantWithNext().Should().Be(participant);
        }

        [Theory, AutoData]
        public void IsSnatchTest(Participant participant)
        {
            var model = new ParticipantCollection();
            participant.Snatchs[0].SetStatus(AttemptStatus.Declared);
            model.AddNewParticipant(participant);
            model.IsSnatch().Should().BeTrue();
        }

        [Theory, AutoParticipantCollectionData]
        public void SortByGrupaThenByNrStartTest(ParticipantCollection participantCollection)
        {
            var model = participantCollection;
            participantCollection.SortByGrupaThenByNrStart();
            model.SortByGrupaThenByNrStart();
            model.Should().BeSameAs(participantCollection);
        }

        [Theory, AutoParticipantCollectionData]
        public void SortByNrStartTest(ParticipantCollection participantCollection)
        {
            var model = participantCollection;
            model.SortByNrStart();
            participantCollection.SortByNrStart();
            model.Should().BeSameAs(participantCollection);
        }

        [Theory, AutoParticipantCollectionData]
        public void SortByGrupaTest(ParticipantCollection participantCollection)
        {
            var model = participantCollection;
            model.SortByGrupa();
            participantCollection.SortByGrupa();
            model.Should().BeSameAs(participantCollection);
        }

        [Theory, AutoParticipantCollectionData]
        public void SortByNrStartThenByGrupaTest(ParticipantCollection participantCollection)
        {
            var model = participantCollection;
            model.SortByNrStartThenByGrupa();
            participantCollection.SortByNrStartThenByGrupa();
            model.Should().BeSameAs(participantCollection);
        }

        [Theory, AutoParticipantCollectionData]
        public void SetAttemptStatusToGoodLiftForParticipantIdTest(ParticipantCollection participantCollection)
        {
            var participantId = participantCollection[0].ParticipantId;
            participantCollection.SetAttemptStatusToGoodLiftForParticipantId(participantId, 0, true);
            participantCollection.GetParticipantByID(participantId).Snatchs[0].StatusIsGoodLift().Should().BeTrue();
        }

        [Theory, AutoParticipantCollectionData]
        public void SetAttemptStatusToNoLiftForParticipantIdTest(ParticipantCollection participantCollection)
        {
            var participantId = participantCollection[0].ParticipantId;
            participantCollection.SetAttemptStatusToNoLiftForParticipantId(participantId, 0, true);
            participantCollection.GetParticipantByID(participantId).Snatchs[0].StatusIsNoLift().Should().BeTrue();
        }

    }
}