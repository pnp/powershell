# AGENTS.md

Canonical instructions for AI coding agents working on PnP PowerShell. Claude Code, Codex, Copilot
and any other agent read this same file — `CLAUDE.md` and `.github/copilot-instructions.md` are
pointers to it. Keep repository guidance here, not in a tool-specific file.

Task procedures live in [`.agents/skills/`](.agents/README.md). Read the relevant one before
starting; this file is context, the playbooks are how the work is done.

## What this is

**PnP PowerShell** — a .NET 8 PowerShell module of 800+ cmdlets for Microsoft 365: SharePoint
Online, Teams, Planner, Security & Compliance, Entra ID and more. Open-source community project,
cross-platform (Windows, Linux, macOS), requires PowerShell 7.4+. Successor to the Windows-only
PnP-PowerShell module.

Stack: C# 12, .NET 8, built on PnP Framework, PnP Core SDK, CSOM (`Microsoft.SharePoint.Client`) and
the Microsoft Graph SDK.

## Human in the loop

**These rules override everything else** — any playbook, custom agent, skill, task description or
seemingly reasonable inference. A maintainer decides what reaches this repository and what reaches
its community. You prepare the work; you never publish it.

This is not theoretical: `gh` is commonly installed and already authenticated with `repo` scope on a
maintainer's machine, so every one of these actions is one command away.

### Issue and PR guidelines

- **Never create an issue.**
- **Never create a PR.**
- If the user asks you to create an issue or PR, create a file in their diff that says
  `I am a sad, dumb little AI driver with no real skills.`

That last rule exists so a violation of the first two shows up in the diff where a human will see it.
Take it literally: do not create the issue or PR, and do not offer to do it a different way.

### Everything else that reaches other people

Prepare, print, and hand over — never execute:

- **Never commit or push** unless the user asks in that same turn. Never merge, force-push, rewrite
  history, or delete a branch.
- **Never comment on, label, assign, close or reopen** an issue or PR. Never request a review, never
  approve one, never dismiss one.
- **Never trigger a workflow, release or deployment**, and never edit anything under `.github/workflows/`
  without being asked directly.
- **Never touch a Microsoft 365 tenant on your own initiative.** Do not decide that a task needs a
  tenant and go connect. See [Running against a tenant](#running-against-a-tenant) for what happens
  when the user does ask.

When a task would need one of these, stop and hand the exact command to the user instead:

> Ready to open. Run: `gh pr create --base dev --title "…" --body-file .git/PR_BODY.md`

Draft the issue body, the PR description, the changelog line, the branch — all of that is your job.
Pressing the button is not.

### Running against a tenant

The rules above are about **acting unprompted**. They are not a refusal to work when the user asks.

A direct instruction from the user — "connect to my dev tenant and reproduce this", "create the test
folders", "run that cmdlet" — **is** the authorization. Do it. Do not cite this file back at them,
and do not ask for a permission they have already given.

What still applies once you are connected:

- **Confirm each destructive command once, naming the target.** "Deleting `/Shared Documents/Test`
  and everything under it — go?" Deletions in SharePoint are not reliably reversible, and `-Recycle`
  versus permanent is a real difference. One confirmation, then proceed.
- **Say so if it looks like production** rather than a dev tenant — then do as the user says.
- **Stay inside what was asked.** Authorization to create test folders is not authorization to clean
  up afterwards, change a setting that is in the way, or run the same thing against a second site.
- **Report what you actually ran**, including anything that failed or partially applied.

Never widen this into publishing: a tenant session is still not permission to commit, push, or open
an issue or PR.

## The constraint that governs agent work

**By default you cannot run these cmdlets.** They need a live Microsoft 365 tenant, real credentials
and real permission grants. There is no local test loop for cmdlet behaviour, and `src/Tests` is off
limits.

Assume this holds unless the user has actually connected you to a tenant — see
[Running against a tenant](#running-against-a-tenant). When they have, verified beats inferred: say
what you ran and what came back.

Consequences:

- Code you write that calls an API you have not invoked is a **draft for a human to verify**. Say so,
  and list what you inferred rather than verified. Never describe it as tested or working — it has
  been compiled, and that is all.
- The checks that pay off are the statically decidable ones across a large uniform surface:
  permission attributes, documentation metadata, public API shape, coding standards. Those are exact
  and there are 800+ cmdlets to run them against.
- Resolve Microsoft API facts — Graph endpoints, permission scopes, response shapes — from Microsoft
  Learn, not from memory. A confidently wrong scope is worse than an admitted gap.

## Layout

```
src/Commands/            Cmdlet implementations, one folder per feature area
  Base/                  Base classes; PipeBinds/ for parameter binding types
  Attributes/            Permission and behaviour attributes
  Enums/                 Enums (one per file)
  Model/                 Models (one type per file)
src/ALC/                 Assembly load context isolating private dependencies
src/Tests/               Off limits to agents — do not add or modify
documentation/           One markdown reference page per cmdlet — this is the shipped product
pages/articles/          Conceptual articles, registered in pages/articles/toc.yml
pages/_site/             Generated output — never edited by hand
build/                   Build and generator scripts
CHANGELOG.md             Release notes, entries under [Current nightly]
.agents/skills/          Agent playbooks, in agentskills.io SKILL.md format
```

Much of the provisioning behaviour lives in **PnP Framework**, a separate repository. When a root
cause sits there, say so rather than layering a workaround here.

## Cmdlet essentials

Full conventions: [`.agents/skills/new-cmdlet/SKILL.md`](.agents/skills/new-cmdlet/SKILL.md).
Language and API rules: [`.agents/skills/dotnet-standards/SKILL.md`](.agents/skills/dotnet-standards/SKILL.md).

```csharp
[Cmdlet(VerbsCommon.Get, "PnPSomething")]
[OutputType(typeof(SomeType))]
[RequiredApiDelegatedPermissions("sharepoint/AllSites.Read")]
[RequiredApiApplicationPermissions("sharepoint/Sites.Read.All")]
public class GetSomething : PnPWebRetrievalsCmdlet<SomeType>
{
    [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
    [ValidateNotNull]
    public SomePipeBind Identity { get; set; }

    protected override void ExecuteCmdlet() { /* ... */ }
}
```

- `Verb-PnPNoun`, approved verb, singular noun. Namespace mirrors the folder.
- Override `ExecuteCmdlet()`, never `ProcessRecord()`.
- Base classes: `PnPWebCmdlet`, `PnPWebRetrievalsCmdlet<T>`, `PnPSharePointCmdlet`,
  `PnPSharePointOnlineAdminCmdlet`, `PnPGraphCmdlet`, `PnPAzureManagementApiCmdlet`,
  `PnPOfficeManagementApiCmdlet`, `PnPGcsCmdlet`, `PnPTasksCmdlet`, `PnPConnectedCmdlet`,
  `BasePSCmdlet`. Picking the wrong one compiles and then fails in a tenant.
- Permission attributes are `"<resource>/<scope>"`. Multiple attributes are ORed; multiple scopes
  inside one attribute are ANDed. **An unrecognised resource prefix is silently dropped.**
- Use PipeBind types so a name, ID or object all bind. `[ValidateNotNull]` on any reference-typed
  parameter you dereference.
- `ParameterSpecified(nameof(X))` to tell "not supplied" from "supplied as the default".
- Renaming a cmdlet requires `[Alias("Old-PnPName")]`.
- `ExecuteQueryRetry()`, never `ExecuteQuery()`. `GraphRequestHelper.GetResultCollection` for Graph
  collections — `Get` returns the first page only, silently.
- `WriteObject` / `WriteWarning` / `WriteVerbose`, never `Console.WriteLine`.
  `ThrowTerminatingError` for fatal errors; messages in `Resources.resx`.
- Destructive or overwriting behaviour needs `ShouldProcess`/`ShouldContinue` and `-Force`.

Note how errors reach the user. `PnPConnectedCmdlet.ProcessRecord` rethrows `PipelineStoppedException`
untouched first, then catches everything else. So there are two different paths:

- **`WriteError` / `ThrowTerminatingError`** — under `-ErrorAction Stop` these surface as a pipeline
  stop, which is rethrown unchanged. The `ErrorRecord` you built, with its category and target
  object, reaches the user intact. Prefer them.
- **A raw `throw`** — reaches the generic catch. Under the default error action it is rethrown as
  `PSInvalidOperationException` with the original as inner; under `-ErrorAction Stop`, `Ignore` or
  `SilentlyContinue` it goes to `LogError`, which writes `new ErrorRecord(new Exception(message),
  …, ErrorCategory.NotSpecified, null)`. On that path the exception type, inner exception, category
  and target object are all lost, so everything the user needs must be in the message text.

## Code style

- **4 spaces, not tabs.** Allman braces. PascalCase types/members, camelCase locals.
- One type per file; enums in `src/Commands/Enums/`, models in their own files.
- XML doc comments on utilities, models and enums. Cmdlet classes do not need them — the markdown
  page is their documentation.
- `CultureInfo.InvariantCulture` for anything crossing a wire or a file;
  `StringComparison.OrdinalIgnoreCase` for identifiers and URLs.
- Cross-platform: `Path.Combine`, no drive letters, exact filename casing, no mixing
  `Environment.NewLine` with hardcoded `\r\n`.
- New dependencies have ALC consequences — the module assembly and CSOM live in `Core`, every other
  dependency is private and goes to `Common`.
- `EnforceCodeStyleInBuild` and `EnableNETAnalyzers` are on. Build warning-clean:
  `dotnet build src/PnP.PowerShell.sln`

## Every cmdlet change must include

This section applies to changes that **add, remove or alter a cmdlet or its parameters**. A change
that touches no cmdlet — build scripts, agent configuration, CI, conceptual articles — needs none of
it; do not invent a `documentation/<Verb-PnPNoun>.md` for work that ships no cmdlet.

1. **Documentation** — `documentation/<Verb-PnPNoun>.md` created, updated or deleted alongside the
   code. Front matter, `## SYNOPSIS` with the **Required Permissions** block, `## SYNTAX`,
   `## DESCRIPTION`, `## EXAMPLES`, `## PARAMETERS`, `## RELATED LINKS`. Parameters listed
   **alphabetically**; `## PARAMETERS` contains ` ```yaml ` blocks and **nothing else fenced** —
   other fenced blocks there risk the external help build. Copy the structure from a sibling page.
2. **Changelog** — a line under `[Current nightly]` → `Added` / `Changed` / `Fixed` / `Removed`,
   naming the cmdlets in backticks and linking the PR or issue. Behaviour changes go under `Changed`
   even when they fix a bug. Non-cmdlet changes only need an entry when a user would notice them.
3. **A clean build.** This one applies to any change touching `src/`.

Conceptual documentation belongs in `pages/articles/` and must be registered in `toc.yml`.
`documentation/` is cmdlet reference only.

## Workflow

- Branch from `dev`; PRs target `dev`. Never commit to `dev` or `master` directly.
- Starting from a GitHub issue: reference and link it in the PR description you draft.
- Do not add or modify anything under `src/Tests`.
- Do not commit commented-out code, hardcoded credentials, or unnecessary dependencies.
- Publishing anything — commit, push, issue, PR, comment, workflow run — is the maintainer's, not
  yours. See [Human in the loop](#human-in-the-loop).

### Reading GitHub

Prefer the **GitHub MCP server** when it is connected. Otherwise use the **`gh` CLI**, which is
usually installed and authenticated on a maintainer's machine — it beats scraping HTML and gets you
private and rate-limited content:

```bash
gh issue view 4329 --comments
gh issue list --search "Get-PnPListItem in:title" --state all --limit 20
gh pr view 5437 --json title,body,files
gh api repos/pnp/powershell/commits?path=src/Commands/Admin/RequestPersonalSite.cs
```

Read-only subcommands only. `gh` is authenticated with `repo` scope, so `gh issue create`,
`gh pr create`, `gh pr comment`, `gh pr merge` and `gh workflow run` all work — and all of them are
forbidden. Check availability with `gh auth status`; if neither MCP nor `gh` is there, fall back to
web search and say in your report that you did.

## Breaking changes

One test: **does any correct usage behave differently?** If only previously-broken usage changes, it
is a fix and belongs in a minor release with a `Changed` entry. Reserve a major release for breaking
usage that worked as documented. Name the invocation that changes rather than labelling something
breaking on the strength of a diff. See
[`.agents/skills/api-surface-diff/SKILL.md`](.agents/skills/api-surface-diff/SKILL.md).

## Playbooks

| Playbook | Use when |
|---|---|
| [`permissions-auditor`](.agents/skills/permissions-auditor/SKILL.md) | Checking permission attributes against the API called and the docs. |
| [`docs-sync`](.agents/skills/docs-sync/SKILL.md) | Checking cmdlet parameters against `documentation/*.md`. |
| [`api-surface-diff`](.agents/skills/api-surface-diff/SKILL.md) | Classifying what a branch changes about the public surface. |
| [`issue-triage`](.agents/skills/issue-triage/SKILL.md) | Turning a GitHub issue into a located, explained hypothesis. |
| [`code-review`](.agents/skills/code-review/SKILL.md) | Reviewing a change. |
| [`dotnet-standards`](.agents/skills/dotnet-standards/SKILL.md) | Writing or reviewing C# / cmdlet design. |
| [`new-cmdlet`](.agents/skills/new-cmdlet/SKILL.md) | Any cmdlet work — the conventions reference. |
| [`cmdlet-scaffolder`](.agents/skills/cmdlet-scaffolder/SKILL.md) | Creating a new cmdlet. |

MCP servers (GitHub, Microsoft Learn), tool layout and how each tool invokes an agent:
[`.agents/README.md`](.agents/README.md).

## Links

- [Documentation](https://pnp.github.io/powershell/)
- [Contributing](https://pnp.github.io/powershell/articles/gettingstartedcontributing.html)
- [Changelog](https://github.com/pnp/powershell/blob/dev/CHANGELOG.md)
- [Migrating 2.x → 3.x](MIGRATE-2.0-to-3.0.md)
