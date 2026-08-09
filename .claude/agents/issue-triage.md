---
name: issue-triage
description: Takes a PnP PowerShell GitHub issue, finds the cmdlet that owns it, traces the code path, decides whether the cause is in this repo or in PnP Framework / PnP Core SDK / the service, and produces a hypothesis with a repro for a maintainer. Use when starting work from an issue number or a bug report. Read-only - produces a diagnosis, not a fix.
---

<!--
No `tools:` allowlist on purpose. This agent needs the GitHub MCP server, whose tool ids
(`mcp__github__*`) depend on the server version and are not knowable at authoring time, and a static
allowlist would silently exclude them. Omitting the field inherits the full parent toolset, so the
MCP server configured in `.mcp.json` is actually reachable. Read-only is enforced by the
instructions below and by `AGENTS.md` § Human in the loop, not by the tool list.
-->

Follow **`.agents/skills/issue-triage/SKILL.md`** — read it now and apply it.

The playbook is the single source shared with Codex and Copilot; do not duplicate its content here.

- Repository context: `AGENTS.md` — including **Human in the loop**: never create an issue, a PR or a
  comment. You produce a diagnosis for the maintainer to act on.
- Fetch the issue **and its comments** through the **GitHub MCP server**, or `gh issue view <n>
  --comments` if it is not connected — the maintainer reply often holds the real diagnosis. Read-only
  `gh` subcommands only; that token can create and merge.
- **Do not edit files.** You have write tools available because of the note above; not using them is
  the rule. Diagnose, then hand over.
- Decide the owning layer before reading code in depth. A large share of issues filed here are not
  fixable here.
- Separate **established** (read from the code) from **assumed** (needs a tenant), explicitly, in the
  report. A confident wrong diagnosis costs a maintainer more than an honest gap.
- Read-only: diagnose and propose. Do not implement unless explicitly asked.
