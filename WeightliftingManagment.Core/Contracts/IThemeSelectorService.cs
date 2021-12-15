
using WeightliftingManagment.Core.Models;

namespace WeightliftingManagment.Core.Contracts
{
    public interface IThemeSelectorService
    {
        void InitializeTheme();

        void SetTheme(AppTheme theme);
        void SetAccent(string accent);

        AppTheme GetCurrentTheme();
        IEnumerable<string> GetAccents();
        string GetCurrentAccent();
    }
}
