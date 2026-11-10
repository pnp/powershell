using Microsoft.VisualStudio.TestTools.UnitTesting;
using PnP.PowerShell.Commands.Admin;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace PnP.PowerShell.Tests.Admin
{
    [TestClass]
    public class SetPnPOfficeFontCatalogTests
    {
        #region Cmdlet contract (no tenant)
        [TestMethod]
        public void SetPnPOfficeFontCatalog_IsNamedAndHasMandatoryParameters()
        {
            var attribute = typeof(SetOfficeFontCatalog).GetCustomAttribute<CmdletAttribute>();
            Assert.AreEqual("Set-PnPOfficeFontCatalog", $"{attribute.VerbName}-{attribute.NounName}");

            var fontFolder = typeof(SetOfficeFontCatalog).GetField(nameof(SetOfficeFontCatalog.FontFolder));
            Assert.IsTrue(fontFolder.GetCustomAttribute<ParameterAttribute>().Mandatory, "FontFolder must be mandatory");
            Assert.IsNotNull(fontFolder.GetCustomAttribute<ValidateNotNullOrEmptyAttribute>(), "FontFolder must not be empty");

            // LibraryUrl defaults to the registered Office font library.
            var libraryUrl = typeof(SetOfficeFontCatalog).GetField(nameof(SetOfficeFontCatalog.LibraryUrl));
            Assert.IsFalse(libraryUrl.GetCustomAttribute<ParameterAttribute>().Mandatory, "LibraryUrl must be optional");
        }

        [TestMethod]
        public void SetPnPOfficeFontCatalog_HasNoSpoModuleParameter()
        {
            Assert.IsNull(typeof(SetOfficeFontCatalog).GetField("SpoModulePath"), "the cmdlet must not depend on the SPO module");
        }
        #endregion

        #region Tenant tests (opt-in, change the tenant's Office fonts)
        [TestMethod]
        [TestCategory("Tenant")]
        public void SetPnPOfficeFontCatalog_PublishesExactlyTheFolderFonts()
        {
            using var scope = OfficeFontsTenantScope.Open();
            var folder = scope.Subset(2);
            try
            {
                scope.Run("Set-PnPOfficeFontCatalog -FontFolder $args[0] -LibraryUrl $args[1] -WarningAction SilentlyContinue", folder, scope.LibraryPath);

                var published = Directory.EnumerateFiles(folder).Select(Path.GetFileName).ToArray();
                CollectionAssert.IsSubsetOf(published, scope.LibraryFonts(), "every font file is in the library");
                foreach (var major in new[] { 1, 2 })
                {
                    CollectionAssert.AreEqual(OfficeFontsTenantScope.Ids(published), scope.CatalogFonts(major), $"catalog v{major} lists exactly the folder fonts");
                }
            }
            finally
            {
                Directory.Delete(folder, true);
                // Leave the library offering every test font.
                scope.Run("Set-PnPOfficeFontCatalog -FontFolder $args[0] -LibraryUrl $args[1] -WarningAction SilentlyContinue", scope.FontFolder, scope.LibraryPath);
            }
        }

        [TestMethod]
        [TestCategory("Tenant")]
        public void SetPnPOfficeFontCatalog_FailsForAMissingFolder()
        {
            using var scope = OfficeFontsTenantScope.Open();
            var missing = Path.Combine(Path.GetTempPath(), "pnp-no-such-folder");

            var ex = Assert.ThrowsException<AssertFailedException>(() =>
                scope.Run("Set-PnPOfficeFontCatalog -FontFolder $args[0] -LibraryUrl $args[1]", missing, scope.LibraryPath));
            StringAssert.Contains(ex.Message, "does not exist");
        }
        #endregion
    }
}
