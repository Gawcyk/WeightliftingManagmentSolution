
using AutoFixture;
using AutoFixture.Xunit2;

using WeightliftingManagment.MvvmSupport.Collections;

namespace WeightliftingManagment.Domain.Model.Tests
{
    internal class AutoParticipantDataAttribute : AutoDataAttribute
    {
        public AutoParticipantDataAttribute() : base(Create)
        {

        }

        private static IFixture Create()
        {
            var fixture = new Fixture();
            var model = fixture.Create<Participant>();
            model.Snatchs = new AttemptCollection(fixture.Build<Attempt>().With(x => x.Status, AttemptStatus.Declared).CreateMany(3).ToList());
            model.CleanJerks = new AttemptCollection(fixture.Build<Attempt>().With(x => x.Status, AttemptStatus.Declared).CreateMany(3).ToList());
            fixture.Register(() => model);
            return fixture;
        }
    }
}