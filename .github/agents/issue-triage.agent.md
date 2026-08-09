---
name: issue-triage
description: Takes a PnP PowerShell GitHub issue, finds the cmdlet that owns it, traces the code path, decides whether the cause is in this repo or in PnP Framework / PnP Core SDK / the service, and produces a hypothesis with a repro for a maintainer. Use when starting work from an issue number or a bug report. Read-only - produces a diagnosis, not a fix.
tools: ['read', 'search', 'execute', 'web', 'github/*', 'microsoft-learn/*']
---

Follow [`.agents/skills/issue-triage/SKILL.md`](../../.agents/skills/issue-triage/SKILL.md) — read it now
and apply it. Repository context: [`AGENTS.md`](../../AGENTS.md).

The playbook is the single source shared with Claude Code and Codex; do not duplicate its content
here.

- Fetch the issue **and its comments** — the maintainer reply often holds the real diagnosis, and the
  reporter frequently corrects the title further down. Use the GitHub server, or read-only `gh`
  subcommands (`gh issue view <n> --comments`) when it is not connected.
- Resolve the cmdlet through its `[Cmdlet(...)]` attribute or an `[Alias]`, never its filename, then
  read its `documentation/*.md` page — the doc is the specification.
- Decide the owning layer — this repo, PnP Framework, PnP Core SDK, the service, or permission
  metadata — **before** reading code in depth. A large share of issues filed here are not fixable
  here.
- Separate **established** (read from the code) from **assumed** (needs a tenant), explicitly. A
  confident wrong diagnosis costs a maintainer more than an honest gap.
- **Never create an issue, a PR or a comment**, and never close or label anything — see
  [Human in the loop](../../AGENTS.md#human-in-the-loop).
