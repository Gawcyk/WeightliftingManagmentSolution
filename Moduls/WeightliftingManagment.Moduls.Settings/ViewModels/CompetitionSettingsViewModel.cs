using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Regions;

using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;

namespace WeightliftingManagment.Moduls.Settings.ViewModels
{
    public class CompetitionSettingsViewModel : RegionViewModelBase
    {
        #region Fields

        private string? _name;
        private DateTime? _date;
        private string? _organizer;
        private string? _site;
        private string? _city;
        private bool _isMasters;
        private bool _isUseYearOnly;
        private bool _isInitialTotalWeightRule;
        private string? _federationName;
        private string? _federationWebSite;
        private string? _federationEmail;
        private string? _federationAddress;

        #endregion


        public CompetitionSettingsViewModel(IRegionManager regionManager) : base(regionManager)
        {
            Date = DateTime.UtcNow.Date;
        }

        #region Property

        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public DateTime? Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        public string? Organizer
        {
            get => _organizer;
            set => SetProperty(ref _organizer, value);
        }

        public string? Site
        {
            get => _site;
            set => SetProperty(ref _site, value);
        }

        public string? City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public bool IsMasters
        {
            get => _isMasters;
            set => SetProperty(ref _isMasters, value);
        }


        public bool IsUseYearOnly
        {
            get => _isUseYearOnly;
            set => SetProperty(ref _isUseYearOnly, value);
        }


        public bool IsInitialTotalWeightRule
        {
            get => _isInitialTotalWeightRule;
            set => SetProperty(ref _isInitialTotalWeightRule, value);
        }


        public string? FederationName
        {
            get => _federationName;
            set => SetProperty(ref _federationName, value);
        }


        public string? FederationAddress
        {
            get => _federationAddress;
            set => SetProperty(ref _federationAddress, value);
        }


        public string? FederationEmail
        {
            get => _federationEmail;
            set => SetProperty(ref _federationEmail, value);
        }


        public string? FederationWebSite
        {
            get => _federationWebSite;
            set => SetProperty(ref _federationWebSite, value);
        }

        #endregion
    }
}
