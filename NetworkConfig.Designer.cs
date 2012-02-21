namespace SMSBrowser
{
    partial class NetworkConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SyncEnabledCheckbox = new System.Windows.Forms.CheckBox();
            this.SyncPortLabel = new System.Windows.Forms.Label();
            this.SyncPasswordLabel = new System.Windows.Forms.Label();
            this.SyncPasswordTextBox = new System.Windows.Forms.TextBox();
            this.SyncPortTextBox = new System.Windows.Forms.TextBox();
            this.SyncAcceptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SyncEnabledCheckbox
            // 
            this.SyncEnabledCheckbox.AutoSize = true;
            this.SyncEnabledCheckbox.Location = new System.Drawing.Point(12, 12);
            this.SyncEnabledCheckbox.Name = "SyncEnabledCheckbox";
            this.SyncEnabledCheckbox.Size = new System.Drawing.Size(135, 17);
            this.SyncEnabledCheckbox.TabIndex = 0;
            this.SyncEnabledCheckbox.Text = "Network Sync Enabled";
            this.SyncEnabledCheckbox.UseVisualStyleBackColor = true;
            this.SyncEnabledCheckbox.CheckedChanged += new System.EventHandler(this.PasswordTextChanged);
            // 
            // SyncPortLabel
            // 
            this.SyncPortLabel.AutoSize = true;
            this.SyncPortLabel.Location = new System.Drawing.Point(9, 47);
            this.SyncPortLabel.Name = "SyncPortLabel";
            this.SyncPortLabel.Size = new System.Drawing.Size(90, 13);
            this.SyncPortLabel.TabIndex = 1;
            this.SyncPortLabel.Text = "Sync Server Port:";
            // 
            // SyncPasswordLabel
            // 
            this.SyncPasswordLabel.AutoSize = true;
            this.SyncPasswordLabel.Location = new System.Drawing.Point(9, 69);
            this.SyncPasswordLabel.Name = "SyncPasswordLabel";
            this.SyncPasswordLabel.Size = new System.Drawing.Size(83, 13);
            this.SyncPasswordLabel.TabIndex = 2;
            this.SyncPasswordLabel.Text = "Sync Password:";
            // 
            // SyncPasswordTextBox
            // 
            this.SyncPasswordTextBox.Location = new System.Drawing.Point(105, 68);
            this.SyncPasswordTextBox.Name = "SyncPasswordTextBox";
            this.SyncPasswordTextBox.Size = new System.Drawing.Size(138, 20);
            this.SyncPasswordTextBox.TabIndex = 3;
            this.SyncPasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextChanged);
            // 
            // SyncPortTextBox
            // 
            this.SyncPortTextBox.Location = new System.Drawing.Point(105, 44);
            this.SyncPortTextBox.MaxLength = 5;
            this.SyncPortTextBox.Name = "SyncPortTextBox";
            this.SyncPortTextBox.Size = new System.Drawing.Size(63, 20);
            this.SyncPortTextBox.TabIndex = 4;
            this.SyncPortTextBox.TextChanged += new System.EventHandler(this.ServerPortTextChanged);
            // 
            // SyncAcceptButton
            // 
            this.SyncAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SyncAcceptButton.Location = new System.Drawing.Point(105, 95);
            this.SyncAcceptButton.Name = "SyncAcceptButton";
            this.SyncAcceptButton.Size = new System.Drawing.Size(112, 23);
            this.SyncAcceptButton.TabIndex = 5;
            this.SyncAcceptButton.Text = "Accept";
            this.SyncAcceptButton.UseVisualStyleBackColor = true;
            // 
            // NetworkConfig
            // 
            this.AcceptButton = this.SyncAcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 130);
            this.ControlBox = false;
            this.Controls.Add(this.SyncAcceptButton);
            this.Controls.Add(this.SyncPortTextBox);
            this.Controls.Add(this.SyncPasswordTextBox);
            this.Controls.Add(this.SyncPasswordLabel);
            this.Controls.Add(this.SyncPortLabel);
            this.Controls.Add(this.SyncEnabledCheckbox);
            this.Name = "NetworkConfig";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "NetworkConfig";
            this.Shown += new System.EventHandler(this.OnShow);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SyncPortLabel;
        private System.Windows.Forms.Label SyncPasswordLabel;
        private System.Windows.Forms.Button SyncAcceptButton;
        internal System.Windows.Forms.CheckBox SyncEnabledCheckbox;
        internal System.Windows.Forms.TextBox SyncPortTextBox;
        internal System.Windows.Forms.TextBox SyncPasswordTextBox;
    }
}