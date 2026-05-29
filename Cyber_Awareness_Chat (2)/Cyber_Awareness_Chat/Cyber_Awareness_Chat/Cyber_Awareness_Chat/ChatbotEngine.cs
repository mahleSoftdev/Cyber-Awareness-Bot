using System;
using System.Collections.Generic;
using System.Linq;

namespace Cyber_Awareness_Chat
{
    public class ChatbotEngine
    {
        public Func<string, string> responseDelegate;

        private readonly Random random;
        private string lastTopic;

        // ─── SINGLE RESPONSES ─────────────────────────────────────
        private readonly Dictionary<string[], string> keywordResponses;

        // ─── RANDOM RESPONSES (multiple per topic) ────────────────
        private readonly Dictionary<string, List<string>> randomResponses;

        public ChatbotEngine()
        {
            random = new Random();
            lastTopic = "";

            // ── Single keyword responses ──────────────────────────
            keywordResponses = new Dictionary<string[], string>(new KeyArrayComparer())
            {
                { new string[] { "password", "passwords" },
                    "Use strong, unique passwords for every account. Mix uppercase, lowercase, numbers, and symbols. Never reuse passwords — use a password manager like Bitwarden to keep track." },

                { new string[] { "scam", "fraud", "con" },
                    "Scammers often impersonate trusted organisations. Never send money, gift cards, or personal info to someone you haven't verified. If it feels off — trust your instincts." },

                { new string[] { "privacy", "personal data", "data privacy" },
                    "Protect your privacy by limiting what you share online. Review app permissions regularly and adjust your social media privacy settings. Less shared = less exposed." },

                { new string[] { "vpn", "virtual private network" },
                    "A VPN encrypts your internet traffic, keeping your activity private — especially important on public Wi-Fi. Trusted options include ProtonVPN and NordVPN." },

                { new string[] { "wifi", "wi-fi", "public wifi" },
                    "Avoid banking or shopping on public Wi-Fi. If you must connect, always use a VPN. Make sure your home network uses WPA2 or WPA3 encryption." },

                { new string[] { "2fa", "two factor", "two-factor", "mfa", "multi factor" },
                    "Two-factor authentication adds a second lock on your accounts. Even if your password is stolen, attackers can't get in without your second factor. Enable it on every account that offers it." },

                { new string[] { "malware", "virus", "ransomware", "spyware" },
                    "Keep your antivirus software updated and avoid downloading files from untrusted sources. Ransomware can encrypt all your files — back up your data regularly!" },

                { new string[] { "firewall" },
                    "A firewall monitors your network traffic and blocks suspicious connections. Make sure your OS firewall is always enabled, and consider a hardware firewall for extra protection." },

                { new string[] { "backup", "backups" },
                    "Follow the 3-2-1 rule: 3 copies of data, on 2 different media types, with 1 stored offsite or in the cloud. Backups are your last line of defence against ransomware." },

                { new string[] { "update", "patch", "software update" },
                    "Always install software and OS updates promptly. Updates patch known vulnerabilities that attackers actively exploit." },

                { new string[] { "social engineering", "manipulation" },
                    "Social engineers manipulate people instead of systems. Always verify someone's identity before sharing sensitive info — even if they seem legitimate or urgent." },

                { new string[] { "data breach", "breach", "hacked", "leaked" },
                    "If your data is in a breach, change your passwords immediately and enable 2FA." },

                { new string[] { "safe browsing", "browser" },
                    "Use a secure browser with HTTPS-only mode enabled. Install uBlock Origin to block malicious ads and trackers. Always verify a website's URL before entering any details." },

                { new string[] { "hello", "hi", "hey", "howzit", "good day" },
                    "Hello! Ask me anything about staying safe online — passwords, phishing, malware, VPNs, and more!" },

                { new string[] { "help", "what can you do", "topics", "menu" },
                    "I can help you with: Passwords • Phishing • Malware • VPNs • Public Wi-Fi • Two-Factor Authentication • Scams • Data Breaches • Safe Browsing • Firewalls • Backups • Privacy." },

                { new string[] { "thank", "thanks", "thank you" },
                    "You're welcome! Stay safe out there. Feel free to ask me anything else." }
            };

            // ── Random responses per topic ────────────────────────
            randomResponses = new Dictionary<string, List<string>>
            {
                { "phishing", new List<string>
                    {
                        "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations like banks or government agencies.",
                        "Always check the sender's email address carefully. Phishing emails often use addresses that look similar to real ones but have subtle differences.",
                        "Never click links in unexpected emails. Instead, go directly to the website by typing the address in your browser.",
                        "Look out for urgent language like 'Your account will be closed!' — this is a common phishing tactic designed to panic you into acting fast.",
                        "Legitimate companies will never ask for your password via email. If you receive such a request, report it as phishing immediately."
                    }
                },
                { "password", new List<string>
                    {
                        "Use a passphrase — a string of random words like 'BlueSkyTigerCake99!' is both strong and memorable.",
                        "Never use personal details like your name, birthday, or pet's name in passwords — these are the first things attackers try.",
                        "Use a different password for every account. A password manager makes this easy and secure.",
                        "Passwords should be at least 12 characters long. Length matters more than complexity.",
                        "Change your passwords immediately if you suspect an account has been compromised."
                    }
                },
                { "privacy", new List<string>
                    {
                        "Review the privacy settings on all your social media accounts — limit who can see your posts and personal details.",
                        "Be careful what you share publicly. Even small details like your location or workplace can be used against you.",
                        "Use a privacy-focused browser like Firefox or Brave, and a search engine like DuckDuckGo.",
                        "Regularly audit which apps have access to your camera, microphone, and location — revoke anything unnecessary.",
                        "Consider using a separate email address for online shopping and newsletters to protect your main inbox."
                    }
                },
                { "scam", new List<string>
                    {
                        "If someone contacts you out of the blue offering a prize or investment opportunity, it's almost certainly a scam.",
                        "Tech support scams are common — Microsoft or Apple will never call you unsolicited to fix your computer.",
                        "Romance scams cost people millions each year. Be very cautious about sending money to people you've only met online.",
                        "Verify charity organisations before donating — scammers often create fake charities after major disasters.",
                        "If you've been scammed, report it to your local cybercrime unit and your bank immediately."
                    }
                }
            };

            responseDelegate = GetResponse;
        }

        // ─── MAIN RESPONSE METHOD ─────────────────────────────────
        private string GetResponse(string input)
        {
            string lower = input.ToLower();

            // ── Handle follow-up requests ─────────────────────────
            if (lower.Contains("another tip") || lower.Contains("tell me more") ||
                lower.Contains("explain more") || lower.Contains("more info") ||
                lower.Contains("give me more") || lower.Contains("elaborate"))
            {
                if (!string.IsNullOrEmpty(lastTopic) && randomResponses.ContainsKey(lastTopic))
                    return GetRandomResponse(lastTopic);

                return "Sure! What topic would you like more information on? You can ask about phishing, passwords, privacy, scams, and more.";
            }

            // ── Check random response topics first ────────────────
            foreach (var topic in randomResponses.Keys)
            {
                if (lower.Contains(topic))
                {
                    lastTopic = topic;
                    return GetRandomResponse(topic);
                }
            }

            // ── Check single keyword responses ────────────────────
            foreach (var entry in keywordResponses)
            {
                foreach (string keyword in entry.Key)
                {
                    if (lower.Contains(keyword))
                    {
                        lastTopic = keyword;
                        return entry.Value;
                    }
                }
            }

            // ── Default fallback ──────────────────────────────────
            return "I'm not sure I understand. Could you try rephrasing? You can ask me about topics like passwords, phishing, malware, VPNs, scams, or privacy.";
        }

        // ─── GET RANDOM RESPONSE FROM TOPIC LIST ─────────────────
        private string GetRandomResponse(string topic)
        {
            List<string> options = randomResponses[topic];
            int index = random.Next(options.Count);
            return options[index];
        }

        public string GetLastTopic()
        {
            return lastTopic;
        }
    }

    // ─── COMPARER FOR STRING ARRAY KEYS ──────────────────────────
    class KeyArrayComparer : IEqualityComparer<string[]>
    {
        public bool Equals(string[]? x, string[]? y)
        {
            if (x is null && y is null) return true;
            if (x is null || y is null) return false;
            return x.SequenceEqual(y);
        }

        public int GetHashCode(string[] obj)
        {
            int hash = 17;
            foreach (var s in obj)
                hash = hash * 31 + (s?.GetHashCode() ?? 0);
            return hash;
        }
    }
}