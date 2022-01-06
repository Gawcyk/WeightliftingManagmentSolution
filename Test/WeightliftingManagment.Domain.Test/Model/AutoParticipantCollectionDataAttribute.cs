using System.Linq;

using AutoFixture;
using AutoFixture.Xunit2;

using WeightliftingManagment.MvvmSupport.Collections;

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
            foreach (var item in data)
            {
                item.Snatchs = new AttemptCollection(fixture.Build<Attempt>().With(x => x.Status, AttemptStatus.Declared).CreateMany(3).ToList());
                item.CleanJerks = new AttemptCollection(fixture.Build<Attempt>().With(x => x.Status, AttemptStatus.Declared).CreateMany(3).ToList());
            }
            fixture.Register(() => new ParticipantCollection(data));
            return fixture;
        }
    }
}