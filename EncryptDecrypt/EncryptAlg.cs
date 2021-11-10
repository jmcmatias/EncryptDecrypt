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
        // Função que vai cifrar a mensagem
        public string caeserCipher(int key,string message)
        {
            string EncryptedMessage = string.Empty; // String que vai receber a mensagem cifrada

            foreach (char element in message)              // Para cada elemento (caracter) na mensagem
            {
                EncryptedMessage += cipher( key, element);   // cifra o elemento (caracter) em função da chave (C = E(key,element) = (p + k) mod 26) adicionando o elemento a string
            }

            return EncryptedMessage.ToUpper();              // retorna a mensagem encriptada em UpperCase
        }

        // Função que vai decifrar a mensagem
        public string caeserDecipher(int key, string message) 
        {
            string DecryptedMessage = string.Empty; // String que vai receber a mensagem decifrada

            foreach (char element in message)               // Para cada elemento (caracter) na mensagem
            {
                DecryptedMessage += decipher(key, element); // decifra o elemento (caracter) em função da chave (p = D(k, C) = (C - k) mod 26) adicionando o elemento a string
            }
            
            return DecryptedMessage.ToLower();            // retorna a string em lowerCase
        }

        // Função que vai cifrar cada elemento em função da key
        private static char cipher(int key,char element)
        {
            if (!char.IsLetter(element))  // Caso o elemento (caracter) não seja uma letra
                return element;           // Retorna o elemento (caracter)

            char UpperLower;

            // Esta condição serve apenas para que a mensagem plainText possa ser inserida com Letras maiusculas e minusculas
            if (char.IsUpper(element))   // se for Letra maiuscula
            {
                UpperLower = 'A';        // Então o caracter de controlo da é 'A'
            }
            else
            {
                UpperLower = 'a';       // Caso contrario é 'a'
            }
            
           
            // MessageBox.Show("element>>"+(Int32)element+" key>>"+key+" element+key>>"+(Int32)(element + key) + " -UpperLower>>"+((Int32)UpperLower)+">>"+((char)(element+key)- UpperLower));
            char cipheredElement = (char)(((element + key) - UpperLower) % 26 + UpperLower); // C = (p + k) mod 26, o valor subtraido pela UpperLower é para que se possa efetuar o mod 26 (Para que se considere que a primeira letra do alfabeto é a posição 0), no final temos que somar novamente UpperLower.
            
            return cipheredElement;
        }

        private static char decipher(int key, char element)
        {
            if (!char.IsLetter(element))
                return element;

            char decipheredElement = (char)( ((element - key)-'A') % 26 + 'A');
            return decipheredElement;
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
