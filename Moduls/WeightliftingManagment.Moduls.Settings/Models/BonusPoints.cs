
using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Moduls.Settings.Models
{
    public class BonusPoints : BindableObject
    {
        #region Fields

        private int _age;
        private int _point;

        public BonusPoints()
        {

        }
        public BonusPoints(int age, int point)
        {
            Age = age;
            Point = point;
        }

        #endregion

        #region Property

        public int Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        public int Point
        {
            get => _point;
            set => SetProperty(ref _point, value);
        }

        #endregion
    }
}
