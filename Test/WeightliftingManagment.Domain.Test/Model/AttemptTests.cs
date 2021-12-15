using WeightliftingManagment.Domain.Model;
using FluentAssertions;

using Xunit;

namespace WeightliftingManagment.Domain.Model.Tests
{
    public class AttemptTests
    {
        [Fact()]
        public void CreatAttemptTest()
        {
            var model = new Attempt();
            model.Value.Should().Be(0);
            model.Status.Should().Be(AttemptStatus.NoDeclared);
        }

        [Fact()]
        public void CreatAttemptFromValueTest()
        {
            var model = new Attempt(120);
            model.Value.Should().Be(120);
            model.Status.Should().Be(AttemptStatus.Declared);
        }

        [Fact()]
        public void SetResultTest()
        {
            var model = new Attempt(120);
            model.SetStatus(AttemptStatus.GoodLift);
            model.Value.Should().Be(120);
            model.Status.Should().Be(AttemptStatus.GoodLift);
        }

        [Theory]
        [InlineData(AttemptStatus.Declared, 0)]
        [InlineData(AttemptStatus.Resignation, 0)]
        [InlineData(AttemptStatus.Max, 1)]
        [InlineData(AttemptStatus.GoodLift, 1)]
        [InlineData(AttemptStatus.NoLift, -1)]
        public void AttemptToIntTest(AttemptStatus result, int expected)
        {
            var model = new Attempt { Value = 100, Status = result };
            (model.Value * expected).Should().Be(model.AttemptToInt());
        }

        [Fact()]
        public void ResultIsZaliczonyTest()
        {
            var model = new Attempt { Value = 100, Status = AttemptStatus.GoodLift };
            model.StatusIsGoodLift().Should().BeTrue();
        }

        [Fact()]
        public void ResultIsSpalonyTest()
        {
            var model = new Attempt { Value = 100, Status = AttemptStatus.NoLift };
            model.StatusIsNoLift().Should().BeTrue();
        }

        [Fact()]
        public void ResultIsAnnonsTest()
        {
            var model = new Attempt { Value = 100, Status = AttemptStatus.Declared };
            model.StatusIsDeclared().Should().BeTrue();
        }

        [Fact()]
        public void ResultIsPodchodziTest()
        {
            var model = new Attempt { Value = 100, Status = AttemptStatus.ComesUp };
            model.StatusIsComesUp().Should().BeTrue();
        }

        [Fact()]
        public void ResultIsNastepnyTest()
        {
            var model = new Attempt { Value = 100, Status = AttemptStatus.Next };
            model.StatusIsNext().Should().BeTrue();
        }

        [Theory]
        [InlineData(AttemptStatus.ComesUp)]
        [InlineData(AttemptStatus.Next)]
        public void ResultIsPodchodziOrNastepnyTest(AttemptStatus result)
        {
            var model = new Attempt { Value = 100, Status = result };
            model.StatusIsComesUpOrNext().Should().BeTrue();
        }

        [Theory]
        [InlineData(AttemptStatus.ComesUp)]
        [InlineData(AttemptStatus.Next)]
        [InlineData(AttemptStatus.Declared)]
        public void ResultIsAnnonsOrPodchodziOrNastepnyTest(AttemptStatus result)
        {
            var model = new Attempt { Value = 100, Status = result };
            model.StatusIsDeclaredOrComesUpOrNext().Should().BeTrue();
        }

        [Fact()]
        public void ResultIsMaxTest()
        {
            var model = new Attempt { Value = 100, Status = AttemptStatus.Max };
            model.StatusIsMax().Should().BeTrue();
        }

        [Fact()]
        public void ResultIsRezygnacjaTest()
        {
            var model = new Attempt { Value = 100, Status = AttemptStatus.Resignation };
            model.StatusIsResignation().Should().BeTrue();
        }

        [Fact()]
        public void StatusIsNoDeclaredTest()
        {
            var model = new Attempt();
            model.StatusIsNoDeclared().Should().BeTrue();
        }

        [Theory()]
        [InlineData(120)]
        [InlineData(10)]
        [InlineData(60)]
        [InlineData(125)]
        public void SetValueTest(int value)
        {
            var model = new Attempt();
            model.SetValue(value);
            model.Value.Should().Be(value); 
        }
    }
}