---
name: Bug report
about: Create a report to help us improve
title: "[BUG]"
labels: bug
assignees: ''

---

### Notice
Many bugs reported are actually related to the PnP Framework which is used behind the scenes. Consider carefully where to report an issue:

1. **Are you using ```Invoke-PnPSiteTemplate``` or ```Get-PnPSiteTemplate```**? The issue is most likely related to the Provisioning Engine. The Provisioning engine is _not_ located in the PowerShell repo. Please report the issue here: https://github.com/pnp/pnpframework/issues.
1. **Is the issue related to the cmdlet itself, its parameters, the syntax, or do you suspect it is the code of the cmdlet that is causing the issue?** Then please continue reporting the issue in this repo.
1. **If you think that the functionality might be related to the underlying libraries that the cmdlet is calling** (We realize that might be difficult to determine), please first double check the code of the cmdlet, which can be found here: https://github.com/pnp/powershell/tree/master/src/Commands. If related to the cmdlet, continue reporting the issue here, otherwise report the issue at https://github.com/pnp/pnpframework/issues

### Reporting an Issue or Missing Feature
Please confirm what it is that your reporting

### Expected behavior 
Please describe what output you expect to see from the PnP PowerShell Cmdlets

### Actual behavior 
Please describe what you see instead. Please provide samples of HTML output or screenshots

### Steps to reproduce behavior
Please include complete code samples in-line or linked from [gists](https://gist.github.com/)

### What is the version of the Cmdlet module you are running?
(you can retrieve this by executing ```Get-Module -Name sharepointpnppowershell* -ListAvailable```)
