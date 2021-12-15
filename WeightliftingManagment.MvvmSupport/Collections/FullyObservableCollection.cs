using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightliftingManagment.MvvmSupport.Collections
{
    public class FullyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property is changed within an item.
        /// </summary>
        public event EventHandler<ItemPropertyChangedEventArgs> ItemPropertyChanged;

        public FullyObservableCollection() : base()
        {
        }

        public FullyObservableCollection(List<T> list) : base(list)
        {
            ObserveAll();
        }

        public FullyObservableCollection(IEnumerable<T> enumerable) : base(enumerable)
        {
            ObserveAll();
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged += ChildPropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged -= ChildPropertyChanged;
                }
            }

            base.OnCollectionChanged(e);
        }

        protected void OnItemPropertyChanged(ItemPropertyChangedEventArgs e) => ItemPropertyChanged?.Invoke(this, e);

        protected void OnItemPropertyChanged(int index, PropertyChangedEventArgs e) => OnItemPropertyChanged(new ItemPropertyChangedEventArgs(index, e));

        public bool AddIfNotContains(T item)
        {
            var result = false;
            if (!Contains(item))
            {
                Add(item);
                result = true;
            }
            return result;
        }

        protected override void ClearItems()
        {
            foreach (var item in Items)
            {
                item.PropertyChanged -= ChildPropertyChanged;
            }

            base.ClearItems();
        }

        private void ObserveAll()
        {
            foreach (var item in Items)
            {
                item.PropertyChanged += ChildPropertyChanged;
            }
        }

        private void ChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var i = IndexOf((T)sender);

            if (i < 0)
            {
                throw new ArgumentException("Received property notification from item not in collection");
            }
            OnItemPropertyChanged(i, e);
        }
    }
}
