# PlaylistLoaderPlugin
This plugin uses the official Playlist loader to load custom playlist files in Beat Saber.
This mod isn't a replacement for PlaylistCore and does not load songs that aren't locally available. Instead, it is for people who prefer a more lightweight mod for loading playlists in the game's native playlist UI.

## Features
- Support for JSON & .bplist custom playlists
- Loads custom playlists in the official playlists UI
- Allows refreshing playlists using the "Refresh Playlists" button in the Mods tab

## How To Use
- Create a folder called "Playlists" in your Beat Saber installation folder
- Store your playlists in the playlists folder
- To manage your playlists, download missing songs or create your own, download [Beatlist](https://github.com/Alaanor/beatlist/releases "Beatlist")

## Download
Download the latest version of the mod (v1.1.1) [here](https://github.com/rithik-b/PlaylistLoaderPlugin/releases/tag/1.1.1 "here")
To install, simply move the plugin dll to the Plugins folder of your Beat Saber install.
BSML and SongCore are required for this mod, so please download the latest version from ModAssistant.

## Issues and Support
If you encounter any issues with this mod, PM me on Discord PixelBoom#6948 or file an Issue on GitHub and I'll fix it as soon as I can.
For support with the mod, DM me or @ me on the #pc-mod-support channel on the BSMG discord.

## Compilation Instructions (If you just want to use the plugin, download it from the Releases Tab)
- Open the project in Visual Studio
- Add missing references by locating them in your Beat Saber\Beat Saber_Data\Managed and Beat Saber\Plugins directories
- Build the project
- The plugin would be under PlaylistLoaderPlugin\bin\Debug

## Screenshots
![](https://i.imgur.com/LbligvQ.png)
