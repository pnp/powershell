# CLAUDE.md

Repository guidance for this project lives in **[AGENTS.md](AGENTS.md)** — read it first. It is the
single source shared by Claude Code, Codex, Copilot and any other agent, so that guidance never
drifts between tools.

Task procedures live in [`.agents/skills/`](.agents/README.md).

Add new repository guidance to `AGENTS.md` or a playbook, **never to this file** — anything written
here is invisible to the other tools.

## Read this before touching GitHub

**Never create an issue. Never create a PR.** Never comment, approve, merge, or push unbidden, and
never connect to a Microsoft 365 tenant on your own initiative. `gh` is authenticated here with
`repo` scope, so all of that is one command away — draft the content, hand over the command, stop.
Full rules: [Human in the loop](AGENTS.md#human-in-the-loop).

This is about acting unprompted. When the user asks you directly to connect to a tenant and run
something, that is the authorization — do it, confirming each destructive command once. See
[Running against a tenant](AGENTS.md#running-against-a-tenant).

## Claude Code specifics

- **Subagents**: `.claude/agents/` — `permissions-auditor`, `docs-sync`, `api-surface-diff`,
  `issue-triage`, `cmdlet-scaffolder`, `release-prep`, `whatif-audit`. Each one is a thin wrapper delegating to its playbook.
  The same set exists for Copilot (`.github/agents/*.agent.md`) and Codex (`.codex/agents/*.toml`),
  pointing at the same playbooks — keep the names in step when adding one.
- **Skills**: `.claude/skills/` — `code-review`, `new-cmdlet`. Same arrangement.
- **MCP**: `.mcp.json` configures the GitHub and Microsoft Learn servers. See
  [`.agents/README.md`](.agents/README.md).

`docs-sync`, `permissions-auditor`, `whatif-audit` and `api-surface-diff` are read-only and fan out well across the
800+ cmdlets. For a whole-repository or multi-folder sweep, run them in parallel over separate
folders under `src/Commands/`. For one folder or a changed-file set, use a single agent or do the
check inline — each subagent adds to the session's cost.

`docs-sync` and `api-surface-diff` pin `model: sonnet` in their frontmatter because their work is
mechanical extraction. The others inherit the session model.
