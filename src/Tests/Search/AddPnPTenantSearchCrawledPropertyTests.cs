using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PnP.PowerShell.Commands.Search;

namespace PnP.PowerShell.Tests.Search
{
    [TestClass]
    public class AddTenantSearchCrawledPropertyTests
    {
        [TestMethod]
        public void BuildSearchConfigurationXmlCreatesTenantCrawledPropertyPayload()
        {
            var propertySetId = Guid.Parse("00130329-0000-0130-C000-000000131346");
            var xml = BuildSearchConfigurationXml("ows_ProjectCode", propertySetId, "SharePoint", true, 143692);
            var document = XDocument.Parse(xml);

            XNamespace portability = "http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Portability";
            XNamespace admin = "http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration";
            XNamespace arrays = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";

            Assert.AreEqual(portability + "SearchConfigurationSettings", document.Root.Name);
            Assert.AreEqual("ows_ProjectCode", document.Descendants(admin + "Name").SingleValue());
            Assert.AreEqual("SharePoint", document.Descendants(admin + "CategoryName").SingleValue());
            Assert.AreEqual(propertySetId.ToString("D"), document.Descendants(admin + "Propset").SingleValue());
            Assert.AreEqual("true", document.Descendants(admin + "IsMappedToContents").SingleValue());
            Assert.AreEqual("143692", document.Descendants(admin + "SchemaId").SingleValue());
            Assert.AreEqual("ows_ProjectCode", document.Descendants(arrays + "Key").LastValue());
        }

        [TestMethod]
        public void BuildSearchConfigurationXmlCreatesStructuredPayloadWithoutMappingToContents()
        {
            var propertySetId = Guid.Parse("ED280121-B677-4E2A-8FBC-0D9E2325B0A2");
            var xml = BuildSearchConfigurationXml("ows_q_TEXT_ProjectCode", propertySetId, "SharePoint", false, 143692);
            var document = XDocument.Parse(xml);

            XNamespace admin = "http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration";

            Assert.AreEqual("ows_q_TEXT_ProjectCode", document.Descendants(admin + "Name").SingleValue());
            Assert.AreEqual(propertySetId.ToString("D"), document.Descendants(admin + "Propset").SingleValue());
            Assert.AreEqual("false", document.Descendants(admin + "IsMappedToContents").SingleValue());
        }

        private static string BuildSearchConfigurationXml(string name, Guid propertySetId, string categoryName, bool mapToContents, int schemaId)
        {
            return AddTenantSearchCrawledProperty.BuildSearchConfigurationXml(name, propertySetId, categoryName, mapToContents, schemaId);
        }
    }

    internal static class SearchCrawledPropertyTestExtensions
    {
        internal static string SingleValue(this System.Collections.Generic.IEnumerable<XElement> elements)
        {
            return System.Linq.Enumerable.Single(elements).Value;
        }

        internal static string LastValue(this System.Collections.Generic.IEnumerable<XElement> elements)
        {
            return System.Linq.Enumerable.Last(elements).Value;
        }
    }
}
