using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Attributes;
using System.Collections;
using System.Management.Automation;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;

namespace PnP.PowerShell.Commands.PeopleSettings
{
    [Cmdlet(VerbsCommon.New, "PnPProfileCardProperty")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/PeopleSettings.ReadWrite.All")]
    [OutputType(typeof(Model.Graph.ProfileCard.ProfileCardProperty))]
    public class NewProfileCardProperty : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public ProfileCardPropertyName PropertyName;

        [Parameter(Mandatory = true)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public Hashtable Localizations;
        
        protected override void ExecuteCmdlet()
        {            
            var bodyContent = new Model.Graph.ProfileCard.ProfileCardProperty
            {
                DirectoryPropertyName = PropertyName.ToString(),
                Annotations = new List<Model.Graph.ProfileCard.ProfileCardPropertyAnnotation>
                {
                    new Model.Graph.ProfileCard.ProfileCardPropertyAnnotation
                    { 
                        DisplayName = DisplayName
                    }
                }
            };

            if(ParameterSpecified(nameof(Localizations)))
            {
                var localizations = new List<Model.Graph.ProfileCard.ProfileCardPropertyLocalization>();
                foreach (var key in Localizations.Keys)
                {
                    localizations.Add(new Model.Graph.ProfileCard.ProfileCardPropertyLocalization
                    {
                        DisplayName = Localizations[key] as string,
                        LanguageTag = key as string
                    });
                }

                bodyContent.Annotations.First().Localizations = localizations;
            }

            var jsonContent = JsonContent.Create(bodyContent);
            WriteVerbose($"Payload: {jsonContent.ReadAsStringAsync().GetAwaiter().GetResult()}");

            var graphApiUrl = $"v1.0/admin/people/profileCardProperties";
            var results = RequestHelper.PostHttpContent(graphApiUrl, jsonContent);
            var resultsContent = results.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var propertyResult = System.Text.Json.JsonSerializer.Deserialize<Model.Graph.ProfileCard.ProfileCardProperty>(resultsContent);

            WriteObject(propertyResult, false);
        }
    }
}