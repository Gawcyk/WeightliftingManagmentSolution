using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Domain.Model
{
    public class Participant : BindableObject
    {
        public Participant()
        {
            Snatchs = new Attempt[] {
                new Attempt(),
                new Attempt(),
                new Attempt()
            };
            CleanJerks = new Attempt[] {
                new Attempt(),
                new Attempt(),
                new Attempt()
            };
        }
        #region Property

        private int _participantId;
        public int ParticipantId
        {
            get => _participantId;
            set => SetProperty(ref _participantId, value);
        }

        private int _startNumber;
        public int StartNumber
        {
            get => _startNumber;
            set => SetProperty(ref _startNumber, value);
        }

        private string? _nameAndSurname;
        public string? NameAndSurname
        {
            get => _nameAndSurname;
            set => SetProperty(ref _nameAndSurname, value);
        }

        private string? _club;
        public string? Club
        {
            get => _club;
            set => SetProperty(ref _club, value);
        }

        private double _bodyWeight;
        public double BodyWeight
        {
            get => _bodyWeight;
            set => SetProperty(ref _bodyWeight, value);
        }

        private int _yearOfBirth;
        public int YearOfBirth
        {
            get => _yearOfBirth;
            set => SetProperty(ref _yearOfBirth, value);
        }

        private Gender _gender;
        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        private Attempt[] _snatchs = new Attempt[3];
        public Attempt[] Snatchs
        {
            get => _snatchs;
            set => SetProperty(ref _snatchs, value);
        }

        private Attempt[] _cleanJerks = new Attempt[3];
        public Attempt[] CleanJerks
        {
            get => _cleanJerks;
            set => SetProperty(ref _cleanJerks, value);
        }

        private int _bonusPoint;
        public int BonusPoint
        {
            get => _bonusPoint;
            set => SetProperty(ref _bonusPoint, value);
        }

        private int _total;
        public int Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        private string? _group;
        public string? Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }

        private int _maxOfCleanJerk;
        public int MaxOfCleanJerk
        {
            get => _maxOfCleanJerk;
            set => SetProperty(ref _maxOfCleanJerk, value);
        }

        private int _maxOfSnatch;
        public int MaxOfSnatch
        {
            get => _maxOfSnatch;
            set => SetProperty(ref _maxOfSnatch, value);
        }

        private bool _next;
        public bool Next
        {
            get => _next;
            set => SetProperty(ref _next, value);
        }

        private int _numberMaxOfCleanJerk;
        public int NumberMaxOfCleanJerk
        {
            get => _numberMaxOfCleanJerk;
            set => SetProperty(ref _numberMaxOfCleanJerk, value);
        }

        private int _numberMaxOfSnatch;
        public int NumberMaxOfSnatch
        {
            get => _numberMaxOfSnatch;
            set => SetProperty(ref _numberMaxOfSnatch, value);
        }

        private bool _comesUp;
        public bool ComesUp
        {
            get => _comesUp;
            set => SetProperty(ref _comesUp, value);
        }

        private double _points;
        public double Points
        {
            get => _points;
            set => SetProperty(ref _points, value);
        }

        private double _sinclairCoefficients;
        public double SinclairCoefficients
        {
            get => _sinclairCoefficients;
            set => SetProperty(ref _sinclairCoefficients, value);
        }

        private string? _position;
        public string? Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private int _promiseTotal;
        public int PromiseTotal
        {
            get => _promiseTotal;
            set => SetProperty(ref _promiseTotal, value);
        }

        private double _promisePoint;
        public double PromisePoint
        {
            get => _promisePoint;
            set => SetProperty(ref _promisePoint, value);
        }

        private string? _licenseNumber;
        public string? LicenseNumber
        {
            get => _licenseNumber;
            set => SetProperty(ref _licenseNumber, value);
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

        private static void UpdateResignationArray(Attempt[] array, int numerPodejscia)
        {
            for (var i = numerPodejscia; i <= array.Length; i++)
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
        public bool IsSnatchDeclared() => Snatchs.Any(item=>item.StatusIsDeclared());

        /// <summary>
        /// Wyznaczamy maksymalne podejcie w rwaniu
        /// </summary>
        public void DesignateMaxOfSnatch()
        {
            var max = Snatchs.MaxBy(item => item.AttemptToInt());
            MaxOfSnatch = max.Value;
            NumberMaxOfSnatch = Snatchs.ToList().IndexOf(max) + 1;
            Snatchs.FirstOrDefault(item => item.StatusIsMax())?.SetStatus(AttemptStatus.GoodLift);
            max.SetStatus(AttemptStatus.Max);
        }

        /// <summary>
        /// Wyznaczamy maksymalne podejscie w podrzucie
        /// </summary>
        public void DesignateMaxOfCleanJerk()
        {
            var max = CleanJerks.MaxBy(item => item.AttemptToInt());
            MaxOfCleanJerk = max.Value;
            NumberMaxOfCleanJerk = CleanJerks.ToList().IndexOf(max) + 1;
            CleanJerks.FirstOrDefault(item => item.StatusIsMax())?.SetStatus(AttemptStatus.GoodLift);
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
            var result = isrwanie ? Snatchs.First(item => item.StatusIsDeclared()) : CleanJerks.First(item => item.StatusIsDeclared());
            return result;
        }

        /// <summary>
        /// Wyznaczam numer podejscia w boju
        /// </summary>
        /// <param name="isrwanie">Flaga mówiąca o tym czy szukamy w rwanie</param>
        /// <returns>Numer podjeścia w boju</returns>
        public int GetNumberOfComesUp(bool isrwanie)
        {
            var index = isrwanie ? Snatchs.ToList().IndexOf(Snatchs.First(item => item.StatusIsComesUp())) + 1 : CleanJerks.ToList().IndexOf(CleanJerks.First(item => item.StatusIsComesUp())) + 1;
            return index;
        }

        /// <summary>
        /// Numer podejscia w danym boju
        /// </summary>
        /// <param name="isrwanie">Flaga mówiąca o tym  który bój</param>
        /// <returns></returns>
        public int GetNumberOfDeclared(bool isrwanie)
        {
            var index = isrwanie ? Snatchs.ToList().IndexOf(Snatchs.First(item => item.StatusIsDeclared())) + 1 : CleanJerks.ToList().IndexOf(CleanJerks.First(item => item.StatusIsDeclared())) + 1;
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
            Snatchs.ToList().ForEach(item => ClearAttempt(item));
            CleanJerks.ToList().ForEach(item => ClearAttempt(item));
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
                ? Snatchs.First(item => item.StatusIsComesUp()).StatusIsComesUp()
                : CleanJerks.First(item => item.StatusIsComesUp()).StatusIsComesUp();

        public int GetComesUpValue()
        {
            var snatch = Snatchs.FirstOrDefault(item => item.StatusIsComesUp());
            if (snatch != null) return snatch.Value;
            var cleanJerk = CleanJerks.FirstOrDefault(item => item.StatusIsComesUp());
            if (cleanJerk != null) return cleanJerk.Value;
            return 0;
        }

        public string GetComesUpPodejscie()
        {
            if (Snatchs[0].StatusIsComesUp())
            {
                return "Rwanie : Podejscie 1";
            }
            else if (Snatchs[1].StatusIsComesUp())
            {
                return "Rwanie : Podejscie 2";
            }
            else if (Snatchs[2].StatusIsComesUp())
            {
                return "Rwanie : Podejscie 3";
            }
            else if (CleanJerks[0].StatusIsComesUp())
            {
                return "Podrzut : Podejscie 1";
            }
            else if (CleanJerks[1].StatusIsComesUp())
            {
                return "Podrzut : Podejscie 2";
            }
            else if (CleanJerks[2].StatusIsComesUp())
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
            var snatch = Snatchs.FirstOrDefault(item => item.StatusIsNext());
            if (snatch != null) return snatch.Value;
            var cleanJerk = CleanJerks.FirstOrDefault(item => item.StatusIsNext());
            if (cleanJerk != null) return cleanJerk.Value;
            return 0;
        }

        public string GetNextPodejscie()
        {
            if (Snatchs[0].StatusIsNext())
            {
                return "Rwanie : Podejscie 1";
            }
            else if (Snatchs[1].StatusIsNext())
            {
                return "Rwanie : Podejscie 2";
            }
            else if (Snatchs[2].StatusIsNext())
            {
                return "Rwanie : Podejscie 3";
            }
            else if (CleanJerks[0].StatusIsNext())
            {
                return "Podrzut : Podejscie 1";
            }
            else if (CleanJerks[1].StatusIsNext())
            {
                return "Podrzut : Podejscie 2";
            }
            else if (CleanJerks[2].StatusIsNext())
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
                ? Snatchs.First(item => item.StatusIsNext()).StatusIsNext()
                : CleanJerks.First(item => item.StatusIsNext()).StatusIsNext();

        #endregion

    }
}
