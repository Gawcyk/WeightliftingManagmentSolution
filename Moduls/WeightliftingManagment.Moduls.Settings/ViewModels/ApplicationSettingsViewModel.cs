using System.Collections.ObjectModel;

using Prism.Commands;
using Prism.Regions;

using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;
using WeightliftingManagment.Domain.Model;
using WeightliftingManagment.Moduls.Settings.Models;

namespace WeightliftingManagment.Moduls.Settings.ViewModels
{
    public class ApplicationSettingsViewModel : RegionViewModelBase
    {
        #region Fields

        private double _womenParamA;
        private double _womenParamB;
        private double _menParamA;
        private double _menParamB;
        private int _age = default;
        private int _point = default;
        private int _editingBonusPoint = default;
        private ObservableCollection<BonusPoints> _bonusPoints;
        private ObservableCollection<Category> _category;
        private bool _isEditingBonusPoint;

        #endregion

        public ApplicationSettingsViewModel(IRegionManager regionManager) : base(regionManager)
        {
            BonusPointsCollection = LoadBonusPoints();
            IsEditingBonusPoint = false;
        }

        private static ObservableCollection<BonusPoints> LoadBonusPoints()
        {
            var bonusPoints = new ObservableCollection<BonusPoints> {
                new (15, 30),
                new (18, 25),
                new (20, 10)
            };

            return bonusPoints;
        }

        #region Property
        public bool IsEditingBonusPoint
        {
            get => _isEditingBonusPoint;
            set => SetProperty(ref _isEditingBonusPoint, value);
        }

        public double WomenParamA { get => _womenParamA; set => SetProperty(ref _womenParamA, value); }


        public double WomenParamB { get => _womenParamB; set => SetProperty(ref _womenParamB, value); }


        public double MenParamA { get => _menParamA; set => SetProperty(ref _menParamA, value); }


        public double MenParamB { get => _menParamB; set => SetProperty(ref _menParamB, value); }


        public int Age { get => _age; set => SetProperty(ref _age, value); }


        public int Point { get => _point; set => SetProperty(ref _point, value); }

        public ObservableCollection<BonusPoints> BonusPointsCollection { get => _bonusPoints; set => SetProperty(ref _bonusPoints, value); }

        public ObservableCollection<Category> CategoryCollection { get => _category; set => SetProperty(ref _category, value); }

        #endregion

        #region Commands

        private DelegateCommand _updateBonusPointsCommand;
        public DelegateCommand UpdateBonusPointsCommand => _updateBonusPointsCommand ??= new DelegateCommand(ExecuteUpdateBonusPoints);
        private void ExecuteUpdateBonusPoints()
        {
            BonusPointsCollection[_editingBonusPoint].Age = Age;
            BonusPointsCollection[_editingBonusPoint].Point = Point;
            ResetValueBonusPoints();
            IsEditingBonusPoint = false;
        }

        private void ResetValueBonusPoints()
        {
            Age = default;
            Point = default;
        }

        private DelegateCommand<BonusPoints> _editItemCommand;
        public DelegateCommand<BonusPoints> EditItemCommand => _editItemCommand ??= new DelegateCommand<BonusPoints>(ExecuteEditItem);
        private void ExecuteEditItem(BonusPoints editItem)
        {
            IsEditingBonusPoint = true;
            _editingBonusPoint = BonusPointsCollection.IndexOf(editItem);
            Age = editItem.Age;
            Point = editItem.Point;
        }

        private DelegateCommand<BonusPoints> _removeItemCommand;
        public DelegateCommand<BonusPoints> RemoveItemCommand => _removeItemCommand ??= new DelegateCommand<BonusPoints>(ExecuteRemoveItem);
        private void ExecuteRemoveItem(BonusPoints removeBonusPoints) => BonusPointsCollection.Remove(removeBonusPoints);

        private DelegateCommand _addNewBonusPointCommand;
        public DelegateCommand AddNewBonusPointCommand => _addNewBonusPointCommand ??= new DelegateCommand(ExecutedAddNewBonusPoint, CanExecuteAddNewBonusPoint).ObservesProperty(() => Age).ObservesProperty(() => Point);

        private bool CanExecuteAddNewBonusPoint() => Age != default && Point != default;

        private void ExecutedAddNewBonusPoint()
        {
            BonusPointsCollection.Add(new BonusPoints(Age, Point));
            ResetValueBonusPoints();
        }

        #endregion

    }
}
