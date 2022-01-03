using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using WeightliftingManagment.Domain.Model;

namespace WeightliftingManagment.Core.Controls
{
    public class DataGridAttemptColumn : DataGridTextColumn
    {
        public AttemptStatus AttemptBinding
        {
            get => (AttemptStatus)GetValue(AttemptBindingProperty);
            set => SetValue(AttemptBindingProperty, value);
        }

        // Using a DependencyProperty as the backing store for AttemptBinding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AttemptBindingProperty =
            DependencyProperty.Register("AttemptBinding", typeof(AttemptStatus), typeof(DataGridAttemptColumn), new PropertyMetadata(AttemptStatus.NoDeclared));


    }
}
