using System.Xml.Linq;

using FluentAssertions;

using WeightliftingManagmentApp.Test.Helpers;

using Xunit;

namespace WeightliftingManagmentApp.Test.Convention
{
    public class ProjectFileConvention
    {
        [Fact]
        public void File_csv_should_mark_as_Content()
        {
            var projectFiles = ConventionHelpers.Project_files().ToList();

            projectFiles.Should().NotBeEmpty();

            foreach (var csproj in projectFiles)
            {
                var csv = from d in XDocument.Load(csproj).Descendants()
                          let include = d.Attribute("Include")
                          where include != null
                          where include.Value.EndsWith(".csv")
                          select d;

                csv.Where(x => x.Name.LocalName != "Content").Should().BeEmpty();
            }
        }


        [Fact]
        public void File_csv_should_be_set_CopyToOutputDirectory_as_PreserveNewest()
        {
            var projectFiles = ConventionHelpers.Project_files().ToList();

            projectFiles.Should().NotBeEmpty();

            foreach (var csproj in projectFiles)
            {
                var csv = from d in XDocument.Load(csproj).Descendants()
                          let include = d.Attribute("Include")
                          where include != null
                          where include.Value.EndsWith(".csv")
                          select d;

                csv.Where(x => x.Element("CopyToOutputDirectory")?.Value != "PreserveNewest").Should().BeEmpty();
            }
        }



        [Fact]
        public void File_ttf_should_mark_as_Content()
        {
            var projectFiles = ConventionHelpers.Project_files().ToList();

            projectFiles.Should().NotBeEmpty();

            foreach (var csproj in projectFiles)
            {
                var ttf = from d in XDocument.Load(csproj).Descendants()
                          let include = d.Attribute("Include")
                          where include != null
                          where include.Value.EndsWith(".ttf")
                          select d;

                ttf.Where(x => x.Name.LocalName != "Content").Should().BeEmpty();
            }
        }


        [Fact]
        public void File_ttf_should_be_set_CopyToOutputDirectory_as_PreserveNewest()
        {
            var projectFiles = ConventionHelpers.Project_files().ToList();

            projectFiles.Should().NotBeEmpty();

            foreach (var csproj in projectFiles)
            {
                var ttf = from d in XDocument.Load(csproj).Descendants()
                          let include = d.Attribute("Include")
                          where include != null
                          where include.Value.EndsWith(".ttf")
                          select d;

                ttf.Where(x => x.Element("CopyToOutputDirectory")?.Value != "PreserveNewest").Should().BeEmpty();
            }
        }

    }
}
