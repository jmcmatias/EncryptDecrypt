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

        static string AlphabetUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";     // Alfabeto letras maiusculas
        static string AlphabetLower = "abcdefghijklmnopqrstuvwxyz";     // Alfabeto letras minusculas
        static string Numeric = "0123456789";                           // "Alfabeto" Númerico

        //Função que vai cifrar a string recebida em função da chave secreta
        public string caeserCipher(int key,string message)
        {
            string EncryptedMessage = string.Empty; // String que vai receber a mensagem cifrada
            foreach (char element in message)              // Para cada elemento (caracter) na mensagem
            {   
                EncryptedMessage += Encipher( key, element);   // cifra o elemento (caracter) em função da chave (C = E(key,element) = (p + k) mod 26) adicionando o elemento a string
            }

            return EncryptedMessage.ToUpper();              // retorna a mensagem encriptada em UpperCase
        }

        // Função que vai decifrar a mensagem recebida em função da chave secreta
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
            if (IsWhiteSpace(element) || element == '\r' || element == '\n')   // Verifica se é White Space ou algum caracter de nova linha
                return element;                                                // Se for retorna o elemento

            string alphabet = SelectAlphabet(element);      // Seleciona o Alfabeto em função do caracter que irá cifrar
            int alphabetSize = alphabet.Length;             // carrega o tamanho do alfabeto selecionado em alphabetSize

            int ElementIndex = alphabet.IndexOf(element);   // Encontra o index do elemento no alfabeto
            
            char cipheredElement = (char)(alphabet[(ElementIndex + key) % alphabetSize]); // C = E(k, p) = (p + k) mod alphabetSize, 
            return cipheredElement;                         // retorna o elemento cifrado
        }

        // Função que vai decifrar cada elemento em função da key, neste caso o algoritmo espera receber a mensagem cifrada em UpperCase
        private static char Decipher(int key, char element)
        {
            if (IsWhiteSpace(element) || element == '\r' || element == '\n') // Verifica se é White Space ou algum caracter de nova linha
                return element;                                              // Se for retorna o elemento

            string alphabet = SelectAlphabet(element);       // Seleciona o Alfabeto em função do caracter que irá decifrar
            int alphabetSize = alphabet.Length;              // carrega o tamanho do alfabeto selecionado em alphabetSize

            int ElementIndex = alphabet.IndexOf(element);    // Encontra o index do elemento no alfabeto

            char decipheredElement = (char)(alphabet[(ElementIndex - key - (alphabet.Length-1) )  % alphabetSize + (alphabet.Length-1)]);   // p = D(k, C) = (C - k) mod alphabetSize, temos de subtrair o ultimo index do array para o caso em que c-k dá negativo saindo da gama de indexação do array,
                                                                                                                                            // após o mod (%) voltamos a somar para obter o valor do index pretendido
            return decipheredElement;                        // Retorna o elemento decifrado
        }
        
        // Função que verifica se o elemento é um espaço
        static bool IsWhiteSpace(char element)
        {
            if (element == ' ')
                return true;
            return false;
        }

        // Função que seleciona o alfabeto em função do elemento
        static string SelectAlphabet (char element)
        {
            string alphabet;
            
            if (!char.IsLetter(element))  // Caso o elemento (caracter) não seja uma letra, esta condição pode-se utilizar, mas tem como pré-condição
            {                             // a "Filtragem do input" no Model, temos de ter a certeza que todos os caracteres inseridos são permitidos

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
         
        // Função que converte a string secretkey para inteiro (se possivel) e verifica se o valor introduzido está entre 1 e 25 
        public int parseKey (string key)
        {
            int parsedKey;
            bool testkey = Int32.TryParse(key, out parsedKey);          // testa se é possivel converter a secretkey num inteiro, se for possivel guarda o valor em parsedKey e devolve true para testKey senão devolve false para tesKey
            if (!testkey || parsedKey < 1 || parsedKey > int.MaxValue)            // se não for possivel ou a chave introduzida não estiver entre 1 e 25
            {
                string info = "Please Insert a Numeric Key Between 1 and "+ int.MaxValue;    
                MessageBox.Show(info,"Invalid Secret Key Detected!!!");                      // Apresenta uma caixa de mensagens com a string info a informar o utilizador 
                return -1;
            }
            else
            {
                return parsedKey;                                       // senão devolve parsedKey
            }
        }

        public bool ValidPlainText(string message)
        {
            if (!Regex.IsMatch(message, @"^[a-zA-Z0-9 ]+$",RegexOptions.Multiline))   // Caso a mensagem contenha caracteres fora da expressão regular  @"^[a-zA-Z0-9 ]+$"
            {
                string info = "Please Insert Valid Chars. Chars Allowed: " +  
                              Environment.NewLine + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                              Environment.NewLine + "abcdefghijklmnopqrstuvwxyz" +
                              Environment.NewLine + "0123456789" +
                              Environment.NewLine + "␣ (white space)" ;
                MessageBox.Show(info, "Invalid Character Detected!!!"); // Apresenta uma caixa de mensagens com a string info a informar o utilizador dos caracteres permitidos pelo algoritmo
                return false;                                           // e retorna false
            }
            return true;                                                // caso contrario retorna true
        }

    }
}
