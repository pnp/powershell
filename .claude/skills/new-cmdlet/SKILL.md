---
name: new-cmdlet
description: The conventions a PnP PowerShell cmdlet must satisfy - base class selection, permission attributes, PipeBinds, parameter validation, Graph and CSOM call patterns, and the documentation plus changelog artefacts that make a cmdlet complete. Use when writing, modifying or reviewing cmdlet code, or when deciding which base class or permission attribute applies.
---

# PnP PowerShell cmdlet conventions

Follow **[`.agents/skills/new-cmdlet/SKILL.md`](../../../.agents/skills/new-cmdlet/SKILL.md)** — read it
now and apply it.

The playbook is the single source shared with Codex and Copilot. Do not duplicate its content here;
changes belong in the playbook.

Supporting references:

- [`.agents/skills/dotnet-standards/SKILL.md`](../../../.agents/skills/dotnet-standards/SKILL.md) — C# 12 /
  .NET 8 and PowerShell cmdlet design rules
- [`.agents/skills/cmdlet-scaffolder/SKILL.md`](../../../.agents/skills/cmdlet-scaffolder/SKILL.md) — the
  step-by-step procedure for creating a new cmdlet
- [`.agents/skills/permissions-auditor/SKILL.md`](../../../.agents/skills/permissions-auditor/SKILL.md) —
  how the permission attributes are interpreted, and how a typo'd resource prefix silently becomes a
  SharePoint scope
- [`AGENTS.md`](../../../AGENTS.md) — repository context

**Never commit, push, or open a PR** — leave the work in the tree and hand it over. See
[Human in the loop](../../../AGENTS.md#human-in-the-loop).

Remember the constraint: you cannot run these cmdlets. Anything calling an API you have not invoked
is a draft — list what you inferred rather than verified.
