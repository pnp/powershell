using PnP.Framework.EnterpriseWiki;
using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Describes a sealed Enterprise Wiki migration plan and its review artifacts.
    /// </summary>
    public sealed class EnterpriseWikiMigrationPlanResult
    {
        public EnterpriseWikiMigrationPlanResult(
            EnterpriseWikiMigrationPackage package,
            string packagePath,
            string reportPath)
        {
            Package = package ?? throw new ArgumentNullException(nameof(package));
            PackagePath = string.IsNullOrWhiteSpace(packagePath)
                ? throw new ArgumentException("A package path is required.", nameof(packagePath))
                : packagePath;
            ReportPath = string.IsNullOrWhiteSpace(reportPath)
                ? throw new ArgumentException("A report path is required.", nameof(reportPath))
                : reportPath;
        }

        public EnterpriseWikiMigrationPackage Package { get; }

        public string PackagePath { get; }

        public string ReportPath { get; }

        public string SchemaVersion => Package.SchemaVersion;

        public DateTimeOffset PlannedAtUtc => Package.PlannedAtUtc;

        public EnterpriseWikiPackageState State => Package.State;

        public bool IsExecutable => Package.Plan?.IsExecutable == true;

        public EnterpriseWikiSnapshot Snapshot => Package.Snapshot;

        public EnterpriseWikiMigrationPlan Plan => Package.Plan;

        public string SnapshotDigest => Package.SnapshotDigest;

        public string PlanDigest => Package.PlanDigest;

        public EnterpriseWikiCustomerReport Report => Package.Report;

        public override string ToString()
        {
            return PackagePath;
        }
    }
}
