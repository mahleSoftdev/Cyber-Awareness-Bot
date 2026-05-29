using System;
using System.Windows.Forms;

namespace Cyber_Awareness_Chat
{
    public partial class Mainform : Form
    {
        // ─── Single instances ─────────────────────────────────────
        private readonly ChatbotEngine bot;
        private readonly MemoryManager memory;
        private readonly SentimentAnalyzer sentiment;
        private readonly AudioHelper audioHelper;

        public Mainform()
        {
            InitializeComponent();

            bot = new ChatbotEngine();
            memory = new MemoryManager();
            sentiment = new SentimentAnalyzer();
            audioHelper = new AudioHelper();

            audioHelper.PlayVoiceGreeting();
            WelcomeMessage();
        }

        // ─── WELCOME MESSAGE ─────────────────────────────────────
        private void WelcomeMessage()
        {
            chatBox.AppendText(
                "=====================================\n" +
                "      CYBERSECURITY AWARENESS BOT    \n" +
                "=====================================\n" +
                "  ____      _               ____ _           _   \n" +
                " / ___|   _| |__   ___ _ __/ ___| |__   __ _| |_ \n" +
                "| |  | | | | '_ \\ / _ \\ '__\\___ \\ '_ \\ / _` | __|\n" +
                "| |__| |_| | |_) |  __/ |   ___) | | | | (_| | |_ \n" +
                " \\____\\__, |_.__/ \\___|_|  |____/|_| |_|\\__,_|\\__|\n" +
                "      |___/                                        \n" +
                "=====================================\n\n" +
                "Hello! I'm your Cybersecurity Awareness Assistant.\n" +
                "I'm here to help you stay safe online.\n\n" +
                "You can ask me about:\n" +
                "  • Passwords & Passphrases\n" +
                "  • Phishing Attacks\n" +
                "  • Malware & Viruses\n" +
                "  • VPNs & Safe Browsing\n" +
                "  • Public Wi-Fi Risks\n" +
                "  • Two-Factor Authentication\n" +
                "  • Scams & Fraud\n" +
                "  • Data Breaches\n" +
                "  • Firewalls & Backups\n" +
                "  • Privacy Protection\n\n" +
                "Tell me your name: 'My name is ...'\n" +
                "Tell me your interest: 'I am interested in ...'\n" +
                "Ask for more tips: 'Give me another tip'\n" +
                "=====================================\n\n"
            );
        }

        // ─── SEND BUTTON ─────────────────────────────────────────
        private void sendButton_Click(object sender, EventArgs e)
        {
            ProcessInput();
        }

        // ─── ENTER KEY SUPPORT ───────────────────────────────────
        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ProcessInput();
            }
        }

        // ─── CORE LOGIC ──────────────────────────────────────────
        private void ProcessInput()
        {
            string input = inputBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                audioHelper.PlaySound("error.wav");
                MessageBox.Show("Please enter a message.", "Empty Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            chatBox.AppendText("YOU: " + input + "\n");
            audioHelper.PlaySound("send.wav");

            string lower = input.ToLower();

            // ── MEMORY: store name ────────────────────────────────
            if (lower.Contains("my name is"))
            {
                string name = lower.Replace("my name is", "").Trim();

                if (name.Length > 0)
                    name = char.ToUpper(name[0]) + name.Substring(1);

                memory.StoreMemory("name", name);
                AppendBotMessage("Nice to meet you, " + name +
                    "! Feel free to ask me anything about cybersecurity.");
                return;
            }

            // ── MEMORY: store favourite topic ─────────────────────
            if (lower.Contains("i am interested in") || lower.Contains("i'm interested in"))
            {
                string topic = lower
                    .Replace("i am interested in", "")
                    .Replace("i'm interested in", "")
                    .Trim();

                if (topic.Length > 0)
                    topic = char.ToUpper(topic[0]) + topic.Substring(1);

                memory.StoreMemory("interest", topic);
                AppendBotMessage("Great! I'll remember that you're interested in " + topic +
                    ". It's a crucial part of staying safe online. " +
                    "Feel free to ask me anything about it!");
                return;
            }

            // ── MEMORY: recall name and interest ──────────────────
            string storedName = memory.RecallMemory("name");
            string storedInterest = memory.RecallMemory("interest");

            // ── SENTIMENT ANALYSIS ────────────────────────────────
            string mood = sentiment.DetectSentiment(input);

            // ── CHATBOT RESPONSE ──────────────────────────────────
            string response = bot.responseDelegate(input);

            // ── PERSONALISE WITH NAME ─────────────────────────────
            if (!string.IsNullOrEmpty(storedName))
                response = storedName + ", " + response;

            // ── REMIND USER OF THEIR INTEREST ─────────────────────
            if (!string.IsNullOrEmpty(storedInterest) &&
                lower.Contains(storedInterest.ToLower()))
            {
                response += "\n   [As someone interested in " + storedInterest +
                    ", this is especially relevant to you!]";
            }

            // ── SENTIMENT ADJUSTMENT ──────────────────────────────
            switch (mood)
            {
                case "worried":
                    response = "It's completely understandable to feel that way — " +
                        "cybersecurity can feel overwhelming. " + response;
                    break;
                case "frustrated":
                    response = "I understand your frustration. " +
                        "Let me help clarify things for you. " + response;
                    break;
                case "curious":
                    response = "Great question! " + response;
                    break;
            }

            AppendBotMessage(response);
        }

        // ─── HELPER: APPEND BOT MESSAGE ──────────────────────────
        private void AppendBotMessage(string message)
        {
            chatBox.AppendText("BOT: " + message + "\n\n");
            audioHelper.PlaySound("receive.wav");

            chatBox.SelectionStart = chatBox.Text.Length;
            chatBox.ScrollToCaret();

            inputBox.Clear();
        }

        // ─── CLEAR BUTTON ────────────────────────────────────────
        private void clearButton_Click(object sender, EventArgs e)
        {
            audioHelper.PlaySound("warning.wav");
            chatBox.Clear();
            memory.ClearMemory();
            WelcomeMessage();
        }

        // ─── EXIT BUTTON ─────────────────────────────────────────
        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                audioHelper.PlaySound("exit.wav");
                Application.Exit();
            }
        }
    }
}