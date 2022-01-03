
using WeightliftingManagment.Core.Models;
using WeightliftingManagment.Core.Models.Config;

namespace WeightliftingManagment.Core.Contracts
{
    public interface IThemeSelectorService
    {
        void InitializeTheme();

        void UpdateInternalConfig(ThemeConfig themeConfig);

        void SetTheme(AppTheme theme);
        void SetAccent(string accent);

        AppTheme GetCurrentTheme();
        IEnumerable<string> GetAccents();
        string GetCurrentAccent();
    }
}
