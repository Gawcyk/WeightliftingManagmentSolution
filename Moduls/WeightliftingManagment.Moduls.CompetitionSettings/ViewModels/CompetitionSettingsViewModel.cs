using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Regions;

using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;

namespace WeightliftingManagment.Moduls.CompetitionSettings.ViewModels
{
    public class CompetitionSettingsViewModel : RegionViewModelBase
    {
        public CompetitionSettingsViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Date = DateTime.UtcNow.Date;
        }

        #region Property

        private string? _name;
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private DateTime? _date;
        public DateTime? Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        private string? _organizer;
        public string? Organizer
        {
            get => _organizer;
            set => SetProperty(ref _organizer, value);
        }

        private string? _site;
        public string? Site
        {
            get => _site;
            set => SetProperty(ref _site, value);
        }

        private string? _city;
        public string? City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private bool? _isMasters;
        public bool? IsMasters
        {
            get => _isMasters;
            set => SetProperty(ref _isMasters, value);
        }

        private bool? _isUseYearOnly;

        public bool? IsUseYearOnly
        {
            get => _isUseYearOnly;
            set => SetProperty(ref _isUseYearOnly, value);
        }

        private bool? _isInitialTotalWeightRule;

        public bool? IsInitialTotalWeightRule
        {
            get => _isInitialTotalWeightRule;
            set => SetProperty(ref _isInitialTotalWeightRule, value);
        }

        private string? _federationName;

        public string? FederationName
        {
            get => _federationName;
            set => SetProperty(ref _federationName, value);
        }

        private string? _federationAddress;

        public string? FederationAddress
        {
            get => _federationAddress;
            set => SetProperty(ref _federationAddress, value);
        }

        private string? _federationEmail;

        public string? FederationEmail
        {
            get => _federationEmail;
            set => SetProperty(ref _federationEmail, value);
        }

        private string? _federationWebSite;

        public string? FederationWebSite
        {
            get => _federationWebSite;
            set => SetProperty(ref _federationWebSite, value);
        }

        #endregion
    }
}
