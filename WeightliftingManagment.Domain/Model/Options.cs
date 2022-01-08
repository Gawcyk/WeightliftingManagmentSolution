using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Domain.Model
{
    public class Options : BindableObject
    {
        public Options()
        {

        }
        public Options(bool? isMasters, bool? isUseYearOnly, bool? isInitialTotalWeightRule)
        {
            IsMasters = isMasters;
            IsUseYearOnly = isUseYearOnly;
            IsInitialTotalWeightRule = isInitialTotalWeightRule;
        }

        #region Fields
        private bool? _isMasters;
        private bool? _isUseYearOnly;
        private bool? _isInitialTotalWeightRule;
        #endregion

        #region Property

        public bool? IsMasters
        {
            get => _isMasters;
            set => SetProperty(ref _isMasters, value);
        }

        public bool? IsUseYearOnly
        {
            get => _isUseYearOnly;
            set => SetProperty(ref _isUseYearOnly, value);
        }

        public bool? IsInitialTotalWeightRule
        {
            get => _isInitialTotalWeightRule;
            set => SetProperty(ref _isInitialTotalWeightRule, value);
        }

        #endregion
    }
}
