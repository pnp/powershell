# GitHub Copilot instructions

Repository guidance for this project lives in **[AGENTS.md](../AGENTS.md)** — read it first. It is
the single source shared by Copilot, Claude Code, Codex and any other agent, so that guidance never
drifts between tools.

Task procedures live in [`.agents/skills/`](../.agents/README.md).

Add new repository guidance to `AGENTS.md` or a playbook, **never to this file** — anything written
here is invisible to the other tools.

## Read this before touching GitHub

**Never create an issue. Never create a PR.** Never comment, approve, merge, or push unbidden, and
never connect to a Microsoft 365 tenant on your own initiative. Draft the content, hand over the
command, stop. Full rules: [Human in the loop](../AGENTS.md#human-in-the-loop).

This is about acting unprompted. When the user asks you directly to connect to a tenant and run
something, that is the authorization — see
[Running against a tenant](../AGENTS.md#running-against-a-tenant).

This applies to the Copilot coding agent as well: when assigned an issue, prepare the change in the
working tree and stop before publishing.

## Copilot specifics

**Custom agents** (`.github/agents/*.agent.md`) — pick from the agents dropdown, or let Copilot
delegate to one. Available in VS Code, Visual Studio 18.4+, JetBrains, Eclipse, Xcode, the Copilot
CLI (`/agents`) and the cloud agent on github.com:

| Agent | Does |
|---|---|
| `permissions-auditor` | Permission attributes vs the API called vs the docs |
| `docs-sync` | Cmdlet parameters vs `documentation/*.md` |
| `api-surface-diff` | Public surface of this branch vs `dev` |
| `issue-triage` | GitHub issue → owning cmdlet → hypothesis |
| `cmdlet-scaffolder` | New cmdlet from a sibling (produces a draft) |
| `code-review` | Review the current change |

VS Code also reads `.claude/agents/` in the Claude sub-agent format, so the same set is available
from there — the two use matching filenames so they deduplicate rather than appearing twice.

> Custom chat modes were renamed to custom agents. `.chatmode.md` files are no longer recognised;
> this repository's have been migrated to `.github/agents/*.agent.md`.

For cmdlet conventions there is no agent — read
[`.agents/skills/new-cmdlet/SKILL.md`](../.agents/skills/new-cmdlet/SKILL.md) directly.

**MCP servers** — `.vscode/mcp.json` configures GitHub and Microsoft Learn. See
[`.agents/README.md`](../.agents/README.md).
