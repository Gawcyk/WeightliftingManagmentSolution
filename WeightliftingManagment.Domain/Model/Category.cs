using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Domain.Model
{
    public class Category : BindableObject
    {
        #region Fields
        
        private string _name;
        private bool _isActive;
        private CompetitionType _competitionType;
        private Gender _gender;
        private int _minAge;
        private int _maxAge;
        private ObservableCollection<string> _bodyWeightCategories;
        
        #endregion
        
        public Category()
        {
            Name = string.Empty;
        }

        public Category(string name)
        {
            Name = name;
        }

        #region Property

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        public CompetitionType CompetitionType
        {
            get => _competitionType;
            set => SetProperty(ref _competitionType, value);
        }

        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public int MinAge
        {
            get => _minAge;
            set => SetProperty(ref _minAge, value);
        }

        public int MaxAge
        {
            get => _maxAge;
            set => SetProperty(ref _maxAge, value);
        }

        public ObservableCollection<string> BodyWeightCategories
        {
            get => _bodyWeightCategories;
            set => SetProperty(ref _bodyWeightCategories, value);
        }

        #endregion
    }
}
