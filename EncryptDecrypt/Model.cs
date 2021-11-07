using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecrypt
{
    class Model
    {
        private View view;

        Message messageModel = new Message();
        Message outputMessage = new Message();
        string secretKeyModel;

        //Delegados
        //Solicita a mensagem plaintext
        public delegate void RequestPlainTextMessage(ref Message messageModel, ref string secretKeyModel);
        public event RequestPlainTextMessage PlainTextMessageWanted;
        //Solicita a mensagem encriptada
        public delegate void RequestEncryptedMessage(ref Message messageModel, ref string secretKeyModel);
        public event RequestEncryptedMessage EncryptedMessageWanted;

        public delegate void NotifyMessageEncrypted();
        public event NotifyMessageEncrypted MessageEncrypted;

        public delegate void NotifyMessageDecrypted();
        public event NotifyMessageDecrypted MessageDecrypted;

        public Model(View v)
        {
            view = v;
        }
    
        public void Encrypt()
        {
            string encrypted;
            PlainTextMessageWanted(ref messageModel, ref secretKeyModel);

            encrypted = messageModel.getPlainMessage() + " Foste Encriptada a secret key é:" + secretKeyModel;

            outputMessage.setEncryptedMessage(encrypted);
            MessageEncrypted();
        }

        public void Decrypt()
        {
            string decrypted;
            EncryptedMessageWanted(ref messageModel, ref secretKeyModel);

            decrypted = messageModel.getEncryptedMessage() + " Foste Desencriptada a secret key é:" + secretKeyModel;

            outputMessage.setPlainMessage(decrypted);
            MessageDecrypted();
        }

        public void GiveEncryptedMessage(ref Message messageView)
        {
            messageView = new Message();
            messageView = outputMessage;
        }

        public void GiveDecryptedMessage(ref Message messageView)
        {
            messageView = new Message();
            messageView = outputMessage;
        }


    }
}
