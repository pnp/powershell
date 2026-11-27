# PnP PowerShell MCP server

The [PnP PowerShell MCP server](https://github.com/pnp/pnp-powershell-mcp-server) lets AI assistants work with PnP PowerShell. MCP, the [Model Context Protocol](https://modelcontextprotocol.io), is the standard way for assistants such as GitHub Copilot in Visual Studio Code, GitHub Copilot CLI, Claude Code, Claude Desktop and Cursor to use tools on your machine. With the server added to your assistant, you can ask in plain language for things like "list the sites created this month" or "write a script that reports on sharing links", and the assistant finds the right cmdlets, reads their documentation, runs them against your tenant, and drafts scripts from the [PnP Script Samples](https://pnp.github.io/script-samples/).

The server is a separate community project in its own repository, and is published to the [MCP Registry](https://registry.modelcontextprotocol.io) as `io.github.pnp/pnp-powershell-mcp-server`. It is in preview: its releases are versioned `0.x` and marked as prereleases.

## How it works

The server runs locally on your machine and runs the PnP PowerShell module installed there. It does not sign in by itself: it connects with `Connect-PnPOnline` and your own [application registration](registerapplication.md), like any other PnP PowerShell script, so in Microsoft 365 it can only do what that account or application is permitted to do. On your machine, it runs PowerShell with your own permissions.

## Installing it

You need [PowerShell 7.4 or later](https://aka.ms/powershell) and the PnP PowerShell module, see [Installation](installation.md). The server itself is installed as a .NET tool, which takes the [.NET SDK](https://dotnet.microsoft.com/download):

```powershell
dotnet tool install --global PnP.PowerShell.MCPServer --prerelease
```

Then add the `pnp-powershell-mcp-server` command to your assistant. In Claude Code, for instance:

```powershell
claude mcp add pnp-powershell --scope user -- pnp-powershell-mcp-server
```

Or in the `.vscode/mcp.json` file of Visual Studio Code:

```json
{
    "servers": {
        "PnP PowerShell MCP Server": {
            "type": "stdio",
            "command": "pnp-powershell-mcp-server"
        }
    }
}
```

The [README of the server](https://github.com/pnp/pnp-powershell-mcp-server#readme) describes the setup for each assistant, the tools the server offers and all of its settings.

## Staying in control

An assistant that can run PnP PowerShell can change your tenant. The server asks you to confirm commands whose verb deletes or revokes, such as the `Remove-*`, `Clear-*`, `Reset-*` and `Revoke-*` cmdlets, before it runs them; with an assistant that cannot show that question, it blocks them instead. All other commands run without asking, including the `Set-*`, `Add-*` and `New-*` cmdlets, and `Add-PnPFile`, which overwrites a file that already exists.

To let an assistant look but not touch, set the `PNP_MCP_READONLY` environment variable to `true` in the server configuration of your assistant. The server then refuses the commands whose verb changes things. As it goes by the verb, it is not a sandbox: a few cmdlets with a reading verb still change your tenant, such as `Resolve-PnPFolder`, which creates the folders it does not find. Connecting with an account or application that only has read permissions is what keeps your tenant unchanged.

## Feedback

Report issues with the server and ideas for it in [its own repository](https://github.com/pnp/pnp-powershell-mcp-server/issues), and issues with the cmdlets it runs in the [PnP PowerShell repository](https://github.com/pnp/powershell/issues).
