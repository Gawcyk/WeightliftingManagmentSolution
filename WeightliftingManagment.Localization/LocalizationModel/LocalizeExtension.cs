using System;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;

namespace WeightliftingManagment.Localization.LocalizationModel
{
    public class LocalizeExtension : MarkupExtension
    {

        #region Properties

        public string Key { get; set; } = string.Empty;

        public Binding? KeySource { get; set; }

        #endregion

        public LocalizeExtension() { }

        public LocalizeExtension(string key)
        {
            Key = key;
        }

        public LocalizeExtension(Binding keySource)
        {
            KeySource = keySource;
        }

        #region Public Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var provideValueTarget = serviceProvider as IProvideValueTarget;
            var targetObject = provideValueTarget?.TargetObject as FrameworkElement;
            var targetProperty = provideValueTarget?.TargetProperty as DependencyProperty;
            var alternativeKey = $"{targetObject?.Name}_{targetProperty?.Name}";

            var multiBinding = new MultiBinding {
                Converter = new LocalizationConverter(Key, alternativeKey),
                NotifyOnSourceUpdated = true
            };

            multiBinding.Bindings.Add(new Binding {
                Source = LocalizationService.Instance,
                Path = new PropertyPath("CurrentCulture")
            });

            if (KeySource != null)
            {
                multiBinding.Bindings.Add(KeySource);
            }

            return multiBinding.ProvideValue(serviceProvider);
        }

        #endregion
    }
}
