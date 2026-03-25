# Docs in PR Workflow

Keep documentation close to code. Update docs in the same PR as code changes.

## PR Checklist

- [ ] Behavior changed -> update relevant task guide.
- [ ] Setup/config changed -> update prerequisites and commands.
- [ ] New technical decision -> add an ADR entry.
- [ ] New failure mode found -> add it to Common Errors.

## Minimum Rule
No functional change is considered complete until docs are updated.

## Suggested Review Rule
During review, check at least one file in `docs/` for every feature or refactor PR.
