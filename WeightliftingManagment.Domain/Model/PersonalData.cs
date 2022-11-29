using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Domain.Model
{
    public class PersonalData : BindableObject
    {
        #region Ctor

        public PersonalData()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        public PersonalData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        #endregion

        #region Fields

        private string _firstName = string.Empty;
        private string _lastName = string.Empty;

        #endregion

        #region Property

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value, () =>  OnPropertyChanged(nameof(Display)));
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value, () => OnPropertyChanged(nameof(Display)));
        }

        public string Display => ToString();

        #endregion

        #region Methods

        public override string ToString() => $"{FirstName} {LastName}";

        public static PersonalData FromString(string personality)
        {
            
            if (!personality.Contains(' ')) return new();
            
            var firstName = personality.Split(" ")[0];
            var lastName = personality.Split(" ")[1];
            
            return new (firstName, lastName);
        }

        #endregion
    }
}
