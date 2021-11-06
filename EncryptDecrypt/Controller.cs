using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptDecrypt
{
    class Controller
    {
        Model model;
        View view;
        bool exit;


        public delegate void InterfaceActivation(object origin);
        public event InterfaceActivation ActivateInterface;

        public Controller()
        {
            exit = false;
            view = new View(model);
            model = new Model(view);

            view.UserClickedEncrypt += UserclickedEncrypt;
            view.UserClickedDecrypt += UserclickedDecrypt;
            view.UserClickedExit += UserClickedExit;

            model.PlainTextMessageWanted += view.GivePlainTextMessage;
            model.EncryptedMessageWanted += view.GiveEncryptedMessage;

            model.MessageEncrypted += view.AskEncryptedMessage;
            model.MessageDecrypted += view.AskDecryptedMessage;

            view.NeedEncryptedMessage += model.GiveEncryptedMessage;
            view.NeedDecryptedMessage += model.GiveDecryptedMessage;

        }

        public void StartApp()
        {
            do
            {
                try
                {
                    view.ActivateInterface();
                }
                catch
                {

                }
            } while (!exit);
            
        }

        public void UserclickedEncrypt(object source, System.EventArgs args)
        {
            model.Encrypt();
        }

        public void UserclickedDecrypt(object source, System.EventArgs args)
        {
            model.Decrypt();
        }

        private void UserClickedExit(object sender, EventArgs e)
        {
            exit = true;
            view.Shutdown();
        }
    }
}
