using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMSBrowser
{
    public partial class SMSBrowserWindow : Form
    {
        public SMSBrowserWindow()
        {
            InitializeComponent();
        }

        private void ImportMoreClick(object sender, EventArgs e)
        {
            if (ImportTextDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            MessageDatabase.ReadFromTextFile(ImportTextDialog.FileName);
            MessageDatabase.PopulateContactsList(ContactsList.Items);
            MessageDatabase.SaveData();
        }

        private void ContactListSelectionChanged(object sender, EventArgs e)
        {
            MessageDatabase.PopulateMessageList(ContactsList.SelectedItem, MessagesList.Rows);
            MessagesList.FirstDisplayedScrollingRowIndex = MessagesList.Rows.Count - 1;
        }

        private void BrowserWindowShown(object sender, EventArgs e)
        {
            if (MessageDatabase.ReadData())
                MessageDatabase.PopulateContactsList(ContactsList.Items);
        }
    }
}
