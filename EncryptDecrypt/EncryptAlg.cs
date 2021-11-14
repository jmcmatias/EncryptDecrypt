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
        static string Numeric = "0123456789";

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
        private static char Encipher(int key, char element)
        {
            if (element == ' ')
            {
                return element;
            }
            string alphabet = SelectAlphabet(element);
            int modValue = SelectModValue(element);

            int ElementIndex = alphabet.IndexOf(element); 
            
            // MessageBox.Show("element>>"+(Int32)element+" key>>"+key+" element+key>>"+(Int32)(element + key) + " -UpperLower>>"+((Int32)UpperLower)+">>"+((char)(element+key)- UpperLower));
            char cipheredElement = (char)(alphabet[(ElementIndex + key) % modValue]); // C = E(k, p) = (p + k) mod 26, 
            return cipheredElement;
        }

        // Função que vai decifrar cada elemento em função da key, neste caso o algoritmo espera receber a mensagem cifrada em UpperCase
        private static char Decipher(int key, char element)
        {
            if (element == ' ')
            {
                return element;
            }
            string alphabet = SelectAlphabet(element);
            int modValue = SelectModValue(element);

            int ElementIndex = alphabet.IndexOf(element);
            char decipheredElement = (char)(alphabet[(ElementIndex - key - (alphabet.Length-1) )  % modValue + (alphabet.Length-1)]);   // p = D(k, C) = (C - k) mod 26, temos de subtrair o ultimo index do array para o caso em que c-k dá negativo saindo da gama,
                                                                                                                                                 // após o mod (%) voltamos a somar para obter o valor do index pretendido
            return decipheredElement;
        }

        public bool ValidPlainText(string message)
        {
            if(!Regex.IsMatch(message, @"^[a-zA-Z0-9 ]+$"))
            {
                string info = "Please Insert Valid Chars. Chars Allowed: " +  // Apresenta uma caixa de texto a informar o utilizador dos caracteres autorizados
                              Environment.NewLine + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + 
                              Environment.NewLine + "abcdefghijklmnopqrstuvwxyz" +
                              Environment.NewLine + "0123456789" +
                              Environment.NewLine + "␣";    

                MessageBox.Show(info);
                return false;
            }
            return true;
        }


        static string SelectAlphabet (char element)
        {
            string alphabet;
            
            if (!char.IsLetter(element))  // Caso o elemento (caracter) não seja uma letra
            {
                alphabet = Numeric;       // selecciona o "alfabeto" numérico
            }
            else
            {
                // Esta condição serve apenas para que a mensagem plainText possa ser inserida com Letras maiusculas e minusculas
                if (char.IsUpper(element))   // se for Letra maiuscula
                    alphabet = AlphabetUpper;        // selecciona o alfabeto de letra maiuscula
                else
                    alphabet = AlphabetLower;       // Caso contrario selecciona o alfabeto de letra minuscula
            }
            return alphabet;
        }

        static int SelectModValue(char element)
        {
            int modValue;
            if (!char.IsLetter(element))  // Caso o elemento (caracter) não seja uma letra
            {
                modValue = 10;       // selecciona o "alfabeto" numérico
            }
            else
            {
                modValue = 26;
            }
            return modValue;
        }

        // Função que converte a string secretkey para inteiro (se possivel) e verifica se o valor introduzido está entre 1 e 25 
        public int parseKey (string key)
        {
            int parsedKey;
            bool testkey = Int32.TryParse(key, out parsedKey);          // testa se é possivel converter a secretkey num inteiro, se for possivel guarda o valor em parsedKey e devolve true para testKey senão devolve false para tesKey
            if (!testkey || parsedKey < 1 || parsedKey > int.MaxValue)            // se não for possivel ou a chave introduzida não estiver entre 1 e 25
            {
                string info = "Please Insert a Numeric Key Between 1 and "+ int.MaxValue;    // Apresenta uma caixa de texto c informar o utilizador 
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
