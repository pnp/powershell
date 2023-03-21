# Running the Unit Tests

Effectively the unit tests are not unit tests but should be more thought of as an integration test as we run the cmdlets against an actual site. 

> [!NOTE] 
> These unit tests are not actively being used at the moment. Please ensure you test your cmdlets manually before submitting a PR. We do have a build process that verifies if your PR will compile, but it will not test if your submission leads to the expected result.

## Setting up your environment

Create a (modern) site collection at any location in your tenant. This site collection will be used for testing. 

## Running the test script

In the build folder of this project you'll find a `Run-Tests.ps1` file. This script allows you to run the tests

### With a Stored Credential (Recommended)

You can use a stored credential to authenticate to your site. We recommend using the `Microsoft.PowerShell.SecretsManagement` module. For more information about setting that one up see [Authentication](authentication.md)

```powershell
./Run-Tests.ps1 -SiteUrl "https://yourtenant.sharepoint.com/sites/yoursite" -CredentialManagerLabel "yourlabel"
```

### With credentials

```powershell
$password = ConvertTo-SecureString "P@ssW0rD!" -AsPlainText -Force
$username = "yourname@domain.com"
./Run-Tests.ps1 -SiteUrl "https://yourtenant.sharepoint.com/sites/yoursite" -Username $username -Password $password
```

If you do not specify the password parameter you will be prompted to enter the password:

```powershell
$username = "yourname@domain.com"
./Run-Tests.ps1 -SiteUrl "https://yourtenant.sharepoint.com/sites/yoursite" -Username $username
Enter password: 
```