

using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Domain.Model
{
    public class Attempt : BindableObject
    {
        /// <summary>
        /// <see cref="Value"/> set 0, <see cref="Status"/> set Niepodane
        /// </summary>
        public Attempt()
        {
            Value = 0;
            Status = AttemptStatus.NoDeclared;
        }

        /// <summary>
        /// <see cref="Value"/> set value, <see cref="Status"/> set Annons
        /// </summary>
        /// <param name="value">value of the Attempt</param>
        public Attempt(int value)
        {
            Value = value;
            Status = AttemptStatus.Declared;
        }
        /// <summary>
        /// <see cref="Value"/> set value, <see cref="Status"/> set result
        /// </summary>
        /// <param name="value">value of the Attempt</param>
        /// <param name="status">result of the Attempt</param>
        public Attempt(int value, AttemptStatus status)
        {
            Value = value;
            Status = status;
        }

        #region Property
        private int _value;
        public int Value 
        { 
            get => _value; 
            set => SetProperty(ref _value, value); 
        }


        private AttemptStatus _status;

        public AttemptStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        #endregion Property

        #region Methods

        public void SetValue(int value) => Value = value;

        public void SetStatus(AttemptStatus AttemptStatus) => Status = AttemptStatus;

        public int AttemptToInt() => Status switch {
            AttemptStatus.Declared => 0,
            AttemptStatus.Resignation => 0,
            AttemptStatus.GoodLift => Value,
            AttemptStatus.Max => Value,
            AttemptStatus.NoLift => Value * -1,
            _ => 0
        };

        private bool StatusIs(AttemptStatus AttemptStatus) => Status == AttemptStatus;
        public bool StatusIsGoodLift() => StatusIs(AttemptStatus.GoodLift);
        public bool StatusIsNoLift() => StatusIs(AttemptStatus.NoLift);
        public bool StatusIsDeclared() => StatusIs(AttemptStatus.Declared);
        public bool StatusIsNoDeclared() => StatusIs(AttemptStatus.NoDeclared);
        public bool StatusIsComesUp() => StatusIs(AttemptStatus.ComesUp);
        public bool StatusIsNext() => StatusIs(AttemptStatus.Next);
        public bool StatusIsComesUpOrNext() => StatusIsNext() || StatusIsComesUp();
        public bool StatusIsDeclaredOrComesUpOrNext() => StatusIsDeclared() || StatusIsComesUpOrNext();
        public bool StatusIsMax() => StatusIs(AttemptStatus.Max);
        public bool StatusIsResignation() => StatusIs(AttemptStatus.Resignation);

        #endregion Methods
    }
}