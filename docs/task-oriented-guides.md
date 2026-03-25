# Task-Oriented Guides

Use this structure for every operational guide (build, run, debug, release).

## Template

### Goal
State what the user will accomplish in one sentence.

### Prerequisites
List required tools, versions, and access.

### Steps
Provide exact commands and UI actions.

### Expected Result
Describe successful output or behavior.

### Common Errors
List likely failures and quick fixes.

## Example: Run the WinForms app

### Goal
Run the `jdPoint` application locally.

### Prerequisites
- .NET SDK 9.0 or compatible
- Windows environment

### Steps
1. Open a terminal in `jdPoint/`.
2. Execute:

```powershell
dotnet run --project jdPoint.csproj
```

### Expected Result
The WinForms window opens without build errors.

### Common Errors
- SDK not found: install the required .NET SDK.
- Build fails from stale artifacts: delete `bin/` and `obj/`, then rerun.
