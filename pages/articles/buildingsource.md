# Building the source code

Make a clone of the repository, navigate to the `build` folder in the repository and run `Build-Debug.ps1`. 

This will restore all references, and copy the required files to the correct location on your computer. 

Notice that we refer to the nuget package of the PnP.Framework project. As this is rebuilt every night you will receive a new version of the PnP.Framework package every day.

If you run on Windows both the .NET Framework and the .NET Core version will be build and installed. 

If you run on MacOS or Linux only the .NET Core version will be build and installed. 

> [!Important] 
> Unlike the older repository for the legacy version of PowerShell for Windows you do not need to have local clone of the PnP Framework repository anymore (we changed the PnP Sites Core library used under the hood to the PnP Framework repository, see for more info about that library here: https://github.com/pnp/pnpframework).
