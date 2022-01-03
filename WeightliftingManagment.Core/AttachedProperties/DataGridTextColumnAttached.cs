using System.Windows;

using WeightliftingManagment.Core.BaseType;
using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Core.AttachedProperties
{
    public class DataGridTextColumnStatusAttached:BaseAttachedProperty<DataGridTextColumnStatusAttached,AttemptStatus>
    {
        
    }


    public class DataGridTextColumnAttached
    {
        public static AttemptStatus GetStatus(DependencyObject obj) => (AttemptStatus)obj.GetValue(StatusProperty);

        public static void SetStatus(DependencyObject obj, AttemptStatus value) => obj.SetValue(StatusProperty, value);

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.RegisterAttached("Status", typeof(AttemptStatus), typeof(DataGridTextColumnAttached), new PropertyMetadata(AttemptStatus.NoDeclared,AttemptStatusCallback));

        private static void AttemptStatusCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
