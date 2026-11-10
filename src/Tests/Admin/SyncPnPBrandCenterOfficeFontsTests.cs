using Microsoft.VisualStudio.TestTools.UnitTesting;
using PnP.PowerShell.Commands.Admin;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace PnP.PowerShell.Tests.Admin
{
    [TestClass]
    public class SyncPnPBrandCenterOfficeFontsTests
    {
        #region Sync plan (no tenant)
        [TestMethod]
        public void Plan_AddsOnlyTheFontsTheLibraryDoesNotHave()
        {
            var plan = OfficeFontSyncPlan.Create(["A.ttf", "b.otf", "C.ttf"], ["a.TTF", "Other.ttf"], removeMissing: false);

            CollectionAssert.AreEqual(new[] { "b.otf", "C.ttf" }, plan.Added.ToArray(), "names are compared ignoring case");
            Assert.AreEqual(0, plan.Removed.Count, "nothing is removed without -RemoveMissing");
            CollectionAssert.AreEquivalent(new[] { "a.TTF", "Other.ttf" }, plan.Kept.ToArray());
            Assert.IsTrue(plan.HasChanges);
        }

        [TestMethod]
        public void Plan_HasNoChangesWhenInSync()
        {
            var plan = OfficeFontSyncPlan.Create(["A.ttf", "B.ttf"], ["b.ttf", "a.ttf"], removeMissing: true);

            Assert.IsFalse(plan.HasChanges);
            Assert.AreEqual(string.Empty, plan.Describe());
        }

        [TestMethod]
        public void Plan_RemoveMissingRemovesOfficeFontsNotInTheSource()
        {
            var plan = OfficeFontSyncPlan.Create(["A.ttf"], ["A.ttf", "Old.otf", "older.ttf"], removeMissing: true);

            Assert.AreEqual(0, plan.Added.Count);
            CollectionAssert.AreEqual(new[] { "Old.otf", "older.ttf" }, plan.Removed.ToArray());
            CollectionAssert.AreEqual(new[] { "A.ttf" }, plan.Kept.ToArray());
            Assert.IsTrue(plan.IsRemoved("OLD.OTF"));
            Assert.IsFalse(plan.IsRemoved("A.ttf"));
            Assert.AreEqual("remove Old.otf, older.ttf", plan.Describe());
        }

        [TestMethod]
        public void Plan_DescribesAddsAndRemoves()
        {
            var plan = OfficeFontSyncPlan.Create(["New.ttf", "Same.ttf"], ["Same.ttf", "Gone.ttf"], removeMissing: true);

            Assert.AreEqual("add New.ttf; remove Gone.ttf", plan.Describe());
        }

        [TestMethod]
        public void Plan_DetectsThatEveryFontWouldBeRemoved()
        {
            Assert.IsTrue(OfficeFontSyncPlan.Create([], ["A.ttf"], removeMissing: true).RemovesEverything);
            Assert.IsFalse(OfficeFontSyncPlan.Create(["B.ttf"], ["A.ttf"], removeMissing: true).RemovesEverything);
        }
        #endregion

        #region Cmdlet contract (no tenant)
        [TestMethod]
        public void SyncPnPBrandCenterOfficeFonts_IsNamedAndSupportsWhatIf()
        {
            var attribute = typeof(SyncBrandCenterOfficeFonts).GetCustomAttribute<CmdletAttribute>();
            Assert.AreEqual("Sync-PnPBrandCenterOfficeFonts", $"{attribute.VerbName}-{attribute.NounName}");
            Assert.IsTrue(attribute.SupportsShouldProcess, "-WhatIf and -Confirm must be supported");
        }

        [TestMethod]
        public void SyncPnPBrandCenterOfficeFonts_ParametersAreOptionalWithBrandCenterAsDefaultSource()
        {
            foreach (var name in new[] { "Source", "LocalFolder", "RemoveMissing", "Force" })
            {
                var field = typeof(SyncBrandCenterOfficeFonts).GetField(name);
                Assert.IsNotNull(field, name);
                Assert.IsFalse(field.GetCustomAttribute<ParameterAttribute>().Mandatory, $"{name} must be optional");
            }
            Assert.AreEqual(OfficeFontSource.BrandCenter, new SyncBrandCenterOfficeFonts().Source);
            CollectionAssert.AreEquivalent(new[] { "BrandCenter", "Local" }, Enum.GetNames<OfficeFontSource>());
        }
        #endregion

        #region Tenant tests (opt-in, change the tenant's Office fonts)
        private const string Sync = "Sync-PnPBrandCenterOfficeFonts -Source Local -LocalFolder $args[0] -Force -WarningAction SilentlyContinue";

        [TestMethod]
        [TestCategory("Tenant")]
        public void SyncPnPBrandCenterOfficeFonts_AddsReportsInSyncAndRemoves()
        {
            using var scope = OfficeFontsTenantScope.Open();
            var two = scope.Subset(2);
            var three = scope.Subset(3);
            var third = scope.FontNames[2];
            try
            {
                // Start from exactly the first two fonts.
                scope.Run(Sync + " -RemoveMissing", two);
                CollectionAssert.AreEquivalent(scope.FontNames.Take(2).ToArray(), scope.LibraryFonts(), "library has exactly two fonts");

                StringAssert.Contains(scope.RunText(Sync, two), "in sync", "nothing to do");

                StringAssert.Contains(scope.RunText(Sync, three), "1 added");
                CollectionAssert.Contains(scope.LibraryFonts(), third);
                CollectionAssert.AreEqual(OfficeFontsTenantScope.Ids(scope.FontNames.Take(3).ToArray()), scope.CatalogFonts(2), "catalog lists the three fonts");

                StringAssert.Contains(scope.RunText(Sync + " -RemoveMissing", two), "1 removed");
                CollectionAssert.DoesNotContain(scope.LibraryFonts(), third);
                CollectionAssert.IsSubsetOf(scope.FontNames.Take(2).ToArray(), scope.LibraryFonts(), "the other fonts stay");
                foreach (var major in new[] { 1, 2 })
                {
                    CollectionAssert.DoesNotContain(scope.CatalogFonts(major), Path.GetFileNameWithoutExtension(third), $"catalog v{major}");
                }
            }
            finally
            {
                Directory.Delete(two, true);
                Directory.Delete(three, true);
                scope.Run(Sync + " -RemoveMissing", scope.FontFolder);
            }
        }

        [TestMethod]
        [TestCategory("Tenant")]
        public void SyncPnPBrandCenterOfficeFonts_WhatIfChangesNothing()
        {
            using var scope = OfficeFontsTenantScope.Open();
            var one = scope.Subset(1);
            try
            {
                var before = scope.LibraryFonts();
                scope.Run("Sync-PnPBrandCenterOfficeFonts -Source Local -LocalFolder $args[0] -RemoveMissing -WhatIf", one);
                CollectionAssert.AreEqual(before, scope.LibraryFonts());
            }
            finally
            {
                Directory.Delete(one, true);
            }
        }
        #endregion
    }
}
