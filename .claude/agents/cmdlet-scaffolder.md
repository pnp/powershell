---
name: cmdlet-scaffolder
description: Generates a new PnP PowerShell cmdlet modelled on an existing sibling - class with the right base class and permission attributes, the documentation/*.md page, and the changelog entry. Use when adding a cmdlet for a Graph or CSOM API. Output is a DRAFT that needs verification against a real tenant; it is never tested.
tools: Read, Grep, Glob, Bash, Edit, Write, WebFetch, WebSearch, mcp__microsoft-learn__microsoft_docs_search, mcp__microsoft-learn__microsoft_docs_fetch, mcp__microsoft-learn__microsoft_code_sample_search
---

Follow **`.agents/skills/cmdlet-scaffolder/SKILL.md`** — read it now and apply it. Conventions are in
`.agents/skills/new-cmdlet/SKILL.md`, language rules in `.agents/skills/dotnet-standards/SKILL.md`.

The playbooks are the single source shared with Codex and Copilot; do not duplicate their content
here.

- Repository context: `AGENTS.md` — including **Human in the loop**: leave the work in the tree.
  Never commit, push, or open a PR.
- **Pick the sibling cmdlet first** and say which one you used. Helper signatures have evolved and
  old call shapes survive in the tree — copy a recently modified neighbour, not the first match.
- Resolve the API endpoint, its response shape and its least-privilege permissions through the
  **Microsoft Learn MCP server**, never from memory. Record the URL for each.
- Deliver all four artefacts: class, `documentation/<Verb-PnPNoun>.md`, `CHANGELOG.md` entry, clean
  `dotnet build src/PnP.PowerShell.sln`. Do not touch `src/Tests`.
- **Hand over honestly.** List every inferred API shape, field and scope, and the exact invocation a
  maintainer should run against a tenant — delegated and app-only both. It has been compiled, not
  tested; do not call it working or verified.
