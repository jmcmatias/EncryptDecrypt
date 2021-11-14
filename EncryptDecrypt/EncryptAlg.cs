using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptDecrypt
{
    public class EncryptAlg
    {
        // Função que vai cifrar a mensagem

        static string AlphabetUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static string AlphabetLower = "abcdefghijklmnopqrstuvwxyz";
        public string caeserCipher(int key,string message)
        {
            string EncryptedMessage = string.Empty; // String que vai receber a mensagem cifrada
            foreach (char element in message)              // Para cada elemento (caracter) na mensagem
            {
                EncryptedMessage += Encipher( key, element);   // cifra o elemento (caracter) em função da chave (C = E(key,element) = (p + k) mod 26) adicionando o elemento a string
            }

            return EncryptedMessage.ToUpper();              // retorna a mensagem encriptada em UpperCase
        }

        // Função que vai decifrar a mensagem
        public string caeserDecipher(int key, string message) 
        {
            string DecryptedMessage = string.Empty; // String que vai receber a mensagem decifrada

            foreach (char element in message)               // Para cada elemento (caracter) na mensagem
            {
                DecryptedMessage += Decipher(key, element); // decifra o elemento (caracter) em função da chave (p = D(k, C) = (C - k) mod 26) adicionando o elemento a string
            }
            
            return DecryptedMessage.ToLower();            // retorna a string em lowerCase
        }

        // Função que vai cifrar cada elemento em função da key
        private static char Encipher(int key,char element)
        {
            if (!char.IsLetter(element))  // Caso o elemento (caracter) não seja uma letra
                return element;           // Retorna o elemento (caracter)

            string alphabet;
            // Esta condição serve apenas para que a mensagem plainText possa ser inserida com Letras maiusculas e minusculas
            if (char.IsUpper(element))   // se for Letra maiuscula
            {
                alphabet = AlphabetUpper;        // Então o caracter de controlo da é 'A'
            }
            else
            {
                alphabet = AlphabetLower;       // Caso contrario é 'a'
            }

            int ElementIndex = alphabet.IndexOf(element);
           
            // MessageBox.Show("element>>"+(Int32)element+" key>>"+key+" element+key>>"+(Int32)(element + key) + " -UpperLower>>"+((Int32)UpperLower)+">>"+((char)(element+key)- UpperLower));
            char cipheredElement = (char)(AlphabetUpper[(ElementIndex + key) % 26]); // C = E(k, p) = (p + k) mod 26, o valor subtraido pela UpperLower é para que se possa efetuar o mod 26 
                                                                                             // (Para que se considere que a primeira letra do alfabeto é a posição 0), no final temos que somar novamente UpperLower.
            return cipheredElement;
        }

        // Função que vai decifrar cada elemento em função da key, neste caso o algoritmo espera receber a mensagem cifrada em UpperCase
        private static char Decipher(int key, char element)
        {
            if (!char.IsLetter(element))                    // Caso o elemento (caracter) não seja uma letra
                return element;                             // Retorna o elemento (caracter)

            int ElementIndex = AlphabetUpper.IndexOf(element);
            char decipheredElement = (char)(AlphabetUpper[(ElementIndex - key - (AlphabetUpper.Length-1) )  % 26 + (AlphabetUpper.Length-1)]);   // p = D(k, C) = (C - k) mod 26, tal como na função Encipher() temos de subtrair o valor neste caso de 'A' para nos posicionarmos em zero  
                                                                                  // (Para que se considere que a primeira letra do alfabeto é a posição 0), no final temos que somar novamente 'A'.
            return decipheredElement;
        }

        public bool ValidPlainText(string message)
        {
            if(!Regex.IsMatch(message, @"^[a-zA-Z0-9 ]+$"))
            {
                string info = "Please Insert Valid Chars. Chars Allowed: " +  // Apresenta uma caixa de texto a informar o utilizador 
                              Environment.NewLine + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + 
                              Environment.NewLine + "abcdefghijklmnopqrstuvwxyz" +
                              Environment.NewLine + "0123456789" +
                              Environment.NewLine + "␣";    

                MessageBox.Show(info);
                return false;
            }
            return true;
        }

        // Função que converte a string secretkey para inteiro (se possivel) e verifica se o valor introduzido está entre 1 e 25 
        public int parseKey (string key)
        {
            int parsedKey;
            bool testkey = Int32.TryParse(key, out parsedKey);          // testa se é possivel converter a secretkey num inteiro, se for possivel guarda o valor em parsedKey e devolve true para testKey senão devolve false para tesKey
            if (!testkey || parsedKey < 1 || parsedKey > 25)            // se não for possivel ou a chave introduzida não estiver entre 1 e 25
            {
                string info = "Please Insert a Numeric Key Between 1 and 25";    // Apresenta uma caixa de texto c informar o utilizador 
                MessageBox.Show(info);       
                return -1;
            }
            else
            {
                return parsedKey;                                       // senão devolve parsedKey
            }
        }
        
    }
}
