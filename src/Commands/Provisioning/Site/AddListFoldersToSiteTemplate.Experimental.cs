using PnP.Core.Provisioning.Model;
using PnP.Core.Provisioning.Model.Configuration;
using PnP.Core.Provisioning.ObjectHandlers;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using CoreListsConfiguration = PnP.Core.Provisioning.Model.Configuration.Lists.Lists;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    public partial class AddListFoldersToSiteTemplate
    {
        private void ExecuteCmdletExperimental()
        {
            if (ParameterSpecified(nameof(TemplateProviderExtensions)))
            {
                LogWarning("-TemplateProviderExtensions has no effect with -Experimental: template provider extensions are not run by the PnP.Core.Provisioning engine.");
            }

            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            var template = CoreProvisioningHelper.LoadSiteTemplateFromFile(Path, LogError);
            if (template == null)
            {
                throw new ApplicationException("Invalid template file.");
            }

            var siteList = List.GetListOrThrow(nameof(List), PnPContext, l => l.Title);
            var tokenParser = TokenParser.CreateAsync(PnPContext, template).GetAwaiter().GetResult();

            var listInstance = template.Lists.FirstOrDefault(l => tokenParser.ParseString(l.Title) == siteList.Title);
            if (listInstance == null)
            {
                throw new ApplicationException("List does not exist in the template file.");
            }

            var folders = ExtractFoldersExperimental(siteList.Title);
            if (folders == null)
            {
                return;
            }

            listInstance.Folders.AddRange(folders);

            CoreProvisioningHelper.SaveSiteTemplateToFile(template, Path);
        }

        /// <summary>
        /// Extracts the folders of one list with the PnP.Core.Provisioning engine.
        /// </summary>
        /// <param name="listTitle">The title of the list to read the folders of</param>
        /// <returns>The extracted folders, or null when the engine extracted no instance for the list</returns>
        private FolderCollection ExtractFoldersExperimental(string listTitle)
        {
            var configuration = new ExtractConfiguration
            {
                Handlers = { ConfigurationHandler.Lists },
                // Only this list's folders are wanted, so there is nothing to compare with the site's base template.
                CompareWithBaseTemplate = false,
                Lists =
                {
                    Lists =
                    {
                        new CoreListsConfiguration.ExtractListsListsConfiguration
                        {
                            Title = listTitle,
                            IncludeFolders = true,
                            MaxFolderDepth = Recursive ? 0 : 1,
                            IncludeSecurity = IncludeSecurity
                        }
                    }
                }
            };

            var reporter = new CoreProvisioningReporter($"Extracting folders of {listTitle}", WriteProgress, LogWarning);
            configuration.ProgressDelegate = reporter.ProgressDelegate;
            configuration.MessagesDelegate = reporter.MessagesDelegate;

            var extracted = reporter.Run(() => PnPContext.GetProvisioningManager().GetTemplateAsync(configuration));

            var extractedList = extracted?.Lists.FirstOrDefault(l => l.Title == listTitle);
            if (extractedList == null)
            {
                LogError($"The list '{listTitle}' could not be extracted, so no folders were added.");
                return null;
            }

            return extractedList.Folders;
        }
    }
}
