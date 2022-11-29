
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

using Prism.Commands;
using Prism.Services.Dialogs;

using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.Dialogs;
using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;
using WeightliftingManagment.Domain.Model;
using WeightliftingManagment.Localization.LocalizationModel;

namespace WeightliftingManagment.Moduls.ParticipantBodyWeight.ViewModels
{
    public class ParticipantUIViewModel : DialogViewModelBase
    {

        #region Fields

        private DialogType _dialogType;
        private readonly ISinclaireCoefficientService _sinclaireCoefficient;
        private readonly LocalizationService _localizationService;
        private int _participantId;
        private int _startNumber;
        private string _lastName = string.Empty;
        private string _firstName = string.Empty;
        private string _club = string.Empty;
        private double _bodyWeight;
        private int _yearOfBirth;
        private Gender _gender = Gender.Undefine;
        private Attempt _snatch = new();
        private Attempt _cleanJerk = new();
        private int _entryTotal;
        private Category _category = new();
        private string _group = string.Empty;
        private string _licenseNumber = string.Empty;
        private ObservableCollection<Category> _categories = new();
        private Participant _participant = new();

        #endregion

        public ParticipantUIViewModel(ISinclaireCoefficientService sinclaireCoefficient, LocalizationService localizationService)
        {
            Snatch = new Attempt();
            CleanJerk = new Attempt();
            Category = new Category();
            _sinclaireCoefficient = sinclaireCoefficient;
            _localizationService = localizationService;
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

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value, () => ValidateString(value));
        }
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value, () => ValidateString(value));
        }

        public string Club
        {
            get => _club;
            set => SetProperty(ref _club, value, () => ValidateString(value));
        }

        public double BodyWeight
        {
            get => _bodyWeight;
            set => SetProperty(ref _bodyWeight, value, () => ValidateDouble(value));
        }

        public int YearOfBirth
        {
            get => _yearOfBirth;
            set => SetProperty(ref _yearOfBirth, value, () => ValidateInt(value));
        }

        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value, () => ValidateGender(value));
        }

        public Attempt Snatch
        {
            get => _snatch;
            set => SetProperty(ref _snatch, value, () => ValidateAttempt(value));
        }

        public Attempt CleanJerk
        {
            get => _cleanJerk;
            set => SetProperty(ref _cleanJerk, value, () => ValidateAttempt(value));
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

        public string Group
        {
            get => _group;
            set => SetProperty(ref _group, value, () => ValidateString(value));
        }

        public string LicenseNumber
        {
            get => _licenseNumber;
            set => SetProperty(ref _licenseNumber, value);
        }

        #endregion Property

        private DelegateCommand _okCommand;
        public DelegateCommand OkCommand => _okCommand ??= new DelegateCommand(ExecuteOk, CanExecuteOk).ObservesProperty(() => IsValid);

        private bool CanExecuteOk() => !IsValid;

        private void ExecuteOk()
        {
            if (_dialogType == DialogType.AddParticipant)
            {
                _participant = Participant.CreateBuilder()
                    .WithStartNumber(0)
                    .WithFirstAndLastName(FirstName, LastName)
                    .WithClub(Club)
                    .WithBodyWeight(BodyWeight)
                    .WithYearOfBirth(YearOfBirth)
                    .WithGender(Gender)
                    .WithSnatch(Snatch)
                    .WithCleanJerk(CleanJerk)
                    .WithGroup(Group)
                    .WithSinclaireCoefficient(_sinclaireCoefficient.Count(BodyWeight, Gender))
                    .WithLicenseNumber(LicenseNumber)
                    .WithCategory(Category)
                    .Build();
            }
            else if (_dialogType == DialogType.EditParticipant)
            {
                UpdateParticipantFromProperty();
            }

            var param = new DialogParameters {
                { "Participant", _participant }
            };
            RaiseRequestClose(ButtonResult.OK, param);
        }



        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ??= new DelegateCommand(ExecuteClose);
        private void ExecuteClose() => RaiseRequestClose(ButtonResult.Cancel);

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _dialogType = parameters.GetValue<DialogType>("DialogType");
            Title = _dialogType switch {
                DialogType.AddParticipant => _localizationService.GetValue("AddParticipant"),
                DialogType.EditParticipant => _localizationService.GetValue("EditParticipant"),
                _ => "Weightlifting Managment"
            };
            var participant = parameters.GetValue<Participant>("Participant");
            UpdatePropertyFromParticipant(participant);
        }

        private void UpdateParticipantFromProperty()
        {
            _participant.PersonalData.FirstName = FirstName;
            _participant.PersonalData.LastName = LastName;
            _participant.Club = Club;
            _participant.BodyWeight = BodyWeight;
            _participant.YearOfBirth = YearOfBirth;
            _participant.Gender = Gender;
            _participant.Snatchs[0] = Snatch;
            _participant.CleanJerks[0] = CleanJerk;
            _participant.Group = Group;
            _participant.LicenseNumber = LicenseNumber;
            _participant.Category = Category;
        }

        private void UpdatePropertyFromParticipant(Participant participant)
        {
            if (participant is not null)
            {
                _participant = participant;
                FirstName = participant.PersonalData.FirstName;
                LastName = participant.PersonalData.LastName;
                Club = participant.Club;
                BodyWeight = participant.BodyWeight;
                YearOfBirth = participant.YearOfBirth;
                Gender = participant.Gender;
                Snatch = participant.Snatchs[0];
                CleanJerk = participant.CleanJerks[0];
                Group = participant.Group;
                LicenseNumber = participant.LicenseNumber;
                Category = participant.Category;
            }
        }

        private bool _isValid = false;
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        private void ValidateString(string value, [CallerMemberName] string? propertyName = null)
        {
            Validate(string.IsNullOrEmpty(value), $"{propertyName} is null or empty", propertyName);
            IsValid = HasErrors;
        }

        private void ValidateDouble(double value, [CallerMemberName] string? propertyName = null)
        {
            Validate(value <= 0, $"{propertyName}  must be greater than 0", propertyName);
            IsValid = HasErrors;
        }

        private void ValidateInt(int value, [CallerMemberName] string? propertyName = null)
        {
            Validate(value <= 0, $"{propertyName}  must be greater than 0", propertyName);
            IsValid = HasErrors;
        }

        private void ValidateGender(Gender value, [CallerMemberName] string? propertyName = null)
        {
            Validate(value == Gender.Undefine, $"{propertyName}  must be set", propertyName);
            IsValid = HasErrors;
        }

        private void ValidateAttempt(Attempt value, [CallerMemberName] string? propertyName = null)
        {
            Validate(!value.StatusIsDeclared(), $"{propertyName}  must be set", propertyName);
            IsValid = HasErrors;
        }

        private void InitilizeCollection() => _categories = new ObservableCollection<Category> {
                new Category("M 55"),
                new Category("M 61"),
                new Category("M 67"),
                new Category("M 73"),
                new Category("M 81"),
                new Category("M 89"),
                new Category("M 96"),
                new Category("M 102"),
                new Category("M 109"),
                new Category("M +109")
            };
    }
}
