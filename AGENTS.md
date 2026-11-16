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

A human contributor is responsible for the change and its public description. These rules override
playbooks and custom agents.

### Agent pull requests

An agent PR contains material agent-authored changes, whether a human or an agent opens it. Prepare
focused changes, verify claims, and provide evidence for the human contributor to review.

- A human must review and understand the final diff and its validation evidence. Do not assume that
  assigning work or asking to create a PR means the review has happened.
- The PR description must disclose the agent's contribution and include validation evidence and
  remaining risks. Follow the repository PR template.
- An agent may open a **draft PR only**, after the human confirms that review and explicitly requests
  the PR.
- For changes to cmdlet or service behavior, passing automated integration-test evidence is required
  before an agent opens a draft PR or recommends that a human open one. Follow
  [Automated integration-test evidence](#automated-integration-test-evidence).
- If the requirements are not met, keep the work local and report what is missing. Do not present
  unverified work as ready.
- Only a human may mark a PR ready for review, approve it, or merge it. An agent review does not
  replace the human review.

### Automated integration-test evidence

For an agent PR that changes cmdlet or service behavior:

- Run relevant automated tests with assertions against the real service in an authorized,
  non-production test tenant. Test the module built from the exact PR commit. Cover the changed
  behavior and a relevant existing behavior or boundary. At least one relevant test must pass; no
  relevant test may fail or be skipped. A build, manual check, mocked test, disabled test, or a run
  with no relevant tests is not integration evidence. For a fix, show the test failing before the
  fix when feasible; do not claim a before-result that was not observed.
- Record the tested commit, test names and source, command with sensitive values redacted, expected
  and actual outcomes, and passed, failed, and skipped counts. Include the operating system,
  PowerShell version, authentication mode, and a sanitized test report or output. Identify whether
  the agent or a human ran the tests; do not claim firsthand verification of a human-reported result.
- Use synthetic or de-identified test data. Before sharing evidence, remove tenant names and URLs,
  account and tenant identifiers, credentials, tokens, customer data, and other sensitive values
  from commands, output, artifacts, and links. Never publish unredacted logs.
- If suitable tests or an authorized tenant are unavailable, report the gap and ask a human to
  provide a suitable automated test or arrange a test run. Do not change `src/Tests`, invent
  results, or substitute a manual test. Do not open or recommend opening the PR until evidence is
  available.
- If the proposed PR head changes, the previous evidence is stale. Rerun the relevant tests and
  update the evidence before an agent pushes or recommends pushing the new commit. A human must
  review the update and evidence before requesting that push.

PRs that do not change cmdlet or service behavior, such as documentation-only changes, do not need
tenant integration tests. Report the checks actually run. This policy does not itself configure or
enforce a CI check.

### Everything else that reaches other people

- **Never commit or push** unless the user asks in that same turn. Never merge, force-push, rewrite
  history, or delete a branch. A request to open an eligible draft PR permits pushing its reviewed
  commits, but does not permit creating commits or publishing unrelated work.
- **Never create an issue.**
- **Never comment on, label, assign, close or reopen** an issue or PR, or request a review. The
  GitHub Copilot code-review service may post findings when a human requests it or enables automatic
  review. Never approve or dismiss a review.
- **Never trigger a workflow, release or deployment**, and never edit anything under `.github/workflows/`
  without being asked directly.
- **Never touch a Microsoft 365 tenant on your own initiative.** Do not decide that a task needs a
  tenant and go connect. See [Running against a tenant](#running-against-a-tenant) for what happens
  when the user does ask.
- Never run untrusted PR code with tenant credentials or privileged tokens. Review the exact commit
  and use an isolated runner with least-privilege access for an authorized integration test. Do not
  use `pull_request_target` or a privileged `workflow_run` to run untrusted PR code with secrets.

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

**By default you cannot run tenant-dependent cmdlets.** They need a live Microsoft 365 tenant, real
credentials and real permission grants. The existing test harness includes tenant-dependent tests
and placeholders; do not assume it covers a change. `src/Tests` is off limits to agents.

Assume this holds unless the user has actually connected you to a tenant — see
[Running against a tenant](#running-against-a-tenant). When they have, verified beats inferred: say
what you ran and what came back.

`build/Run-Tests.ps1` requires administrator privileges, a site URL, and credentials, and runs the
full test project. Some tests can change tenant state, and `PSTestScope` calls
`Set-ExecutionPolicy Unrestricted` on Windows. Do not run the suite without explicit authorization
for tenant and system changes and the required destructive-operation confirmations. The current PR
build compiles the code but does not run these integration tests.

Consequences:

- Code that calls an API you have not verified is a **draft for a human to verify**. List what was
  inferred rather than verified. Compilation alone does not establish runtime behavior.
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
resources/               RegisterPnPAssemblyResolver.ps1 bootstraps the assembly load context that
                         isolates private dependencies; the context itself is
                         src/Commands/Base/PnPAssemblyLoadContext.cs and PnPPowerShellModuleInitializer.cs
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

    protected override void ExecuteCmdlet()
    {
        // ...
    }
}
```

- `Verb-PnPNoun`, approved verb, singular noun. Namespace mirrors the folder.
- Override `ExecuteCmdlet()`, never `ProcessRecord()`.
- Base classes: `PnPWebCmdlet`, `PnPWebRetrievalsCmdlet<T>`, `PnPSharePointCmdlet`,
  `PnPSharePointOnlineAdminCmdlet`, `PnPGraphCmdlet`, `PnPAzureManagementApiCmdlet`,
  `PnPOfficeManagementApiCmdlet`, `PnPGcsCmdlet`, `PnPTasksCmdlet`, `PnPConnectedCmdlet`,
  `BasePSCmdlet`. Picking the wrong one compiles and then fails in a tenant.
- Permission attributes are `"<resource>/<scope>"`. Multiple attributes are ORed; multiple scopes
  inside one attribute are ANDed. **An unrecognised resource prefix is silently treated as
  SharePoint** — `TokenHandler.DefineResourceTypeFromAudience` defaults anything it does not
  recognise to SharePoint, so `"garph/Group.Read.All"` becomes a SharePoint scope, not an error. A
  string with no `/` at all fails the regex and is dropped entirely.
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
  `PSInvalidOperationException` with the original as inner. Under `-ErrorAction Stop` or
  `SilentlyContinue` it goes to `LogError`, which writes `new ErrorRecord(new Exception(message),
  …, ErrorCategory.NotSpecified, null)` — exception type, inner exception, category and target
  object are all lost, so everything the user needs must be in the message text. Under
  `-ErrorAction Ignore` `LogError` is skipped entirely, so the error is **suppressed rather than
  rebuilt** and `$Error` is not populated.

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
   code. Front matter, `## SYNOPSIS` with the **Required Permissions** block *where the cmdlet
   declares API permissions* — a cmdlet carrying `ApiPermissionsNotRequired` must omit it, as every
   such page does today — `## SYNTAX`,
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
- Commit, push, and open a draft PR only as allowed by [Human in the loop](#human-in-the-loop).

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

Use read-only subcommands by default. An explicitly requested draft PR is the only permitted PR
creation, subject to [Human in the loop](#human-in-the-loop). Check availability with
`gh auth status`; if neither MCP nor `gh` is there, fall back to web search and say in your report
that you did.

## Breaking changes

Before replacing or bypassing a call, identify its existing behavior, including relevant side
effects and failure paths. Verify that the change preserves documented behavior, or explicitly
describe an intentional change.

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
| [`release-prep`](.agents/skills/release-prep/SKILL.md) | Checking versions and the changelog before a stable release. |
| [`whatif-audit`](.agents/skills/whatif-audit/SKILL.md) | Sweeping for `-WhatIf`, `-Confirm` and `-Force` that do not do what they promise. |

MCP servers (GitHub, Microsoft Learn), tool layout and how each tool invokes an agent:
[`.agents/README.md`](.agents/README.md).

## Links

- [Documentation](https://pnp.github.io/powershell/)
- [Contributing](https://pnp.github.io/powershell/articles/gettingstartedcontributing.html)
- [Changelog](https://github.com/pnp/powershell/blob/dev/CHANGELOG.md)
- [Migrating 2.x → 3.x](MIGRATE-2.0-to-3.0.md)
