using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Domain.Builder
{
    public class ParticipantBuilder
    {
        #region Fields
        private int? _startNumber = null;
        private PersonalData? _personalData = null;
        private string? _club = null;
        private double? _bodyWeight = null;
        private int? _yearOfBirth = null;
        private Gender? _gender = null;
        private Attempt? _snatch = null;
        private Attempt? _cleanJerk = null;
        private string? _group = null;
        private double? _sinclairCoefficient = null;
        private string? _licenseNumber = null;
        private Category? _category = null;
        #endregion


        public Participant Build()
        {
            var model = new Participant();
            if (_startNumber is not null)
                model.StartNumber = _startNumber.Value;
            if (_personalData is not null)
                model.PersonalData = _personalData;
            if (_club is not null)
                model.Club = _club;
            if (_bodyWeight is not null)
                model.BodyWeight = _bodyWeight.Value;
            if (_yearOfBirth is not null)
                model.YearOfBirth = _yearOfBirth.Value;
            if(_gender is not null)
                model.Gender = _gender.Value;
            if (_snatch is not null)
                model.Snatchs = new AttemptCollection(_snatch);
            if (_cleanJerk is not null)
                model.CleanJerks = new AttemptCollection (_cleanJerk);
            if (_group is not null)
                model.Group = _group;
            if (_licenseNumber is not null)
                model.LicenseNumber = _licenseNumber;
            if(_sinclairCoefficient is not null)
                model.SinclairCoefficient = _sinclairCoefficient.Value;
            if(_category is not null)
                model.Category = _category;


            return model;
        }

        public ParticipantBuilder WithStartNumber(int startNumber)
        {
            _startNumber = startNumber;
            return this;
        }

        public ParticipantBuilder WithFirstAndLastName(string firstName, string lastName)
        {
            _personalData = PersonalData.FromString($"{firstName} {lastName}");
            return this;
        }

        public ParticipantBuilder WithClub(string club)
        {
            _club = club;
            return this;
        }

        public ParticipantBuilder WithBodyWeight(double bodyWeight)
        {
            _bodyWeight = bodyWeight;
            return this;
        }

        public ParticipantBuilder WithYearOfBirth(int yearOfBirth)
        {
            _yearOfBirth = yearOfBirth;
            return this;
        }

        public ParticipantBuilder WithGender(Gender gender)
        {
            _gender = gender;
            return this;
        }

        public ParticipantBuilder WithSnatch(Attempt snatch)
        {
            _snatch = snatch;
            return this;
        }
        public ParticipantBuilder WithSnatch(int snatch)
        {
            _snatch = Attempt.CreateBuilder().WithValue(snatch).Build();
            return this;
        }

        public ParticipantBuilder WithCleanJerk(Attempt cleanJerk)
        {
            _cleanJerk = cleanJerk;
            return this;
        }
        public ParticipantBuilder WithCleanJerk(int cleanJerk)
        {
            _cleanJerk = Attempt.CreateBuilder().WithValue(cleanJerk).Build();
            return this;
        }

        public ParticipantBuilder WithGroup(string group)
        {
            _group = group;
            return this;
        }

        public ParticipantBuilder WithSinclaireCoefficient(double sinclaireCoefficient)
        {
            _sinclairCoefficient = sinclaireCoefficient;
            return this;
        }

        public ParticipantBuilder WithLicenseNumber(string licenseNumber)
        {
            _licenseNumber = licenseNumber;
            return this;
        }

        public ParticipantBuilder WithCategory(Category category)
        {
            _category = category;
            return this;
        }
    }
}
