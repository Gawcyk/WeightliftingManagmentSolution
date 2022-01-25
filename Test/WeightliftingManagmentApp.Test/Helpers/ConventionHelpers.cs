using System.Reflection;

using WeightliftingManagment.Core.BaseType;
using WeightliftingManagment.Moduls.Dialogs;
using WeightliftingManagment.Moduls.Judge;
using WeightliftingManagment.Moduls.ParticipantBodyWeight;
using WeightliftingManagment.Moduls.Settings;
using WeightliftingManagment.ViewModels;

namespace WeightliftingManagmentApp.Test.Helpers
{
    public static class ConventionHelpers
    {
        public static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(ShellWindowViewModel).Assembly;
            yield return typeof(BaseMarkupValueConverter).Assembly;
            yield return typeof(SettingsModule).Assembly;
            yield return typeof(DialogsModule).Assembly;
            yield return typeof(JudgeModule).Assembly;
            yield return typeof(ParticipantBodyWeightModule).Assembly;
        }

        public static IEnumerable<Type> GetTypes() => GetAssemblies().SelectMany(x => x.GetTypes());

        public static IEnumerable<Type> GetClasses() => GetTypes().Where(x => x.IsClass);

        public static IEnumerable<Type> GetInterfaces() => GetTypes().Where(x => x.IsInterface);

        public static string Sln_directory()
        {
            var dir = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (dir.EnumerateFiles("*.sln").Any() == false)
            {
                dir = dir.Parent;
            }

            return dir.FullName;
        }

        public static string Db_directory() => Path.Combine(Sln_directory(), "db");
        public static string ViewModels_directory() => Path.Combine(Sln_directory(), "ViewModels");

        public static IEnumerable<string> Project_files() => Directory.EnumerateFiles(Sln_directory(), "*.csproj", SearchOption.AllDirectories);

        public static bool IsAssignableTo<T>(this Type @this) => typeof(T).IsAssignableFrom(@this);

        public static bool IsConcrete(this Type @this) => !@this.IsAbstract && !@this.IsInterface;

        public static IEnumerable<string> GetSourceFiles()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var combine = Path.Combine(currentDirectory, "..", "..", "..");
            var path = Path.GetFullPath(combine);
            return Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
        }
    }
}
