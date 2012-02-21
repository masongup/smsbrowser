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
    public partial class NetworkConfig : Form
    {
        int lastPort = 0;
        
        public NetworkConfig()
        {
            InitializeComponent();
        }

        private void ServerPortTextChanged(object sender, EventArgs e)
        {
            int currentPort;
            if (!int.TryParse(SyncPortTextBox.Text, out currentPort))
                SyncPortTextBox.Text = lastPort.ToString();
            else
                lastPort = currentPort;
        }

        private void OnShow(object sender, EventArgs e)
        {
            lastPort = int.Parse(SyncPortTextBox.Text);
        }

        private void PasswordTextChanged(object sender, EventArgs e)
        {
            if (SyncPasswordTextBox.Text == string.Empty && SyncEnabledCheckbox.Checked)
                SyncAcceptButton.Enabled = false;
            else
                SyncAcceptButton.Enabled = true;
        }
    }
}
