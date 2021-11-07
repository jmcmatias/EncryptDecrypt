using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptDecrypt
{
    public class EncryptAlg
    {
        public string caeserCipher(string message, int key)
        {
            string EncryptedMessage = string.Empty;

            foreach (char element in message)
            {
                EncryptedMessage += cipher(element, key);
            }

            //EncryptedMessage = message + "teste class Key é" + key;

            return EncryptedMessage;
        }

        public string caeserDecipher(string message, int key)
        {
            return caeserCipher(message, 26 - key);
        }

        private static char cipher(char element, int key)
        {
            if (!char.IsLetter(element))
                return element;

            char d = char.IsUpper(element) ? 'A' : 'a';

            return (char)((((element + key) - d ) % 26) + d );
        }

        public int parseKey (string key)
        {
            int parsedKey;
            bool testkey = Int32.TryParse(key, out parsedKey);
            if (!testkey || parsedKey < 1 || parsedKey > 25)
            {
                string message = "Please Insert a Numeric Key Between 1 and 25";
                MessageBox.Show(message);
                return -1;
            }
            else
            {
                return parsedKey;
            }
           
        }
        
    }
}
