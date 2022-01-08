using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Domain.Model
{
    public class TurnamentSettings : BindableObject
    {
        public TurnamentSettings()
        {

        }

        public TurnamentSettings(Competition competition, Federation federation, Options options)
        {
            Competition = competition;
            Federation = federation;
            Options = options;
        }

        private Competition _competition = new();
        public Competition Competition
        {
            get => _competition;
            set => SetProperty(ref _competition, value);
        }

        private Options _options = new();
        public Options Options
        {
            get => _options;
            set => SetProperty(ref _options, value);
        }

        private Federation _federation =new();
        public Federation Federation
        {
            get => _federation;
            set => SetProperty(ref _federation, value);
        }
    }
}
