namespace Cyber_Awareness_Chat
{
    partial class Mainform
    {
        private System.ComponentModel.IContainer components = null;

        // ─── CONTROLS ─────────────────────────────────────────────
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel topPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();

            this.SuspendLayout();

            // ── FORM ──────────────────────────────────────────────
            this.Text = "Cybersecurity Awareness Bot";
            this.Size = new System.Drawing.Size(850, 650);
            this.MinimumSize = new System.Drawing.Size(850, 650);
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 30);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Consolas", 9.5f);

            // ── TOP PANEL ─────────────────────────────────────────
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 55;
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(26, 26, 46);

            // ── TITLE LABEL ───────────────────────────────────────
            this.titleLabel.Text = "🔒  CYBERSECURITY AWARENESS BOT";
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(0, 212, 255);
            this.titleLabel.Font = new System.Drawing.Font("Consolas", 13f,
                                            System.Drawing.FontStyle.Bold);
            this.titleLabel.AutoSize = false;
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── CHAT BOX ──────────────────────────────────────────
            this.chatBox.Location = new System.Drawing.Point(15, 70);
            this.chatBox.Size = new System.Drawing.Size(805, 460);
            this.chatBox.BackColor = System.Drawing.Color.FromArgb(13, 13, 23);
            this.chatBox.ForeColor = System.Drawing.Color.FromArgb(180, 220, 255);
            this.chatBox.Font = new System.Drawing.Font("Consolas", 10f);
            this.chatBox.ReadOnly = true;
            this.chatBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chatBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.chatBox.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            // ── INPUT BOX ─────────────────────────────────────────
            this.inputBox.Location = new System.Drawing.Point(15, 548);
            this.inputBox.Size = new System.Drawing.Size(530, 30);
            this.inputBox.BackColor = System.Drawing.Color.FromArgb(30, 30, 50);
            this.inputBox.ForeColor = System.Drawing.Color.White;
            this.inputBox.Font = new System.Drawing.Font("Consolas", 10f);
            this.inputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputBox.Anchor =
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;
            this.inputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(
                                            this.inputBox_KeyDown);

            // ── SEND BUTTON ───────────────────────────────────────
            this.sendButton.Location = new System.Drawing.Point(560, 545);
            this.sendButton.Size = new System.Drawing.Size(85, 34);
            this.sendButton.Text = "Send";
            this.sendButton.BackColor = System.Drawing.Color.FromArgb(0, 150, 200);
            this.sendButton.ForeColor = System.Drawing.Color.White;
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendButton.Font = new System.Drawing.Font("Consolas", 9.5f,
                                            System.Drawing.FontStyle.Bold);
            this.sendButton.Anchor =
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);

            // ── CLEAR BUTTON ──────────────────────────────────────
            this.clearButton.Location = new System.Drawing.Point(655, 545);
            this.clearButton.Size = new System.Drawing.Size(85, 34);
            this.clearButton.Text = "Clear";
            this.clearButton.BackColor = System.Drawing.Color.FromArgb(180, 120, 0);
            this.clearButton.ForeColor = System.Drawing.Color.White;
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearButton.Font = new System.Drawing.Font("Consolas", 9.5f,
                                            System.Drawing.FontStyle.Bold);
            this.clearButton.Anchor =
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);

            // ── EXIT BUTTON ───────────────────────────────────────
            this.exitButton.Location = new System.Drawing.Point(750, 545);
            this.exitButton.Size = new System.Drawing.Size(85, 34);
            this.exitButton.Text = "Exit";
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(180, 30, 30);
            this.exitButton.ForeColor = System.Drawing.Color.White;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Consolas", 9.5f,
                                            System.Drawing.FontStyle.Bold);
            this.exitButton.Anchor =
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);

            // ── ADD CONTROLS ──────────────────────────────────────
            this.topPanel.Controls.Add(this.titleLabel);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.topPanel);

            this.ResumeLayout(false);
        }
    }
}