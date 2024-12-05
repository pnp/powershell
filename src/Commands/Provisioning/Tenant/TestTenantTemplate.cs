﻿using PnP.Framework.Provisioning.Model;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsDiagnostic.Test, "PnPTenantTemplate")]
    public class TestTenantTemplate : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ProvisioningHierarchy Template;

        protected override void ExecuteCmdlet()
        {
            List<string> issues = new List<string>();
            foreach(var sequence in Template.Sequences)
            {
                foreach (var site in sequence.SiteCollections)
                {
                    foreach (var template in site.Templates)
                    {
                        if(Template.Templates.FirstOrDefault(t => t.Id == template) == null)
                        {
                            issues.Add($"Template {template} referenced in site {site.Id} is not present in tenant template.");
                        }
                    }
                    foreach(var subsite in site.Sites.Cast<TeamNoGroupSubSite>())
                    {
                        foreach (var template in subsite.Templates)
                        {
                            if (Template.Templates.FirstOrDefault(t => t.Id == template) == null)
                            {
                                issues.Add($"Template {template} referenced in subsite with url {subsite.Url} in site {site.Id} is not present in tenant template.");
                            }
                        }
                    }
                }
            }
            foreach(var template in Template.Templates)
            {
                var used = false;
                foreach(var sequence in Template.Sequences)
                {
                    foreach(var site in sequence.SiteCollections)
                    {
                        if(site.Templates.Contains(template.Id))
                        {
                            used = true;
                            break;
                        }

                        foreach(var subsite in site.Sites)
                        {
                            if(subsite.Templates.Contains(template.Id))
                            {
                                used = true;
                                break;
                            }
                        }
                        if (used)
                        {
                            break;
                        }
                    }
                    if(used)
                    {
                        break;
                    }
                }
                if(!used)
                {
                    issues.Add($"Template {template.Id} is not used by any site in the tenant template sequence.");
                }
            }
            if(issues.Any())
            {
                WriteObject(issues, true);
            }

        }
    }
}