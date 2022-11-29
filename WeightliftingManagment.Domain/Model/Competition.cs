using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Domain.Model
{
    public class Competition : BindableObject
    {
        #region Fields
        private string? _name;
        private DateTime? _date;
        private string? _organizer;
        private string? _site;
        private string? _city;
        #endregion

        public Competition()
        {

        }
        public Competition(string? name, DateTime? date, string? organizer, string? site, string? city)
        {
            Name = name;
            Date = date;
            Organizer = organizer;
            Site = site;
            City = city;
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

        #endregion
    }
}
