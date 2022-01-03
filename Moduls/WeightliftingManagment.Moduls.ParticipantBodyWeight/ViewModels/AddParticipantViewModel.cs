
using System.Collections.ObjectModel;

using Prism.Commands;
using Prism.Services.Dialogs;

using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;
using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Moduls.ParticipantBodyWeight.ViewModels
{
    public class AddParticipantViewModel : DialogViewModelBase
    {
        #region Fields

        private readonly ISinclaireCoefficientService _sinclaireCoefficient;
        private int _participantId;
        private int _startNumber;
        private string? _lastName;
        private string? _firstName;
        private string? _club;
        private double _bodyWeight;
        private int _yearOfBirth;
        private Gender _gender;
        private Attempt _snatch;
        private Attempt _cleanJerk;
        private int _entryTotal;
        private Category _category;
        private string? _group;
        private string? _licenseNumber;
        private ObservableCollection<Category> _categories;

        #endregion

        public AddParticipantViewModel(ISinclaireCoefficientService sinclaireCoefficient)
        {
            Snatch = new Attempt();
            CleanJerk = new Attempt();
            Category = new Category();
            _sinclaireCoefficient = sinclaireCoefficient;
            InitilizeCollection();
        }

        #region Property

        public ObservableCollection<Category> Categories => _categories;

        public int ParticipantId
        {
            get => _participantId;
            set => SetProperty(ref _participantId, value);
        }

        public int StartNumber
        {
            get => _startNumber;
            set => SetProperty(ref _startNumber, value);
        }

        public string? LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        public string? FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string? Club
        {
            get => _club;
            set => SetProperty(ref _club, value);
        }

        public double BodyWeight
        {
            get => _bodyWeight;
            set => SetProperty(ref _bodyWeight, value);
        }

        public int YearOfBirth
        {
            get => _yearOfBirth;
            set => SetProperty(ref _yearOfBirth, value);
        }

        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public Attempt Snatch
        {
            get => _snatch;
            set => SetProperty(ref _snatch, value);
        }

        public Attempt CleanJerk
        {
            get => _cleanJerk;
            set => SetProperty(ref _cleanJerk, value);
        }

        public int EntryTotal
        {
            get => _entryTotal;
            set => SetProperty(ref _entryTotal, value);
        }

        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public string? Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }

        public string? LicenseNumber
        {
            get => _licenseNumber;
            set => SetProperty(ref _licenseNumber, value);
        }

        #endregion Property

        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand => _addCommand ??= new DelegateCommand(ExecuteAdd);
        private void ExecuteAdd()
        {
            var newParticipant = new Participant(0, $"{FirstName} {LastName}", Club, BodyWeight, YearOfBirth, Gender, Snatch, CleanJerk, Group, _sinclaireCoefficient.Count(BodyWeight, Gender), LicenseNumber,Category);
            var param = new DialogParameters {
                { "NewParticipant", newParticipant }
            };
            RaiseRequestClose(ButtonResult.OK, param);
        }

        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ??= new DelegateCommand(ExecuteClose);
        private void ExecuteClose() => RaiseRequestClose(ButtonResult.Cancel);

        public override void OnDialogOpened(IDialogParameters parameters) => Title = parameters.GetValue<string>("Title");

        private void InitilizeCollection() => _categories = new ObservableCollection<Category> {
                new Category("M 55", 55, 0),
                new Category("M 61", 61, 55.1),
                new Category("M 67", 67, 61.1),
                new Category("M 73", 73, 67.1),
                new Category("M 81", 81, 73.1),
                new Category("M 89", 89, 81.1),
                new Category("M 96", 96, 89.1),
                new Category("M 102", 102, 96.1),
                new Category("M 109", 109, 102.1),
                new Category("M +109", double.PositiveInfinity, 109.1)
            };
    }
}
