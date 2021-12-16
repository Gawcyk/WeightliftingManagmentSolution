using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Prism.Modularity;

using WeightliftingManagment.Core.BaseType;
using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;

using WeightliftingManagmentApp.Test.Helpers;
using Xunit;

namespace WeightliftingManagmentApp.Test.Convention
{
    public class NamingConventions
    {
        [Fact]
        public void Each_interfaces_name_should_starts_capital_I()
        {
            var interfaces = ConventionHelpers.GetInterfaces();

            interfaces.Should().NotBeEmpty();

            var interfaces_not_starts_capital_I = interfaces.Where(x => x.Name.StartsWith("I") == false);
            
            interfaces_not_starts_capital_I.Should().BeEmpty();
        }

        [Fact]
        public void ViewModel_name_should_end_with_ViewModel()
        {
            var viewModels = ConventionHelpers.GetClasses().Where(x => x.IsAssignableTo<ViewModelBase>() && !x.Name.EndsWith("ViewModelBase")).ToList();

            viewModels.Should().NotBeEmpty();

            foreach (var viewmodel in viewModels)
            {
                viewmodel.Name.Should().EndWith("ViewModel");
            }
        }


        [Fact]
        public void PrismModuls_name_should_end_with_Module()
        {
            var moduls = ConventionHelpers.GetClasses().Where(x => x.IsAssignableTo<IModule>()).ToList();
            moduls.Should().NotBeEmpty();

            foreach (var modul in moduls)
            {
                modul.Name.Should().EndWith("Module");
            }
        }

        [Fact]
        public void ValueConverter_name_should_end_with_ValueConverter()
        {
            var converters = ConventionHelpers.GetClasses().Where(x => x.IsAssignableTo<BaseMarkupValueConverter>()).ToList();
            converters.Should().NotBeEmpty();
            foreach (var converter in converters)
            {
                converter.Name.Should().EndWith("ValueConverter");
            }
        }
    }
}
