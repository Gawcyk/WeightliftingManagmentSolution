using System.Linq;

using AutoFixture;
using AutoFixture.Xunit2;

namespace WeightliftingManagment.Domain.Model.Tests
{
    internal class AutoParticipantCollectionDataAttribute : AutoDataAttribute
    {
        public AutoParticipantCollectionDataAttribute():base(Create) 
        {

        }

        private static IFixture Create()
        {
            var fixture = new Fixture();
            var data = fixture.CreateMany<Participant>(count: 10).ToList();
            fixture.Register(() => new ParticipantCollection(data));
            return fixture;
        }
    }
}