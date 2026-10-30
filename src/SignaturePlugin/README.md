# SignaturePlugin

An [OCRX](https://ocrx.org) plugin for Star Citizen mining. It reads the **RS signature number**
shown in scan mode and tells you, on screen, which ore cluster it is — `Ice x4`, `Bexalite x5`, and
so on — so you do not have to look the number up.

## Using it

1. Start the OCRX engine and install SignaturePlugin: right-click the tray icon, choose
   **Plugins…**, and install it.
2. Launch Star Citizen. The engine offers to capture it: accept the **Capture Star Citizen** offer,
   which also starts the plugin. (Otherwise pick **Launch** for SignaturePlugin in the tray menu;
   the engine never starts a plugin on its own.)
3. In game, open scan mode and point at a rock. The overlay appears as soon as the signature is
   read and disappears when the badge does.

That is all there is to it. Nothing needs calibrating: the plugin follows your in-game field of
view by reading it from the game's own profile, and moves its reading zone to match.

A few things worth knowing:

- **Ambiguous totals show both candidates.** Some totals can be two different clusters — 19200 is
  Savrilium x6 *and* Aslarite x5, and nothing on screen tells them apart. The overlay then reads
  `Savrilium x6 / Aslarite x5` rather than guessing.
- **It stays put on a bad read.** One misread digit will not rename the ore, and a brief blank does
  not hide the overlay. A different value has to be read twice in a row before it replaces the
  current one, and the overlay only clears after about 3 seconds of blank reads (at the default
  scan rate). As a backstop it also clears about 10 seconds after the last real match, so a stale
  value never stays on screen.
- **Everything stays on your machine.** Nothing is sent anywhere.

## Settings

Four settings are available in the engine's plugin settings. Changes apply immediately
while the plugin is running and persist to its config file.

| Setting | Choices / range | Default |
| --- | --- | --- |
| **Theme** | Default, Star Citizen, Retro | Default |
| **Position** | Top Left, Top Center, Top Right, Middle Left, Center, Middle Right, Bottom Left, Bottom Center, Bottom Right | Top Center |
| **Counter crop width** | 70–400 reference pixels | 170 |
| **Counter horizontal offset** | −300 to +300 reference pixels | 0 |

**Theme** selects the overlay pill's look. **Position** anchors it on the primary screen.
**Counter crop width** widens or narrows the signature OCR region around its original centre.
**Counter horizontal offset** shifts that centre; positive values move it right. The ROI remains
subject to the plugin's automatic Star Citizen FOV adjustment.

### Editing by hand

The values are saved in `%LOCALAPPDATA%\\OCRX\\SignaturePlugin\\config.json` as
`overlayTheme`, `position`, `counterRoiWidth`, and `counterRoiHorizontalOffset`. If editing the
file directly, restart the plugin to apply the changes. Theme and position use lowercase ids; for
example:

```json
"overlayTheme": "citizen",
"position": "topright",
"counterRoiWidth": 170,
"counterRoiHorizontalOffset": 0
```

The same file holds the `outputs` list. By default every change of reading (and every clear) is appended to
`captures/signatures.jsonl` next to the config, one JSON line each, for use in your own tools. Set
`outputs` to `[]` to switch off both the file and the overlay.

## Troubleshooting

- **Nothing shows up.** Make sure scan mode is open and the signature number is visible, and that
  the engine is running. If you installed an older version, check two values under `overlay` in
  `config.json`: `lingerMs` must be `0`, otherwise the overlay hides itself a moment after
  appearing and never comes back for that rock; and `template` should be `{cluster}`, otherwise an
  ambiguous total shows only its first candidate.
- **Wrong ore.** The table of ore signatures is `signature-table.json` in the same folder. It is
  created on first launch and never overwritten, so you can edit it (then restart the plugin) if a
  game patch changes the values.
- **Still stuck?** Stop the plugin from the tray menu, then run `SignaturePlugin.exe --verbose`
  from its install folder (`%LOCALAPPDATA%\OCRX\plugins\<plugin-id>`) with the engine running. The
  per-tick log shows the raw OCR text, the number parsed from it, and what the table made of it.

## For contributors

ROI calibration, the replay corpus, the overlay's debounce rules, and build and test commands are
in [DEVELOPMENT.md](DEVELOPMENT.md).
