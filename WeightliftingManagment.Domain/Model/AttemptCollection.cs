
using WeightliftingManagment.MvvmSupport.BindableBase;
using WeightliftingManagment.MvvmSupport.Collections;

namespace WeightliftingManagment.Domain.Model
{
    public class AttemptCollection : BindableObject
    {
        private readonly FullyObservableCollection<Attempt> _collections = new();

        public AttemptCollection()
        {
            _collections = new FullyObservableCollection<Attempt> {
                new Attempt(),
                new Attempt(),
                new Attempt()
            };
        }

        public AttemptCollection(int value)
        {
            _collections = new FullyObservableCollection<Attempt> {
                new Attempt(value),
                new Attempt(),
                new Attempt()
            };
        }

        public AttemptCollection(int value, AttemptStatus status)
        {
            _collections = new FullyObservableCollection<Attempt> {
                new Attempt(value,status),
                new Attempt(),
                new Attempt()
            };
        }

        public AttemptCollection(Attempt attempt)
        {
            _collections = new FullyObservableCollection<Attempt> {
                attempt,
                new Attempt(),
                new Attempt()
            };
        }

        public AttemptCollection(Attempt attempt1, Attempt attempt2, Attempt attempt3)
        {
            _collections = new FullyObservableCollection<Attempt> {
                attempt1,
                attempt2,
                attempt3
            };
        }

        public AttemptCollection(List<Attempt> attempts)
        {
            _collections = new FullyObservableCollection<Attempt>(attempts);
        }

        public Attempt this[int i]
        {
            get => _collections[i];
            set => _collections[i] = value;
        }

        public FullyObservableCollection<Attempt> Collection => _collections;

        public Attempt First
        {
            get => _collections[0];
            set {
                _collections[0] = value;
                OnPropertyChanged();
            }
        }

        public Attempt Second
        {
            get => _collections[1];
            set {
                _collections[1] = value;
                OnPropertyChanged();
            }
        }
        public Attempt Third
        {
            get => _collections[2];
            set {
                _collections[2] = value;
                OnPropertyChanged();
            }
        }

    }
}
