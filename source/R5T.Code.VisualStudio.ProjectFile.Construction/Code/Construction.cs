using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;


namespace R5T.Code.VisualStudio.ProjectFile.Construction
{
    public static class Construction
    {
        public static void SubMain()
        {
            //Construction.XPathManipulation();
            Construction.ExploreLinqToXml();
        }

        private static void ExploreLinqToXml()
        {
            //var xmlFilePath = @"C:\Organizations\Rivet\Repositories\Libraries\R5T.NetStandard.IO.Paths\source\R5T.NetStandard.IO.Paths.Testing\R5T.NetStandard.IO.Paths.Testing.csproj"; // Contains both package references and project references.
            //var xmlFilePath = @"C:\Organizations\Rivet\Repositories\Libraries\R5T.NetStandard.Types\source\R5T.NetStandard.Types\R5T.NetStandard.Types.csproj"; // Package references only.
            var xmlFilePath = @"C:\Organizations\Rivet\Repositories\Libraries\R5T.Code.VisualStudio.Types\source\R5T.Code.VisualStudio.Types\R5T.Code.VisualStudio.Types.csproj"; // Project references only.

            var xDoc = XElement.Load(xmlFilePath);

            //var x = new XElement()

            var packagesItemGroup = xDoc.Descendants("ItemGroup").Where(x => x.Elements("PackageReference").Any()).SingleOrDefault();
            var packagesItemGroupFound = packagesItemGroup != default;
            Console.WriteLine($"PackageReference ItemGroup found: {packagesItemGroupFound}");
        }

        private static void XPathManipulation()
        {
            var xmlFilePath = @"C:\Organizations\Rivet\Repositories\Libraries\R5T.NetStandard.IO.Paths\source\R5T.NetStandard.IO.Paths.Testing\R5T.NetStandard.IO.Paths.Testing.csproj"; // Contains both package references and project references.
            //var xmlFilePath = @"C:\Organizations\Rivet\Repositories\Libraries\R5T.NetStandard.Types\source\R5T.NetStandard.Types\R5T.NetStandard.Types.csproj"; // Package references only.
            //var xmlFilePath = @"C:\Organizations\Rivet\Repositories\Libraries\R5T.Code.VisualStudio.Types\source\R5T.Code.VisualStudio.Types\R5T.Code.VisualStudio.Types.csproj"; // Project references only.

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            var packagesItemGroupXPathValue = "//Project/ItemGroup[PackageReference]";
            var packagesItemGroupNodeFound = xmlDoc.SelectNodes(packagesItemGroupXPathValue).OfType<XmlNode>().Any();
            Console.WriteLine($"PackageReference ItemGroup found: {packagesItemGroupNodeFound}");

            var packagesItemGroupNode = xmlDoc.SelectNodes(packagesItemGroupXPathValue).OfType<XmlNode>().SingleOrDefault();
            if(packagesItemGroupNode != default)
            {
                Console.WriteLine("Packages:");

                var packageReferenceNodes = packagesItemGroupNode.SelectNodes("//PackageReference").OfType<XmlNode>();
                foreach (var packageReferenceNode in packageReferenceNodes)
                {
                    Console.WriteLine($"Include: {packageReferenceNode.Attributes["Include"].Value}");
                }
            }

            var projectsItemGroupXPathValue = "//Project/ItemGroup[ProjectReference]";
            var projectsItemGroupNodeFound = xmlDoc.SelectNodes(projectsItemGroupXPathValue).OfType<XmlNode>().Any();
            Console.WriteLine($"ProjectReference ItemGroup found: {projectsItemGroupNodeFound}");

            var projectsItemGroupNode = xmlDoc.SelectNodes(projectsItemGroupXPathValue).OfType<XmlNode>().SingleOrDefault();
            if (projectsItemGroupNode != default)
            {
                Console.WriteLine("Projects:");

                var projectReferenceNodes = packagesItemGroupNode.SelectNodes("//ProjectReference").OfType<XmlNode>();
                foreach (var projectReferenceNode in projectReferenceNodes)
                {
                    Console.WriteLine($"Include: {projectReferenceNode.Attributes["Include"].Value}");
                }
            }
        }
    }
}
