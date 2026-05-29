# Cybersecurity Awareness Chatbot

A Windows desktop chatbot application built with **C# / WPF (.NET Framework 4.7.2)** that teaches South African users about online safety and cybersecurity. The bot greets users by name, detects emotional sentiment in messages, remembers a user's favourite topic, and serves a curated library of cybersecurity tips — all within a sleek dark-themed chat UI.

---

## Features

- **Personalised sessions** — users enter their name before chatting; the bot addresses them by name throughout.
- **Sentiment detection** — recognises worried, curious, or frustrated language and responds with appropriately empathetic prefixes.
- **Topic memory** — the bot tracks the current topic and handles follow-up requests ("tell me more", "another tip", etc.).
- **Favourite topic** — users can tell the bot their favourite topic and it will personalise future responses accordingly.
- **Quick-access topic chips** — clickable buttons in the UI let users jump straight to any topic without typing.
- **Audio greeting** — plays `Greeting.wav` when a session starts.
- **Input validation** — rejects empty messages and enforces a 500-character limit.
- **Graceful exit** — exit words (`bye`, `quit`, `exit`, etc.) trigger a farewell message and lock the input.
- **Clear chat** — a button wipes the conversation panel at any time.

---

## Covered Cybersecurity Topics

| Topic | Keywords |
|---|---|
| Password safety | `password`, `passphrase`, `strong password` |
| Phishing | `phishing`, `fake email`, `scam email` |
| Safe browsing | `safe browsing`, `browsing`, `web safety` |
| Malware & viruses | `malware`, `virus`, `ransomware`, `trojan`, `spyware` |
| Social engineering | `social engineering`, `vishing`, `smishing`, `pretexting` |
| Two-factor authentication | `2fa`, `mfa`, `otp`, `authenticator` |
| Public Wi-Fi risks | `public wifi`, `hotspot`, `free wifi` |
| Data backups | `backup`, `data loss` |
| Privacy & tracking | `privacy`, `popia`, `cookies`, `tracking` |
| Online scams & fraud | `scam`, `fraud`, `419`, `romance scam` |

---

## Project Structure

```
Chatbot/
├── Chatbot.slnx               # Visual Studio solution file
└── Chatbot/
    ├── App.xaml / App.xaml.cs     # WPF application entry point
    ├── MainWindow.xaml            # UI layout (dark theme, chat bubbles, topic chips)
    ├── MainWindow.xaml.cs         # UI logic — message rendering, input handling, audio
    ├── ChatBotEngine.cs           # Core engine — responses, sentiment, topic memory
    ├── Greeting.wav               # Played on session start
    ├── Chatbot.csproj             # MSBuild project file (.NET Framework 4.7.2)
    └── Properties/
        ├── AssemblyInfo.cs
        ├── Resources.resx / Resources.Designer.cs
        └── Settings.settings / Settings.Designer.cs
```

---

## Requirements

| Requirement | Detail |
|---|---|
| OS | Windows 10 or Windows 11 |
| .NET Framework | 4.7.2 (pre-installed on Windows 10 1803+) |
| IDE | Visual Studio 2022 (Community edition is free) |

No third-party NuGet packages are required — the project uses only standard WPF/BCL assemblies.

---

## Getting Started

1. **Clone or extract** the project so that `Chatbot.slnx` is at the root.
2. **Open** `Chatbot.slnx` in Visual Studio 2022.
3. **Build** the solution (`Ctrl+Shift+B` or **Build → Build Solution**).
4. **Run** with `F5` (Debug) or `Ctrl+F5` (without debugger).

The compiled executable is output to `Chatbot/bin/Debug/Chatbot.exe`.

---

## Usage

1. Enter your name in the welcome panel and press **Start** (or hit **Enter**).
2. Type a question in the input box, or click one of the **topic chip buttons** (Password, Phishing, etc.).
3. Press **Enter** or click **Send** to submit.
4. Ask for "more", "another tip", or "tell me more" to get an additional response on the current topic.
5. Tell the bot "my favourite topic is [topic]" to have it personalised.
6. Type `bye`, `exit`, `quit`, or `done` to end the session gracefully.
7. Click **Clear** at any time to wipe the chat panel.

---

## Architecture Notes

ChatBotEngine.cs

The engine is fully decoupled from the UI. Key design decisions:

- Keyword dictionary** — responses are stored in a `Dictionary<string[], List<string>>` keyed on arrays of synonymous trigger words. A custom `KeyArrayComparer` uses reference equality so each key array is unique.
- Random response selection** — each topic has 2–3 response variants; `Random` picks one to avoid repetitive answers.
- Sentiment pipeline** — runs before keyword matching, prepending an empathy prefix when worry, frustration, or curiosity keywords are detected.
- Follow-up handling** — if the user's input matches follow-up phrases and a current topic is set, the engine returns another variant for that topic.

### `MainWindow.xaml.cs`

- Chat bubbles are constructed programmatically as `Border → StackPanel → TextBlock` trees, with right-alignment for user messages and left-alignment for bot messages.
- Multi-line responses (using `\n`) are split and rendered as separate `TextBlock` elements to preserve formatting.
- Audio is played via `System.Media.SoundPlayer`; failures are silently swallowed so a missing WAV never crashes the app.

> **Note:** The `PlayGreeting()` method currently contains a hardcoded absolute path (`C:\Users\asema\...`). To make the greeting work on other machines, replace the path with:
> ```csharp
> string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Greeting.wav");
> ```

---

## Known Limitations

- **Windows only** — WPF does not run on macOS or Linux.
- **Hardcoded greeting path** — see the note above; the WAV will not play on machines other than the original developer's unless the path is fixed.
- **Rule-based responses** — the bot uses keyword matching, not a language model; it will not understand paraphrased or highly ambiguous input.
- **No conversation persistence** — chat history is lost when the application is closed.

---

## Acknowledgements

Responses are hand-crafted for accuracy and relevance to South African users, with AI assistance used for portions of the UI rendering code. Cybersecurity guidance references South African institutions (SARS, SAFPS, POPIA) and locally relevant threat statistics where applicable.
