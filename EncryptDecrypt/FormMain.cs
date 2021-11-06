using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptDecrypt
{
    public partial class FormMain : Form
    {
        View view;

        public View View
        {
            get => view;
            set => view = value;
        }

        public FormMain()
        {
            InitializeComponent();
        }

        public void Shutdown()
        {
            Application.Exit();
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            view.EncryptClicked(sender, e);
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            view.DecryptClicked(sender, e);
        }


        public void SetOutputWithEncrypted(ref Message outputMessage)
        {
            //EncryptedTextBox.Text = "teste";
            Output.Items.Add("Encrypted Text");
            Output.Items.Add(outputMessage.getEncryptedMessage());
            Output.Items.Add("");
        }

        public void SetOutputWithDecrypted(ref Message outputMessage)
        {
            Output.Items.Add("Decrypted Text");
            Output.Items.Add(outputMessage.getPlainMessage());
            Output.Items.Add("");
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            view.ExitClick(e);
        }

        public void ReadPlainMessageBox(ref Message messageView)
        {
            messageView.setPlainMessage(PlainTextBox.Text);
        }

        public void ReadEncryptedMessageBox(ref Message messageView)
        {
            messageView.setEncryptedMessage(EncryptedTextBox.Text);
        }

        public void ReadSecretKey(ref string secretKey)
        {
            secretKey = SecretKeyBox.Text;
        }


        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Really Exit??", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }


        // Teste
        public void ImprimeCredenciaisComoTeste(ref Message messageView)
        {
            string message = messageView.getPlainMessage();
            Output.Items.Add(message);
        }

    }
}
