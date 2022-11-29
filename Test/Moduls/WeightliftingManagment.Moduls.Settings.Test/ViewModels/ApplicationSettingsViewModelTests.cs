using System.ComponentModel;

using FluentAssertions;

using Moq.AutoMock;

using WeightliftingManagment.Moduls.Settings.Models;

using Xunit;

namespace WeightliftingManagment.Moduls.Settings.ViewModels.Tests
{
    public class ApplicationSettingsViewModelTests
    {

        [Fact]
        public void WhenRemoveItemCommandExecute_ThenBonusPointCollectionNotContainsBonusPoints()
        {
            AutoMocker mocker = new();
            var sut = mocker.CreateInstance<ApplicationSettingsViewModel>();

            BonusPoints bonusPoints = new(15, 45);
            sut.BonusPointsCollection.Add(bonusPoints);

            sut.RemoveItemCommand.Execute(bonusPoints);
            
            sut.BonusPointsCollection.Count(x => x.Age == bonusPoints.Age && x.Point == bonusPoints.Point).Should().Be(0);
        }

        [Fact]
        public void WhenAddNewBonusPointCommandExecute_ThenBonusPointCollectionContainsNewBonusPoints()
        {
            AutoMocker mocker = new();
            var sut = mocker.CreateInstance<ApplicationSettingsViewModel>();

            BonusPoints bonusPoints = new(15, 45);
            sut.Age = bonusPoints.Age;
            sut.Point = bonusPoints.Point;

            sut.AddNewBonusPointCommand.Execute();

            sut.BonusPointsCollection.Count(x => x.Age == bonusPoints.Age && x.Point == bonusPoints.Point).Should().Be(1);
        }

        [Fact]
        public void WhenAddNewBonusPointCommandExecute_ThenAgeAndPointIsDefault()
        {
            AutoMocker mocker = new();
            var sut = mocker.CreateInstance<ApplicationSettingsViewModel>();

            BonusPoints bonusPoints = new(15, 45);
            sut.Age = bonusPoints.Age;
            sut.Point = bonusPoints.Point;

            sut.AddNewBonusPointCommand.Execute();

            sut.Age.Should().Be(default);
            sut.Point.Should().Be(default);
        }


        [Fact]
        public void WhenEditItemCommandExecute_ThenIsEditingBonusPointIsTrue()
        {
            AutoMocker mocker = new();
            var sut = mocker.CreateInstance<ApplicationSettingsViewModel>();
            List<bool> propertyValues = new();
            sut.PropertyChanged += Sut_PropertyChanged;
            void Sut_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(ApplicationSettingsViewModel.IsEditingBonusPoint))
                {
                    propertyValues.Add(sut.IsEditingBonusPoint);
                }
            }

            BonusPoints bonusPoints = new();
            sut.EditItemCommand.Execute(bonusPoints);

            propertyValues[0].Should().BeTrue();
            propertyValues[0].Should().Be(sut.IsEditingBonusPoint);
            propertyValues.Contains(sut.IsEditingBonusPoint).Should().BeTrue();
        }

        [Fact]
        public void WhenEditItemCommandExecute_ThenAgeIsSet()
        {
            AutoMocker mocker = new();
            var sut = mocker.CreateInstance<ApplicationSettingsViewModel>();
            List<int> propertyValues = new();
            sut.PropertyChanged += Sut_PropertyChanged;
            void Sut_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(ApplicationSettingsViewModel.Age))
                {
                    propertyValues.Add(sut.Age);
                }
            }

            BonusPoints bonusPoints = new(15, 20);
            sut.EditItemCommand.Execute(bonusPoints);

            propertyValues[0].Should().Be(bonusPoints.Age);
            propertyValues.Contains(sut.Age).Should().BeTrue();
        }

        [Fact]
        public void WhenEditItemCommandExecute_ThenPointIsSet()
        {
            AutoMocker mocker = new();
            var sut = mocker.CreateInstance<ApplicationSettingsViewModel>();
            List<int> propertyValues = new();
            sut.PropertyChanged += Sut_PropertyChanged;
            void Sut_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(ApplicationSettingsViewModel.Point))
                {
                    propertyValues.Add(sut.Point);
                }
            }

            BonusPoints bonusPoints = new(15, 20);
            sut.EditItemCommand.Execute(bonusPoints);

            propertyValues[0].Should().Be(bonusPoints.Point);
            propertyValues.Contains(sut.Point).Should().BeTrue();
        }
    }
}