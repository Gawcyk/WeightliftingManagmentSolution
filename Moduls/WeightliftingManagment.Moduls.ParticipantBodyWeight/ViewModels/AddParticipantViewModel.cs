using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;
using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Moduls.ParticipantBodyWeight.ViewModels
{
    public  class AddParticipantViewModel : ViewModelBase
    {
        public AddParticipantViewModel()
        {
            Snatch = new Attempt();
            CleanJerk = new Attempt();
            Category = new Category();
        }
        #region Property

        private int _participantId;
        public int ParticipantId
        {
            get => _participantId;
            set => SetProperty(ref _participantId, value);
        }

        private int _startNumber;
        public int StartNumber
        {
            get => _startNumber;
            set => SetProperty(ref _startNumber, value);
        }

        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        private string? _firstName;
        public string? FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string? _club;
        public string? Club
        {
            get => _club;
            set => SetProperty(ref _club, value);
        }

        private double _bodyWeight;
        public double BodyWeight
        {
            get => _bodyWeight;
            set => SetProperty(ref _bodyWeight, value);
        }

        private int _yearOfBirth;
        public int YearOfBirth
        {
            get => _yearOfBirth;
            set => SetProperty(ref _yearOfBirth, value);
        }

        private Gender _gender;
        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        private Attempt _snatch;
        public Attempt Snatch
        {
            get => _snatch;
            set => SetProperty(ref _snatch, value);
        }

        private Attempt _cleanJerk;
        public Attempt CleanJerk
        {
            get => _cleanJerk;
            set => SetProperty(ref _cleanJerk, value);
        }

        private int _entryTotal;
        public int EntryTotal
        {
            get => _entryTotal;
            set => SetProperty(ref _entryTotal, value);
        }

        private Category _category;
        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private string? _group;
        public string? Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }

        private string? _licenseNumber;
        public string? LicenseNumber
        {
            get => _licenseNumber;
            set => SetProperty(ref _licenseNumber, value);
        }

        #endregion Property
    }
}
