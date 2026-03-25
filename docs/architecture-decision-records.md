# Architecture Decision Records (ADR)

Use ADRs to capture important technical decisions and avoid repeated debates.

## ADR Template

```markdown
# ADR-XXXX: <Short Decision Title>

## Context
What problem are we solving? What constraints exist?

## Decision
What did we choose?

## Consequences
What improves? What trade-offs are accepted?
```

## Example

### Context
The app is a desktop Windows application and must support rapid UI iteration.

### Decision
Use WinForms for the UI layer in `jdPoint`.

### Consequences
- Fast iteration for desktop forms.
- Tighter coupling to Windows platform.

## Naming
Use increasing IDs, e.g. `ADR-0001`, `ADR-0002`.
Store individual ADR files under `docs/adr/` when the list grows.
