using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecrypt
{
    public class Message
    {
        private string plainMessage;
        private string encryptedMessage;

        public void setPlainMessage(string plainmessage) { plainMessage = plainmessage; }
        public void setEncryptedMessage(string encryptedmessage) { encryptedMessage = encryptedmessage; }
        public string getPlainMessage() { return plainMessage; }
        public string getEncryptedMessage() { return encryptedMessage; }
    }
}
