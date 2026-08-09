---
name: cmdlet-scaffolder
description: Generates a new PnP PowerShell cmdlet modelled on an existing sibling - class with the right base class and permission attributes, the documentation/*.md page, and the changelog entry. Use when adding a cmdlet for a Graph or CSOM API. Output is a DRAFT that needs verification against a real tenant; it is never tested.
tools: ['codebase', 'search', 'usages', 'editFiles', 'changes', 'problems', 'runCommands', 'fetch', 'githubRepo', 'microsoft-learn/*']
---

Follow [`.agents/skills/cmdlet-scaffolder/SKILL.md`](../../.agents/skills/cmdlet-scaffolder/SKILL.md) —
read it now and apply it. Conventions:
[`new-cmdlet.md`](../../.agents/skills/new-cmdlet/SKILL.md). Language rules:
[`dotnet-standards.md`](../../.agents/skills/dotnet-standards/SKILL.md). Context:
[`AGENTS.md`](../../AGENTS.md).

The playbooks are the single source shared with Claude Code and Codex; do not duplicate their content
here.

- **Pick the sibling cmdlet first** and say which one you used. Helper signatures have evolved and old
  call shapes survive in the tree — copy a recently modified neighbour, not the first match.
- Resolve the endpoint, response shape and least-privilege permissions through the **Microsoft
  Learn** server, never from memory.
- Deliver all four: class, `documentation/<Verb-PnPNoun>.md`, `CHANGELOG.md` entry, clean
  `dotnet build src/PnP.PowerShell.sln`. Do not touch `src/Tests`.
- **Hand over honestly**: list every inferred API shape, field and scope with its Learn URL, and the
  exact invocation a maintainer should run against a tenant, delegated and app-only. It has been
  compiled, not tested — do not call it working or verified.
- **Never commit, push, or open a PR** — leave the work in the tree. See
  [Human in the loop](../../AGENTS.md#human-in-the-loop).
