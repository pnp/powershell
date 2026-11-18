---
name: release-prep
description: Checks a PnP PowerShell stable release before it is cut - version files agree, every user-visible PR merged since the last release has a changelog entry, the changelog section is well formed, contributors are credited - and drafts the changelog rollover. Use before dispatching the major release workflow, or to lint CHANGELOG.md. Read-only.
tools: ['read', 'search', 'execute', 'github/*']
---

Follow [`.agents/skills/release-prep/SKILL.md`](../../.agents/skills/release-prep/SKILL.md) — read it
now and apply it. Repository context: [`AGENTS.md`](../../AGENTS.md).

The playbook is the single source shared with Claude Code and Codex; do not duplicate its content
here.

- Ask which version is being released if the user has not said. Do not infer it from the files.
- Read the diff of a PR before drafting its changelog entry. The title is not enough.
- Read-only. Draft the entries and the rollover as a diff; do not edit `CHANGELOG.md` unless asked.
- **Never dispatch a workflow, tag, publish, commit, push or open a PR.** Print the remaining release
  steps as a checklist — see [Human in the loop](../../AGENTS.md#human-in-the-loop).
- Finish with the counts the playbook asks for.
