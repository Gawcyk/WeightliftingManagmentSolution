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

        private string _name= string.Empty;
        public string Name
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
        private string _organizer = string.Empty;
        public string Organizer
        {
            get => _organizer;
            set => SetProperty(ref _organizer, value);
        }

        private string _site = string.Empty;
        public string Site
        {
            get => _site;
            set => SetProperty(ref _site, value);
        }

        private string _city = string.Empty;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        #endregion
    }
}
