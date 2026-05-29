using System;
using System.IO;

namespace Cyber_Awareness_Chat
{
    public class AudioHelper
    {
        private const string SoundsFolder = "Sounds";

        // ─── PLAY VOICE GREETING ──────────────────────────────────
        public void PlayVoiceGreeting()
        {
            PlaySound("GReeting.wav");
        }

        // ─── PLAY ANY SOUND BY FILENAME ───────────────────────────
        public void PlaySound(string fileName)
        {
            try
            {
                string path = Path.Combine(AppContext.BaseDirectory, SoundsFolder, fileName);

                if (!File.Exists(path))
                {
                    Console.WriteLine("Sound file not found: " + path);
                    return;
                }

                if (OperatingSystem.IsWindows())
                    PlayWav(path);
            }
            catch
            {
                // fail silently
            }
        }

        // ─── INTERNAL WAV PLAYER ──────────────────────────────────
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        private static void PlayWav(string path)
        {
            using var player = new System.Media.SoundPlayer(path);
            player.PlaySync();
        }
    }
}