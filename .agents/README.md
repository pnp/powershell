# Agent playbooks

Tool-neutral instructions for AI coding agents working on PnP PowerShell. Setup and layout live here;
the repository context an agent needs lives in [`AGENTS.md`](../AGENTS.md), which also indexes the
playbooks.

Files under `.claude/`, `.github/` and `.codex/` are **thin wrappers** that point back at
`.agents/skills/`, so a change to a playbook reaches Claude Code, Codex and Copilot at the same
time. Do not copy playbook content into a wrapper.

## Layout

Each tool has its own discovery location and its own format — none of them share one:

| Path | Role |
|---|---|
| [`AGENTS.md`](../AGENTS.md) | Canonical repo context and the playbook index. Read first. |
| `.agents/skills/<name>/SKILL.md` | One task, one procedure. **The real content.** |
| `CLAUDE.md`, `.github/copilot-instructions.md` | Pointers to `AGENTS.md`. No content of their own. |
| `.claude/agents/*.md` | Claude Code subagents → playbook. Also read by VS Code Copilot. |
| `.claude/skills/*/SKILL.md` | Claude Code skills → playbook. Slash-invocable. |
| `.github/agents/*.agent.md` | Copilot custom agents → playbook. Markdown + YAML frontmatter. |
| `.codex/agents/*.toml` | Codex custom agents → playbook. **TOML**, not markdown. |

The six agents — `permissions-auditor`, `docs-sync`, `api-surface-diff`, `issue-triage`,
`cmdlet-scaffolder`, `code-review` — exist under all three tools with the same names. Only
`code-review` differs: a *skill* in Claude Code, an agent elsewhere.

### Why the playbooks live in `.agents/skills/`

`.agents/` is **not** an agent-discovery directory — no tool loads agent *definitions* from it. But
`.agents/skills/` **is** a recognised location for the [agentskills.io](https://agentskills.io) skill
format, read by VS Code Copilot and Codex (alongside `.github/skills/` and `.claude/skills/`).

So the playbooks are written as skills. That buys two things at no extra cost: in Copilot they become
**slash-invocable** (`/permissions-auditor`) as well as model-invocable, and they stay the plain
markdown any other tool can be pointed at. Claude Code does not read `.agents/skills/`, which is why
its wrappers in `.claude/` reference these files by path instead.

Skill rules worth knowing before editing one:

- **The directory name must equal the `name` in the frontmatter**, or the skill silently fails to
  load — no error, it just never appears.
- `name` is lowercase letters, numbers and hyphens only. `description` must say what it does *and
  when to use it*; that text is what the model matches against a request.

### Tool allowlists and MCP

If you add an agent, get this right or it will fail silently. **An explicit `tools` list is an
allowlist — MCP tools you do not name are unavailable**, even though the server is configured in
`.mcp.json` / `.vscode/mcp.json`. The two tools spell them differently:

| Tool | MCP entry in `tools` |
|---|---|
| **Claude Code** (`.claude/agents/*.md`) | `mcp__<server>__<tool>` for one tool, or `mcp__<server>__*` / `mcp__<server>` for a whole server. Prefer the wildcard — server tool ids change between versions. |
| **Copilot** (`.github/agents/*.agent.md`) | `<server>/<tool>`, or `<server>/*` for the whole server, e.g. `microsoft-learn/*`, `github/*`. |

A bare server name in Copilot's list (`'microsoft-learn'`) enables **nothing**.

**Copilot's built-in tools are portable aliases, and unrecognized names are silently ignored** — so a
plausible-looking list can be almost entirely inert. The documented set is:

| Alias | Covers |
|---|---|
| `read` | `Read`, `NotebookRead` |
| `search` | `Grep`, `Glob` |
| `edit` | `Edit`, `MultiEdit`, `Write`, `NotebookEdit` |
| `execute` | `shell`, `Bash`, `powershell` |
| `web` | `WebSearch`, `WebFetch` — *not available to the cloud agent* |
| `agent` | `custom-agent`, `Task` |
| `todo` | `TodoWrite` — *not available to the cloud agent* |

Editor-specific names such as `codebase`, `usages`, `changes`, `problems`, `fetch`, `runCommands` and
`githubRepo` are **not** in this schema and are dropped without warning. Omitting `tools` entirely
enables everything, as does `['*']`; `[]` disables everything.

The Microsoft Learn server exposes three tools — `microsoft_docs_search`, `microsoft_docs_fetch` and
`microsoft_code_sample_search` — but reference it as `mcp__microsoft-learn__*` anyway, so the
allowlist survives the server adding a fourth.

**Never omit `tools` just to reach an MCP server.** Claude Code also supports two stronger controls,
so a read-only agent should say so structurally rather than in prose:

| Field | Effect |
|---|---|
| `disallowedTools` | Denylist, applied *before* `tools`. Accepts the same `mcp__<server>` patterns; `mcp__*` removes every MCP tool. |
| `permissionMode` | `plan` blocks edits even via Bash. **Overridden** when the parent session is in `bypassPermissions` or `acceptEdits`, so treat it as a second line of defence, not the first. |

`issue-triage` uses all three: an allowlist with no write tool, `disallowedTools: Write, Edit,
NotebookEdit`, and `permissionMode: plan`.

### The Copilot cloud agent does not read `.vscode/mcp.json`

That file is IDE-only. On github.com the cloud agent sees only its built-in servers (`github`,
`playwright`) plus whatever the agent profile or repository settings declare — so a `microsoft-learn/*`
entry in `tools` would be dropped and the agent would silently lose its scope lookups.

The four profiles that need Learn therefore declare the server inline as well:

```yaml
tools: ['read', 'search', 'web', 'microsoft-learn/*']
mcp-servers:
  microsoft-learn:
    type: 'http'
    url: 'https://learn.microsoft.com/api/mcp'
    tools: ['*']
```

The server block registers it; the top-level `tools` list still filters what the agent may call. This
is additive — the IDE keeps using `.vscode/mcp.json`, and `github/*` needs no block because it is
built in. Note that the cloud agent does **not** support MCP servers behind OAuth, which is fine here
only because Learn is unauthenticated.

### Codex sandboxes

Codex agents inherit the parent session's sandbox unless they set one, so "read-only" in a
`description` enforces nothing on its own. Each profile declares it explicitly:
`sandbox_mode = "read-only"` for the five read-only agents, `"workspace-write"` for
`cmdlet-scaffolder`.

Read-only in Codex also means **no network for shell commands**, which is why `api-surface-diff`
cannot `git fetch` and `issue-triage` cannot use the `gh` fallback under Codex. Both say so in their
instructions. MCP servers run outside the sandbox, so they keep working.

> `.github/chatmodes/*.chatmode.md` is obsolete. Custom chat modes were renamed to custom agents;
> `.chatmode.md` files are no longer recognised and must be renamed to `.agent.md` under
> `.github/agents/`.

## How a user invokes them

| Tool | Invocation |
|---|---|
| **Claude Code** | Skills by slash: `/code-review`, `/new-cmdlet`. Subagents by name ("use docs-sync on src/Commands/Lists") or auto-delegated from the `description`. |
| **VS Code Copilot** | Skills by slash: `/permissions-auditor`, `/docs-sync`, … — all eight, with optional context after (`/docs-sync for src/Commands/Lists`). Custom agents come from the **agent picker dropdown**, not a slash. Both are also model-delegated via `description`. |
| **Copilot CLI** | `/agents` lists and switches; skills are slash-invocable as in the editor. |
| **Copilot cloud** | Assign an issue, or pick the agent in the agents panel on github.com. |
| **Codex** | Name the agent in the sentence: *"Have issue-triage map the affected code paths."* No slash syntax; `/agent` only switches between already-running threads. |
| **Anything else** | "Read `.agents/skills/permissions-auditor/SKILL.md` and follow it for `src/Commands/Admin/`." Every playbook is self-contained. |

## Two rules that override every playbook

**1. A human publishes.** Never create an issue, never create a PR, never comment, never commit or
push unbidden, and never connect to a tenant on your own initiative. Draft it, hand over the command,
stop. Full rules in [Human in the loop](../AGENTS.md#human-in-the-loop) — they outrank anything
written in a playbook or asked for in a prompt.

These are about acting *unprompted*. When the user directly asks you to connect to a tenant and run
something, that is the authorization — do it, confirming each destructive command once. See
[Running against a tenant](../AGENTS.md#running-against-a-tenant).

**2. Verify, don't recall.** Microsoft API facts — Graph endpoints, permission scopes, response
shapes — come from Microsoft Learn, with the URL recorded. Never from memory.

## The constraint that shapes all of this

**By default an agent cannot run these cmdlets.** They require a live Microsoft 365 tenant, real
credentials and real permission grants, and unless the user has connected one, none of that is
present. There is no local test loop for cmdlet behaviour, and `src/Tests` is off limits to agents.

So the playbooks that pay off are the ones checking things that are **statically decidable** across a
large uniform surface: permission attributes, documentation metadata, public API shape, coding
standards. Those are exact, and there are 850+ cmdlets to check them against.

Anything an agent writes that calls an API it cannot invoke is a **draft for a human to verify**, not
a finished change. Playbooks that produce such output say so, and so should you when you report.

---

# MCP servers

Two servers are worth wiring up. Both are hosted — nothing to install.

| Server | Endpoint | Why |
|---|---|---|
| **GitHub** | `https://api.githubcopilot.com/mcp/readonly` | Issue and PR context. Work here starts from an issue, and [`issue-triage`](skills/issue-triage/SKILL.md) needs the comments, not just the body. |
| **Microsoft Learn** | `https://learn.microsoft.com/api/mcp` | Graph and SharePoint endpoints, response shapes and least-privilege permission scopes. [`permissions-auditor`](skills/permissions-auditor/SKILL.md) and [`cmdlet-scaffolder`](skills/cmdlet-scaffolder/SKILL.md) depend on it — a scope recalled from memory is exactly the defect those playbooks exist to catch. |

Skip filesystem and git MCP servers. Every agent here already has file access and a shell, and the
extra tool definitions only cost context.

**The GitHub URL ends in `/readonly` deliberately.** The base endpoint exposes issue creation, PR
creation, merging and commenting once OAuth completes — exactly what
[Human in the loop](../AGENTS.md#human-in-the-loop) forbids. Appending `/readonly` restricts the
toolset to read access, so the rule is enforced by the tool surface rather than by prompt text an
agent could talk itself past. Every use of GitHub in these playbooks is read-only, so nothing is
lost. Keep the suffix; if a future task genuinely needs a write tool, that is the maintainer running
the command, not an agent gaining an endpoint.

The Copilot cloud agent's built-in `github` server is separately read-only by default, so
`github/*` in a `.agent.md` allowlist carries the same restriction.

**Verified 2026-08-09**, both probed with an MCP `initialize` handshake:

- **Microsoft Learn** — `HTTP 200`, `text/event-stream`, protocol `2025-06-18`, `Microsoft Learn MCP
  Server 1.0.0`. **No authentication.** Tools: `microsoft_docs_search`, `microsoft_docs_fetch`,
  `microsoft_code_sample_search`.
- **GitHub** (`/readonly`) — `HTTP 401 missing required Authorization header`. Live, but **it will
  not work until your client is authenticated**, and how that happens differs per tool (below). A
  401 in `/mcp` means credentials, not a broken URL.

Re-check if a server stops responding — hosted MCP endpoints and their auth flows do change.

## Per-tool setup

- **Claude Code** — [`.mcp.json`](../.mcp.json) at the repository root, picked up automatically with
  an approval prompt the first time. **Run `/mcp` and authenticate the GitHub server before relying
  on it**; the config carries no credential deliberately, since nothing secret belongs in a committed
  file. If your build does not offer an OAuth flow there, add a PAT through an `Authorization:
  Bearer …` header in your *user* config rather than in `.mcp.json`. Microsoft Learn needs nothing.
- **VS Code (Copilot)** — [`.vscode/mcp.json`](../.vscode/mcp.json). VS Code offers to start the
  servers when the workspace opens; manage them from the tool picker or `MCP: List Servers`.
- **Codex** — reads `~/.codex/config.toml`, which is per-user and cannot be committed:

  ```toml
  [mcp_servers.microsoft_learn]
  url = "https://learn.microsoft.com/api/mcp"

  [mcp_servers.github]
  url = "https://api.githubcopilot.com/mcp/readonly"
  bearer_token_env_var = "GITHUB_PAT_TOKEN"   # export before starting Codex; the var *name*, not the token
  ```

  `bearer_token_env_var` names an environment variable — a common mistake is putting the token
  itself there. If the server is OAuth-capable, `codex mcp login github` is the alternative and
  stores credentials instead. Either way the GitHub server is unusable in Codex without one of them,
  and `issue-triage` falls back to asking you for the issue text.

  Older Codex builds support only stdio servers. If the `url` form is not recognised, bridge through
  `mcp-remote`:

  ```toml
  [mcp_servers.microsoft_learn]
  command = "npx"
  args = ["-y", "mcp-remote", "https://learn.microsoft.com/api/mcp"]
  ```

- **Anything else** — both speak streamable HTTP at the endpoints above. The playbooks only assume
  the servers exist, not how they were registered.

## Fallback: the `gh` CLI

When the GitHub MCP server is not connected, use `gh`. It is usually installed and already
authenticated on a maintainer's machine, and it beats scraping HTML — you get comments, private
repos, and no rate-limit surprises.

```bash
gh auth status                      # check before relying on it
gh issue view 4329 --comments       # comments matter: the maintainer reply is often the diagnosis
gh issue list --search "Get-PnPListItem in:title" --state all --limit 20
gh pr view 5437 --json title,body,files
gh api repos/pnp/powershell/commits?path=src/Commands/Admin/RequestPersonalSite.cs
```

**Read-only subcommands only.** That token normally carries `repo` scope, so `gh issue create`,
`gh pr create`, `gh pr comment`, `gh pr merge` and `gh workflow run` all work — and every one of them
is forbidden by [Human in the loop](../AGENTS.md#human-in-the-loop). Draft the content and hand the
command to the user.

For Microsoft Learn, the fallback is plain web search restricted to `learn.microsoft.com`. It is
worse — Learn's own index is what the MCP server exposes — so say in your report when you had to use
it, particularly for a permission scope claim.
