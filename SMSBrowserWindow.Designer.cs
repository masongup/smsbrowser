namespace SMSBrowser
{
    partial class SMSBrowserWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.ExportCurrentButton = new System.Windows.Forms.Button();
            this.ExportAllButton = new System.Windows.Forms.Button();
            this.ImportMoreButton = new System.Windows.Forms.Button();
            this.MessagesList = new System.Windows.Forms.DataGridView();
            this.Info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Body = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListSplitContainer = new System.Windows.Forms.SplitContainer();
            this.ImportTextDialog = new System.Windows.Forms.OpenFileDialog();
            this.ContactsListView = new System.Windows.Forms.DataGridView();
            this.ContactName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumMessages = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastMessageDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TopPanel.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MessagesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListSplitContainer)).BeginInit();
            this.ListSplitContainer.Panel1.SuspendLayout();
            this.ListSplitContainer.Panel2.SuspendLayout();
            this.ListSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContactsListView)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.SearchBox);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(737, 59);
            this.TopPanel.TabIndex = 0;
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(437, 12);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(140, 20);
            this.SearchBox.TabIndex = 0;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.ExportCurrentButton);
            this.BottomPanel.Controls.Add(this.ExportAllButton);
            this.BottomPanel.Controls.Add(this.ImportMoreButton);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 523);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(737, 140);
            this.BottomPanel.TabIndex = 1;
            // 
            // ExportCurrentButton
            // 
            this.ExportCurrentButton.Location = new System.Drawing.Point(423, 32);
            this.ExportCurrentButton.Name = "ExportCurrentButton";
            this.ExportCurrentButton.Size = new System.Drawing.Size(165, 34);
            this.ExportCurrentButton.TabIndex = 2;
            this.ExportCurrentButton.Text = "Export";
            this.ExportCurrentButton.UseVisualStyleBackColor = true;
            // 
            // ExportAllButton
            // 
            this.ExportAllButton.Location = new System.Drawing.Point(11, 66);
            this.ExportAllButton.Name = "ExportAllButton";
            this.ExportAllButton.Size = new System.Drawing.Size(169, 34);
            this.ExportAllButton.TabIndex = 1;
            this.ExportAllButton.Text = "Export All";
            this.ExportAllButton.UseVisualStyleBackColor = true;
            // 
            // ImportMoreButton
            // 
            this.ImportMoreButton.Location = new System.Drawing.Point(15, 15);
            this.ImportMoreButton.Name = "ImportMoreButton";
            this.ImportMoreButton.Size = new System.Drawing.Size(166, 31);
            this.ImportMoreButton.TabIndex = 0;
            this.ImportMoreButton.Text = "Import More";
            this.ImportMoreButton.UseVisualStyleBackColor = true;
            this.ImportMoreButton.Click += new System.EventHandler(this.ImportMoreClick);
            // 
            // MessagesList
            // 
            this.MessagesList.AllowUserToAddRows = false;
            this.MessagesList.AllowUserToDeleteRows = false;
            this.MessagesList.AllowUserToResizeColumns = false;
            this.MessagesList.AllowUserToResizeRows = false;
            this.MessagesList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.MessagesList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MessagesList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.MessagesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MessagesList.ColumnHeadersVisible = false;
            this.MessagesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Info,
            this.Body});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MessagesList.DefaultCellStyle = dataGridViewCellStyle1;
            this.MessagesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessagesList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.MessagesList.Location = new System.Drawing.Point(0, 0);
            this.MessagesList.Name = "MessagesList";
            this.MessagesList.ReadOnly = true;
            this.MessagesList.RowHeadersVisible = false;
            this.MessagesList.Size = new System.Drawing.Size(410, 464);
            this.MessagesList.TabIndex = 3;
            // 
            // Info
            // 
            this.Info.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Info.HeaderText = "Info";
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            this.Info.Width = 60;
            // 
            // Body
            // 
            this.Body.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Body.HeaderText = "Body";
            this.Body.Name = "Body";
            this.Body.ReadOnly = true;
            // 
            // ListSplitContainer
            // 
            this.ListSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListSplitContainer.Location = new System.Drawing.Point(0, 59);
            this.ListSplitContainer.Name = "ListSplitContainer";
            // 
            // ListSplitContainer.Panel1
            // 
            this.ListSplitContainer.Panel1.Controls.Add(this.ContactsListView);
            // 
            // ListSplitContainer.Panel2
            // 
            this.ListSplitContainer.Panel2.Controls.Add(this.MessagesList);
            this.ListSplitContainer.Size = new System.Drawing.Size(737, 464);
            this.ListSplitContainer.SplitterDistance = 323;
            this.ListSplitContainer.TabIndex = 4;
            // 
            // ImportTextDialog
            // 
            this.ImportTextDialog.DefaultExt = "txt";
            this.ImportTextDialog.Filter = "Text Files|*.txt";
            // 
            // ContactsListView
            // 
            this.ContactsListView.AllowUserToAddRows = false;
            this.ContactsListView.AllowUserToDeleteRows = false;
            this.ContactsListView.AllowUserToResizeRows = false;
            this.ContactsListView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ContactsListView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.ContactsListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ContactsListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ContactName,
            this.NumMessages,
            this.LastMessageDate});
            this.ContactsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContactsListView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ContactsListView.Location = new System.Drawing.Point(0, 0);
            this.ContactsListView.MultiSelect = false;
            this.ContactsListView.Name = "ContactsListView";
            this.ContactsListView.ReadOnly = true;
            this.ContactsListView.RowHeadersVisible = false;
            this.ContactsListView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ContactsListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ContactsListView.Size = new System.Drawing.Size(323, 464);
            this.ContactsListView.TabIndex = 0;
            this.ContactsListView.SelectionChanged += new System.EventHandler(this.ContactListSelectionChanged);
            // 
            // ContactName
            // 
            this.ContactName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ContactName.HeaderText = "Name";
            this.ContactName.Name = "ContactName";
            this.ContactName.ReadOnly = true;
            // 
            // NumMessages
            // 
            this.NumMessages.HeaderText = "Number of Messages";
            this.NumMessages.Name = "NumMessages";
            this.NumMessages.ReadOnly = true;
            this.NumMessages.Width = 70;
            // 
            // LastMessageDate
            // 
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.LastMessageDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.LastMessageDate.HeaderText = "Last Message Date";
            this.LastMessageDate.Name = "LastMessageDate";
            this.LastMessageDate.ReadOnly = true;
            // 
            // SMSBrowserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 663);
            this.Controls.Add(this.ListSplitContainer);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.TopPanel);
            this.Name = "SMSBrowserWindow";
            this.Text = "SMSBrowser";
            this.Shown += new System.EventHandler(this.BrowserWindowShown);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MessagesList)).EndInit();
            this.ListSplitContainer.Panel1.ResumeLayout(false);
            this.ListSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ListSplitContainer)).EndInit();
            this.ListSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ContactsListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.DataGridView MessagesList;
        private System.Windows.Forms.SplitContainer ListSplitContainer;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button ExportCurrentButton;
        private System.Windows.Forms.Button ExportAllButton;
        private System.Windows.Forms.Button ImportMoreButton;
        private System.Windows.Forms.OpenFileDialog ImportTextDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Info;
        private System.Windows.Forms.DataGridViewTextBoxColumn Body;
        private System.Windows.Forms.DataGridView ContactsListView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactName;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumMessages;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastMessageDate;
    }
}

