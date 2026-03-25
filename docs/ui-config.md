# UI Config

This project uses one central file to configure UI behavior:

- `jdPoint/ui.config.json`

## What it controls

- Window title and size
- Navigation model (`TopBar` or `Sidebar`)
- Main spacing values (`Margin`, `TopBarHeight`, `SidebarWidth`)
- UI colors (form, grid, points, missile preview)

## Switch layout model

Change:

```json
"NavigationModel": "TopBar"
```

to:

```json
"NavigationModel": "Sidebar"
```

Then run the app again.

## Color format

Use HTML hex colors, for example:

- `#FFFFFF`
- `#B22222`
- `#4169E1`

If a color is invalid, the app falls back to safe defaults.
