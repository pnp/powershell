---
name: release-prep
description: Check a PnP PowerShell stable release before it is cut - version files agree, every user-visible PR merged since the last release has a changelog entry, the changelog section is well formed, contributors are credited - and draft the changelog rollover. Use before dispatching the major release workflow, or to lint CHANGELOG.md at any time.
---

# Playbook: release-prep

Check that a stable release is ready to cut, and draft the changelog work that goes with it.

## Why

A release is assembled by hand from several files that must agree, and nothing checks them. The
changelog is the part users read before upgrading, and it is the part that slips: a merged PR with no
entry is invisible in the release notes, and nobody notices until a user asks what changed.

> Read-only. This playbook produces a report and drafts. **Never dispatch a workflow, create a tag,
> publish a release, commit, push, or open the dev-to-main PR** — hand over the command instead. See
> [Human in the loop](../../../AGENTS.md#human-in-the-loop). Do not edit `CHANGELOG.md` unless the user
> asks for the drafts to be applied.

## 1. Versions

Three places carry the version, and `Build-Release.ps1` is edited by hand:

| File | What it holds |
|---|---|
| `build/Build-Release.ps1`, line 1 | `$version = "X.Y.Z"` — the version the release will publish |
| `version.txt` | The last published version, written by the build |
| `version.json` | The same, as `Version`; nightly builds read it |

Check:

- `$version` in `Build-Release.ps1` is the intended release, and is higher than the last stable tag.
  Stable tags are the bare `X.Y.Z` ones; a `vX.Y.Z` tag for the GitHub release exists beside each.

  ```
  git tag --sort=-creatordate
  ```

- `version.txt` and `version.json` agree with each other. The nightly build publishes
  Major.Minor.(Patch + 1) of `version.json`, or replaces a `%` in it with `0`, so a hand edit that
  leaves them different changes the next nightly number.
- `src/Commands/PnP.PowerShell.csproj` pins for `PnP.Framework` and `PnP.Core`. Report the version
  each pair of rows carries, and say so if the two rows of a pair disagree.

If the user has not said which version is being released, ask. Do not infer it from the files.

## 2. Merged work with no changelog entry

List what merged since the last stable tag:

```
git log --merges --first-parent <last-stable-tag>..HEAD --oneline
```

For each PR number, look for it in the `[Current nightly]` section of `CHANGELOG.md`, either as its
own `/pull/NNNN` link or through the issue it closes. Then sort the PRs that have no entry:

- **User-visible** — the PR touches `src/Commands/`, `documentation/`, or anything else a user of the
  module would notice. This needs an entry. Draft one.
- **Not user-visible** — CI, agent configuration, build scripts with no change to the shipped module.
  No entry needed; list these so the reader can see they were considered.

Read the diff before drafting an entry. The PR title is not enough to say what changed for a user.

## 3. Changelog form

Apply these to the section being released. They also work as a lint of any section.

- Headings appear in this order, and an empty heading is removed from a released section:
  `### Added`, `### Changed`, `### Fixed`, `### Removed`, `### Contributors`.
- Each entry starts with the word of its heading: "Added …", "Changed …", "Fixed …", "Removed …".
  "Fix …" under Fixed is a finding, and so is a "Changed …" entry sitting under Fixed.
- A change in behaviour sits under `Changed` even when it fixes a bug.
- Cmdlet and parameter names are in backticks.
- Each entry ends with one link, `[#NNNN](https://github.com/pnp/powershell/pull/NNNN)` or the
  `/issues/NNNN` form. The number in the label equals the number in the URL, and the URL has no
  trailing slash.
- An entry says what changed for the user and why they would care, in the style of its neighbours.

## 4. Contributors

The `### Contributors` list credits people as `- Full Name [githubhandle]`, or `- [handle]` when no
name is known. Compare the authors of the PRs from step 2 against the list and report anyone
missing. The earlier sections show who is conventionally left out; follow them and say what you
assumed.

Author names need GitHub. Use the GitHub MCP server, or read-only `gh pr view <n> --json author`. If
neither is available, list the PR numbers whose authors you could not resolve.

## 5. Draft the rollover

Draft, as a diff the maintainer can apply:

- `## [Current nightly]` renamed to `## [X.Y.Z]`, with the fixes from steps 2 to 4 applied.
- A new `## [Current nightly]` above it with empty `### Added`, `### Changed` and `### Fixed`
  headings.

## 6. Hand over

The release itself is the maintainer's. Print the remaining steps as a checklist, without running
any of them:

1. Dispatch `majorrelease.yml`. It refuses to run unless `dev` HEAD is the commit it was started on.
2. Open the PR from `dev` to `main`.
3. Dispatch `stabledockerimages.yml` from `main`, after the module is on the PowerShell Gallery.
4. Publish the draft GitHub release the workflow created.

## Reporting

Lead with whether the release is ready, in one sentence. Then:

- **Versions** — the three values, and any disagreement
- **Missing entries** — PR number, what it changes for a user, and the drafted entry
- **Form** — line number, the rule broken, and the corrected line
- **Contributors** — who is missing
- **Rollover** — the drafted diff
- **Not checked** — anything you could not resolve, and what would settle it

Finish with counts: PRs merged since the last tag, with an entry, without one and user-visible,
without one and not user-visible.
