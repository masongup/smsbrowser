using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SMSBrowser
{
    public partial class SMSBrowserWindow : Form
    {
        private const string registryLocation = "HKEY_CURRENT_USER\\Software\\SMSSync";
        private const int defaultPort = 6178;

        public SMSBrowserWindow()
        {
            InitializeComponent();

            if (!int.TryParse((string)Registry.GetValue(registryLocation, "SyncPort", defaultPort.ToString()), out Synchronizer.SyncPort))
            {
                Registry.SetValue(registryLocation, "SyncPort", defaultPort.ToString());    //set it here to make sure that the key exists
                Synchronizer.SyncPort = defaultPort;                                        //if the key was missing, the other calls would return null
            }

            Synchronizer.SyncPassword = (string)Registry.GetValue(registryLocation, "SyncPassword", "");

            if (((string)Registry.GetValue(registryLocation, "SyncEnabled", "False")) == "True")
                Synchronizer.StartSync();
        }

        private void BrowserWindowClosed(object sender, FormClosedEventArgs e)
        {
            Registry.SetValue(registryLocation, "SyncPort", Synchronizer.SyncPort.ToString());
            Registry.SetValue(registryLocation, "SyncPassword", Synchronizer.SyncPassword);

            if (Synchronizer.EndSync())
                Registry.SetValue(registryLocation, "SyncEnabled", "True");
            else
                Registry.SetValue(registryLocation, "SyncEnabled", "False");
        }

        private void ImportMoreClick(object sender, EventArgs e)
        {
            if (ImportTextDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            MessageDatabase.ReadFromTextFile(ImportTextDialog.FileName);
            MessageDatabase.PopulateContactsList(ContactsListView.Rows);
            MessageDatabase.SaveData();
        }

        private void ContactListSelectionChanged(object sender, EventArgs e)
        {
            if (ContactsListView.SelectedRows.Count == 0)
                return;

            MessageDatabase.PopulateMessageList(ContactsListView.CurrentRow.Tag, MessagesList.Rows);
            if (MessagesList.Rows.Count != 0)
                MessagesList.FirstDisplayedScrollingRowIndex = MessagesList.Rows.Count - 1;
            foreach (DataGridViewRow row in MessagesList.SelectedRows)
                row.Selected = false;
        }

        private void BrowserWindowShown(object sender, EventArgs e)
        {
            if (MessageDatabase.ReadData())
                MessageDatabase.PopulateContactsList(ContactsListView.Rows);

            foreach (DataGridViewRow row in ContactsListView.SelectedRows)
                row.Selected = false;
        }

        private void ExportAllClick(object sender, EventArgs e)
        {
            ExportFileDialog.FileName = "Messages.txt";
            if (ExportFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            MessageDatabase.ExportAllToText(ExportFileDialog.FileName);
        }

        private void ExportCurrentClick(object sender, EventArgs e)
        {
            if (ContactsListView.CurrentRow == null || ContactsListView.CurrentRow.Tag == null)
                return;
            
            ExportFileDialog.FileName = ContactsListView.CurrentRow.Cells[0].Value.ToString();
            if (ExportFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            MessageDatabase.ExportCurrentToText(ExportFileDialog.FileName, ContactsListView.CurrentRow.Tag);
        }

        private void ExportConversationClick(object sender, EventArgs e)
        {
            if (ContactsListView.CurrentRow == null || MessagesList.CurrentRow == null || MessagesList.CurrentRow.Tag == null)
                return;
            
            ExportFileDialog.FileName = ContactsListView.CurrentRow.Cells[0].Value.ToString();
            if (ExportFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            MessageDatabase.ExportConversationToText(ExportFileDialog.FileName, MessagesList.CurrentRow.Tag);
        }

        private void ListClicked(object sender, EventArgs e)
        {
            if (ContactsListView.CurrentRow == null || ContactsListView.CurrentRow.Tag == null)
                ExportCurrentButton.Enabled = false;
            else
                ExportCurrentButton.Enabled = true;

            if (ContactsListView.CurrentRow == null || MessagesList.CurrentRow == null || MessagesList.CurrentRow.Tag == null)
                ExportConversationButton.Enabled = false;
            else
                ExportConversationButton.Enabled = true;
        }

        private void SearchBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Enter)
                return;

            if (string.IsNullOrEmpty(SearchBox.Text) || MessagesList.Rows.Count == 0)
                return;

            e.SuppressKeyPress = true;

            foreach (DataGridViewRow selectedRow in MessagesList.SelectedRows)
                selectedRow.Selected = false;

            var foundRows = from DataGridViewRow r in MessagesList.Rows where r.Cells[1].Value.ToString().Contains(SearchBox.Text) select r;

            foreach (var row in foundRows)
                row.Selected = true;
            
            if (MessagesList.SelectedRows.Count > 0)
                MessagesList.FirstDisplayedScrollingRowIndex = MessagesList.SelectedRows[0].Index;
        }

        private void NetworkLoadTimerTick(object sender, EventArgs e)
        {
            if (MessageDatabase.TryUpdateFromNetwork())
            {
                string lastContactSelected = string.Empty;
                if (ContactsListView.CurrentRow != null)
                    lastContactSelected = ContactsListView.CurrentRow.Cells[0].Value.ToString();
                MessageDatabase.PopulateContactsList(ContactsListView.Rows);
                foreach (DataGridViewRow row in ContactsListView.Rows)
                    row.Selected = row.Cells[0].Value.ToString().Equals(lastContactSelected);

                MessageDatabase.SaveData();
            }
        }

        private void ConfigureSyncButtonClicked(object sender, EventArgs e)
        {
            NetworkConfig configDialog = new NetworkConfig();
            configDialog.SyncPortTextBox.Text = Synchronizer.SyncPort.ToString();
            configDialog.SyncPasswordTextBox.Text = Synchronizer.SyncPassword;
            configDialog.SyncEnabledCheckbox.Checked = Synchronizer.syncRunning;
            configDialog.ShowDialog();

            Synchronizer.EndSync();
            Synchronizer.SyncPort = int.Parse(configDialog.SyncPortTextBox.Text);
            Synchronizer.SyncPassword = configDialog.SyncPasswordTextBox.Text;
            if (configDialog.SyncEnabledCheckbox.Checked)
                Synchronizer.StartSync();
        }

        private void MessageListScrolled(object sender, ScrollEventArgs e)
        {
            if (e.NewValue == 1)
            {
                DataGridViewRow oldFirstRow = MessagesList.Rows[0];
                MessageDatabase.PopulateMessageList(ContactsListView.CurrentRow.Tag, MessagesList.Rows, true);
                MessagesList.FirstDisplayedScrollingRowIndex = MessagesList.Rows.IndexOf(oldFirstRow);
            }
        }
    }
}
