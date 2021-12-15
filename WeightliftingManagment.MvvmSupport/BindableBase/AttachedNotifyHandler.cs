using System;
using System.ComponentModel;

namespace WeightliftingManagment.MvvmSupport.BindableBase
{
    internal sealed class AttachedNotifyHandler : IDisposable
    {
        private readonly string _propertyName;
        private readonly BindableObject _currentObject;
        private readonly INotifyPropertyChanged _attachedObject;

        public AttachedNotifyHandler(string propertyName, BindableObject currentObject, INotifyPropertyChanged attachedObject)
        {
            _propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            _currentObject = currentObject ?? throw new ArgumentNullException(nameof(currentObject));
            _attachedObject = attachedObject ?? throw new ArgumentNullException(nameof(attachedObject));

            attachedObject.PropertyChanged += TrackedObjectOnPropertyChanged;
        }

        public void Dispose() => _attachedObject.PropertyChanged -= TrackedObjectOnPropertyChanged;

        private void TrackedObjectOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) => _currentObject.OnPropertyChanged(_propertyName);
    }
}
