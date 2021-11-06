using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecrypt
{
    public class View
    {
        private Model model;
        private FormMain window;

        Message messageView = new Message();
        String secretKeyView;

        // Ao clicar no botão encrypt
        public event System.EventHandler UserClickedEncrypt;
        //Ao clicar no botão decrypt
        public event System.EventHandler UserClickedDecrypt;
        //Ao clicar em sair
        public event System.EventHandler UserClickedExit;

        //Solicitação da Mensagem Encriptada
        public delegate void GetEncryptedMessage(ref Message message);
        public event GetEncryptedMessage NeedEncryptedMessage;
        //Solicitação de Mensagem Desencriptada
        public delegate void RequestDecryptedMessage(ref Message message);
        public event RequestDecryptedMessage NeedDecryptedMessage;



        internal View(Model m)
        {
            model = m;
        }


        // Interação com a form
        public void ActivateInterface()
        {
            window = new FormMain
            {
                View = this
            };
            window.ShowDialog();
        }

        public void Shutdown()
        {
            window.Shutdown();
        }


        public void EncryptClicked(object origin,EventArgs e)
        {
            window.ReadPlainMessageBox(ref messageView);
            window.ReadSecretKey(ref secretKeyView);
            UserClickedEncrypt(origin,e);
        }

        public void DecryptClicked(object origin,EventArgs e)
        {
            window.ReadEncryptedMessageBox(ref messageView);
            window.ReadSecretKey(ref secretKeyView);
            UserClickedDecrypt(origin, e);
        }

        public void ExitClick(EventArgs e)
        {
            UserClickedExit(this, e);
        }

        public void GivePlainTextMessage(ref Message messageModel,ref string secretKeyModel)
        {
            messageModel = new Message();
            messageModel.setPlainMessage(messageView.getPlainMessage());
            secretKeyModel = secretKeyView;
        }

        public void GiveEncryptedMessage(ref Message messageModel,ref string secretKeyModel)
        {
            messageModel = new Message();
            messageModel.setEncryptedMessage(messageView.getEncryptedMessage());
            secretKeyModel=secretKeyView;


        }

        public void AskEncryptedMessage()
        {
            NeedEncryptedMessage(ref messageView);
            PrintEncryptedMessage();
        }

        public void AskDecryptedMessage()
        {
            NeedDecryptedMessage(ref messageView);
            PrintDecryptedMessage();
        }

        public void PrintEncryptedMessage()
        {
            window.SetOutputWithEncrypted(ref messageView);
        }

        public void PrintDecryptedMessage()
        {
             window.SetOutputWithDecrypted(ref messageView);
        }
    }
}
