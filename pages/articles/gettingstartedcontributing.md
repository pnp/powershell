# Contribution guidance

Sharing is caring! All contributions to this repository are very welcome. This guidance should help you getting started contributing to PnP PowerShell by just following some easy steps.

There are various ways to accomplish the same goal. We'll go through a process here that should be easy to follow and accomplish for anyone. If you prefer using other tools over the ones mentioned here, such as using the cloning feature within Visual Studio, of course, feel free to use that instead.

## Getting started

Follow the paragraphs below to get yourself started with contributing to PnP PowerShell.

## Installing Git Tools

We'll be using the command line Git Tools to complete the steps. If you prefer using other tools, such as Visual Studio or the desktop client of Git, feel free to use that instead.

1. If you haven't got them already, install the Git Tools for your environment. They're available for Windows, Linux and Mac. Simply download the latest installer from: https://git-scm.com/downloads

  ![Downloading Git Tools](./../images/contributing/downloadgittools.png)

  ![Downloading Git Tools](./../images/contributing/downloadgittools2.png)

1. There will be a scary amount of questions asked during the installer. Just use all defaults and next-next-finish through the installation process
   
   ![Downloading Git Tools](./../images/contributing/downloadgittools3.png)

## Create your own Fork

To contribute to a GitHub project, what you do first, is create a thing called a fork. Basically it means you will get your own copy of the sourcecode. To do so, follow the steps below.

1. Go to the [PnP PowerShell repository](https://github.com/pnp/powershell) on GitHub

1. Make sure you're logged on to GitHub. If you dont have a GitHub account yet, create one and log on first before you continue.

1. Click the **Fork** button in the top right corner of the page or use this [direct link](https://github.com/pnp/powershell/fork) instead
   
   ![Forking the repository](./../images/contributing/createfork.png)

1. If it shows you a notice like this, you already have a fork. Continue with the next paragraph.

  ![Fork already exists](./../images/contributing/forkalreadyexists.png)

  If it instead shows something like this, click on **Create fork** to create your own fork. You can leave all the defaults.

  ![Fork does not exist yet](./../images/contributing/createnewfork.png)

  ![Fork does not exist yet](./../images/contributing/forkbeingcreated.png)

## Updating your Fork

Now that you have your own fork, you need to make sure it's up to date with the latest changes from the main repository. Do this every time before you start working on a change. If you don't do so, it will become much harder for us to review and merge your changes. To update your fork, follow the steps below.

1. First identify if your fork is already up to date. If it is, it will show you a message like this:

  ![Fork is up to date](./../images/contributing/forkuptodate.png)

  If it instead shows something like this, you need to update your fork first:

  ![Fork is not up to date](./../images/contributing/forkisnotuptodate.png)