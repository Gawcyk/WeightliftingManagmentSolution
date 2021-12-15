using System.Windows;

namespace WeightliftingManagment.Core.BaseType
{
    public abstract class BaseAttachedProperty<Parent, Property>
       where Parent : BaseAttachedProperty<Parent, Property>, new()
    {
        #region Public Event

        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (dependencyObject, dependencyPropertyChangedEventArgs) => { };

        #endregion

        #region Public Properties

        public static Parent Instance { get; } = new Parent();

        #endregion

        #region Attached Properties definiotions

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Property), typeof(BaseAttachedProperty<Parent, Property>), new FrameworkPropertyMetadata() { BindsTwoWayByDefault = true, PropertyChangedCallback = new PropertyChangedCallback(OnValuePropertyChanged) });

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Instance.OnValueChanged(d, e);

            Instance.ValueChanged(d, e);
        }

        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);

        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);

        #endregion

        #region Event Methods

        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
        }
        #endregion
    }
}
