namespace WeightliftingManagment.Domain.Model
{
    public enum AttemptStatus : int
    {
        NoDeclared = -3,
        Declared = 0,
        GoodLift = 1,
        NoLift = -1,
        Resignation = -2,
        Max = 4,
        ComesUp = 2,
        Next = 3
    }
}