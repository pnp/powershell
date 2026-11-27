# Using PnP PowerShell in Azure Automation Runbooks

In this article we will setup an Azure Automation Rubook to use PnP PowerShell.

## Create the Azure Automation Runbook

As the UI in [the Azure Portal](https://portal.azure.com) changes every now and then, but the principles stay the same, follow the following steps:

1. Go to the [Azure Portal](https://portal.azure.com) and login with your Azure credentials

1. Create a new Azure Automation Account using the **Create a resource** button and searching for **Automation** or use this [direct link](https://portal.azure.com/#create/Microsoft.AutomationAccount) to locate it
   
   ![Creating an Azure resource](./../images/azureautomation/createresource.png)

   ![Creating an Azure Automation resource](./../images/azureautomation/createautomationresource.png)

1. Fill out the details for the Azure Automation Account as desired and click **Review + Create** at the bottom left followed by clicking on **Create** on the review screen

   ![Create Automation Resource basics](./../images/azureautomation/automationcreateinstance.png)

1. Once the resource has been created, click on **Go to resource** to open the Azure Automation resource

   ![Go to resource](./../images/azureautomation/createautomationgotoresource.png)

## Configure the Azure Automation Account

Now your Azure Automation Account has been created, proceed with the next paragraphs to configure it for using PnP PowerShell.

### Add the PnP PowerShell module to the Azure Automation Account

PnP PowerShell 3.x requires PowerShell 7.4 or later, so it runs on the PowerShell 7.4 and 7.6 runtime versions of Azure Automation. These are only available through a [Runtime environment](https://learn.microsoft.com/azure/automation/runtime-environment-overview). The PowerShell 7.1 and 7.2 runtime versions, which only run PnP PowerShell 2.12.0 or older, are no longer supported by Azure Automation since 30 September 2026. PowerShell 7.4 itself reaches end of support on 10 November 2026, so use 7.6 for new runbooks.

To add PnP PowerShell to the Azure Automation Account, follow these steps:

1. In your Azure Automation Account, select **Runtime Environments** under **Process Automation**. If it is not there, first select **Try Runtime environment experience** on the **Overview** page.

1. Select **Create**, enter a name for the Runtime environment, select **PowerShell** as the **Language** and **7.6** as the **Runtime version**, and select **Next**.

1. On the **Packages** tab, add PnP PowerShell using one of the following options, then select **Next** and **Create**. Importing the module can take several minutes.

#### Stable version

   Select **Add from gallery**, search for **PnP.PowerShell** and select it.

#### Latest prerelease version

   If you wish to use the latest prerelease/nightly build version, open up a PowerShell 7 console and execute:

   ```powershell
   Save-Module PnP.PowerShell -AllowPrerelease -Path c:\temp
   ```

   This creates a folder `c:\temp\PnP.PowerShell` holding a folder named after the version number. Rename that version folder to `PnP.PowerShell`, as Azure Automation only imports a module from a folder that has the name of the module, and compress it into a ZIP file. Select **Add a file** and select the ZIP file.

## Decide how you want to authenticate in your Azure Automation Runbooks

### By using a Managed Identity

The recommended option is to use a [managed identity in Azure](https://learn.microsoft.com/azure/active-directory/managed-identities-azure-resources/overview) to allow your Azure Automation Runbook to connect to Microsoft Graph or SharePoint Online using PnP PowerShell. Using this method, you specifically grant permissions for your Azure Runbook to access these permissions, without having any client secret or certificate pair that potentially could fall into wrong hands. This makes this option the most secure option by far. Since version 1.11.95-nightly, Managed Identities are both supported against SharePoint Online as well as Microsoft Graph cmdlets. Before this version, only Microsoft Graph was being supported.

#### Enabling the managed identity for an Azure Automation Runbook

1. In your Azure Automation account, in the left menu, go to **Identity** under Account Settings

1. Ensure you are on the **System assigned** tab and flip the switch for Status to On, if not already done

1. Click the **Save** button and confirm your action in the dialog box that will be shown

A new entry will now automatically be created in your Azure Active Directory for this app having the same name as your Azure Function and the Object (principal) ID shown on this page. Take notice of the Object (principal) ID. We will need it in the next section to assign permissions to.

#### Assigning permissions to the managed identity

Next step is to assign permissions to this managed identity so it is authorized to access the Microsoft Graph and/or SharePoint Online.

1. If you don't know which permissions exist yet, you can use the below sample to get a list of all available permissions:

    ```powershell
    Get-PnPAzureADServicePrincipal -BuiltInType MicrosoftGraph | Get-PnPAzureADServicePrincipalAvailableAppRole
    Get-PnPAzureADServicePrincipal -BuiltInType SharePointOnline | Get-PnPAzureADServicePrincipalAvailableAppRole
    ```

1. Once you know which permissions you would like to assign, you can use the below samples. Note that the Principal requires the object Id (not the application/client id) or the application name.

   ```powershell
   Add-PnPAzureADServicePrincipalAppRole -Principal "62614f96-cb78-4534-bf12-1f6693e8237c" -AppRole "Group.Read.All" -BuiltInType MicrosoftGraph
   Add-PnPAzureADServicePrincipalAppRole -Principal "mymanagedidentity" -AppRole "Sites.FullControl.All" -BuiltInType SharePointOnline
   ```

## Create a Runbook

We're now ready to create a Runbook in which your PnP PowerShell script will run.

1. In the Azure Portal, in the left menu, click on **Runbooks** under **Process Automation**

   ![Navigate to Runbooks](../images/azureautomation/azureportaladdrunbookmenuitem.png)

1. Click on **Create a runbook** at the top of the screen

   ![Create a Runbook](../images/azureautomation/azureportaladdrunbookoption.png)

1. Give the Runbook a name, select the Runbook type **PowerShell**, select the Runtime environment you created in which PnP PowerShell has been added and click on **Create** at the bottom left.

1. On the Edit PowerShell Runbook page, enter your PnP PowerShell code in the large white area, i.e.:

   ```powershell
   Connect-PnPOnline tenant.sharepoint.com -ManagedIdentity

   Get-PnPMicrosoft365Group
   ```

   Once done, click on **Save** at the top of the screen and then on **Test pane** to test your Runbook.
   
   ![Start coding your Runbook PowerShell](../images/azureautomation/azureportaleditrunbookps.png)

1. Click on **Start** to start testing the Runbook. It might take a few minutes for the Runbook to start. Once it's done, you will see the output of your PnP PowerShell script in the large black output section.

   ![Runbook test output](../images/azureautomation/azureportaltestrunbook.png)
