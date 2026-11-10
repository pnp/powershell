using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    [Cmdlet(VerbsDiagnostic.Test, "PnPSiteTemplate", DefaultParameterSetName = ParameterSetPath)]
    [OutputType(typeof(SiteTemplateValidationResult))]
    [ApiPermissionsNotRequired(Remarks = "This cmdlet validates a local site template and performs no request.")]
    public class TestSiteTemplate : BasePSCmdlet
    {
        private const string ParameterSetPath = "By Path";
        private const string ParameterSetStream = "By Stream";
        private const string ParameterSetXml = "By XML";
        private const string ParameterSetTemplate = "By Template";
        private Stream _validationStream;
        private readonly Dictionary<ProvisioningTemplate, string> _schemaNamespaces = new(ReferenceEqualityComparer.Instance);
        private readonly Dictionary<ProvisioningTemplate, XElement> _sourceElements = new(ReferenceEqualityComparer.Instance);
        private readonly Dictionary<ProvisioningTemplate, string> _templateFiles = new(ReferenceEqualityComparer.Instance);
        private bool _isPackage;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSetPath)]
        [ValidateNotNullOrEmpty]
        public string Path { get; set; }

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSetStream)]
        [ValidateNotNull]
        public Stream Stream { get; set; }

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSetXml)]
        [ValidateNotNullOrEmpty]
        public string Xml { get; set; }

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSetTemplate)]
        [ValidateNotNull]
        public ProvisioningTemplate Template { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetPath)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSetStream)]
        [ValidateNotNullOrEmpty]
        public string TemplateId { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetPath)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSetStream)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSetXml)]
        public ITemplateProviderExtension[] TemplateProviderExtensions { get; set; }

        protected override void ExecuteCmdlet()
        {
            _schemaNamespaces.Clear();
            _sourceElements.Clear();
            _templateFiles.Clear();
            // A null element would otherwise be dereferenced while pre-processing.
            TemplateProviderExtensions = TemplateProviderExtensions?.Where(extension => extension != null).ToArray();
            ResolveAndValidatePath();

            using var bufferedStream = ParameterSetName == ParameterSetStream ? BufferStream(Stream) : null;
            _validationStream = bufferedStream;

            var schemaIssues = new List<(SiteTemplateValidationIssue Issue, string TemplateFile)>();
            string sourceSchemaNamespace = null;
            List<ProvisioningTemplate> templates;
            try
            {
                sourceSchemaNamespace = GetSchemaNamespace();
                templates = LoadTemplates(schemaIssues);
            }
            catch (UnauthorizedAccessException exception)
            {
                ThrowTerminatingError(new ErrorRecord(exception, "SiteTemplateAccessDenied", ErrorCategory.PermissionDenied, Path));
                return;
            }
            catch (Exception exception) when (
                exception is System.Xml.XmlException or InvalidDataException or FormatException or PnP.Framework.Provisioning.Connectors.OpenXML.PnPPackageFormatException ||
                _isPackage && exception is FileNotFoundException or InvalidOperationException)
            {
                schemaIssues.Add((_isPackage
                    ? SiteTemplateValidationHelper.CreateIssue("InvalidPackage", exception.Message, "Package")
                    : SiteTemplateValidationHelper.CreateSchemaIssue(exception), null));
                templates = [];
            }

            foreach (var template in templates.Where(template => !_schemaNamespaces.ContainsKey(template)))
            {
                _schemaNamespaces.Add(template, sourceSchemaNamespace);
            }

            if (templates.Count == 0)
            {
                if (schemaIssues.Count == 0)
                {
                    schemaIssues.Add((ParameterSpecified(nameof(TemplateId))
                        ? SiteTemplateValidationHelper.CreateIssue("TemplateNotFound", $"The source does not contain a template with ID '{TemplateId}'.", "ProvisioningTemplate")
                        : SiteTemplateValidationHelper.CreateSchemaIssue(new InvalidDataException("The source does not contain a site template.")), null));
                }

                WriteObject(new SiteTemplateValidationResult
                {
                    TemplateId = TemplateId,
                    SchemaVersion = sourceSchemaNamespace,
                    SchemaChecked = ParameterSetName != ParameterSetTemplate,
                    Issues = schemaIssues.Select(schemaIssue => schemaIssue.Issue).ToList()
                });
                return;
            }

            foreach (var template in templates)
            {
                // When one template was asked for it does not answer for a member that failed elsewhere, but an
                // unfiltered run still has to report those failures somewhere, so they stay on every result.
                var templateFile = _templateFiles.GetValueOrDefault(template);
                var scopeToTemplateFile = ParameterSpecified(nameof(TemplateId));
                var issues = schemaIssues
                    .Where(schemaIssue => !scopeToTemplateFile || schemaIssue.TemplateFile == null || schemaIssue.TemplateFile == templateFile)
                    .Select(schemaIssue => schemaIssue.Issue)
                    .ToList();
                var schemaNamespace = _schemaNamespaces.GetValueOrDefault(template);
                var schemaVersionIssue = SiteTemplateValidationHelper.CreateSchemaVersionIssue(schemaNamespace);
                if (schemaVersionIssue != null)
                {
                    issues.Add(schemaVersionIssue);
                }
                var sourceElement = _sourceElements.GetValueOrDefault(template);
                issues.AddRange(SiteTemplateValidationHelper.Validate(template, sourceElement));
                var sourceTemplateId = (string)sourceElement?.Attribute("ID");
                var resultTemplateId = !string.IsNullOrWhiteSpace(template.Id)
                    ? template.Id
                    : !string.IsNullOrWhiteSpace(sourceTemplateId)
                        ? sourceTemplateId
                        : ParameterSpecified(nameof(TemplateId)) ? TemplateId : null;

                WriteObject(new SiteTemplateValidationResult
                {
                    TemplateId = resultTemplateId,
                    SchemaVersion = schemaNamespace,
                    ResourcesChecked = template.Connector != null,
                    SchemaChecked = _sourceElements.ContainsKey(template),
                    Issues = issues
                });
            }
        }

        private List<ProvisioningTemplate> LoadTemplates(List<(SiteTemplateValidationIssue Issue, string TemplateFile)> schemaIssues)
        {
            Action<Exception, string> exceptionHandler = (exception, templateFile) => schemaIssues.Add(
                (SiteTemplateValidationHelper.CreateSchemaIssue(exception, templateFile), templateFile));
            Action<string, string> duplicateTemplateIdHandler = (duplicateId, templateFile) => schemaIssues.Add(
                (SiteTemplateValidationHelper.CreateIssue(
                    "DuplicateTemplateId",
                    $"Template ID '{duplicateId}' occurs more than once in '{templateFile}'.",
                    "ProvisioningTemplate"), templateFile));
            Action<string> unaddressableTemplateHandler = templateFile => schemaIssues.Add(
                (SiteTemplateValidationHelper.CreateIssue(
                    "MissingTemplateId",
                    $"'{templateFile}' contains several templates of which at least one has no ID, so it cannot be validated separately.",
                    "ProvisioningTemplate"), templateFile));

            switch (ParameterSetName)
            {
                case ParameterSetPath:
                    using (var fileStream = System.IO.File.OpenRead(Path))
                    {
                        var isPackage = FileUtilities.IsOpenOfficeFile(fileStream);
                        fileStream.Position = 0;
                        _isPackage = isPackage;
                        return ProvisioningHelper.LoadSiteTemplatesFromStreamStrict(
                            fileStream,
                            TemplateId,
                            isPackage ? null : new PnP.Framework.Provisioning.Connectors.FileSystemConnector(System.IO.Path.GetDirectoryName(Path), string.Empty),
                            TemplateProviderExtensions,
                            exceptionHandler,
                            AddSchemaNamespace,
                            duplicateTemplateIdHandler,
                            unaddressableTemplateHandler);
                    }
                case ParameterSetStream:
                    _isPackage = IsPackageStream();
                    return ProvisioningHelper.LoadSiteTemplatesFromStreamStrict(_validationStream, TemplateId, null, TemplateProviderExtensions, exceptionHandler, AddSchemaNamespace, duplicateTemplateIdHandler, unaddressableTemplateHandler);
                case ParameterSetXml:
                    using (var xmlStream = ProvisioningHelper.CreateXmlStream(Xml))
                    {
                        return ProvisioningHelper.LoadSiteTemplatesFromStreamStrict(
                            xmlStream,
                            null,
                            null,
                            TemplateProviderExtensions,
                            exceptionHandler,
                            AddSchemaNamespace,
                            duplicateTemplateIdHandler,
                            unaddressableTemplateHandler);
                    }
                case ParameterSetTemplate:
                    return [Template];
                default:
                    return [];
            }
        }

        private string GetSchemaNamespace()
        {
            try
            {
                switch (ParameterSetName)
                {
                    case ParameterSetPath:
                        using (var stream = System.IO.File.OpenRead(Path))
                        {
                            if (FileUtilities.IsOpenOfficeFile(stream))
                            {
                                return null;
                            }
                            stream.Position = 0;
                            using var reader = new StreamReader(stream);
                            return SiteTemplateValidationHelper.GetSchemaNamespace(reader.ReadToEnd());
                        }
                    case ParameterSetXml:
                        return SiteTemplateValidationHelper.GetSchemaNamespace(Xml);
                    case ParameterSetStream when !IsPackageStream():
                        if (_validationStream.CanSeek)
                        {
                            _validationStream.Position = 0;
                        }
                        using (var reader = new StreamReader(_validationStream, leaveOpen: true))
                        {
                            var schemaNamespace = SiteTemplateValidationHelper.GetSchemaNamespace(reader.ReadToEnd());
                            if (_validationStream.CanSeek)
                            {
                                _validationStream.Position = 0;
                            }
                            return schemaNamespace;
                        }
                }
            }
            catch (System.Xml.XmlException)
            {
                // The provider reports the actionable schema or XML parsing error.
            }

            return null;
        }

        private bool IsPackageStream()
        {
            if (ParameterSetName != ParameterSetStream || _validationStream == null)
            {
                return false;
            }

            _validationStream.Position = 0;
            var isPackage = FileUtilities.IsOpenOfficeFile(_validationStream);
            _validationStream.Position = 0;
            return isPackage;
        }

        private void AddSchemaNamespace(ProvisioningTemplate template, string templateFile, string schemaNamespace, XElement sourceElement)
        {
            _schemaNamespaces[template] = schemaNamespace;
            if (templateFile != null)
            {
                _templateFiles[template] = templateFile;
            }
            if (sourceElement != null)
            {
                _sourceElements[template] = sourceElement;
            }
        }

        private void ResolveAndValidatePath()
        {
            if (ParameterSetName != ParameterSetPath)
            {
                return;
            }

            Path = SessionState.Path.GetUnresolvedProviderPathFromPSPath(Path, out ProviderInfo provider, out _);
            if (!provider.Name.Equals("FileSystem", StringComparison.OrdinalIgnoreCase))
            {
                ThrowTerminatingError(new ErrorRecord(
                    new ArgumentException("Path must refer to the FileSystem provider.", nameof(Path)),
                    "SiteTemplatePathNotFileSystem",
                    ErrorCategory.InvalidArgument,
                    Path));
            }

            if (!System.IO.File.Exists(Path))
            {
                ThrowTerminatingError(new ErrorRecord(
                    new FileNotFoundException(Properties.Resources.FileDoesNotExist, Path),
                    "SiteTemplateNotFound",
                    ErrorCategory.ObjectNotFound,
                    Path));
            }
        }

        private static MemoryStream BufferStream(Stream source)
        {
            var originalPosition = source.CanSeek ? source.Position : 0;
            if (source.CanSeek)
            {
                source.Position = 0;
            }

            var bufferedStream = new MemoryStream();
            source.CopyTo(bufferedStream);
            bufferedStream.Position = 0;

            if (source.CanSeek)
            {
                source.Position = originalPosition;
            }

            return bufferedStream;
        }
    }
}