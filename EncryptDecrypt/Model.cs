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

        //Delegados
        //Solicita a mensagem plaintext
        public delegate void RequestPlainTextMessage(ref Message messageModel);
        public event RequestPlainTextMessage PlainTextMessageWanted;
        //Solicita a mensagem encriptada
        public delegate void RequestEncryptedMessage(ref Message messageModel);
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
            PlainTextMessageWanted(ref messageModel);

            encrypted = messageModel.getPlainMessage() + " Foste Encriptada";

            outputMessage.setEncryptedMessage(encrypted);
            MessageEncrypted();
        }

        public void Decrypt()
        {
            string decrypted;
            EncryptedMessageWanted(ref messageModel);

            decrypted = messageModel.getEncryptedMessage() + " Foste Desencriptada";

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
