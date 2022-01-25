using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WeightliftingManagment.MvvmSupport.BindableBase
{
    public class ErrorsBindableObject : BindableObject, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new();
        public bool HasErrors => _propertyErrors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors([CallerMemberName] string propertyName = null) => _propertyErrors.GetValueOrDefault(propertyName, null);

        public bool HasErrorsForPropertyName([CallerMemberName] string propertyName = null) => _propertyErrors.ContainsKey(propertyName);

        public bool Validate(bool conditional, string errMessage, [CallerMemberName] string propertyName = null)
        {
            ClearErrors(propertyName);
            if (conditional)
            {
                AddError(errMessage, propertyName);
            }
            return HasErrorsForPropertyName(propertyName);
        }

        public void AddError(string errorMessage, [CallerMemberName] string propertyName = null)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }

            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        public void ClearErrors([CallerMemberName] string propertyName = null)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        public void ClearAllErrors()
        {
            foreach (var errors in _propertyErrors)
            {
                var propertyName = errors.Key;
                ClearErrors(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName) => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName) => GetErrors(propertyName);
    }
}
