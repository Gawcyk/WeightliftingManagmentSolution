using WeightliftingManagment.MvvmSupport.BindableBase;
using WeightliftingManagment.MvvmSupport.Collections;

namespace WeightliftingManagment.Domain.Model
{
    public class Participant : BindableObject
    {
        /// <summary>
        /// New Participant
        /// </summary>
        public Participant()
        {
            ParticipantId = Guid.NewGuid();
            Snatchs = new AttemptCollection();
            CleanJerks = new AttemptCollection();
            Category = new Category();
        }

        /// <summary>
        /// New Participant
        /// </summary>
        /// <param name="startNumber">Start number of the participant</param>
        /// <param name="nameAndSurname">Name and Surname participant</param>
        /// <param name="club">Name of Club the participant</param>
        /// <param name="bodyWeight">Weight of Body participant</param>
        /// <param name="yearOfBirth">The year of Birth participant</param>
        /// <param name="gender">The gender of the participant</param>
        /// <param name="snatch">The first attempt in Snatchs type of<see cref="Attempt"/></param>
        /// <param name="cleanJerk">The first attempt in Clean&Jerk type of<see cref="Attempt"/></param>
        /// <param name="group">Start group participant</param>
        /// <param name="sinclairCoefficients">The Sinclaire Coefficient of the participant</param>
        /// <param name="licenseNumber">License number participant</param>
        /// <param name="category">Category participant</param>
        public Participant(int startNumber, string nameAndSurname, string club, double bodyWeight, int yearOfBirth, Gender gender, Attempt snatch, Attempt cleanJerk, string group, double sinclairCoefficients, string licenseNumber,Category category)
        {
            ParticipantId = Guid.NewGuid();
            StartNumber = startNumber;
            PersonalData = PersonalData.FromString(nameAndSurname);
            Club = club;
            BodyWeight = bodyWeight;
            YearOfBirth = yearOfBirth;
            Gender = gender;
            Snatchs = new AttemptCollection(snatch);
            CleanJerks = new AttemptCollection(cleanJerk);
            Group = group;
            SinclairCoefficients = sinclairCoefficients;
            LicenseNumber = licenseNumber;
            Category = category;
        }


        /// <summary>
        /// New Participant
        /// </summary>
        /// <param name="startNumber">Start number of the participant</param>
        /// <param name="nameAndSurname">Name and Surname participant</param>
        /// <param name="club">Name of Club the participant</param>
        /// <param name="bodyWeight">Weight of Body participant</param>
        /// <param name="yearOfBirth">The year of Birth participant</param>
        /// <param name="gender">The gender of the participant</param>
        /// <param name="snatch">The first attempt in Snatchs type of<see cref="int"/></param>
        /// <param name="cleanJerk">The first attempt in Clean&Jerk type of<see cref="int"/></param>
        /// <param name="group">Start group participant</param>
        /// <param name="sinclairCoefficients">The Sinclaire Coefficient of the participant</param>
        /// <param name="licenseNumber">License number participant</param>
        /// <param name="category">Category participant</param>
        public Participant(int startNumber, string nameAndSurname, string club, double bodyWeight, int yearOfBirth, Gender gender, int snatch, int cleanJerk, string group, double sinclairCoefficients, string licenseNumber, Category category)
        {
            ParticipantId = Guid.NewGuid();
            StartNumber = startNumber;
            PersonalData = PersonalData.FromString(nameAndSurname);
            Club = club;
            BodyWeight = bodyWeight;
            YearOfBirth = yearOfBirth;
            Gender = gender;
            Snatchs = new AttemptCollection(snatch);
            CleanJerks = new AttemptCollection(cleanJerk);
            Group = group;
            SinclairCoefficients = sinclairCoefficients;
            LicenseNumber = licenseNumber;
            Category = category;
        }


        public Participant(Guid participantId, int startNumber, string nameAndSurname, string club, double bodyWeight, int yearOfBirth, Gender gender, AttemptCollection snatchs, AttemptCollection cleanJerks, int bonusPoint, int total, string group, int maxOfCleanJerk, int maxOfSnatch, bool next, int numberMaxOfCleanJerk, int numberMaxOfSnatch, bool comesUp, double points, double sinclairCoefficients, string position, int promiseTotal, double promisePoint, string licenseNumber, Category  category)
        {
            ParticipantId = participantId;
            StartNumber = startNumber;
            PersonalData = PersonalData.FromString(nameAndSurname);
            Club = club;
            BodyWeight = bodyWeight;
            YearOfBirth = yearOfBirth;
            Gender = gender;
            Snatchs = snatchs;
            CleanJerks = cleanJerks;
            BonusPoint = bonusPoint;
            Total = total;
            Group = group;
            MaxOfCleanJerk = maxOfCleanJerk;
            MaxOfSnatch = maxOfSnatch;
            Next = next;
            NumberMaxOfCleanJerk = numberMaxOfCleanJerk;
            NumberMaxOfSnatch = numberMaxOfSnatch;
            ComesUp = comesUp;
            Points = points;
            SinclairCoefficients = sinclairCoefficients;
            Position = position;
            PromiseTotal = promiseTotal;
            PromisePoint = promisePoint;
            LicenseNumber = licenseNumber;
            Category = category;
        }

        #region Fields

        private Guid _participantId;
        private int _startNumber;
        private PersonalData _personalData = new();
        private string _club = string.Empty;
        private double _bodyWeight;
        private int _yearOfBirth;
        private Gender _gender;
        private AttemptCollection _snatchs = new();
        private AttemptCollection _cleanJerks = new();
        private int _bonusPoint;
        private int _total;
        private string _group = string.Empty;
        private int _maxOfCleanJerk;
        private int _maxOfSnatch;
        private bool _next;
        private int _numberMaxOfCleanJerk;
        private int _numberMaxOfSnatch;
        private bool _comesUp;
        private double _points;
        private double _sinclairCoefficients;
        private string _position = string.Empty;
        private int _promiseTotal;
        private double _promisePoint;
        private string _licenseNumber = string.Empty;
        private Category _category = new();

        #endregion

        #region Property

        public Guid ParticipantId
        {
            get => _participantId;
            set => SetProperty(ref _participantId, value);
        }

        public int StartNumber
        {
            get => _startNumber;
            set => SetProperty(ref _startNumber, value);
        }

        public PersonalData PersonalData
        {
            get => _personalData;
            set => SetProperty(ref _personalData, value);
        }

        public string Club
        {
            get => _club;
            set => SetProperty(ref _club, value);
        }

        public double BodyWeight
        {
            get => _bodyWeight;
            set => SetProperty(ref _bodyWeight, value);
        }

        public int YearOfBirth
        {
            get => _yearOfBirth;
            set => SetProperty(ref _yearOfBirth, value);
        }

        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public AttemptCollection Snatchs
        {
            get => _snatchs;
            set => SetProperty(ref _snatchs, value);
        }

        public AttemptCollection CleanJerks
        {
            get => _cleanJerks;
            set => SetProperty(ref _cleanJerks, value);
        }

        public int BonusPoint
        {
            get => _bonusPoint;
            set => SetProperty(ref _bonusPoint, value);
        }

        public int Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        public string Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }

        public int MaxOfCleanJerk
        {
            get => _maxOfCleanJerk;
            set => SetProperty(ref _maxOfCleanJerk, value);
        }

        public int MaxOfSnatch
        {
            get => _maxOfSnatch;
            set => SetProperty(ref _maxOfSnatch, value);
        }

        public bool Next
        {
            get => _next;
            set => SetProperty(ref _next, value);
        }

        public int NumberMaxOfCleanJerk
        {
            get => _numberMaxOfCleanJerk;
            set => SetProperty(ref _numberMaxOfCleanJerk, value);
        }

        public int NumberMaxOfSnatch
        {
            get => _numberMaxOfSnatch;
            set => SetProperty(ref _numberMaxOfSnatch, value);
        }

        public bool ComesUp
        {
            get => _comesUp;
            set => SetProperty(ref _comesUp, value);
        }

        public double Points
        {
            get => _points;
            set => SetProperty(ref _points, value);
        }

        public double SinclairCoefficients
        {
            get => _sinclairCoefficients;
            set => SetProperty(ref _sinclairCoefficients, value);
        }

        public string Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        public int PromiseTotal
        {
            get => _promiseTotal;
            set => SetProperty(ref _promiseTotal, value);
        }

        public double PromisePoint
        {
            get => _promisePoint;
            set => SetProperty(ref _promisePoint, value);
        }

        public string LicenseNumber
        {
            get => _licenseNumber;
            set => SetProperty(ref _licenseNumber, value);
        }

        public Category Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        #endregion Property

        #region Methods

        public void CountPromiseTotal()
        {
            var annonsSnatchWeight = GetDeclaredWeight(true);
            var promiseMaxOfSnatch = annonsSnatchWeight == 0 ? MaxOfSnatch : annonsSnatchWeight;
            var annonsCleanJerkWeight = GetDeclaredWeight(false);
            var promiseMaxOfCleanJerk = annonsCleanJerkWeight == 0 ? MaxOfCleanJerk : annonsCleanJerkWeight;
            var promiseTotal = promiseMaxOfSnatch + promiseMaxOfCleanJerk;
            PromiseTotal = promiseTotal == 0 ? Total : promiseTotal;
        }

        public void CountPromisePoint()
        {
            var promisePoint = (PromiseTotal * SinclairCoefficients) + BonusPoint;
            PromisePoint = promisePoint == 0 ? Points : promisePoint;
        }

        /// <summary>
        /// Ustawia resultat w 3 podejsciach
        /// </summary>
        /// <param name="result">Resultat <see cref="AttemptStatus"/> do ustawienia</param>
        /// <param name="flagRwanie">Flaga typu Bool jeżeli True - Rwanie, False - Podrzut</param>
        public void SetAttempt(AttemptStatus result, bool flagRwanie)
        {
            var attempt = flagRwanie ? Snatchs[2] : CleanJerks[2];
            if (attempt.StatusIsDeclaredOrComesUpOrNext())
            {
                attempt.SetStatus(result);
            }
        }

        /// <summary>
        /// Ustawia żadany resultat w podejściu <paramref name="numerPodejcia1"/>
        /// </summary>
        /// <param name="numerPodejcia1">Numer podejscia aktualnego</param>
        /// <param name="result">Resultat <see cref="AttemptStatus"/> do ustawienia</param>
        /// <param name="flagRwanie">Flaga typu Bool jeżeli True - Rwanie, False - Podrzut</param>
        public void SetAttempt(int numerPodejcia, AttemptStatus result, bool flagRwanie)
        {
            if (numerPodejcia > 1) return;
            Attempt oldAtt, newAtt;
            if (flagRwanie)
            {
                oldAtt = Snatchs[numerPodejcia];
                newAtt = Snatchs[numerPodejcia + 1];
            }
            else
            {
                oldAtt = CleanJerks[numerPodejcia];
                newAtt = CleanJerks[numerPodejcia+1];
            }

            if (oldAtt.StatusIsDeclaredOrComesUpOrNext())
            {
                oldAtt.SetStatus(result);
                newAtt.SetValue(oldAtt.StatusIsGoodLift() ? oldAtt.Value + 1 : oldAtt.Value);
                newAtt.SetStatus(AttemptStatus.Declared);
            }
        }

        /// <summary>
        /// Ustawia Rezygnacje w podejsciu i kolejnych
        /// </summary>
        /// <param name="numerPodejscia">Numer podejscia w którym ustawiam rezygnacje</param>
        /// <param name="flagRwanie">Czy jest to rwanie</param>
        public void SetAttemptResignation(int numerPodejscia, bool flagRwanie)
        {
            if (flagRwanie)
            {
                UpdateResignationArray(Snatchs, numerPodejscia);
                return;
            }
            UpdateResignationArray(CleanJerks, numerPodejscia);
        }

        private static void UpdateResignationArray(AttemptCollection array, int numerPodejscia)
        {
            for (var i = numerPodejscia; i <= array.Collection.Count; i++)
            {
                array[i - 1].SetStatus(AttemptStatus.Resignation);
            }
        }

        /// <summary>
        /// Naprawia rok gdy wpisze 2 cyfrowy
        /// </summary>
        public void RepairYear()
        {
            if (YearOfBirth < 100)
            {
                var rok = YearOfBirth < 10 ? int.Parse($"200{YearOfBirth}") : int.Parse($"20{YearOfBirth}");
                YearOfBirth = rok <= DateTime.UtcNow.Year ? rok : int.Parse($"19{YearOfBirth}");
            }
        }

        /// <summary>
        /// Oblicza współczynnik korelacji sinclaira
        /// </summary>
        public void SetSinclairCoefficients(double coefficient) => SinclairCoefficients = coefficient;

        /// <summary>
        /// Oblicza bonifikate punktową według wieku
        /// </summary>
        public void SetBonusPoint(int bonusPoint) => BonusPoint = bonusPoint;

        /// <summary>
        /// Czy dalej są podejscia w rwaniu
        /// </summary>
        /// <returns>Flaga True jeżeli jeszcze są annonse w rwaniu</returns>
        public bool IsSnatchDeclared() => Snatchs.Collection.Any(item=>item.StatusIsDeclared());

        /// <summary>
        /// Wyznaczamy maksymalne podejcie w rwaniu
        /// </summary>
        public void DesignateMaxOfSnatch()
        {
            var max = Snatchs.Collection.MaxBy(item => item.AttemptToInt());
            MaxOfSnatch = max.Value;
            NumberMaxOfSnatch = Snatchs.Collection.ToList().IndexOf(max) + 1;
            Snatchs.Collection.FirstOrDefault(item => item.StatusIsMax())?.SetStatus(AttemptStatus.GoodLift);
            max.SetStatus(AttemptStatus.Max);
        }

        /// <summary>
        /// Wyznaczamy maksymalne podejscie w podrzucie
        /// </summary>
        public void DesignateMaxOfCleanJerk()
        {
            var max = CleanJerks.Collection.MaxBy(item => item.AttemptToInt());
            MaxOfCleanJerk = max.Value;
            NumberMaxOfCleanJerk = CleanJerks.Collection.ToList().IndexOf(max) + 1;
            CleanJerks.Collection.FirstOrDefault(item => item.StatusIsMax())?.SetStatus(AttemptStatus.GoodLift);
            max.SetStatus(AttemptStatus.Max);
        }

        /// <summary>
        /// Oblicza dwuboj zawodnika
        /// </summary>
        public void CountTotal() => Total = MaxOfSnatch > 0 && MaxOfCleanJerk > 0 ? MaxOfSnatch + MaxOfCleanJerk : 0;

        /// <summary>
        /// Oblicza punkty zawodnika
        /// </summary>
        public void CountPoints() => Points = Total > 0 ? (Total * SinclairCoefficients) + BonusPoint : 0;

        /// <summary>
        ///  Pobieram ciężar podejścia anonsowanego
        /// </summary>
        /// <param name="isrwanie">Flaga mówiąca o tym  który bój</param>
        /// <returns>Ciężar podjescia anonsowanego</returns>
        public int GetDeclaredWeight(bool isrwanie) => (GetDeclared(isrwanie)?.Value) ?? 0;

        /// <summary>
        /// Pobieram annonsowane podejscie
        /// </summary>
        /// <param name="isrwanie">Flaga mówiąca o tym  który bój</param>
        /// <returns>Podejscia w którym <see cref="AttemptStatus"/> jest Annosns</returns>
        public Attempt? GetDeclared(bool isrwanie)
        {
            var result = isrwanie ? Snatchs.Collection.First(item => item.StatusIsDeclared()) : CleanJerks.Collection.First(item => item.StatusIsDeclared());
            return result;
        }

        /// <summary>
        /// Wyznaczam numer podejscia w boju
        /// </summary>
        /// <param name="isrwanie">Flaga mówiąca o tym czy szukamy w rwanie</param>
        /// <returns>Numer podjeścia w boju</returns>
        public int GetNumberOfComesUp(bool isrwanie)
        {
            var index = isrwanie ? Snatchs.Collection.ToList().IndexOf(Snatchs.Collection.First(item => item.StatusIsComesUp())) + 1 : CleanJerks.Collection.ToList().IndexOf(CleanJerks.Collection.First(item => item.StatusIsComesUp())) + 1;
            return index;
        }

        /// <summary>
        /// Numer podejscia w danym boju
        /// </summary>
        /// <param name="isrwanie">Flaga mówiąca o tym  który bój</param>
        /// <returns></returns>
        public int GetNumberOfDeclared(bool isrwanie)
        {
            var index = isrwanie ? Snatchs.Collection.ToList().IndexOf(Snatchs.Collection.First(item => item.StatusIsDeclared())) + 1 : CleanJerks.Collection.ToList().IndexOf(CleanJerks.Collection.First(item => item.StatusIsDeclared())) + 1;
            return index;
        }

        /// <summary>
        /// Podejscie o podanym numerze
        /// </summary>
        /// <param name="number">Numer podejscia</param>
        /// <param name="isrwanie">Flaga mówiąca o tym  który bój</param>
        /// <returns></returns>
        public int GetAttemptWeightOfNumber(int number, bool isrwanie) => isrwanie ? Snatchs[number - 1].Value : CleanJerks[number - 1].Value;

        /// <summary>
        /// Czyszczenie Następny i Podchodzi.
        /// </summary>
        public void ClearNextAndComesUp()
        {
            Snatchs.Collection.ToList().ForEach(item => ClearAttempt(item));
            CleanJerks.Collection.ToList().ForEach(item => ClearAttempt(item));
        }

        private static void ClearAttempt(Attempt item)
        {
            if (item.StatusIsComesUpOrNext())
            {
                item.SetStatus(AttemptStatus.Declared);
            }
        }

        /// <summary>
        /// Dodanie ostatniego cięzaru do aktualnego
        /// </summary>
        /// <param name="number">Wskazuje które podejscie</param>
        /// <param name="isSnatch" >Wskazuje który bój</param>
        public void SetAttemptValueFromLastAttemptValue(int number, bool isSnatch)
        {
            switch (number, isSnatch)
            {
                case (0, true):
                case (0, false):
                    throw new ArgumentException("Nie można wykonać operacji dla podanego argumentu", $"{number}");
                case (1, true):
                case (2, true):
                    Snatchs[number].Value += Snatchs[number - 1].Value;
                    break;
                case (1, false):
                case (2, false):
                    CleanJerks[number].Value += CleanJerks[number - 1].Value;
                    break;
                default:
                    throw new ArgumentException("Nie można wykonać operacji dla podanego argumentu", $"{number}");
            }
        }

        /// <summary>
        /// Czyszczenie rezygnacji z podejscia
        /// </summary>
        /// <param name="number">Wskazuje które podejscie</param>
        /// <param name="isSnatch" >Wskazuje który bój</param>
        public void ClearAttemptResignation(int number, bool isSnatch)
        {
            switch (number, isSnatch)
            {
                case (0, true):
                    {
                        if (Snatchs[0].StatusIsResignation())
                        {
                            Snatchs[1].SetStatus(AttemptStatus.NoDeclared);
                            Snatchs[2].SetStatus(AttemptStatus.NoDeclared);
                        }
                        Snatchs[0].SetStatus(AttemptStatus.Declared);
                    };
                    break;
                case (1, true):
                    {
                        if (Snatchs[1].StatusIsResignation())
                        {
                            Snatchs[2].SetStatus(AttemptStatus.NoDeclared);
                        }
                        Snatchs[1].SetStatus(AttemptStatus.Declared);
                    }
                    break;
                case (2, true):
                    Snatchs[2].SetStatus(AttemptStatus.Declared);
                    break;
                case (0, false):
                    {
                        if (CleanJerks[0].StatusIsResignation())
                        {
                            CleanJerks[1].SetStatus(AttemptStatus.NoDeclared);
                            CleanJerks[2].SetStatus(AttemptStatus.NoDeclared);
                        }
                        CleanJerks[0].SetStatus(AttemptStatus.Declared);
                    }
                    break;
                case (1, false):
                    {
                        if (CleanJerks[1].StatusIsResignation())
                        {
                            CleanJerks[2].SetStatus(AttemptStatus.NoDeclared);
                        }
                        CleanJerks[1].SetStatus(AttemptStatus.Declared);
                    }
                    break;
                case (2, false):
                    CleanJerks[2].SetStatus(AttemptStatus.Declared);
                    break;
                default:
                    throw new ArgumentException("Nie można wykonać operacji dla podanego argumentu", $"{number}");
            }
        }

        /// <summary>
        /// Ustawia <see cref="ComesUp"/>
        /// </summary>
        /// <param name="isRwanie">Jeżeli isRwanie jest true to jest rwanie </param>
        public void SetComesUp(bool isRwanie) => ComesUp = isRwanie
                ? Snatchs.Collection.First(item => item.StatusIsComesUp()).StatusIsComesUp()
                : CleanJerks.Collection.First(item => item.StatusIsComesUp()).StatusIsComesUp();

        public int GetComesUpValue()
        {
            var snatch = Snatchs.Collection.FirstOrDefault(item => item.StatusIsComesUp());
            if (snatch != null) return snatch.Value;
            var cleanJerk = CleanJerks.Collection.FirstOrDefault(item => item.StatusIsComesUp());
            if (cleanJerk != null) return cleanJerk.Value;
            return 0;
        }

        public string GetComesUpPodejscie()
        {
            if (Snatchs.First.StatusIsComesUp())
            {
                return "Rwanie : Podejscie 1";
            }
            else if (Snatchs.Second.StatusIsComesUp())
            {
                return "Rwanie : Podejscie 2";
            }
            else if (Snatchs.Third.StatusIsComesUp())
            {
                return "Rwanie : Podejscie 3";
            }
            else if (CleanJerks.First.StatusIsComesUp())
            {
                return "Podrzut : Podejscie 1";
            }
            else if (CleanJerks.Second.StatusIsComesUp())
            {
                return "Podrzut : Podejscie 2";
            }
            else if (CleanJerks.Third.StatusIsComesUp())
            {
                return "Podrzut : Podejscie 3";
            }
            else
            {
                return string.Empty;
            }
        }

        public int GetNextValue()
        {
            var snatch = Snatchs.Collection.FirstOrDefault(item => item.StatusIsNext());
            if (snatch != null) return snatch.Value;
            var cleanJerk = CleanJerks.Collection.FirstOrDefault(item => item.StatusIsNext());
            if (cleanJerk != null) return cleanJerk.Value;
            return 0;
        }

        public string GetNextPodejscie()
        {
            if (Snatchs.First.StatusIsNext())
            {
                return "Rwanie : Podejscie 1";
            }
            else if (Snatchs.Second.StatusIsNext())
            {
                return "Rwanie : Podejscie 2";
            }
            else if (Snatchs.Third.StatusIsNext())
            {
                return "Rwanie : Podejscie 3";
            }
            else if (CleanJerks.First.StatusIsNext())
            {
                return "Podrzut : Podejscie 1";
            }
            else if (CleanJerks.Second.StatusIsNext())
            {
                return "Podrzut : Podejscie 2";
            }
            else if (CleanJerks.Third.StatusIsNext())
            {
                return "Podrzut : Podejscie 3";
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Ustawia <see cref="Next"/>
        /// </summary>
        /// <param name="isRwanie">Jeżeli isRwanie jest true to jest rwanie </param>
        public void SetNext(bool isRwanie) => Next = isRwanie
                ? Snatchs.Collection.First(item => item.StatusIsNext()).StatusIsNext()
                : CleanJerks.Collection.First(item => item.StatusIsNext()).StatusIsNext();

        #endregion

    }
}
