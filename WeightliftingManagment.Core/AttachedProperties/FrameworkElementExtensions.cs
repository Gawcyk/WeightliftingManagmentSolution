using System.Windows;

namespace WeightliftingManagment.Core.AttachedProperties
{
    public class FrameworkElementExtensions
    {
        private static Dictionary<string, List<FrameworkElement>> _scopes = new();

        public static readonly DependencyProperty WidthShareScopeProperty = DependencyProperty.RegisterAttached(
            "WidthShareScope", typeof(string), typeof(FrameworkElementExtensions), new PropertyMetadata(default(string), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject is not FrameworkElement elem) return;

            if (dependencyPropertyChangedEventArgs.NewValue is not string scope) return;

            if (!_scopes.ContainsKey(scope))
            {
                _scopes.Add(scope, new List<FrameworkElement>());
            }

            if (!_scopes[scope].Contains(elem))
            {
                _scopes[scope].Add(elem);
                elem.SizeChanged += elem_SizeChanged;
            }
        }

        private static void elem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is not FrameworkElement elem) return;

            var scope = GetWidthShareScope(elem);
            ArrangeScope(scope);
        }

        private static void ArrangeScope(string scope)
        {
            if (!_scopes.ContainsKey(scope)) return;

            var list = _scopes[scope];

            var maxWidth = list.Max(elem => elem.ActualWidth);
            list.ForEach(elem => elem.Width = maxWidth);
        }

        public static void SetWidthShareScope(DependencyObject element, string value) => element.SetValue(WidthShareScopeProperty, value);

        public static string GetWidthShareScope(DependencyObject element) => (string)element.GetValue(WidthShareScopeProperty);


    }
}
