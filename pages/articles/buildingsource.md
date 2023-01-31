---
uid: buildingsourcecode
---
# Building the project

Make a clone of the repository, navigate to the `build` folder in the repository and run `Build-Debug.ps1`. 

This will restore all references and copy the required files to the correct location on your computer. 

Notice that we refer to the nuget package of the PnP.Framework project. As this is rebuilt every night you will receive a new version of the PnP.Framework package every day.

If you run on Windows both the .NET Framework and the .NET Core version will be build and installed. 

If you run on MacOS or Linux only the .NET Core version will be build and installed. 

> [!NOTE] 
> Unlike the older repository for the legacy version of PowerShell for Windows you do not need to have local clone of the PnP Framework repository anymore (we changed the PnP Sites Core library used under the hood to the PnP Framework repository, see for more info about that library here: https://github.com/pnp/pnpframework).

## Folder Structure

In the repository you'll find the following folder structure

```
- build
- documentation
- pages
- resources
- samples
- src/ALC
     /Commands
     /Resources
     /Tests
```

### Build folder
The build folder contains scripts used to build the project, build the Helpfile, etc. While debugging locally the `Build-Debug.ps1` script is the script to use to build the project and copy the correct files to the correct location on your machine. The other build scripts are used in GitHub actions to automate the nightly builds etc.

### Documentation folder
The documentation folder contains the markdown files all describing every single cmdlet available. If you create a new cmdlet we *require* you to also provide a documentation file. Notice that the documentation files *require* a front-matters yaml header as specified in the other files. Updated accordingly.

### Pages folder
The pages folder contains the structure which is published to https://pnp.github.io/powershell. We automatically copy the cmdlet documentation in there at build time, but you can create PRs on the 'articles' folder if you want.

### Resources
The resources folder contains an XML file which is copied into the output folder of the build. This file defines how PowerShell should parse and render the objects shown as output from the PnP PowerShell cmdlets

### src/ALC
Due to possible conflicts with already loaded assemblies in PowerShell we create an Assembly Load Context for a specific assembly. See https://learn.microsoft.com/dotnet/core/dependency-loading/understanding-assemblyloadcontext for more information about ALCs.

### src/Commands
This is the main location of all the cmdlet code.

### src/Resources
Any resources used by cmdlets go into this folder

### src/Tests
This is where the the unit/integration tests reside. See [Running test](runningtests.md) for more information.

# Running against a local copy of the PnP Framework
If your code changes require changes to the PnP Framework you might want to use a local version of the PnP Framework which you modified. In order to do this you will need to have both the PnP.PowerShell repository (https://github.com/pnp/powershell) and the PnP.Framework repository (https://github.com/pnp/pnpframework) locally on your computer. Make sure that both repositories are located at the same folder level. E.g.:

```console
c:\repos\powershell
c:\repos\pnpframework
```

Now, build the PnP Framework by navigating to the `build` folder in the `pnpframework` folder and run `Build-Debug.ps1`. This will compile the PnP Framework.

Next, navigate to the `build` folder of the `powershell` folder and run `Build-Debug.ps1 -LocalPnPFramework`. This will compile PnP PowerShell and refer to the -locally- compiled version of the PnP Framework. If you do not specify the `-LocalPnPFramework` switch it will refer to the latest nightly build available on NuGet.org instead.

# Running against a local copy of the PnP Core SDK
If your code changes require changes to the PnP Core SDK (meaning any of the PnP Core SDK libraries like: PnP.Core, PnP.Core.Auth, PnP.Core.Admin, PnP.Core.Transformation, or PnP.Core.Transformation.SharePoint) you might want to use a local version of the PnP Core SDK which you modified. In order to do this you will need to have both the PnP.PowerShell repository (https://github.com/pnp/powershell) and the PnP.Core repository (https://github.com/pnp/pnpcore) locally on your computer. Make sure that both repositories are located at the same folder level. E.g.:

```console
c:\repos\powershell
c:\repos\pnpcore
```

Now, build the PnP Core SDK by navigating to the `build` folder in the `pnpcore` folder and run `Build-Debug.ps1`. This will compile the whole PnP Core SDK solution, including PnP.Core.Auth, PnP.Core.Admin, PnP.Core.Transformation, and PnP.Core.Transformation.SharePoint.

Next, navigate to the `build` folder of the `powershell` folder and run `Build-Debug.ps1 -LocalPnPCore`. This will compile PnP PowerShell and refer to the -locally- compiled version of the PnP Core SDK. If you do not specify the `-LocalPnPCore` switch it will refer to the latest nightly build available on NuGet.org instead.
