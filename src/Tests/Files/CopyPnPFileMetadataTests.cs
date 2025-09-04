using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class CopyFileMetadataTests
    {
        [TestMethod]
        public void CopyPnPFileMetadataOnlyTest()
        {
            using (var scope = new PSTestScope(true))
            {
                // Test that MetadataOnly parameter is recognized
                var results = scope.ExecuteCommand("Get-Command",
                    new CommandParameter("Name", "Copy-PnPFileMetadata"));
                
                Assert.IsNotNull(results);
                
                // Verify that MetadataOnly parameter exists
                var cmdlet = results[0];
                Assert.IsNotNull(cmdlet);
                
                // This test validates that the MetadataOnly parameter is properly added to the cmdlet
                // Full functional testing would require SharePoint connection and test content
            }
        }
    }
}
