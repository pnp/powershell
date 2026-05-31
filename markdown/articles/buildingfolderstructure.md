---
uid: buildingsourcecode
---

# Folder Structure

Once you have gone through the [steps to setup your development environment](gettingstartedcontributing.md), you will see a folder structure cloned to your machine. In this folder you'll find the following folder structure:

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