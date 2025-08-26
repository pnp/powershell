# Using PnP PowerShell in Docker containers

Using Docker allows you to use any software inside virtual environments, without having to install this software directly on your laptop or server. These virtual environments are containers, which might be imagined as lightweight virtual machines. Having their own virtual disks, memory and processes, containers are rationally isolated from your laptop operating system, providing you a great way of experimenting and using any version of PnP.PowerShell without installing it.

## Why should I run PnP PowerShell in Docker

The main advantage of running PnP PowerShell in Docker containers is that you will always have the exact same environment in which your scripts run, guaranteed. This in contrary to when you run it directly within your operating system. Updates to the operation system, file structure, PowerShell version, the .NET Framework, security and system patches, they all can have an impact on the successfull execution of your script. With Docker you are guaranteed that every time you run your script, it runs in exactly the same environment as where you built and tested it, so you know it will always work.

Another advantage is that it becomes easier to work with different versions of PnP PowerShell on one environment. Say you build and test a script with PnP PowerShell 2.12.0 and then schedule it to run unattended every night. Few month later you start on another project which you're going to base on PnP PowerShell 3.1.0 because you want to leverage some new features in that release. You ideally don't want to just upgrade to 3.1.0 and assume your 2.12.0 script will still work in it. Even more so because a new major version, going from 2.x to 3.x in this case, means there are likely breaking changes. In particular with this sample, the underlying .NET Framework has changed and the PowerShell 7 version it requires has changed. So you either have to thoroughly test and potentially rewrite all your older scripts, or, use Docker so you can keep running your 2.12.0 script in 2.12.0 with the appropriate .NET Framework version and PowerShell version you built it against, while writing, testing and eventually running your new script in 3.1.0 with the new .NET Framework, the new PowerShell 7 version, both on the same machine, with no conflicts and no risks.

And a last arguement, if you're still not convinced, is that your script will run in an isolated container. Meaning it only has access to what you want it to have access to. If you run it locally, it has the same permissions and access as you would have, which is likely way more than it needs, thus opposes a risk.

## How can I use it

The good news is that you will not even need to install PnP.PowerShell in containers by your own: the PnP team is already publishing Docker container images for each stable and nightly release, [here](https://hub.docker.com/r/m365pnp/powershell). You will however need to install docker runtime.

If you use Windows, we would recommend you using Linux containers with help of WSL. Alternatively, even though it is not the most common way, you might want to run PnP.PowerShell in Windows containers. Mind you that some use cases might be limited when using Windows containers. If you use Mac OS or Linux, the easiest way is to use Linux containers.

### Play with PnP.PowerShell online

You can try using m365pnp/powershell Docker containers online, without installing anything on your desktop/laptop.

1. Open https://labs.play-with-docker.com/

2. Login

3. Click `Start`

4. Click `+ ADD NEW INSTANCE`

5. Run in the online console:

    ```powershell
    docker run --rm -it m365pnp/powershell
    ```

After that you can start running commands like `Connect-PnPOnline`.

### Installing Docker locally

- Windows:

    ```powershell
    iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
    choco install -y docker-engine
    ```

    Or just download and install Docker manually from [here](https://www.docker.com/get-started/).

- Mac OS:

    1. Install

    ```bash
    brew install --cask docker
    ```

    2. Launch Docker:

        - Press ⌘ + Space to bring up Spotlight Search and enter Docker to launch Docker.
        - In the Docker needs privileged access dialog box, click OK.
        - Enter password and click OK.

- Linux:

    ```bash
    bash <(curl -s https://get.docker.com/)
    sudo usermod -aG docker $USER
    newgrp docker
    ```

- Linux containers on Windows (using WSL):

    1. Start installing WSL:

        ```powershell
        Enable-WindowsOptionalFeature -Online -FeatureName "Microsoft-Windows-Subsystem-Linux", "VirtualMachinePlatform" -NoRestart
        Invoke-WebRequest -Uri https://wslstorestorage.blob.core.windows.net/wslblob/wsl_update_x64.msi -OutFile wsl_update_x64.msi -UseBasicParsing
        Start-Process msiexec.exe -Wait -ArgumentList "/I $((Get-Location).Path)\wsl_update_x64.msi /quiet"
        ```

    2. Reboot the machine before you continue

    3. Finish installing WSL:

        ```powershell
        wsl --set-default-version 2
        wsl --install --distribution Ubuntu
        ```

    4. Install docker in WSL:

        ```bash
        bash <(curl -s https://get.docker.com/)
        sudo usermod -aG docker $USER
        newgrp docker
        ```

## Using PnP.PowerShell

Sometimes you want to run inline PnP.PowerShell commands.

- Latest stable version:

    ```bash
    docker run --rm -it m365pnp/powershell:latest
    ```

- Latest nightly version:

    ```powershell
    docker run --rm -it m365pnp/powershell:nightly
    ```

After that you can start running commands like `Connect-PnPOnline`.

Mind you that in the case above, the container will have an isolated disk system so the commands that you run inside the container will not be able to access files from your host machine. However, sometimes you might want to run a script or use some files from your host OS (for example, when you have a ps1 file in a git repository that you cloned to your laptop). In this case you will need to mount a directory on your host as a volume inside the container. See the following examples.

- Linux/WSL/Mac OS:

    ```bash
    docker run --rm -it -v "$(pwd):/home" -w /home m365pnp/powershell
    ```

- Windows (run in PowerShell console):

    ```powershell
    docker run --rm -it -v "${pwd}:C:\workplace" -w C:\workplace m365pnp/powershell
    ```

In such container you can run `Get-ChildItem` and see the contents of the current directory.

The examples above start new container and allow you to interactively use container, running inline commands. In other cases however, you might want to just start a container for a short time (a few seconds maybe) and run a ps1 script unattended and then automatically terminate the container. Here is how you can do it.

- Linux/WSL/Mac OS:

    ```bash
    docker run --rm -v "$(pwd):/home" -w /home m365pnp/powershell pwsh test.ps1
    ```

- Windows (run in PowerShell console):

    ```powershell
    docker run --rm -v "${pwd}:C:\workplace" -w C:\workplace m365pnp/powershell:1.10.0-nanoserver-1809 pwsh test.ps1
    ```

Finally, your scripts might have parametrization so that you can run the same code in different cases/environments. This is the way to provide variable values.

- Linux/WSL/Mac OS:

    ```bash
    ParameterValue="test"
    docker run --rm -v "$(pwd):/home" -w /home m365pnp/powershell pwsh -c "./test.ps1 -Parameter1 $ParameterValue"
    ```

- Windows (run in PowerShell console):

    ```powershell
    $ParameterValue="test"
    docker run --rm -v "${pwd}:C:\workplace" -w C:\workplace m365pnp/powershell:1.10.0-nanoserver-1809 pwsh -c "./test.ps1 -Parameter1 $ParameterValue"
    ```

Please see [Docker documentation](https://docs.docker.com/engine/reference/run/) to see arguments for `docker run` command.

## Featured tags

### Latest

* latest: The latest stable image (i.e. 3.1.0)

  * `docker pull m365pnp/powershell:stable` or `docker pull m365pnp/powershell:latest` or even more simple just `docker pull m365pnp/powershell`

### Nightly

* nightly: The latest nightly image (i.e. 3.1.127-nightly)

  * `docker pull m365pnp/powershell:nightly`

## Tag explanation

Tags names mean the following:

`<version>[-nightly/-stable]-<architecture>`

Currently supported architectures:

* [windows-amd64](/pnp/powershell/blob/dev/docker/windows-amd64.dockerfile): Windows NanoServer LTSC 2025 64 bits
* [linux-arm64](/pnp/powershell/blob/dev/docker/linux-arm64.dockerfile): Linux Debian Bullseye Slim 64 bits for ARM devices (i.e. Raspberry Pi 4 or later)
* [linux-amd64](/pnp/powershell/blob/dev/docker/linux-amd64.dockerfile): Alpine 64 bits

Tag name examples:

* nightly - auto picks the correct architecture with the latest available nightly build
* stable - auto picks the correct architecture with the latest available stable build (same as latest)
* latest - auto picks the correct architecture with the latest available stable build (same as stable)
* 3.1.127-nightly - auto picks the correct architecture with the 3.1.127 nightly build
* 3.1.127-nightly-windows-amd64 - forces the Windows 64 bit architecture with the 3.1.127 nightly build
* 3.1.0-stable - auto picks the correct architecture with the 3.1.0 stable build
* 3.1.0-stable-linux-amd64 - forces the Linux 64 bit architecture with the 3.1.0 stable build

To find an overview of all the available tags please visit https://hub.docker.com/r/m365pnp/powershell/tags
