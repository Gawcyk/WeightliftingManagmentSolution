using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using WeightliftingManagment.MvvmSupport.Dispose;

namespace WeightliftingManagment.MvvmSupport.BindableBase
{
    /// <summary>
    /// Notifies subscribers that a property in this instance is changing or has changed.
    /// </summary>
    public abstract class BindableObject : Disposable, INotifyPropertyChanged //, INotifyPropertyChanging
    {
        #region Public Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { PropertyChanged += value; }
            remove { PropertyChanged -= value; }
        }

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        // event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
        // {
        //     add { this.PropertyChanging += value; }
        //     remove { this.PropertyChanging -= value; }
        // }

        #endregion

        #region Private Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        private event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        // private event PropertyChangingEventHandler PropertyChanging;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the when property changed observable event. Occurs when a property value changes.
        /// </summary>
        /// <value>
        /// The when property changed observable event.
        /// </value>
        public IObservable<string> WhenPropertyChanged
        {
            get {
                ThrowIfDisposed();

                return Observable
                    .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                        h => PropertyChanged += h,
                        h => PropertyChanged -= h)
                    .Select(x => x.EventArgs.PropertyName);
            }
        }

        /// <summary>
        /// Gets the when property changing observable event. Occurs when a property value is changing.
        /// </summary>
        /// <value>
        /// The when property changing observable event.
        /// </value>
        // public IObservable<EventPattern<PropertyChangingEventArgs>> WhenPropertyChanging
        // {
        //     get
        //     {
        //         return Observable
        //             .FromEventPattern<PropertyChangingEventHandler, PropertyChangingEventArgs>(
        //                 h => this.PropertyChanging += h,
        //                 h => this.PropertyChanging -= h)
        //             .AsObservable();
        //     }
        // }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Assert(
                string.IsNullOrEmpty(propertyName) ||
                (GetType().GetRuntimeProperty(propertyName) != null),
                "Check that the property name exists for this instance.");

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyNames">The property names.</param>
        public void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }

            foreach (var propertyName in propertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Raises the PropertyChanging event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public virtual void OnPropertyChanging([CallerMemberName] string propertyName = null) => Debug.Assert(
                string.IsNullOrEmpty(propertyName) ||
                (GetType().GetRuntimeProperty(propertyName) != null),
                "Check that the property name exists for this instance.");
        // PropertyChangingEventHandler eventHandler = this.PropertyChanging;// if (eventHandler != null)// {//     eventHandler(this, new PropertyChangingEventArgs(propertyName));// }

        /// <summary>
        /// Raises the PropertyChanging event.
        /// </summary>
        /// <param name="propertyNames">The property names.</param>
        public void OnPropertyChanging(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }

            foreach (var propertyName in propertyNames)
            {
                OnPropertyChanging(propertyName);
            }
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<TProp>(ref TProp currentValue, TProp newValue, [CallerMemberName] string propertyName = null)
        {
            ThrowIfDisposed();

            if (!Equals(currentValue, newValue))
            {
                OnPropertyChanging(propertyName);
                currentValue = newValue;
                OnPropertyChanged(propertyName);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="action">Action that is called after the property value has been changed.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<TProp>(ref TProp currentValue, TProp newValue, Action action, [CallerMemberName] string propertyName = null)
        {
            ThrowIfDisposed();

            if (!Equals(currentValue, newValue))
            {
                OnPropertyChanging(propertyName);
                currentValue = newValue;
                OnPropertyChanged(propertyName);
                action?.Invoke();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="beforeAction">Action that is called before the property value has been changed.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<TProp>(ref TProp currentValue, TProp newValue, Func<bool> beforeAction, [CallerMemberName] string propertyName = null)
        {
            ThrowIfDisposed();

            if (beforeAction())
            {
                return false;
            }

            if (!Equals(currentValue, newValue))
            {
                OnPropertyChanging(propertyName);
                currentValue = newValue;
                OnPropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="beforeAction">Action that is called before the property value has been changed.</param>
        /// <param name="afterAction">Action that is called after the property value has been changed.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<TProp>(ref TProp currentValue, TProp newValue, Func<bool> beforeAction, Action afterAction, [CallerMemberName] string propertyName = null)
        {
            ThrowIfDisposed();

            if (beforeAction())
            {
                return false;
            }

            if (!Equals(currentValue, newValue))
            {
                OnPropertyChanging(propertyName);
                currentValue = newValue;
                OnPropertyChanged(propertyName);
                afterAction?.Invoke();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="currentValue">The current value of the property.</param>
        /// <param name="newValue">The new value of the property.</param>
        /// <param name="propertyNames">The names of all properties changed.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty<TProp>(
            ref TProp currentValue,
            TProp newValue,
            params string[] propertyNames)
        {
            ThrowIfDisposed();

            if (!Equals(currentValue, newValue))
            {
                OnPropertyChanging(propertyNames);
                currentValue = newValue;
                OnPropertyChanged(propertyNames);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <param name="equal">A function which returns <c>true</c> if the property value has changed, otherwise <c>false</c>.</param>
        /// <param name="action">The action where the property is set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty(
            Func<bool> equal,
            Action action,
            [CallerMemberName] string propertyName = null)
        {
            ThrowIfDisposed();

            if (equal())
            {
                return false;
            }

            OnPropertyChanging(propertyName);
            OnPropertyChanged(propertyName);
            action?.Invoke();

            return true;
        }

        /// <summary>
        /// Sets the value of the property to the specified value if it has changed.
        /// </summary>
        /// <param name="equal">A function which returns <c>true</c> if the property value has changed, otherwise <c>false</c>.</param>
        /// <param name="action">The action where the property is set.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <returns><c>true</c> if the property was changed, otherwise <c>false</c>.</returns>
        protected bool SetProperty(
            Func<bool> equal,
            Action action,
            params string[] propertyNames)
        {
            ThrowIfDisposed();

            if (equal())
            {
                return false;
            }

            OnPropertyChanging(propertyNames);
            OnPropertyChanged(propertyNames);
            action?.Invoke();

            return true;
        }

        #endregion

        private readonly Dictionary<string, AttachedNotifyHandler> _attachedHandlers = new();

        public void AttachPropertyChanged(INotifyPropertyChanged notifyPropertyChanged,
            [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            DetachCurrentPropertyChanged(propertyName);
            if (notifyPropertyChanged != null)
            {
                _attachedHandlers.Add(propertyName, new AttachedNotifyHandler(propertyName, this, notifyPropertyChanged));
            }
        }

        protected void DetachCurrentPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (_attachedHandlers.TryGetValue(propertyName, out var handler))
            {
                handler.Dispose();
                _attachedHandlers.Remove(propertyName);
            }
        }
    }
}
