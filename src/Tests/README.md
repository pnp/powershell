# Unit tests / Integration tests

Unit tests for PnP Powershell are undergoing a revamp, this area may grow or change, and maybe subject to test breakages in the short term.

## Running the tests

Run the tests using the Run-Tests.ps1 script available in the `build` folder of the root of this project. You need to specify a site url, and either a credential manager entry (which can be located in the Windows Credential Manager, the MacOS Keychain, or the SecretManagement module), or a username/password combination.

### With credential manager key
```powershell
./Run-Tests.ps1 -SiteUrl https://contoso.sharepoint.com/sites/tests -CredentialManagerLabel mylabel
```

### With username/password
```powershell
$pw = Read-Host -Prompt "Enter password" -AsSecureString
./Run-Tests.ps1 -SiteUrl https://contoso.sharepoint.com/sites/tests -Username "youruser@name.com" -Password $pw
```

## Tips for Writing Tests

### Organising tests

* The tests are organised in a subfolder and namespace that matches the cmdlets in the main project for easy findability and association of tests.
* Each cmdlet should have its own test class.
* A test placeholder has been pre-generated for cmdlet, a cmdlet may have multiple combinations of test inputs, 
copy the placeholder as required for each combination ensuring the each execution is a test

### Performance

* Aim for quickest possible test, if a test requires a component to be setup that is time consuming or has a long running operation, this 
should be added to the test suite setup as a one time setup.
* [TestInitialize] and [TestCleanup] runs on EACH test, if you have setup requirements for a suite of tests use [ClassInitialize] and [ClassCleanup]

### Sites

* When writing your tests ensure that you utilise the existing sites set out by the requirements for running the suite of tests.
* Site creation for certiain template types can take a long time to execute and risk timing out the test run.

### Distructive Tests

> Important!

* Ensure that for tests the are deletions create the objects requied first, do not assume or use existing assets.
* Be vary careful with the tenant level global settings - if there is potential of losing access to the tenant - mark test and inconclusive. 
These ideally should be run on a demo tenant, an area that does not contain your work

# Notes

## Useful attributes on a test

* [Priority] - assigns priority in tests
* [Ignore]  - disables the test